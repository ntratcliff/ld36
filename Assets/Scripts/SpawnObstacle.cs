using UnityEngine;
using System.Collections.Generic;

public class SpawnObstacle : MonoBehaviour
{
    private RunnerLane[] lanes;

    // Use this for initialization
    void Start()
    {
        //get lanes
        lanes = GetComponentsInChildren<RunnerLane>();
    }

    // Update is called once per frame
    void Update()
    {
        //get a random lane and try to spawn something
        int lane = Random.Range(0, lanes.Length);
        lanes[lane].SpawnObstacle();

        if(lanes[lane].name == "Center")
        {
            for (int i = 0; i < lanes.Length; i++)
            {
                if (i != lane)
                    lanes[lane].StartCooldown(lanes[lane].Cooldown);
            }
        }
    }
}
