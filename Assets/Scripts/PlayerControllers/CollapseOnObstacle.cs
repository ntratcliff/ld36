using UnityEngine;
using System.Collections;

public class CollapseOnObstacle : MonoBehaviour
{
    public Vector3 ImpulseTorque;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RunnerObstacle")
        {
            //make the player fall over and start colliding with the world

            Transform player = transform.parent;

            //disable player controller
            player.GetComponent<BoulderPlayerController>().enabled = false;

            //update player physics
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            player.gameObject.layer = LayerMask.NameToLayer("Default");
            StartCoroutine(DetachDelay(player, 0.13f));

            //add impulse torque
            player.GetComponent<Rigidbody>().AddTorque(ImpulseTorque, ForceMode.Impulse);
        }
    }

    IEnumerator DetachDelay(Transform player, float delay)
    {
        yield return new WaitForSeconds(delay);
        player.parent = null;
    }
}
