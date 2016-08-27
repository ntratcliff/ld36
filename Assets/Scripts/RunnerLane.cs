using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RunnerLane : MonoBehaviour
{
    public GameObject[] ObstaclePrefabs;
    private List<GameObject> objectPool;
    private List<GameObject> activeObjects;

    public bool CanSpawn = true;
    public float MinCooldown = 1f, MaxCooldown = 5f;
    [HideInInspector]
    public float Cooldown;
    public float DespawnDist = 20f;

    private Coroutine activeCooldown;

    public void Start()
    {
        objectPool = new List<GameObject>();
        activeObjects = new List<GameObject>();
    }

    public void Update()
    {
        if (activeObjects.Count > 0)
        {
            GameObject furthestObj = activeObjects[0];
            Vector3 furthestObjPos = furthestObj.transform.position;
            Debug.DrawRay(furthestObjPos, Vector3.up);
            float dist = Vector3.Distance(transform.position, furthestObjPos);
            Debug.Log(dist);
            Debug.Log(furthestObjPos);
            Debug.Log(transform.position);
            if (dist >= DespawnDist)
                DespawnObstacle(activeObjects[0]);
        }
    }

    public void SpawnObstacle()
    {
        if (CanSpawn)
        {
            int i = Random.Range(0, ObstaclePrefabs.Length);
            GameObject obstacle = getGameObject(i);
            //move obstacle to this lane's spawn point, relative to prefab root pos
            obstacle.transform.position = transform.TransformPoint(getRootLocalPos(obstacle));
            activeObjects.Add(obstacle);

            Cooldown = Random.Range(MinCooldown, MaxCooldown);
            StartCooldown(Cooldown);



            if (obstacle.name == "RunnerSkeleton")
            {
                //set cooldown on neighbor
                RunnerLane neighbor;
                if (this.name == "P1")
                    neighbor = transform.parent.FindChild("P2").GetComponent<RunnerLane>();
                else if (this.name == "P2")
                    neighbor = transform.parent.FindChild("P3").GetComponent<RunnerLane>();
                else
                    neighbor = transform.parent.FindChild("P4").GetComponent<RunnerLane>();

                neighbor.StartCooldown(Cooldown);
            }
        }
    }

    public void DespawnObstacle(GameObject obj)
    {
        activeObjects.Remove(obj);
        //add to pool
        objectPool.Add(obj);
    }

    public void StartCooldown(float time)
    {
        if (activeCooldown != null)
            StopCoroutine(activeCooldown);

        activeCooldown = StartCoroutine(SetCooldown(time));
    }

    public void StartCooldown()
    {
        StartCooldown(Random.Range(MinCooldown, MaxCooldown));
    }

    IEnumerator SetCooldown(float time)
    {
        CanSpawn = false;
        yield return new WaitForSeconds(time);
        CanSpawn = true;
    }

    private Vector3 getRootLocalPos(GameObject obj)
    {
        for (int i = 0; i < ObstaclePrefabs.Length; i++)
        {
            if (obj.name == ObstaclePrefabs[i].name)
                return ObstaclePrefabs[i].transform.localPosition;
        }

        return Vector3.zero;
    }

    private GameObject getGameObject(int prefabIndex)
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (objectPool[i].name == ObstaclePrefabs[prefabIndex].name)
            {
                GameObject ret = objectPool[i];
                objectPool.RemoveAt(i);
                return ret;
            }
        }

        GameObject newObj = GameObject.Instantiate(ObstaclePrefabs[prefabIndex]);
        newObj.name = ObstaclePrefabs[prefabIndex].name;
        //newObj.GetComponent<RunnerObstacle>().Parent = this;
        return newObj;
    }
}
