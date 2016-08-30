using UnityEngine;
using System.Collections;

public class PlayVO : MonoBehaviour
{
    public float Delay;
    public AudioClip Clip;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(PlayOneShotDelayed(Delay, Clip));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator PlayOneShotDelayed(float delay, AudioClip clip)
    {
        yield return new WaitForSeconds(delay);

        GameObject voObj = GameObject.FindGameObjectWithTag("NarrationPlayer");
        if (voObj != null)
            voObj.GetComponent<AudioSource>().PlayOneShot(clip);
        else
            Debug.LogWarning("No VO Object found in scene!");
    }
}
