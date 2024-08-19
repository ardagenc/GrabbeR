using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerLeaveDestination : MonoBehaviour, ICustomerDestination
{
    [SerializeField] private Vector3 leaveDestination1;
    [SerializeField] private Vector3 leaveDestination2;

    public Vector3 DetermineDestination()
    {
        int randomIndex = Random.Range(0, 2);

        if (randomIndex == 0)
        {
            return leaveDestination1;
        }
        else
        {
            return leaveDestination2;
        }
    }
}