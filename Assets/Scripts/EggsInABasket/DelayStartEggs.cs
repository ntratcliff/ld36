using UnityEngine;
using System.Collections;

public class DelayStart : MonoBehaviour {
    public float startDelay;
    private float timeSinceStart;
    private GameObject[] players;
	// Use this for initialization
	void Start () {
        timeSinceStart = 0;
        gameObject.GetComponent<Scoreboard>().UseTimer = false;
        gameObject.GetComponent<RandomSpawner>().enabled = false;
        players = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<JoystickMovement>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        timeSinceStart += Time.deltaTime;

        if(timeSinceStart > startDelay)
        {
            gameObject.GetComponent<Scoreboard>().UseTimer = true;
            gameObject.GetComponent<RandomSpawner>().enabled = true;
            for (int i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<JoystickMovement>().enabled = true;
            }
        }
	}
}
