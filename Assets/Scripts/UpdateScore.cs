using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class UpdateScore : MonoBehaviour
{
    public int Player;

    private string hexColor;
    private PlayerScore playerScore;
    // Use this for initialization
    void Start()
    {
        PlayerInfo[] players = FindObjectsOfType<PlayerInfo>();

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].PlayerNum == Player)
            {
                CharacterColor playerColor = players[i].GetComponent<CharacterColor>();
                hexColor = Palette.GetHex(playerColor.Color);

                playerScore = players[i].GetComponent<PlayerScore>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //set score text
        

        GetComponent<Text>().text = "<color='#" + hexColor + "'>" + playerScore.Score + "</color>";
    }
}
