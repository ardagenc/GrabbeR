using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingForOrderState : CustomerState
{
    private float waitTime;
    public WaitingForOrderState(Customer customer, CustomerSO customerData) : base(customer, customerData)
    {
        this.waitTime = customerData.customerWaitTime;
    }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}