using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private CustomerSpawner customerSpawner;
    [SerializeField] private CustomerMoveDestination customerMoveDestination;
    [SerializeField] private CustomerLeaveDestination customerLeaveDestination;

    private void Awake()
    {
        customerSpawner.Initialize(customerMoveDestination, customerLeaveDestination);
    }
}