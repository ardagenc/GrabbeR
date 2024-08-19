using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private CustomerSpawner customerSpawner;
    [SerializeField] private CustomerMoveDestination customerMoveDestination;

    private void Awake()
    {
        customerSpawner.Initialize(customerMoveDestination);
    }
}