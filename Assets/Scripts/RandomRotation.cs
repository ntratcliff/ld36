using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour
{
    public float MaxTorque;
    // Use this for initialization
    void Start()
    {
        transform.rotation = Quaternion.Euler(
            Random.Range(0, 360),
            Random.Range(0, 360),
            Random.Range(0, 360));

        //apply torque if rigidbody attatched 
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddTorque(new Vector3(
                Random.Range(0, MaxTorque),
                Random.Range(0, MaxTorque),
                Random.Range(0, MaxTorque)),
                ForceMode.Impulse);
        }
    }
}
