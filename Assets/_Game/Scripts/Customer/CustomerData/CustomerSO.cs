using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Customer Type", menuName = "Customer Type")]
public class CustomerSO : ScriptableObject
{
    public string customerID;
    public GameObject customerPrefab;
    public float customerWaitTimeOnOrdering;
    public float customerMovementSpeed;
    public int customerPoolCount;
}