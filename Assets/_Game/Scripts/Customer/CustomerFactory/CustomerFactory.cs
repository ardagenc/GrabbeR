using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFactory
{
    public static Customer CreateCustomer(CustomerSO customerData, ICustomerDestination moveDest, ICustomerDestination leaveDest)
    {
        GameObject customerObj = CustomerObjectPool.Instance.GetPooledCustomer(customerData);
        Customer customer = customerObj.GetComponent<Customer>();

        customer.Initialize(customerData, moveDest, leaveDest);

        Vector3 destination = moveDest.DetermineDestination();
        customer.SetState(new MovingToTableState(customer, customerData, destination));

        return customer;
    }
}