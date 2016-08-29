using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayersReady : MonoBehaviour
{
    [Tooltip("Input axis to be checked")]
    public string[] ReadyAxis;

    public float HoldTime = 1f;
    private float currentHoldTime;

    private GlobalPlayerInfo gPlayerInfo;
    private NextScene nextScene;

    // Use this for initialization
    void Start()
    {
        //get GlobalPlayerInfo if it exists
        gPlayerInfo = FindObjectOfType<GlobalPlayerInfo>();

        //get attached NextScene component
        nextScene = GetComponent<NextScene>();
        if (nextScene == null)
            Debug.LogError("No NextScene script attached to this object!");

        currentHoldTime = HoldTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(gPlayerInfo != null)
        {
            //get numbers for all players in scene
            int[] playerNums = gPlayerInfo.GetPlayerNums();

            //if all players are ready, go to next scene
            if (arePlayersReady(playerNums) && currentHoldTime <= 0)
            {
                nextScene.GoToScene();
            }
            else if (arePlayersReady(playerNums))
                currentHoldTime -= Time.deltaTime;
            else
                currentHoldTime = HoldTime;
        }
        else
        {
            //debug case, just check p1
            if (isPlayerReady(1))
                nextScene.GoToScene();
        }
    }

        

    private bool isPlayerReady(int p)
    {
        if (ReadyAxis.Length == 1)
            return getAxis(p, ReadyAxis[0]) > 0;

        for (int i = 0; i < ReadyAxis.Length; i++)
        {
            if(getAxis(p, ReadyAxis[i]) == 0)
                return false;
        }

        return true;
    }

    private bool arePlayersReady(int[] p)
    {
        for (int i = 0; i < p.Length; i++)
        {
            if (!isPlayerReady(p[i]))
                return false;
        }

        return true;
    }

    private float getAxis(int p, string axis)
    {
        return Input.GetAxis("P" + p + " " + axis);
    }
}
