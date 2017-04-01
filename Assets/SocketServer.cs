using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using UnityEngine.UI; 


public class SocketServer : MonoBehaviour {

    public Text myText;

    private string server = "192.168.1.120";
    private int port;
    // Use this for initialization
    void Start () {


        // myText.text = SocketSendReceive("sjernaroy.tvedten.com", 8086);
        myText.text = SocketSendReceive("192.168.1.177", 80);
        // Socket s = ConnectSocket("192.168.1.120", 80);

        //myText.text =  sendCommand("S",s);
    }

    private string sendCommand(string str, Socket s)
    {
        
        Byte[] bytesSent = Encoding.ASCII.GetBytes(str);
        Byte[] bytesReceived = new Byte[256];

        // Create a socket connection with the specified server and port.
        //Socket s = ConnectSocket(server, port);

        if (s == null)
            return ("Connection failed");

        // Send request to the server.
        s.Send(bytesSent, bytesSent.Length, 0);


        // Receive the server home page content.
        int bytes = 0;
        string page = "Default HTML page on " + server + ":\r\n";

        // The following will block until te page is transmitted.
       
            do
            {
                bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
            }
            while (bytes > 0);
    
        return page;

    }

    // This method requests the home page content for the specified server.
    private static string SocketSendReceive(string server, int port)
    {
        string request = "GET / HTTP/1.1\r\nHost: " + server +  "\r\nConnection: Close\r\n\r\n";
        Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
        Byte[] bytesReceived = new Byte[256];

        // Create a socket connection with the specified server and port.
        Socket s = ConnectSocket(server, port);
        
        if (s == null)
            return ("Connection failed");

        // Send request to the server.
        s.Send(bytesSent, bytesSent.Length, 0);
     
       
        // Receive the server home page content.
        int bytes = 0;
        string page = "Default HTML page on " + server + ":\r\n";

        // The following will block until te page is transmitted.
        do
        {
            bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
            page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
        }
        while (bytes > 0);

        return page;
    }

    


    private static Socket ConnectSocket(string server, int port)
    {
        Socket s = null;
        IPHostEntry hostEntry = null;

        // Get host related information.
        hostEntry = Dns.GetHostEntry(server);

        // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
        // an exception that occurs when the host IP Address is not compatible with the address family
        // (typical in the IPv6 case).
        foreach (IPAddress address in hostEntry.AddressList)
        {
            IPEndPoint ipe = new IPEndPoint(address, port);
            Socket tempSocket =
                new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            tempSocket.Connect(ipe);

            if (tempSocket.Connected)
            {
                s = tempSocket;
                Debug.Log("Socket connected");
                break;
            }
            else
            {
                Debug.Log("Socket NOT connected");
                continue;
            }
        }
        return s;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
