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
        int lane = Random.Range(0, lanes.Length - 1);
        lanes[lane].SpawnObstacle();
    }
}
