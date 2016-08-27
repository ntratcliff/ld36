using UnityEngine;
using System.Collections;

public class FixedRotate : MonoBehaviour
{
    [Tooltip("Axis to rotate on")]
    public bool X, Y, Z;

    [Tooltip("Speed to rotate (in deg/s)")]
    public float Speed;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //calculate rotation for this frame
        float deltaRot = Time.deltaTime * Speed;

        //update euler angles
        Vector3 euler = transform.rotation.eulerAngles;

        if (X)
            euler.x += deltaRot;

        if (Y)
            euler.y += deltaRot;

        if (Z)
            euler.z += deltaRot;

        //apply rotation
        transform.rotation = Quaternion.Euler(euler);
    }
}
