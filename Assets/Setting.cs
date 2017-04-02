using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Setting : MonoBehaviour {

    public string host;
    public int port;

    public GameObject settingsPanel;
    public TMP_InputField textHost;
    public TMP_InputField textPort;

    void Awake()
    {
        if (PlayerPrefs.GetString("host") !="") {
               host = PlayerPrefs.GetString("host");
               port = PlayerPrefs.GetInt("port");
            Debug.Log("IP prefs loaded:" + host + ": " + port);

            textHost.text = host;
            textPort.text = port.ToString();
        }
        else
        {
            // default if no saved IP
            host = "home.tvedten.com";
            port = 80;
        }

    }

   public void SavePrefs()
    {
        
        host = textHost.text;
        port = Convert.ToInt32(textPort.text);
        PlayerPrefs.SetString("host", host);
        PlayerPrefs.SetInt("port", port);
        GetComponent<socketScript>().CloseSocketAndReconnect();

        settingsPanel.SetActive(false);
    }

    public string GetSavedHost()
    {
        return host;
    }

    public int GetSavedPort()
    {
        return port;
    }
}
