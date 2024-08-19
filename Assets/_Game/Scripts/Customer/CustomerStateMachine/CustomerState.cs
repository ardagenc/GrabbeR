using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomerState
{
    protected Customer customer;
    protected CustomerSO customerData;

    public CustomerState(Customer customer, CustomerSO customerData)
    {
        this.customer = customer;
        this.customerData = customerData;
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
}
