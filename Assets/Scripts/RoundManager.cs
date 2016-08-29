using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    private int rounds = 3;
    private int currentRound = 1;

    public int CurrentRound
    {
        get { return currentRound; }
    }

    // Use this for initialization
    void Start()
    {

    }

    public void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "Scoreboard" && currentRound == rounds)
        {
            //end of 3 rounds, set main menu as next scene
            FindObjectOfType<NextScene>().SceneName = "MainMenu";
        }

        if (SceneManager.GetActiveScene().name == "Scoreboard")
        {
            currentRound++;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
