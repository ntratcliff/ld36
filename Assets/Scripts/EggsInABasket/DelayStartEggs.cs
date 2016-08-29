using UnityEngine;
using System.Collections;

public class DelayStartEggs : MonoBehaviour
{
    public float startDelay;
    private float timeSinceStart;
    private bool fired;
    // Use this for initialization
    void Start()
    {
        timeSinceStart = 0;
        gameObject.GetComponent<Scoreboard>().UseTimer = false;
        gameObject.GetComponent<RandomSpawner>().enabled = false;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<JoystickMovement>().enabled = false;
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
            gameObject.GetComponent<Scoreboard>().UseTimer = true;
            gameObject.GetComponent<RandomSpawner>().enabled = true;
            for (int i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<JoystickMovement>().enabled = true;
            }
        }
    }
}
