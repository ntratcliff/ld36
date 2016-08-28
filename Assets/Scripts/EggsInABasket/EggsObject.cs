using UnityEngine;
using System.Collections;

public class EggsObject : MonoBehaviour
{

    [HideInInspector]
    public RandomSpawner Spawner;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Despawn()
    {
        Spawner.Despawn(this.gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Ground")
        {
            //disable pickup once on ground
            transform.tag = "Untagged";
        }
    }
}
