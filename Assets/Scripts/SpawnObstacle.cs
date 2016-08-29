using UnityEngine;
using System.Collections.Generic;

public class SpawnObstacle : MonoBehaviour
{
    public int delayBeforeSpawn;
    private double timeSinceStart;
    private RunnerLane[] lanes;

    // Use this for initialization
    void Start()
    {
        timeSinceStart = 0;
        //get lanes
        lanes = GetComponentsInChildren<RunnerLane>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceStart += Time.deltaTime;
        //get a random lane and try to spawn something
        if (timeSinceStart > delayBeforeSpawn)
        {
            int lane = Random.Range(0, lanes.Length);
            lanes[lane].SpawnObstacle();

            if (lanes[lane].name == "Center")
            {
                for (int i = 0; i < lanes.Length; i++)
                {
                    if (i != lane)
                        lanes[lane].StartCooldown(lanes[lane].Cooldown);
                }
            }
        }
    }
}
