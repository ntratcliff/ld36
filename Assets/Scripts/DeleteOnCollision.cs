using UnityEngine;
using System.Collections;

public class DeleteOnCollision : MonoBehaviour
{
    public string Tag;

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == Tag)
        {
            if (GetComponent<EggsObject>() != null)
                GetComponent<EggsObject>().Despawn();
            else
                GameObject.Destroy(this.gameObject);
        }
            
    }
}
