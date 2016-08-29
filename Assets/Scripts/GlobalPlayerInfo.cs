using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class GlobalPlayerInfo : MonoBehaviour
{
    public GameObject PlayerInfoPrefab;

    private List<GameObject> players;
    private GameObject[] playersInScene;

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
        for (int i = players.Count() - 1; i < playersInScene.Length; i++)
        {
            playersInScene[i].SetActive(false);
        }
    }

    /// <summary>
    /// Sets player information for players in the scene
    /// </summary>
    private void setPlayerInfoInScene()
    {
        for (int i = 0; i < playersInScene.Length; i++)
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
        playerinfo.GetComponent<CharacterColor>().Color = (PaletteColor)p;
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

    public GameObject[] GetPlayersInScene()
    {
        return GameObject.FindGameObjectsWithTag("Player")
            .OrderBy(x => x.GetComponent<PlayerInfo>().PlayerNum).ToArray();
    }
}
