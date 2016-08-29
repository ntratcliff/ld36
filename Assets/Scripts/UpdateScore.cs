using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class UpdateScore : MonoBehaviour
{
    public int Player;

    private string hexColor;
    private PlayerScore playerScore;
    private GlobalPlayerInfo gPlayerInfo;

    public bool SetColor = true;

    // Use this for initialization
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        gPlayerInfo = FindObjectOfType<GlobalPlayerInfo>();
        if(gPlayerInfo != null)
        {
            playerScore = gPlayerInfo.GetPlayerScore(Player);
            if (!gPlayerInfo.HasPlayer(Player))
            {
                GameObject.Destroy(this.gameObject);
                return;
            }
            hexColor = Palette.GetHex(gPlayerInfo.GetPlayerColor(Player));
        }

        //default to minigame local score if it exists
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<PlayerInfo>().PlayerNum == Player)
            {
                playerScore = players[i].GetComponent<PlayerScore>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //set score text
        if (SetColor && hexColor != null)
            GetComponent<Text>().text = "<color='#" + hexColor + "'>" + playerScore.Score + "</color>";
        else
            GetComponent<Text>().text = playerScore.Score.ToString();
    }
}
