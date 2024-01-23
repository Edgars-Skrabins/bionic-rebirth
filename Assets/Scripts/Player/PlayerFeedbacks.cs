using System;
using MoreMountains.Feedbacks;
using UnityEngine;

public class PlayerFeedbacks : MonoBehaviour
{

    [SerializeField] private MMF_Player m_playerTakeLaserDamage;
    [SerializeField] private MMF_Player m_playerTakeTurretDamage;
    [SerializeField] private MMF_Player m_playerInteract;
    [SerializeField] private MMF_Player m_playerShootGun;

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    
    private void SubscribeEvents()
    {
        EventManager.I.OnPlayerTakeLaserDamage += m_playerTakeLaserDamage.PlayFeedbacks;
        EventManager.I.OnPlayerTakeTurretDamage += m_playerTakeTurretDamage.PlayFeedbacks;
        EventManager.I.OnPlayerInteract += m_playerInteract.PlayFeedbacks;
        EventManager.I.OnPlayerShootGun += m_playerShootGun.PlayFeedbacks;
    }

    private void UnsubscribeEvents()
    {
        EventManager.I.OnPlayerTakeLaserDamage -= m_playerTakeLaserDamage.PlayFeedbacks;
        EventManager.I.OnPlayerTakeTurretDamage -= m_playerTakeTurretDamage.PlayFeedbacks;
        EventManager.I.OnPlayerInteract -= m_playerInteract.PlayFeedbacks;
        EventManager.I.OnPlayerShootGun -= m_playerShootGun.PlayFeedbacks;
    }

}
