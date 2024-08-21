using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    private PlayerController playerController;

    public List<GameObject> fruits;

    public float grabForce;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();

        PlayerController.onGrab += Grab;
    }

    void FixedUpdate()
    {
        if (!playerController.IsOpen)
        {
            foreach(GameObject fruit in fruits)
            {
                fruit.GetComponent<Rigidbody>().velocity = (transform.position - fruit.transform.position) * grabForce;
            }
        }
    }

    private void Grab()
    {   
        if(playerController.IsOpen)
        {
            for (int i = 0; i < fruits.Count; i++)
            {
                fruits[i].transform.parent = this.transform;
            } 
        }
        else
        {
            for (int i = 0; i < fruits.Count; i++)
            {
                fruits[i].transform.parent = null;
            } 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fruit"))
        {
            Debug.Log("Collision");
            fruits.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        fruits.Remove(other.gameObject);
    }
}
