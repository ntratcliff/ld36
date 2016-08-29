using UnityEngine;
using System.Collections;

public class DelayStartSaw : MonoBehaviour
{
    public float startDelay;
    private float timeSinceStart;
    private bool fired;
    // Use this for initialization
    void Start()
    {
        timeSinceStart = 0;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.Find("Saw").GetComponent<SawController>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceStart += Time.deltaTime;

        if (timeSinceStart > startDelay && !fired)
        {
            fired = true;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < players.Length; i++)
            {
                players[i].transform.Find("Saw").GetComponent<SawController>().enabled = true;
            }
        }
    }
}
