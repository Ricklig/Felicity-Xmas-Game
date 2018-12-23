using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Info : NetworkBehaviour
{
    public GameObject UIcam;
    public string hint;
    public Text h;


    void Start()
    {
        h.text = hint;

    }


    // Start is called before the first frame update
    public void interact()
    {
        
            UIcam.GetComponent<Canvas>().enabled = true;
        

    }

    public void close()
    {
        UIcam.GetComponent<Canvas>().enabled = false;
    }

}
