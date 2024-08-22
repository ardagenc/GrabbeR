using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFactory
{
    public static Customer CreateCustomer(CustomerSO customerData, ICustomerPositionPoints moveDest, ICustomerPositionPoints leaveDest)
    {
        GameObject customerObj = CustomerObjectPool.Instance.GetPooledCustomer(customerData);
        Customer customer = customerObj.GetComponent<Customer>();

        customer.Initialize(customerData, moveDest, leaveDest);

        Vector3 destination = moveDest.DetermineDestinationPoint();
        customer.SetState(new MovingToTableState(customer, customerData, destination));

        return customer;
    }
}