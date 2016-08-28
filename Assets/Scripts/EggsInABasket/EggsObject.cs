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
}
