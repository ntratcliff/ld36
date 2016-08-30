using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RunnerWinState : MonoBehaviour
{
    public float WinConfirmationDelay;
    private GameObject playersContainer;
    private GlobalPlayerInfo gPlayerInfo;
    private bool coroutineRunning;

    public float NextSceneDelay;

    private bool singlePlayerMode;

    // Use this for initialization
    void Start()
    {
        playersContainer = GameObject.Find("Runner/Players");

        gPlayerInfo = FindObjectOfType<GlobalPlayerInfo>();
        if(gPlayerInfo != null)
            singlePlayerMode = gPlayerInfo.NumPlayers == 1;
    }

    // Update is called once per frame
    void Update()
    {
        //check if there is a winner!
        int playerCount = playersContainer.transform.childCount;

        if ((!singlePlayerMode && playerCount <= 1 && !coroutineRunning)
            || playerCount == 0 && !coroutineRunning)
        {
            StartCoroutine(WinDelay());
            coroutineRunning = true;
        }
    }

    IEnumerator WinDelay()
    {
        yield return new WaitForSeconds(WinConfirmationDelay);

        //set win text
        Text uiText = GameObject.Find("Canvas/WinText").GetComponent<Text>();
        string winText = "Tie!";

        //get VO player
        GameObject voObj = GameObject.FindGameObjectWithTag("NarrationPlayer");

        int playerCount = playersContainer.transform.childCount;
        if (playerCount == 1 && !singlePlayerMode)
        {
            Transform winner = playersContainer.transform.GetChild(0);
            PaletteColor c = winner.GetComponentInChildren<CharacterColor>().Color;
            string hexColor = Palette.GetHex(c);

            winText = "<color='#" + hexColor + "'>" + winner.name + "</color> wins!";

            //increment winner's score
            int playerNum = winner.GetComponent<PlayerInfo>().PlayerNum;

            gPlayerInfo.IncrementPlayerScore(playerNum);

            //play win VO
            if (voObj != null)
                voObj.GetComponent<VOWinAnnoucement>().PlayWinner(playerNum);
        }
        else
        {
            //increment winners' scores
            for (int i = 0; i < playerCount; i++)
            {
                Transform player = playersContainer.transform.GetChild(i);
                gPlayerInfo.IncrementPlayerScore(player.GetComponent<PlayerInfo>().PlayerNum);
            }

            //play tie VO
            if (voObj != null)
                voObj.GetComponent<VOWinAnnoucement>().PlayTie();
        }

        uiText.text = winText;

        yield return new WaitForSeconds(NextSceneDelay);
        GetComponent<NextScene>().GoToScene();
    }
}
