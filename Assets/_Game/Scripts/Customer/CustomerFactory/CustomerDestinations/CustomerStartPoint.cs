using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerStartPoint : MonoBehaviour, ICustomerPositionPoints
{
    [SerializeField] private Vector3 spawnPoint1;
    [SerializeField] private Vector3 spawnPoint2;

    public Vector3 DetermineDestinationPoint()
    {
        int spawnIndex = Random.Range(0, 2);

        if (spawnIndex == 0)
        {
            return spawnPoint1;
        }
        else
        {
            return spawnPoint2;
        }
    }
}
