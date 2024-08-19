using UnityEngine;

public class CustomerMoveDestination : MonoBehaviour, ICustomerDestination
{
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float minZ = -10f;
    [SerializeField] private float maxZ = 10f;

    public Vector3 DetermineDestination()
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);
        return new Vector3(x, 0f, z);
    }
}