using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bob : MonoBehaviour
{
    public float MinScale;
    public float MaxScale;
    public bool StartMax;
    public float LerpTime;
    public float Tolerance;

    private float currentScale;
    private float targetScale;
    private float currentStep;
    private float fromScale;
    private Vector3 rootScale;

    // Use this for initialization
    void Start()
    {
        rootScale = GetComponent<RectTransform>().localScale;
        targetScale = MaxScale;
        currentScale = MinScale;

        if(StartMax)
        {
            targetScale = MinScale;
            currentScale = MaxScale;
        }


        fromScale = currentScale;
    }

    // Update is called once per frame
    void Update()
    {
        //calculate step
        float t = Time.deltaTime / LerpTime;
        currentStep += t;

        //calculate scale
        currentScale = Mathf.Lerp(fromScale, targetScale, currentStep);

        if (targetScale == MinScale && currentScale - Tolerance <= targetScale)
        {
            targetScale = MaxScale;
            fromScale = MinScale;
            currentStep = 0f;
        }
        else if (targetScale == MaxScale && currentScale + Tolerance >= targetScale)
        {
            targetScale = MinScale;
            fromScale = MaxScale;
            currentStep = 0f;
        }

        //update transform
        GetComponent<RectTransform>().localScale = rootScale * currentScale;
    }
}
