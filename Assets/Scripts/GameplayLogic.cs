using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using TMPro;
using BeardedManStudios.Forge.Networking.Lobby;

public class GameplayLogic : GameplayLogicBehavior
{
    public LineRenderer trainTracksRenderer;
    public Transform trainParent;
    public TMP_InputField leftInput;
    public TMP_InputField rightInput;
    public TrainDisplayPanel resultsPanel; // for viewing a responded train
    public TrainDisplayPanel choicePanel; // for responding to a stopped train
    public GameObject createTrainPanel; // for creating a new train
    public HeldButton stopTrainHeldButton;
    public Camera playerCamera;
    public GameObject startGameButton;
    [Header("Buttons taht get enabled and disabled by settings:")]
    public GameObject keepUnfinishedTrainButton;
    public GameObject dontAnswerTrainButton;
    [Space]
    public Transform launchTrainButtonParent;
    public GameObject launchTrainButtonPrefab;
    public TextMeshProUGUI messageDisplay;

    [Space]
    [Header("Gameplay settings")]
    public TextTripleChoices words;
    public float trainRadius = 4.5f;
    public int trainTrackRendererPoints = 20;
    [Tooltip("number of trains the players can launch at once. -1 is infinite")]
    public int numberOfTrainsAllowed = -1;
    public float trainTimePerPlayer = 1f; // the number of seconds each train takes in each player's section.
    // I'm thinking scaling by player sections makes more sense than scaling by time around the circle but I'm not sure...
    public List<Material> PlayerMaterials;
    public List<Color> PlayerColors;
    public int numSpies = 2;
    public float stopDegrees = 20f;
    public bool finishedTrainsInvisibleInitialSetting = false;
    public bool hasToAnswerStoppedTrainsInitialSetting = true;
    public bool finishedTrainsImmutableInitialSetting = true;


    // priavte variables
    private List<IClientMockPlayer> currentPlayers = new List<IClientMockPlayer>();
    public Train stoppedTrain = null;
    public List<Train> trains = new List<Train>();
    private NetworkedPlayer myself; // the networked player I spawn
    private bool settingsUI = false;
    private List<NetworkedPlayer> players = new List<NetworkedPlayer>();
    public List<SendToPersonScript> spawnedButtons = new List<SendToPersonScript>();

    public override void LaunchMessage(RpcArgs args)
    {
        //throw new System.NotImplementedException();
    }

    public override void MakeGuess(RpcArgs args)
    {
        //throw new System.NotImplementedException();
    }

    private void OnGUI()
    {
        if (networkObject.IsOwner)
        {
            if (Input.GetKeyDown(KeyCode.F3))
            {
                settingsUI = true;
                Debug.Log("F3/F4 pressed! Toggle UI. Visible: " + settingsUI);
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                settingsUI = false;
                Debug.Log("F4 pressed! Toggle UI. Visible: " + settingsUI);
            }
            if (settingsUI)
            {
                //Debug.Log("viewing UI");
                GUILayout.Space(100);
                networkObject.finishedTrainsInvisible = GUILayout.Toggle(networkObject.finishedTrainsInvisible, "Finished trains invisible");
                networkObject.finishedTrainsImmutable = GUILayout.Toggle(networkObject.finishedTrainsImmutable, "Finished trains immutable");
                networkObject.hijackerHasToRespond = GUILayout.Toggle(networkObject.hijackerHasToRespond, "Hijacker has to respond");
                networkObject.AllowedToKeepYourUnfinishedTrainsGoing = GUILayout.Toggle(networkObject.AllowedToKeepYourUnfinishedTrainsGoing, "Allowed to keep unfinished trains going");
            }
        }
    }

    public void RegisterTrain(Train t)
    {
        if (!trains.Contains(t))
        {
            trains.Add(t);
            BeardedManStudios.Forge.Logging.BMSLog.Log("Added new train!");
        }
        else
        {
            BeardedManStudios.Forge.Logging.BMSLog.Log("tried to add already added train!");
        }
    }

    public void DeregisterTrain(Train t)
    {
        trains.Remove(t);
        BeardedManStudios.Forge.Logging.BMSLog.Log("Removed train!");
    }

    public void ShowCreateTrainPanel()
    {
        createTrainPanel.SetActive(true);
        resultsPanel.gameObject.SetActive(false);
        choicePanel.gameObject.SetActive(false);
    }

    public void HideAllPanels()
    {
        createTrainPanel.SetActive(false);
        resultsPanel.gameObject.SetActive(false);
        choicePanel.gameObject.SetActive(false);
    }

    public string GetPlayerName(int playerID)
    {
        string personName = "Error";
        // find the person's name!
        for (int j = 0; j < players.Count; j++)
        {
            if (players[j].id == playerID)
            {
                personName = players[j].playername;
                break;
            }
        }
        return personName;
        //if (playerID < 0 || playerID >= currentPlayers.Count)
        //{
        //    return "ERROR";
        //}
        //return currentPlayers[playerID].Name;
    }

    public void SpawnNewTrain(int destination)
    {
        if (leftInput.text.Length > 0 && rightInput.text.Length > 0)
        {
            SpawnTrain(leftInput.text, rightInput.text, destination);
            leftInput.text = ""; // clear the question you just sent
            rightInput.text = "";
        } else
        {
            Debug.Log("Need to enter question for train");
        }
    }

    public void AnswerStoppedTrain(Train t)
    {
        // we're assuming that we're allowed to stop it. Here's hoping that's correct :P
        // now handle stopping it!
        stoppedTrain = t;
        t.networkObject.SendRpc(Train.RPC_STOP_TRAIN, Receivers.All);
        // the train was stopped! Set the UI up so that we can answer it!
        if (t.networkObject.From == myself.networkObject.ID)
        {
            // then it was from me! so display the results of the question in a nice popup box I guess?
            // may cause issues if unanswered but for now this works
            DisplayTrainResults(t);
        } else
        {
            if (t.response != "")
            {
                // it's been answered already!
                if (networkObject.finishedTrainsImmutable)
                {
                    DisplayTrainResults(t);
                }
                else
                {
                    // I guess edit it but in reality not
                    DisplayTrainChoice(t);
                }
            }
            else
            {
                // respond to it! It's not answered
                DisplayTrainChoice(t);
            }
        }
    }

    [ContextMenu("Release stopped train")]
    public void ReleaseStoppedTrain()
    {
        // when you've made a choice as a response it sends away the train afterwards!
        stoppedTrain.networkObject.SendRpc(Train.RPC_START_TRAIN, Receivers.All);
        stoppedTrain = null;
    }

    public void RespondToTrainWithSetAnswer(bool leftAnswer)
    {
        RespondToStoppedTrain(leftAnswer ? stoppedTrain.firstChoice : stoppedTrain.secondChoice);
    }

    public void RespondToStoppedTrain(string response)
    {
        // when you've made a choice as a response it sends away the train afterwards!
        stoppedTrain.networkObject.SendRpc(Train.RPC_SET_RESPONSE, Receivers.All, myself.networkObject.ID, response);
        stoppedTrain = null;
    }

    [ContextMenu("Destroy stopped train")]
    public void DoneWithFinishedTrain()
    {
        // called when we release a finished train. If we own it then delete it. If we don't own it then release it
        if (stoppedTrain._from == myself.networkObject.ID)
        {
            // then destroy it since it's mine!
            DestroyStoppedTrain();
        }
        else
        {
            ReleaseStoppedTrain();
        }
    }

    public void DestroyStoppedTrain()
    {
        stoppedTrain.networkObject.Destroy(); // destroy the train!
        stoppedTrain = null;
    }

    private void DisplayTrainResults(Train t)
    {
        resultsPanel.DisplayTrainStuff(t);
        resultsPanel.gameObject.SetActive(true);
        // hide the other panels
        createTrainPanel.SetActive(false);
        choicePanel.gameObject.SetActive(false);
    }

    private void DisplayTrainChoice(Train t)
    {
        choicePanel.DisplayTrainStuff(t);
        choicePanel.gameObject.SetActive(true);
        // hide the other panels
        createTrainPanel.SetActive(false);
        resultsPanel.gameObject.SetActive(false);
    }

    public void SpawnTrain(string left, string right, int destination)
    {
        // theoretically I should know who I am... do I? not sure...
        TrainBehavior t = NetworkManager.Instance.InstantiateTrain(0);
        //t.networkObject.SendRpc(TrainBehavior.RPC_SET_CHOICES, Receivers.All, left, right, myself.networkObject.ID, destination);
        Train train = t.gameObject.GetComponent<Train>();
        //train.networkObject.SendRpc(TrainBehavior.RPC_SET_CHOICES, Receivers.All, left, right, myself.networkObject.ID, destination);
        train.firstChoice = left;
        train.secondChoice = right;
        train._from = myself.networkObject.ID;
        train._to = destination; // FIX IDs
        //train.SetChoicesWhenReady(left, right, myself.networkObject.ID, destination); // FIX correct player IDs
        //train.networkObject.SendRpc(Train.RPC_SET_CHOICES, true, Receivers.AllBuffered, left, right, myself.networkObject.ID, destination); // fix correct player IDs

    }

    //[ContextMenu("Test send RPC")]
    //public void TestSendRPC()
    //{
    //    TrainBehavior t = FindObjectOfType<TrainBehavior>();
    //    t.networkObject.SendRpc(TrainBehavior.RPC_SET_CHOICES, Receivers.All, "left", "right", 0, 1);
    //}

    [ContextMenu("Update Train Tracks Line")]
    public void CreateTrainTracksLine()
    {
        trainTrackRendererPoints = Mathf.Max(3, trainTrackRendererPoints); // no dividing by 0 or negative here!
        float angle = 360f / trainTrackRendererPoints;
        List<Vector3> trackPoints = new List<Vector3>();
        for (int i = 0; i < trainTrackRendererPoints; i++)
        {
            float currAngle = angle * i;
            trackPoints.Add(new Vector3(Mathf.Cos(currAngle * Mathf.Deg2Rad), 0, Mathf.Sin(currAngle * Mathf.Deg2Rad)) * trainRadius + trainTracksRenderer.transform.position);
        }
        //Debug.Log(trackPoints.Count);
        trainTracksRenderer.positionCount = trackPoints.Count;
        trainTracksRenderer.SetPositions(trackPoints.ToArray());
    }

    public override void StartGame(RpcArgs args)
    {
        currentPlayers = LobbyService.Instance.MasterLobby.LobbyPlayers;
        players.Clear();
        players.AddRange(FindObjectsOfType<NetworkedPlayer>());
    }

    public void SpawnButtons(int ignoredPlayer)
    {
        //Debug.Log("Spawning buttons");
        foreach (SendToPersonScript go in spawnedButtons)
        {
            Destroy(go.gameObject);
        }
        spawnedButtons.Clear();
        for (int i = 0; i < players.Count; i++)
        {
            if (i == ignoredPlayer)
            {
                continue; // skip yourself!
            }
            SendToPersonScript stps = Instantiate(launchTrainButtonPrefab, launchTrainButtonParent).GetComponent<SendToPersonScript>();
            // GetPlayerName(i) doesn't seem to be working, because those are the wrong order I guess... welp.
            string personName = "Train";
            // find the person's name!
            for (int j = 0; j < players.Count; j++)
            {
                if (players[j].id == i)
                {
                    personName = players[j].playername;
                    break;
                }
            }
            stps.SetStuff(i, personName, PlayerColors[i], this);
            spawnedButtons.Add(stps);
        }
    }

    public void StartGame()
    {
        if (networkObject.IsServer && networkObject.IsOwner)
        {
            currentPlayers = LobbyService.Instance.MasterLobby.LobbyPlayers;
            networkObject.SendRpc(RPC_START_GAME, Receivers.Others); // tell them to update themselves and start the game!
            foreach (Train t in trains)
            {
                t.networkObject.Destroy();
            }
            trains.Clear();
            // then divide everyone up!
            players.Clear();
            players.AddRange(FindObjectsOfType<NetworkedPlayer>());
            if (players.Count < -1) // FIX I lowered that because I need to test it
            {
                Debug.Log("Unable to play the game, not enough players");
            }
            List<NetworkedPlayer> toChoosePlayers = new List<NetworkedPlayer>();
            toChoosePlayers.AddRange(players);
            // pick the spies!
            List<int> spies = new List<int>();
            numSpies = Mathf.Min(numSpies, players.Count); // can't have more spies than players that's an infinite loop
            while (spies.Count < numSpies)
            {
                int try_spy = Random.Range(0, players.Count);
                if (!spies.Contains(try_spy))
                {
                    spies.Add(try_spy);
                }
            }

            // randomize the positions too
            List<int> positions = new List<int>();
            for (int i =0; i < players.Count; i++)
            {
                positions.Add(i);
            }

            TextTripleChoices.TripleWords specialMessages = words.RandomWords();


            //string spymessage = "Spies:";
            //for (int i =0; i < spies.Count; i++)
            //{
            //    spymessage += " " + spies[i];
            //}
            //Debug.Log(spymessage);


            // then set everything!!!
            for (int i = 0; i < players.Count; i++)
            {
                int playerIndex = Random.Range(0, toChoosePlayers.Count);
                bool spy = spies.Contains(i); // it's a spy if the index is in that list
                NetworkedPlayer player = toChoosePlayers[playerIndex];
                int positionIndex = Random.Range(0, positions.Count);
                int position = positions[positionIndex];

                // assign the player stuff!
                string playerName = LobbyService.Instance.MasterLobby.LobbyPlayersMap[player.networkObject.Owner.NetworkId].Name;
                string secretMessage = spy ? specialMessages.twistWord1 : specialMessages.baseWord; // chose the special message
                //Debug.Log("Player name: " + player.networkObject.Owner.NetworkId + " has name " + playerName + " Is spy? " + spy + " has message " + secretMessage);
                player.networkObject.SendRpc(NetworkedPlayer.RPC_SET_FOR_NEW_GAME, Receivers.All, spy, position, playerName, secretMessage);

                // remove the used ones!
                toChoosePlayers.RemoveAt(playerIndex);
                positions.RemoveAt(positionIndex);
            }

            // now needs to chose the secret codes everyone gets! Possibly incldue that back when it chose spies vs no spies so no obvious
            // cheating... hmmm...

            // now has randomized everything and thus needs to start the game!
        }
    }

    private void SpawnPlayer()
    {
        // spawn the player object that we gotta use I guess?
        NetworkedPlayer np = NetworkManager.Instance.InstantiatePlayer() as NetworkedPlayer;
        np.manager = this;
        myself = np;
    }

    private void OnAbleToKeepUnfinishedTrainsChanged(bool value, ulong timestep)
    {
        MainThreadManager.Run(() =>
        {
            keepUnfinishedTrainButton.SetActive(value);
        });
    }

    private void OnHijackerHasToAnswerChanged(bool value, ulong timestep)
    {
        MainThreadManager.Run(() =>
        {
            // disable the button not to answer if you have to answer!
            dontAnswerTrainButton.SetActive(!value);
        });
    }

    protected override void NetworkStart()
    {
        base.NetworkStart();

        SpawnPlayer(); // everyone gets a player!

        currentPlayers = LobbyService.Instance.MasterLobby.LobbyPlayers;
        if (networkObject.IsServer)
        {
            //foreach(IClientMockPlayer p in currentPlayers)
            //{
            //    Debug.Log("Player name: " + p.Name);
            //}
            // do server initializations like get players etc.
            if (networkObject.IsOwner)
            {
                networkObject.numPlayers = currentPlayers.Count;
                networkObject.hijackerHasToRespond = hasToAnswerStoppedTrainsInitialSetting;
                networkObject.finishedTrainsInvisible = finishedTrainsInvisibleInitialSetting;
                networkObject.finishedTrainsImmutable = finishedTrainsImmutableInitialSetting;
            }
        }
        else
        {
            // do regular player initialization. Perhaps the server needs to do this as well.
            startGameButton.SetActive(false); // hide the button to start the game
        }
        networkObject.AllowedToKeepYourUnfinishedTrainsGoingChanged += OnAbleToKeepUnfinishedTrainsChanged;
        networkObject.hijackerHasToRespondChanged += OnHijackerHasToAnswerChanged;

        OnAbleToKeepUnfinishedTrainsChanged(true, 0);
        OnHijackerHasToAnswerChanged(hasToAnswerStoppedTrainsInitialSetting, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
