using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RunnerWinState : MonoBehaviour
{
    public float WinConfirmationDelay;
    private GameObject playersContainer;
    private bool coroutineRunning;

    public float NextSceneDelay;

    // Use this for initialization
    void Start()
    {
        playersContainer = GameObject.Find("Runner/Players");
    }

    // Update is called once per frame
    void Update()
    {
        //check if there is a winner!
        int playerCount = playersContainer.transform.childCount;

        if (playerCount <= 1 && !coroutineRunning)
        {
            StartCoroutine(WinDelay());
            coroutineRunning = true;
        }
    }

    IEnumerator WinDelay()
    {
        yield return new WaitForSeconds(WinConfirmationDelay);

        Text uiText = GameObject.Find("Canvas/WinText").GetComponent<Text>();
        string winText = "Tie!";

        int playerCount = playersContainer.transform.childCount;
        if (playerCount == 1)
        {
            Transform winner = playersContainer.transform.GetChild(0);
            PaletteColor c = winner.GetComponentInChildren<CharacterColor>().Color;
            string hexColor = Palette.GetHex(c);

            winText = "<color='#" + hexColor + "'>" + winner.name + "</color> wins!";
        }

        uiText.text = winText;

        yield return new WaitForSeconds(NextSceneDelay);
        GetComponent<NextScene>().GoToScene();
    }
}
