using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerJoin : MonoBehaviour
{
    public string JoinAxis;
    public string ExitAxis;
    public int Player;

    private GlobalPlayerInfo gPlayerInfo;
    private bool playerJoined;
    private bool buttonDown;

    // Use this for initialization
    void Start()
    {
        gPlayerInfo = FindObjectOfType<GlobalPlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!buttonDown && !playerJoined 
            && Input.GetAxis("P" + Player + " " + JoinAxis) != 0)
        {
            transform.FindChild("JoinText").gameObject.SetActive(false);
            transform.FindChild("NameText").gameObject.SetActive(true);
            gPlayerInfo.AddPlayer(Player);
            playerJoined = true;
        }
        else if (!buttonDown && playerJoined 
            && Input.GetAxis("P" + Player + " " + ExitAxis) != 0)
        {
            transform.FindChild("JoinText").gameObject.SetActive(true);
            transform.FindChild("NameText").gameObject.SetActive(false);
            gPlayerInfo.RemovePlayer(Player);
            playerJoined = false;
        }
        else if (!buttonDown && !playerJoined
            && Player == 1 && Input.GetAxis("P1 " + ExitAxis) != 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if( !buttonDown && playerJoined
            && Player == 1 && Input.GetAxis("P1 " + JoinAxis) != 0)
        {
            //TODO: replace this when rounds are added
            SceneManager.LoadScene("SawGood");
        }

        buttonDown = (Input.GetAxis("P" + Player + " " + JoinAxis) != 0
            || Input.GetAxis("P" + Player + " " + ExitAxis) != 0);

    }
}
