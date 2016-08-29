using UnityEngine;
using System.Collections;

public class SecondaryLoop : MonoBehaviour
{
    public AudioClip LoopClip;
    private AudioSource source;

    // Use this for initialization
    void Start()
    {
        //get attached source
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            source.clip = LoopClip;
            source.loop = true;
            source.Play();
        }
    }
}
