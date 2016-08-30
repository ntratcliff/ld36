using UnityEngine;
using System.Collections;

public class VOWinAnnoucement : MonoBehaviour
{
    public AudioClip[] WinClips;
    public AudioClip[] BroClips;

    public void PlayWinner(int p)
    {
        GetComponent<AudioSource>().PlayOneShot(WinClips[p - 1]);
    }

    public void PlayTie()
    {
        GetComponent<AudioSource>().PlayOneShot(BroClips[Random.Range(0, BroClips.Length)]);
    }
}
