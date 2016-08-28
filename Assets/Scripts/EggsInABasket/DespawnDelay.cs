using UnityEngine;
using System.Collections;

public class DespawnDelay : MonoBehaviour
{
    public float Delay;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Despawn());
    }
    
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(Delay);
        GetComponent<EggsObject>().Despawn();
    }
}
