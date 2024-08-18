using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerObjectPool : MonoBehaviour
{
    public static CustomerObjectPool Instance { get; private set; }

    [SerializeField] private CustomerPool[] _pools = null;

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
        for (int j = 0; j < _pools.Length; j++)
        {
            _pools[j].pooledCustomers = new Queue<GameObject>();

            for (int i = 0; i < _pools[j].pooledCustomerCount; i++)
            {
                GameObject obj = Instantiate(_pools[j].customerPrefab);
                obj.SetActive(false);

                _pools[j].pooledCustomers.Enqueue(obj);
            }
        }
    }

    public GameObject GetPooledBullet(CustomerType customerType)
    {
        int customerTypeIndex = (int)customerType;

        if (customerTypeIndex >= _pools.Length)
        {
            return null;
        }

        GameObject obj = _pools[customerTypeIndex].pooledCustomers.Dequeue();
        obj.SetActive(true);

        _pools[customerTypeIndex].pooledCustomers.Enqueue(obj);

        return obj;
    }

    [System.Serializable]
    public struct CustomerPool
    {
        public Queue<GameObject> pooledCustomers;
        public GameObject customerPrefab;
        public int pooledCustomerCount;
    }

    public enum CustomerType
    {
        StandartBullet,
        AOEBullet,
    }
}