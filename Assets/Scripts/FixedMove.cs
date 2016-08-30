using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class FixedMove : MonoBehaviour
{
    [Tooltip("Direction as eulser angles")]
    public Vector3 Direction;

    [Tooltip("Movement direction (normalized)")]
    public Vector3 NormalizedDirection;

    [Tooltip("Movement speed (in units/s)")]
    public float Speed;

    [Tooltip("Is movement applied to local pos or global pos")]
    public bool Local;

    public float RampMultiplier;
    private float elapsedTime;
    private float speedRamp;
    private float baseSpeed;
    public float MaxSpeed;

    private Quaternion quaternionDirection;

    public void Start()
    {
        baseSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        //update NormalizedDirection based on Direction
        quaternionDirection = Quaternion.Euler(Direction);
        NormalizedDirection = (quaternionDirection * Vector3.right);

#if UNITY_EDITOR
        if (Application.isPlaying)
        {
#endif
            //get displacement for this frame
            Vector3 displacement = NormalizedDirection * Speed * Time.deltaTime;

            //add displacement to position
            if (Local)
                transform.localPosition += displacement;
            else
                transform.position += displacement;

            if(Speed < MaxSpeed)
            {
                speedRamp += RampMultiplier * elapsedTime;
                Speed = baseSpeed + speedRamp;
            }
            else
            {
                Speed = MaxSpeed;
            }
            
            elapsedTime += Time.deltaTime;
#if UNITY_EDITOR
        }
#endif
    }
}
