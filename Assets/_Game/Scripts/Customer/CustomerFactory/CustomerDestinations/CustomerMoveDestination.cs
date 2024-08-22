using UnityEngine;

public class CustomerMoveDestination : MonoBehaviour, ICustomerPositionPoints
{
    [SerializeField] private Vector3 moveDestination1;
    [SerializeField] private Vector3 moveDestination2;

    public Vector3 DetermineDestinationPoint()
    {
        int randomIndex = Random.Range(0, 2);

        if (randomIndex == 0)
        {
            return moveDestination1;
        }
        else
        {
            return moveDestination2;
        }
    }
}