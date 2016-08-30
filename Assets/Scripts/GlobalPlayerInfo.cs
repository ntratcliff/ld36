using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;

public class GlobalPlayerInfo : MonoBehaviour
{
    public GameObject PlayerInfoPrefab;

    private List<GameObject> players;
    private List<GameObject> playersInScene;

    public List<GameObject> PlayersInScene
    {
        get { return playersInScene; }
    }

    public int NumPlayers
    {
        get { return players.Count(); }
    }

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
        players = new List<GameObject>();
    }

    public void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            //destroy this object on main menu
            GameObject.Destroy(this.gameObject);
            return;
        }

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
        int playerNum = 1;
        for (int i = 0; i < playersInScene.Count(); i++)
        {
            if (!players.Any(x => x.GetComponent<PlayerInfo>().PlayerNum == playerNum))
            {
                //playersInScene[i].SetActive(false);
                GameObject.Destroy(playersInScene[i]);
                playersInScene.RemoveAt(i);
                i--;
            }
            playerNum++;

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
        return players.Any(x => x.GetComponent<PlayerInfo>().PlayerNum == p);
    }

    public List<GameObject> GetPlayersInScene()
    {
        return GameObject.FindGameObjectsWithTag("Player")
            .OrderBy(x => x.GetComponent<PlayerInfo>().PlayerNum).ToList();
    }

    public int[] GetPlayerNums()
    {
        return players.Select(p => p.GetComponent<PlayerInfo>().PlayerNum).ToArray();
    }

    public PaletteColor[] GetPlayerColors()
    {
        return players.Select(p => p.GetComponent<CharacterColor>().Color).ToArray();
    }

    public PaletteColor GetPlayerColor(int p)
    {
        int[] nums = GetPlayerNums();
        PaletteColor[] colors = GetPlayerColors();
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == p)
                return colors[i];
        }

        return PaletteColor.InvalidColor;
    }

    public PlayerScore GetPlayerScore(int p)
    {
        if (!HasPlayer(p))
            return null;

        return transform.FindChild("P" + p).GetComponent<PlayerScore>();
    }

    public void IncrementPlayerScore(int p)
    {
        if (!HasPlayer(p))
            return;

        GetPlayerScore(p).Score++;
    }

    public GameObject GetWinningPlayer()
    {
        return players.OrderBy(p => p.GetComponent<PlayerScore>().Score).First();
    }

    public GameObject GetLosingPlayer()
    {
        return players.OrderByDescending(p => p.GetComponent<PlayerScore>().Score).First();
    }
}
