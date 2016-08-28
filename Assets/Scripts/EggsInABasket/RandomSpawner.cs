﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] Prefabs;

    private List<GameObject> objectPool;
    private List<GameObject> activeObjects;

    public float Radius, Height;

    public float MinCooldown, MaxCooldown;

    private bool coroutineRunning = false;

    // Use this for initialization
    void Start()
    {
        objectPool = new List<GameObject>();
        activeObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!coroutineRunning)
        {
            StartCoroutine(Spawn(Random.Range(MinCooldown, MaxCooldown)));
        }
    }

    private GameObject depool(int i)
    {
        GameObject o = objectPool[i];
        objectPool.RemoveAt(i);
        o.SetActive(true);
        activeObjects.Add(o);
        return o;
    }

    private void enpool(GameObject o)
    {
        activeObjects.Remove(o);
        o.GetComponent<Rigidbody>().velocity = Vector3.zero;
        o.SetActive(false);
        objectPool.Add(o);
    }

    public void Despawn(GameObject o)
    {
        enpool(o);
    }

    private GameObject getGameObject(int prefabIndex)
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (objectPool[i].name == Prefabs[prefabIndex].name)
            {
                return depool(i);
            }
        }

        GameObject newObj = GameObject.Instantiate(Prefabs[prefabIndex]);
        newObj.name = Prefabs[prefabIndex].name;
        activeObjects.Add(newObj);
        return newObj;
    }

    IEnumerator Spawn(float delay)
    {
        coroutineRunning = true;
        yield return new WaitForSeconds(delay);
        if(Prefabs.Length > 0)
        {
            int i = Random.Range(0, Prefabs.Length);
            GameObject obj = getGameObject(i);

            Vector2 pos2D = Random.insideUnitCircle * Radius;
            obj.transform.position = new Vector3(pos2D.x, Height, pos2D.y);

            obj.GetComponent<EggsObject>().Spawner = this;
        }
        coroutineRunning = false;
    }
}