using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    

    private void OnEnable()
    {
        EventManager.I.OnFacilityShutdown += OpenElevatorDoor;
    }

    private void OnDisable()
    {
        EventManager.I.OnFacilityShutdown -= OpenElevatorDoor;
    }

    private void OpenElevatorDoor()
    {
        // Opens elevator door
    }
}
