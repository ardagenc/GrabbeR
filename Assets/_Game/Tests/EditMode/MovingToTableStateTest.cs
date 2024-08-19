using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MovingToTableStateTest
{
    [Test]
    public void EnterState_DebugLogMessage()
    {
        //Arrange
        var customerSO = ScriptableObject.CreateInstance<CustomerSO>();
        customerSO.customerID = "TestCustomer";

        var customer = new GameObject().AddComponent<Customer>();

        Vector3 destination = Vector3.zero;

        var state = new MovingToTableState(customer, customerSO, destination);

        //Act
        LogAssert.Expect(LogType.Log, "TestCustomer start to moving!");
        state.EnterState();

        //Clean up
        Object.DestroyImmediate(customer.gameObject);
        Object.DestroyImmediate(customerSO);
    }
}