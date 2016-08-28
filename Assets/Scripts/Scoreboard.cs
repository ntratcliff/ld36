using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public float RoundTime;
    private PlayerScore[] scores;

    [Tooltip("Should the scoreboard declare a winner at the end of round time?")]
    public bool DeclareWinner = true;

    [Tooltip("Objects to be disabled when the round ends")]
    public GameObject[] DisableOnRoundOver;

    [HideInInspector]
    public bool RoundOver;

    // Use this for initialization
    void Start()
    {
        //get player score scripts and order by player number
        scores = GameObject.FindObjectsOfType<PlayerScore>();
        scores = (from obj in scores
                  orderby obj.GetComponent<PlayerInfo>().PlayerNum
                  select obj).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if(!RoundOver)
            RoundTime -= Time.deltaTime;

        if(RoundTime <= 0)
        {
            RoundTime = 0f;
            EndRound();
        }
    }

    public void EndRound()
    {
        RoundOver = true;
        disableObjects();
        if (DeclareWinner)
        {
            PlayerScore[] winners = getWinners();
            setWinText(winners);
        }
    }

    private PlayerScore[] getWinners()
    {
        PlayerScore[] sortedScores = (from obj in scores
                                      orderby obj.Score descending
                                      select obj).ToArray();

        PlayerScore[] winners = (from obj in scores
                                 where obj.Score == sortedScores[0].Score
                                 select obj).ToArray();

        return winners;
    }

    private void setWinText(PlayerScore[] winners)
    {

        string winText = "Tie!";
        if(winners.Length == 1)
        {
            PaletteColor c = winners[0].GetComponent<CharacterColor>().Color;
            string hexColor = Palette.GetHex(c);

            winText = "<color='#" + hexColor + "'>" + winners[0].name + "</color> wins!";
        }

        Text uiText = GameObject.Find("Canvas/WinText").GetComponent<Text>();
        uiText.text = winText;
    }

    private void disableObjects()
    {
        for (int i = 0; i < DisableOnRoundOver.Length; i++)
        {
            DisableOnRoundOver[i].SetActive(false);
        }
    }

    public int[] Scores
    {
        get
        {
            return (from score in scores
                    select score.Score).ToArray();
        }
    }
}
