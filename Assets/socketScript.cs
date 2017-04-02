using UnityEngine;

using System.Collections;

using System.Collections.Generic;

//using CielaSpike;

using System.Text;

using System;
using UnityEngine.UI;
using TMPro;



public class socketScript : MonoBehaviour {

    public Toggle sensorA;
    public Toggle sensorB;
    public TextMeshProUGUI textOut;
    public Text textBtnLED;
    //variables
    private int buttonCount = 0;

	private TCPConnection myTCP;

	private string serverMsg;

	public string msgToServer;





	void Awake() {

		//add a copy of TCPConnection to this game object

		myTCP = gameObject.AddComponent<TCPConnection>();

	}



	void Start () {
        Debug.Log("Start");
        sensorA.isOn = false;
        sensorB.isOn = false;
        sensorA.interactable = false;
        sensorB.interactable = false;

        StartCoroutine(setUpSocket());

    }
    void OnApplicationQuit()
    {
        Debug.Log("Application ending");
        myTCP.closeSocket();
    }



    void Update () {

		

		//keep checking the server for messages, if a message is received from server, it gets logged in the Debug console (see function below)

		SocketResponse ();



	}


    IEnumerator setUpSocket()
    {
        //if connection has not been made, display button to connect

        if (myTCP.socketReady == false)
        {

            myTCP.setupSocket();
            Debug.Log("Attempting to connect..");
            yield return null;



        }



        //once connection has been made, display editable text field with a button to send that string to the server (see function below)

        if (myTCP.socketReady == true)
        {

            Debug.Log("Socket ready");
    
            textOut.SetText("Socket ready:" + myTCP.conName + "\n" + myTCP.conHost + ":" + myTCP.conPort);
            SendToServer("X");
            yield return null;
            //msgToServer = GUILayout.TextField(msgToServer);



            /*if (GUILayout.Button("Write to server", GUILayout.Height(30)))
            {

                SendToServer(msgToServer);
                */
            }
            
        }

	

	public void buttonLED ()
    {
        SendToServer("D");
    }

	//socket reading script

	void SocketResponse() {

		string serverSays = myTCP.readSocket();
        

        if (serverSays != "") {
           
            Debug.Log("[SERVER]" + serverSays);
         

            if (serverSays.IndexOf("A1", 0) > -1) {
                sensorA.isOn  = true;
            }
            if (serverSays.IndexOf("A0", 0) > -1)
            {
                sensorA.isOn = false;
            }


            if (serverSays.IndexOf("B1", 0) > -1)
            {
                sensorB.isOn = true;
            }
            if (serverSays.IndexOf("B0", 0) > -1)
            {
                sensorB.isOn = false;
            }

            if (serverSays.IndexOf("L1", 0) > -1)
            {
                textBtnLED.text = "LED ON";
            }
            if (serverSays.IndexOf("L0", 0) > -1)
            {
                textBtnLED.text = "LED OFF";
            }

            if (serverSays.IndexOf("Knapp",0) > -1)
            {
                buttonCount++;
                textOut.SetText("Knapp trykket " + buttonCount);
            }

        }



	}



	//send message to the server

	public void SendToServer(string str) {

		myTCP.writeSocket(str);

		Debug.Log ("[CLIENT] -> " + str);

	}



}

