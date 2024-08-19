using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToTableState : CustomerState
{
    private Vector3 destination;
    private float moveSpeed = 5f;

    public MovingToTableState(Customer customer, CustomerSO customerData, Vector3 destination) : base(customer, customerData)
    {
        this.destination = destination;
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