using UnityEngine;
using System.Collections;

public class BoulderPlayerController : MonoBehaviour
{
    public int PlayerNum;

    public float JumpForce;

    private bool onGround = true;
    private Vector3 initLocalPos;

    // Use this for initialization
    void Start()
    {
        initLocalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("P" + PlayerNum + " Action") > 0 && onGround)
        {
            //jump
            GetComponent<Rigidbody>().AddForce(transform.up * JumpForce, ForceMode.Impulse);
            onGround = false;
        }
        else if (!onGround)
        {
            onGround = (GetComponent<Rigidbody>().velocity == Vector3.zero);
        }
    }
}