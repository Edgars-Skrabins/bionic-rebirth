using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{

    [SerializeField] private float m_bulletSpeed;
    [SerializeField] private int m_bulletDamage;

    private void Update()
    {
        transform.position += transform.forward * (m_bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(other.TryGetComponent(out Health health))
            {
                health.TakeDamage(m_bulletDamage, true);
                EventManager.I.InvokeOnPlayerTakeTurretDamage();
            }
        }
        
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Player"))
        {
            if(other.collider.TryGetComponent(out Health health))
            {
                health.TakeDamage(m_bulletDamage, true);
                EventManager.I.InvokeOnPlayerTakeTurretDamage();
            }
        }
        
        Destroy(gameObject);
    }
}
