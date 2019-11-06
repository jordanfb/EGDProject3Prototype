using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking;

public class EndpointButtonListing : MonoBehaviour
{
    private NetWorker.BroadcastEndpoints endpoints;
    private MultiplayerMenu multiplayerMenu;
    public TMPro.TextMeshProUGUI displayText;

    private void Start()
    {
        multiplayerMenu = GameObject.FindObjectOfType<MultiplayerMenu>();
    }

    public void SetEndpoint(NetWorker.BroadcastEndpoints e)
    {
        endpoints = e;
        displayText.text = e.Address + ":" + e.Port;
    }

    public void Connect()
    {
        //if (connectUsingMatchmaking)
        //{
        //    ConnectToMatchmaking();
        //    return;
        //}
        ushort port = endpoints.Port;

        NetWorker client;

        if (multiplayerMenu.useTCP)
        {
            client = new TCPClient();
            ((TCPClient)client).Connect(endpoints.Address, (ushort)port);
        }
        else
        {
            client = new UDPClient();
            if (multiplayerMenu.natServerHost.Trim().Length == 0)
                ((UDPClient)client).Connect(endpoints.Address, (ushort)port);
            else
                ((UDPClient)client).Connect(endpoints.Address, (ushort)port, multiplayerMenu.natServerHost, multiplayerMenu.natServerPort);
        }

        multiplayerMenu.Connected(client);
    }
}