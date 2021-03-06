﻿using UnityEngine;
using System.Collections;

public class StartDelayBoulderDash : MonoBehaviour
{
    public float startDelay;
    private float timeSinceStart;
    private GameObject[] players;
    // Use this for initialization
    void Start()
    {
        timeSinceStart = 0;
        
        players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<BoulderPlayerController>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceStart += Time.deltaTime;

        if (timeSinceStart > startDelay)
        {
            
            for (int i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<BoulderPlayerController>().enabled = true;
            }
        }
    }
}
