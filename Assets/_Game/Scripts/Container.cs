using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] private int fruitCount;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fruit"))
        {
            fruitCount++;

        }
    }
}
