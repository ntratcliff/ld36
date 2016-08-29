using UnityEngine;
using System.Collections;
using System.Linq;

public class SawGoodGame : MonoBehaviour
{
    [Tooltip("How many trigger presses will it take to get to the bottom of the log")]
    public int TargetScore;

    private Scoreboard scoreboard;
    private PlayerInfo[] players;

    // Use this for initialization
    void Start()
    {
        scoreboard = GetComponent<Scoreboard>();

        players = FindObjectsOfType<PlayerInfo>().OrderBy(x => x.PlayerNum).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if(!scoreboard.RoundOver)
        for (int i = 0; i < scoreboard.Scores.Length; i++)
        {
            if(scoreboard.Scores[i] == TargetScore)
            {
                //declare winner!
                scoreboard.EndRound();
            }
        }
    }


}
