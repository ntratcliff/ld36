using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateTimer : MonoBehaviour
{
    public int RedThreshold;

    private Scoreboard scoreboard;

    // Use this for initialization
    void Start()
    {
        scoreboard = FindObjectOfType<Scoreboard>();
    }

    // Update is called once per frame
    void Update()
    {
        //get round time in seconds as a string
        int seconds = Mathf.RoundToInt(scoreboard.RoundTime);
        string roundTime = seconds.ToString();

        //make round time red if time below threshold
        if(seconds <= RedThreshold)
        {
            //TODO: replace with dark red Palette color
            roundTime = "<color='#960B0B'>" + roundTime + "</color>";
        }

        GetComponent<Text>().text = roundTime;
    }
}
