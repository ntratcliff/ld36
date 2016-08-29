using UnityEngine;
using System.Collections;

public class SetPhysicsOnLogComplete : MonoBehaviour
{
    public Vector3 MinLogTorque, MaxLogTorque;
    public Vector3 SawForce;

    private PlayerScore playerScore;
    private SawGoodGame gameController;

    private bool fired;

    // Use this for initialization
    void Start()
    {
        playerScore = GetComponent<PlayerScore>();
        gameController = FindObjectOfType<SawGoodGame>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!fired && playerScore.Score == gameController.TargetScore)
        {
            Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
            for (int i = 0; i < bodies.Length; i++)
            {
                bodies[i].isKinematic = false;

                if(bodies[i].name == "Saw")
                {
                    bodies[i].AddForce(SawForce, ForceMode.Impulse);
                }
                else if(bodies[i].name == "LogHalf")
                {
                    Vector3 torque = (MaxLogTorque - MinLogTorque);
                    Vector3 random = Random.insideUnitSphere;

                    torque.x *= Mathf.Abs(random.x);
                    torque.y *= Mathf.Abs(random.y);
                    torque.z *= Mathf.Abs(random.z);

                    torque += MinLogTorque;

                    bodies[i].AddRelativeTorque(torque, ForceMode.Impulse);
                }
            }

            fired = true;
        }
    }
}
