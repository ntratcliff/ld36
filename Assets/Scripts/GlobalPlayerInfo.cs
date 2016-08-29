using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class GlobalPlayerInfo : MonoBehaviour
{
    public GameObject PlayerInfoPrefab;

    private List<GameObject> players;
    private List<GameObject> playersInScene;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
        players = new List<GameObject>();
    }

    public void OnLevelWasLoaded(int level)
    {
        playersInScene = GetPlayersInScene();
        disableInactivePlayers();

        setPlayerInfoInScene();
    }

    /// <summary>
    /// Disables any players in the scene 
    /// who don't have a player in real life
    /// </summary>
    private void disableInactivePlayers()
    {
        for (int i = 0; i < playersInScene.Count(); i++)
        {
            if (!players.Any(x => x.GetComponent<PlayerInfo>().PlayerNum == i + 1))
            {
                //playersInScene[i].SetActive(false);
                GameObject.Destroy(playersInScene[i]);
                playersInScene.RemoveAt(i);
                i--;
            }
            //bool playerFound = false;
            //for (int ii = 0; ii < players.Count(); ii++)
            //{
            //    if (players[ii].GetComponent<PlayerInfo>().PlayerNum == playersInScene[i].GetComponent<PlayerInfo>().PlayerNum)
            //        break;
            //}

            //if(!playerFound)
            //{
            //    GameObject.Destroy(playersInScene[i]);
            //    playersInScene.RemoveAt(i);
            //}

        }
    }

    /// <summary>
    /// Sets player information for players in the scene
    /// </summary>
    private void setPlayerInfoInScene()
    {
        for (int i = 0; i < playersInScene.Count(); i++)
        {
            //set color
            playersInScene[i].GetComponent<CharacterColor>().Color = players[i].GetComponent<CharacterColor>().Color;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPlayer(int p)
    {
        GameObject playerinfo = GameObject.Instantiate(PlayerInfoPrefab);
        playerinfo.GetComponent<PlayerInfo>().PlayerNum = p;
        playerinfo.GetComponent<CharacterColor>().Color = (PaletteColor)(p - 1);
        playerinfo.name = "P" + p;

        playerinfo.transform.parent = this.transform;

        players.Add(playerinfo);

        //sort players
        players = players.OrderBy(x => x.GetComponent<PlayerInfo>().PlayerNum).ToList();
    }

    public void RemovePlayer(int p)
    {
        GameObject obj = players.Find(x => x.GetComponent<PlayerInfo>().PlayerNum == p);
        players.Remove(obj);
        GameObject.Destroy(obj);
    }

    public bool HasPlayer(int p)
    {
        if (players.Find(x => x.GetComponent<PlayerInfo>().PlayerNum == p))
            return true;
        return false;
    }

    public List<GameObject> GetPlayersInScene()
    {
        return GameObject.FindGameObjectsWithTag("Player")
            .OrderBy(x => x.GetComponent<PlayerInfo>().PlayerNum).ToList();
    }
}
