using UnityEngine;
using System.Collections;

public class CatchTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ScoreObject")
            GetComponentInParent<PlayerScore>().Score++;

        if(other.tag == "ScoreObject" && other.GetComponent<EggsObject>() != null)
            other.GetComponent<EggsObject>().Despawn();
    }    
}
