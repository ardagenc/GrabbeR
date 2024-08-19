using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFactory
{
    public static Customer CreateCustomer(CustomerObjectPool.CustomerType customerType, CustomerSO customerData, ICustomerDestination moveDestination, ICustomerDestination leaveDestination)
    {
        GameObject customerObj = CustomerObjectPool.Instance.GetPooledCustomer(customerType);
        Customer customer = customerObj.GetComponent<Customer>();

        customer.Initialize(customerData, moveDestination, leaveDestination);

        Vector3 destination = moveDestination.DetermineDestination();
        customer.SetState(new MovingToTableState(customer, customerData, destination));

        return customer;
    }
}