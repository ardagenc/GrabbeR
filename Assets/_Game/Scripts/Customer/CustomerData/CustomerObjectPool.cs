using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerObjectPool : MonoBehaviour
{
    public static CustomerObjectPool Instance { get; private set; }

    [SerializeField] private CustomerSO[] customerTypes = null;

    private Dictionary<CustomerSO, Queue<GameObject>> pools = new Dictionary<CustomerSO, Queue<GameObject>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        InitializePools();
    }

    private void InitializePools()
    {
        foreach (var customerType in customerTypes)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < customerType.customerPoolCount; i++)
            {
                GameObject obj = Instantiate(customerType.customerPrefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            pools[customerType] = queue;
        }
    }

    public GameObject GetPooledCustomer(CustomerSO customerType)
    {
        if (!pools.ContainsKey(customerType))
        {
            Debug.LogError($"No pool found for customer type: {customerType.customerID}");
            return null;
        }

        var pool = pools[customerType];
        GameObject obj = pool.Dequeue();
        obj.SetActive(true);
        pool.Enqueue(obj);
        return obj;
    }
}