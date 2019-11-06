using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class Train : TrainBehavior
{
    public string firstChoice = "";
    public string secondChoice = "";
    public string response = "";
    public int _from = -1; // set these so that we know who it's from etc.
    public int _to = -1;
    public int _responder = -1;
    public Transform rotationTransform; // the transform to rotate to move the train
    public Transform radiusTransform; // the transform to put it on the track
    public GameObject graphics;
    public MeshRenderer cabGraphics;
    public MeshRenderer carGraphics;

    private GameplayLogic manager;
    private float rotation = 0;

    public override void SetChoices(RpcArgs args)
    {
        //Debug.Log("Run RPC");
        firstChoice = args.GetNext<string>();
        secondChoice = args.GetNext<string>();
        //if (networkObject.IsOwner)
        //{
        //    networkObject.From = args.GetNext<int>();
        //    networkObject.To = args.GetNext<int>();
        //    // rotate the correct way!
        //    rotation = networkObject.From * 360 / manager.networkObject.numPlayers;
        //    rotationTransform.localRotation = Quaternion.Euler(0, rotation, 0);
        //    networkObject.Rotation = rotation;
        //    networkObject.Moving = true;
        //}
        if (!networkObject.IsOwner)
        {
            _from = networkObject.From;
            _to = networkObject.To;
        }
        UpdateGraphics();
        graphics.SetActive(true);
    }

    public override void SetResponse(RpcArgs args)
    {
        _responder = args.GetNext<int>();
        response = args.GetNext<string>();
        graphics.SetActive(true);
        if (networkObject.IsOwner)
        {
            networkObject.Responder = _responder; // they set the responder!
            networkObject.Moving = true;
        }

        carGraphics.gameObject.SetActive(false);
    }

    public override void StopTrain(RpcArgs args)
    {
        //Debug.Log("Train stopped");
        if (networkObject.IsOwner)
        {
            //if (response == "")
            //{
            // then it can be stopped!
            networkObject.Moving = false;
            
            //}
        }
        graphics.SetActive(false);
    }

    public override void StartTrain(RpcArgs args)
    {
        if (networkObject.IsOwner)
        {
            networkObject.Moving = true;
        }
        graphics.SetActive(true);
    }

    private void OnDestroy()
    {
        manager.DeregisterTrain(this);
    }

    //public void SetChoicesWhenReady(string left, string right, int from, int to)
    //{
    //    StartCoroutine(SetChoicesWhenReadyCoroutine(left, right, from, to));
    //}

    //private IEnumerator SetChoicesWhenReadyCoroutine(string left, string right, int from, int to)
    //{
    //    yield return new WaitUntil(() => { return networkObject.NetworkReady; });
    //    yield return null; // wait another frame to make sure?
    //    networkObject.SendRpc(RPC_SET_CHOICES, true, Receivers.AllBuffered, left, right, from, to);
    //}

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameplayLogic>();
        radiusTransform.localPosition = new Vector3(manager.trainRadius, 0, 0); // put the x position on the track
        manager.RegisterTrain(this);
        transform.parent = manager.trainParent; // set the parent
    }

    protected override void NetworkStart()
    {
        base.NetworkStart();

        // then tell everyone who we are!
        if (networkObject.IsOwner)
        {
            networkObject.From = _from;
            networkObject.To = _to;
            rotation = networkObject.From * 360 / manager.networkObject.numPlayers;
            rotationTransform.localRotation = Quaternion.Euler(0, rotation, 0);
            networkObject.Rotation = rotation;
            networkObject.Moving = true;

            // tell everyone else what the strings are!
            networkObject.SendRpc(RPC_SET_CHOICES, true, Receivers.OthersBuffered, firstChoice, secondChoice, _from, _to);
            UpdateGraphics();
            graphics.SetActive(true);
        }
    }

    private void UpdateGraphics()
    {
        cabGraphics.material = manager.PlayerMaterials[networkObject.From];
        carGraphics.material = manager.PlayerMaterials[networkObject.To];
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(networkObject.Owner.Name); // that works!
        if (!networkObject.IsOwner)
        {
            //Debug.Log("not owned, Rotating");
            rotationTransform.localRotation = Quaternion.Euler(0, networkObject.Rotation, 0);
            if (!networkObject.Moving && graphics.activeSelf)
            {
                Debug.Log("Disabled graphics");
                graphics.SetActive(false); // it's being looked at by someone so turn invisible I guess?
            } else if (networkObject.Moving && !graphics.activeSelf)
            {
                Debug.Log("Enabled graphics");
                graphics.SetActive(true); // it's being looked at by someone so turn invisible I guess?
            }
        } else
        {
            // the owner
            // move the train if it's moving!
            if (networkObject.Moving)
            {
                // then move!!!!
                rotation -= Time.deltaTime * 360f / (manager.trainTimePerPlayer * manager.networkObject.numPlayers);
                //Debug.Log("rotation :" + rotation);
                rotationTransform.localRotation = Quaternion.Euler(0, rotation, 0);
                networkObject.Rotation = rotation;
            }
        }
    }
}
