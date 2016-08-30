using UnityEngine;
using System.Collections;

public class VOAnnounceLeader : MonoBehaviour
{

    public AudioClip[] LeaderClips;
    public AudioClip[] LoserClips;
    public float Delay;
    public float LoserDelay;

    private AudioSource voSource;

    // Use this for initialization
    void Start()
    {
        GameObject voObj = GameObject.FindGameObjectWithTag("NarrationPlayer");
        GlobalPlayerInfo gPlayerInfo = FindObjectOfType<GlobalPlayerInfo>();

        if (voObj == null || gPlayerInfo == null)
            return;

        if (gPlayerInfo.GetComponent<RoundManager>().CurrentRound > 3)
            return;

        voSource = voObj.GetComponent<AudioSource>();

        int winningPlayer = gPlayerInfo.GetWinningPlayer().GetComponent<PlayerInfo>().PlayerNum;
        int losingPlayer = gPlayerInfo.GetLosingPlayer().GetComponent<PlayerInfo>().PlayerNum;

        AudioClip winningClip = LeaderClips[winningPlayer - 1];
        AudioClip losingClip = LoserClips[losingPlayer - 1];

        StartCoroutine(PlayDelayed(
            winningClip, 
            Delay));

        if(gPlayerInfo.NumPlayers > 1)
            StartCoroutine(PlayDelayed(
                losingClip,
                Delay + winningClip.length + LoserDelay));
    }

    IEnumerator PlayDelayed(AudioClip clip, float Delay)
    {
        yield return new WaitForSeconds(Delay);

        voSource.PlayOneShot(clip);
    }
}
