﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Key : NetworkBehaviour
{

    public GameObject UIcam;
    public string answer;
    public string hint;
    public GameObject mainInputField;
    public GameObject door;
    public Text h;

    public AudioSource beep;
    public AudioSource boop;


    void Start()
    {
        h.text = hint;
        mainInputField.SetActive(false);

    }


    // Start is called before the first frame update
    public void interact()
    {
       beep.Play();

        UIcam.GetComponent<Canvas>().enabled = true;
        mainInputField.SetActive(true);

    }

    public void close()
    {
        beep.Play();

        gameObject.GetComponent<AudioSource>().Play();

        UIcam.GetComponent<Canvas>().enabled = false;
        mainInputField.SetActive(false);
    }

    public void validate(string val)
    {
        beep.Play();

        if (val.Equals(answer))
        {
            boop.Play();
            CmdDestroy(door);
        }

        close();

    }

    public void DebugInput(string val)
    {
        Debug.Log("Input: " + val);
    }

    [Command]
    void CmdDestroy(GameObject state)
    {
        // make the change local on the server
        Debug.Log("Triggered");
        NetworkServer.Destroy(state);

    }

}
