using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPool
{
    private static ObjectPool instance;
    public static ObjectPool Instance
    {
        get 
        { 
            if (instance == null) instance = new ObjectPool();
            return instance; 
        }
    }

    private readonly Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    public void CreatePool(GameObject prefab, int poolSize, Transform parent)
    {
        string poolKey = prefab.name;

        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<GameObject>());
            
            for (int i = 0; i < poolSize; i++)
            {
                GameObject poolObject = GameObject.Instantiate(prefab, parent);
                poolObject.name = poolKey;
                poolObject.SetActive(false);
                poolDictionary[poolKey].Enqueue(poolObject);
            }
        }
    }

    public GameObject GetObject(GameObject prefab)
    {
        string poolKey = prefab.name;

        if (poolDictionary.ContainsKey(poolKey) && poolDictionary[poolKey].Count > 0)
        {
            GameObject objectToSpawn = poolDictionary[poolKey].Dequeue();
            objectToSpawn.SetActive(true);
            Debug.Log(objectToSpawn);
            return objectToSpawn;
        }

        return null;
    }

    public void ReturnObject(GameObject prefab, GameObject objectToReturn)
    {
        string poolKey = prefab.name;

        if (poolDictionary.ContainsKey(poolKey))
        {
            objectToReturn.SetActive(false);
            poolDictionary[poolKey].Enqueue(objectToReturn);
        }

    }

}
