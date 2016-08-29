using UnityEngine;
using System.Collections;

public class TitleAudio : MonoBehaviour
{
    public AudioClip TitleClip;
    public AudioClip[] Subtitles;
    public float Volume = 1f;
    public float Delay;
    public float SubtitleDelay;

    private float titleLength;
    // Use this for initialization
    void Start()
    {
        //schedule title clip
        StartCoroutine(PlayOneShotDelayed(
            Delay,
            TitleClip));

        //schedule subtitle clip
        titleLength = TitleClip.length;
        StartCoroutine(PlayOneShotDelayed(
                Delay + titleLength + SubtitleDelay, 
                getRandomSubtitle()));
    }

    private AudioClip getRandomSubtitle()
    {
        return Subtitles[Random.Range(0, Subtitles.Length)];
    }

    IEnumerator PlayOneShotDelayed(float delay, AudioClip clip)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<AudioSource>().PlayOneShot(clip, Volume);
    }
}
