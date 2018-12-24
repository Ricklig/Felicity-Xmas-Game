using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class PicInfo : MonoBehaviour
{
    public GameObject UIcam;
    public Sprite hint;
    public Image h;


    void Start()
    {
        h.sprite = hint;

    }


    // Start is called before the first frame update
    public void interact()
    {
        gameObject.GetComponent<AudioSource>().Play();

        UIcam.GetComponent<Canvas>().enabled = true;


    }

    public void close()
    {
        gameObject.GetComponent<AudioSource>().Play();

        UIcam.GetComponent<Canvas>().enabled = false;
    }

}
