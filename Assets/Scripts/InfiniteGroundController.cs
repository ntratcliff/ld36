using UnityEngine;
using System.Collections;
using System.Linq;

public class InfiniteGroundController : MonoBehaviour
{
    [Tooltip("Distance from camera when backmost ground piece is moved to front")]
    public float Threshold;

    //pool for ground objects
    private Queue objectPool;

    //distance between ground objects
    private Vector3 posDelta;

    // Use this for initialization
    void Start()
    {
        objectPool = new Queue();

        //add ground objects to objectPool in order of distance from camera
        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground");

        Vector3 cameraPos = Camera.main.transform.position;
        groundObjects = (from obj in groundObjects
                         orderby Vector3.Distance(cameraPos, obj.transform.position)
                         select obj).ToArray();

        for (int i = 0; i < groundObjects.Length; i++)
        {
            objectPool.Enqueue(groundObjects[i]);
        }

        //calculate distance between ground objects
        if (groundObjects.Length > 1)
            posDelta = groundObjects[1].transform.position - groundObjects[0].transform.position;
        else
            Debug.LogWarning("Only one ground object found in scene!");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        GameObject backmost = objectPool.Peek() as GameObject;
        Vector3 backmostPos = backmost.transform.position;

        if (Vector3.Distance(cameraPos, backmostPos) >= Threshold)
        {
            //move backmost ground object to the front
            GameObject ground = objectPool.Dequeue() as GameObject;
            ground.transform.position = (objectPool.Peek() as GameObject).transform.position + posDelta;
            objectPool.Enqueue(ground);
        }
    }
}
