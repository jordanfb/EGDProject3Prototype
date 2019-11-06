using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using TMPro;

public class NetworkedPlayer : PlayerBehavior
{
    public GameplayLogic manager;
    public MeshRenderer graphicsRenderer;
    public TextMeshPro playerName;
    public string playername = "";
    public int id = -1;
    string secretMessage = "";

    private float angle = 0;

    private void Start()
    {
        manager = FindObjectOfType<GameplayLogic>();
    }

    public override void SetForNewGame(RpcArgs args)
    {
        // now position yourself around the circle!
        MainThreadManager.Run(() =>
        {
            bool spy = args.GetNext<bool>();
            id = args.GetNext<int>();
            playername = args.GetNext<string>();
            secretMessage = args.GetNext<string>();

            if (networkObject.IsOwner)
            {
                // only set the UI if this is you!
                manager.messageDisplay.text = (spy ? "You are a spy" : "You are a townsperson") + "\n" + "Secret Phrase " + secretMessage;
            }
            // run this stuff on the main thread I guess? It's not great but hopefully it'll work...
            angle = -360f * id / manager.networkObject.numPlayers;
            transform.position = manager.trainParent.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * manager.trainRadius;
            transform.rotation = Quaternion.Euler(0, angle, 0);
            graphicsRenderer.material = manager.PlayerMaterials[id];

            if (networkObject.IsOwner)
            {
                networkObject.IsSpy = spy;
                networkObject.ID = id;

                // rotate the camera
                manager.playerCamera.transform.localRotation = Quaternion.Euler(90, -angle - 90, 0);
                // then spawn the buttons for launching to the other players too!
                // plus spawn buttons!
                manager.SpawnButtons(id);
            } else
            {
                // find a button if it exists and has your ID then set the name!
                foreach(SendToPersonScript stps in manager.spawnedButtons)
                {
                    if (stps.destination == id)
                    {
                        // then set the name!
                        stps.SetButtonName(playername);
                    }
                }
            }
            playerName.text = playername; // manager.GetPlayerName(id);
                                          //playerName.transform.rotation = Quaternion.Euler(90, angle - 90, 0);
                                          // try copying the camera's rotation to align to it?
            playerName.transform.rotation = Quaternion.Euler(90, manager.playerCamera.transform.localRotation.eulerAngles.y, 0);
        });
    }

    protected override void NetworkStart()
    {
        base.NetworkStart();


        // plus, tell the gameplay logic I exist?
    }

    // Update is called once per frame
    void Update()
    {
        if (networkObject.IsOwner)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // then quit to menu I guess?
                NetworkManager.Instance.Disconnect();
                UnityEngine.SceneManagement.SceneManager.LoadScene(0); // go back to main menu!
            }
            // update yourself!
            if (manager.stopTrainHeldButton.Held && manager.stoppedTrain == null)
            {
                //BeardedManStudios.Forge.Logging.BMSLog.Log("Trying to stop train! num trains " + manager.trains.Count);
                // then you're trying to stop trains!
                // check if any of the trains are close to you
                Train closest = null;
                float distance = -1;
                foreach (Train t in manager.trains)
                {
                    if (manager.networkObject.finishedTrainsInvisible)
                    {
                        if (t.response != "" && t._from != networkObject.ID)
                        {
                            // if it's answered but originally asked by us then skip it
                            continue;
                        }
                    }
                    // compare the angles?
                    float tangle = t.networkObject.Rotation % 360;
                    float d = Mathf.Min(Mathf.Abs(tangle + angle), Mathf.Min(Mathf.Abs(tangle + angle - 360), Mathf.Abs(tangle + angle + 360))); // not quite what I want FIX this allows for overshooting which I think is bad? Not sure
                    BeardedManStudios.Forge.Logging.BMSLog.Log("Trying stop. Distance: " + d + " my angle " + angle + " train angle " + tangle);
                    if (distance == -1 || d < distance)
                    {
                        distance = d;
                        closest = t;
                    }
                }
                if (closest != null && distance < manager.stopDegrees && distance > -1)
                {
                    manager.AnswerStoppedTrain(closest);
                    BeardedManStudios.Forge.Logging.BMSLog.Log("Stopped train!");
                }
                else
                {
                    BeardedManStudios.Forge.Logging.BMSLog.Log("Couldn't find train to stop");
                }
                // otherwise there are no trains to stop!
            }
        }
    }
}
