using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (transform.position != Camera.main.transform.position)
            transform.position = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != Camera.main.transform.position)
            transform.position = Camera.main.transform.position;
    }
}
