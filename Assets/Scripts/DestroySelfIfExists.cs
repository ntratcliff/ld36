using UnityEngine;
using System.Collections;

public class DestroySelfIfExists : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (this.tag != "Untagged")
        {
            GameObject[] others = GameObject.FindGameObjectsWithTag(this.tag);
            if (others.Length > 1)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }

}
