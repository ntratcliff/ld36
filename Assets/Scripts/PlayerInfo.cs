using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour
{
    public int PlayerNum = 1;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetAxis(string axis)
    {
        return Input.GetAxis("P" + PlayerNum + " " + axis);
    }
}
