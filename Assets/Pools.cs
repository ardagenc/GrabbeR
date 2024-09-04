using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pools : MonoBehaviour
{
    public GameObject tomatoPrefab;
    public Transform tomatoParent;
    void Start()
    {
        CreateObjectPool(tomatoPrefab, 100, tomatoParent);
    }

    private void CreateObjectPool(GameObject prefab, int poolSize, Transform parent)
    {
        ObjectPool.Instance.CreatePool(prefab, poolSize, parent);
    }
}
