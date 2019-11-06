using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking;

public class ServerScrollViewThing : MonoBehaviour
{
    public GameObject listingPrefab;
    public List<GameObject> listingButtons = new List<GameObject>();

    public Transform listingParent;

    private bool updateListing = false;

    // Start is called before the first frame update
    void Start()
    {
        NetWorker.localServerLocated += LocalServerLocated;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //NetWorker.RefreshLocalUdpListings();
            updateListing = true;
        }

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    NetWorker.localServerLocated -= LocalServerLocated;
        //    NetWorker.localServerLocated += LocalServerLocated;
        //    NetWorker.RefreshLocalUdpListings();
        //}

        if (updateListing)
        {
            UpdateLocalEndpointsListing();
        }
    }

    private void LocalServerLocated(NetWorker.BroadcastEndpoints endpoint, NetWorker sender)
    {
        //Debug.Log("Found endpoint: " + endpoint.Address + ":" + endpoint.Port);
        updateListing = true;
    }

    private void DeleteListings()
    {
        // If this runs multiple times a frame then it messes the mesh generation up and crashes unity?
        foreach (GameObject g in listingButtons)
        {
            Destroy(g);
        }
        listingButtons = new List<GameObject>();
    }

    private void UpdateLocalEndpointsListing()
    {
        DeleteListings();
        for (int i = 0; i < NetWorker.LocalEndpoints.Count; i++)
        {
            if (i < NetWorker.LocalEndpoints.Count && NetWorker.LocalEndpoints[i].IsServer)
            {
                // make a listing for each of them!
                GameObject g = Instantiate(listingPrefab, listingParent);
                EndpointButtonListing e = g.GetComponent<EndpointButtonListing>();
                e.SetEndpoint(NetWorker.LocalEndpoints[i]);
                listingButtons.Add(g);
            }
        }
        updateListing = false;
    }

    //public void Connect()
    //{
    //    //if (connectUsingMatchmaking)
    //    //{
    //    //    ConnectToMatchmaking();
    //    //    return;
    //    //}
    //    ushort port;
    //    if (!ushort.TryParse(portNumber.text, out port))
    //    {
    //        Debug.LogError("The supplied port number is not within the allowed range 0-" + ushort.MaxValue);
    //        return;
    //    }

    //    NetWorker client;

    //    if (useTCP)
    //    {
    //        client = new TCPClient();
    //        ((TCPClient)client).Connect(ipAddress.text, (ushort)port);
    //    }
    //    else
    //    {
    //        client = new UDPClient();
    //        if (natServerHost.Trim().Length == 0)
    //            ((UDPClient)client).Connect(ipAddress.text, (ushort)port);
    //        else
    //            ((UDPClient)client).Connect(ipAddress.text, (ushort)port, natServerHost, natServerPort);
    //    }

    //    Connected(client);
    //}
}
