using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CustomerObjectPool;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 5f;
    [SerializeField] private int _maxCustomers = 10;

    private int _currentCustomerCount = 0;

    [SerializeField] private CustomerSO[] customerTypes;
    private ICustomerDestination moveDestination;
    private ICustomerDestination leaveDestination;

    public void Initialize(ICustomerDestination moveDes, ICustomerDestination leaveDes)
    {
        this.moveDestination = moveDes;
        this.leaveDestination = leaveDes;
    }

    void Start()
    {
        if (moveDestination == null || leaveDestination == null)
        {
            Debug.LogError("MoveDestination or LeaveDestination is not set!");
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

            CustomerSO customerData = customerTypes[Random.Range(0, customerTypes.Length)];

            Customer newCustomer = CustomerFactory.CreateCustomer(customerType, customerData, moveDestination, leaveDestination);
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