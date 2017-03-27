using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Xml;

class WebRequest : MonoBehaviour
{

    private XmlDocument xmlDoc;
    private WWW www;
    private TextAsset textXml;
    private List<string> Players;
    private string fileName;

    void Start()
    {
    //  StartCoroutine(GetText());
   
      //  string myXML = "<?xml version = \"1.0\" encoding=\"UTF - 8\" ?>  < tempdata numDevices = \"3\" > < tempsensor id = \"0\" > 5.44 </ tempsensor > < tempsensor id = \"1\" > 5.19 </ tempsensor >  < tempsensor id = \"2\" > 3.88 </ tempsensor >  < relay status = \"0\" />     </ tempdata > ";
      GetXML();

    }



   void GetXML()
    {
        XmlReader xmlReader = XmlReader.Create("http://sjernaroy.tvedten.com:8086/", new XmlReaderSettings { IgnoreWhitespace = true });
        //   xmlReader.ReadInnerXml();

        //  XmlReader xmlReader = XmlReader.Create(xml);


        xmlReader.MoveToContent();
       xmlReader.Read();
      
        while (xmlReader.Name == "tempsensor")
        {
            Debug.Log(xmlReader.GetAttribute("id") + " : " + xmlReader.ReadInnerXml());

        }

     //   xmlReader.Read();
        
           
            Debug.Log("Relay: " + xmlReader.GetAttribute("status"));

      //  }
        //        Debug.Log(xmlReader.GetAttribute("id")  + " : " + xmlReader.ReadInnerXml());
        //        Debug.Log(xmlReader.GetAttribute("id") + " : " + xmlReader.ReadInnerXml());
        // xmlReader.MoveToElement();
        //Debug.Log("Element: " + xmlReader.);
        // while (xmlReader.Read())
        //{

        // Debug.Log(xmlReader.NodeType + " " + xmlReader.Name);
        //Debug.Log(xmlReader.Name + " - " + xmlReader.Value);
        if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "tempsensor"))

            {

             //    Debug.Log("Text: " + xmlReader.ReadInnerXml());
                
               // Debug.Log(xmlReader.GetAttribute("id") + ": " + xmlReader.GetAttribute("temperature") + " " + );

            }
        //}

    }


    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://sjernaroy.tvedten.com:8086/");
        yield return www.Send();



        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
          //  GetXML(www.downloadHandler.text);

           // xmlDoc.LoadXml(www.downloadHandler.text);
           // Or retrieve results as binary data
           // Debug.Log(xmlDoc);
            byte[] results = www.downloadHandler.data;
           // readXml();
        }
    }


    // Following method reads the xml file and display its content 
    private void readXml()
    {
        foreach (XmlElement node in xmlDoc.SelectNodes("tempdata/tempsensor"))
        {

            Debug.Log(node.GetAttribute("id"));
            Debug.Log(node.SelectSingleNode("tempsensor ").InnerText);

            /*
            Player tempPlayer = new Player();

            tempPlayer.Id = int.Parse(node.GetAttribute("id"));
            tempPlayer.name = node.SelectSingleNode("name").InnerText;
            tempPlayer.score = int.Parse(node.SelectSingleNode("score").InnerText);
            Players.Add(tempPlayer); displayPlayeData(tempPlayer);
            */
        }

        //- See more at: http://www.theappguruz.com/blog/unity-xml-parsing-unity#sthash.Bs6scgLo.dpuf
    }
}
/*
 <?xml version = "1.0" encoding="UTF-8"?>
<tempdata numDevices = "3" >
    < tempsensor id="0">5.44</tempsensor>
    <tempsensor id = "1" > 5.19 </ tempsensor >
    < tempsensor id="2">3.88</tempsensor>
    <relay status = "0" />
 </ tempdata >
*/