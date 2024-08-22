using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 5f;
    [SerializeField] private int _maxCustomers = 10;
    private int _currentCustomerCount = 0;

    [SerializeField] private CustomerSO[] customerTypes;

    private ICustomerPositionPoints moveDestination;
    private ICustomerPositionPoints leaveDestination;

    public void Initialize(ICustomerPositionPoints moveDes, ICustomerPositionPoints leaveDes)
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

            CustomerSO customerData = customerTypes[Random.Range(0, customerTypes.Length)];

            Customer newCustomer = CustomerFactory.CreateCustomer(customerData, moveDestination, leaveDestination);

            _currentCustomerCount++;
        }
    }
}