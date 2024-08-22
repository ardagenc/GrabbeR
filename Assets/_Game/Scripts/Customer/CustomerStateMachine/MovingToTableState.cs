using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToTableState : CustomerState
{
    private Vector3 destination;

    public MovingToTableState(Customer customer, CustomerSO customerData, Vector3 destination) : base(customer, customerData)
    {
        this.destination = destination;
    }

    public override void EnterState()
    {
        customer.transform.DOMove(destination, 5f);
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
    }
}