using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 5f;
    [SerializeField] private int _maxCustomers = 10;

    private int _currentCustomerCount = 0;

    private ICustomerDestination _customerDestination;

    public void Initialize(ICustomerDestination customerDestination)
    {
        _customerDestination = customerDestination;
    }

    void Start()
    {
        if (_customerDestination == null)
        {
            Debug.LogError("CustomerDestination is not set!");
            return;
        }

        StartCoroutine(SpawnCustomers());
    }

    private IEnumerator SpawnCustomers()
    {
        while (_currentCustomerCount < _maxCustomers)
        {
            yield return new WaitForSeconds(_spawnInterval);

            CustomerObjectPool.CustomerType customerType = DetermineCustomerType();

            Vector3 destination = _customerDestination.DetermineDestination();

            //Customer newCustomer = CustomerFactory.CreateCustomer(customerType, destination);

            _currentCustomerCount++;
        }
    }

    private CustomerObjectPool.CustomerType DetermineCustomerType()
    {
        //int randomTypeIndex = Random.Range(0, System.Enum.GetValues(typeof(CustomerObjectPool.CustomerType)).Length);
        //return (CustomerObjectPool.CustomerType)randomTypeIndex;

        CustomerObjectPool.CustomerType[] values =
        {
           CustomerObjectPool.CustomerType.RegularCustomer,
           CustomerObjectPool.CustomerType.NervousCustomer,
           CustomerObjectPool.CustomerType.CalmCustomer
        };

        return values[Random.Range(0, values.Length)];
    }
}