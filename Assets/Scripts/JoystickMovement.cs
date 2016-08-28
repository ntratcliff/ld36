﻿using UnityEngine;
using System.Collections;

public class JoystickMovement : MonoBehaviour
{
    public float SpeedMultiplier;
    public float RotationSpeed;

    private PlayerInfo playerInfo;

    // Use this for initialization
    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        Transform view = Camera.main.transform.parent.parent;

        //move forward for vertical axis
        float vertical = playerInfo.GetAxis("Vertical") * SpeedMultiplier;
        float horizontal = playerInfo.GetAxis("Horizontal") * SpeedMultiplier;

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction = view.TransformDirection(direction);


        Vector3 velo = new Vector3(direction.x, rb.velocity.y, direction.z);

        rb.velocity = velo;

        //point player to movement direction
        direction.Normalize();
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                Time.deltaTime * RotationSpeed);
        }

        //report velocity to animator if it exists
        if(GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().SetFloat("VelocityMagnitude", velo.magnitude/SpeedMultiplier);
        }
    }
}
