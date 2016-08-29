using UnityEngine;
using System.Collections;

public class SceneMusicPlayer : MonoBehaviour
{

    private AudioSource source;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void OnLevelWasLoaded(int level)
    {
        //set audio source values to scene defined values
        SceneInfo sceneInfo = FindObjectOfType<SceneInfo>();
        if(sceneInfo != null && sceneInfo.SceneTheme != source.clip)
        {
            source.Stop();
            source.clip = sceneInfo.SceneTheme;
            source.Play();
        }   

        if(sceneInfo != null)
        {
            source.loop = sceneInfo.LoopTheme;
            source.volume = sceneInfo.ThemeVolume;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
