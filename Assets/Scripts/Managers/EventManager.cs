using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{

    public event Action OnPlayerTakeLaserDamage;
    public event Action OnPlayerTakeTurretDamage;
    public event Action OnPlayerInteract;
    public event Action OnPlayerShootGun;

    public event Action OnFacilityShutdown;
    public bool OnFacilityShutdownHasInvoked;

    public void InvokeOnPlayerTakeLaserDamageEvent()
    {
        OnPlayerTakeLaserDamage?.Invoke();
    }

    public void InvokeOnPlayerTakeTurretDamage()
    {
        OnPlayerTakeTurretDamage?.Invoke();
    }

    public void InvokeOnPlayeInteract()
    {
        OnPlayerInteract?.Invoke();
    }

    public void InvokeOnPlayerShootGun()
    {
        OnPlayerShootGun?.Invoke();
    }

    public void InvokeOnFacilityShutdown()
    {
        OnFacilityShutdown?.Invoke();
        OnFacilityShutdownHasInvoked = true;
    }

}
