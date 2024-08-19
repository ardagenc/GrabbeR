using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private CustomerState currentState;
    private CustomerSO customerData;
    public ICustomerDestination MoveDestination { get; private set; }
    public ICustomerDestination LeaveDestination { get; private set; }

    public void Initialize(CustomerSO data, ICustomerDestination moveDestination, ICustomerDestination leaveDestination)
    {
        customerData = data;
        MoveDestination = moveDestination;
        LeaveDestination = leaveDestination;
    }

    public void SetState(CustomerState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    void Update()
    {
        currentState?.UpdateState();
    }
}
