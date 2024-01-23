using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [Header("Shooting settings")]
    [Space(5)]
    [SerializeField] private Transform[] m_muzzlePoints;
    [SerializeField] private float m_firerateInSeconds;
    [SerializeField] private GameObject m_bullet;

    [SerializeField] private Transform m_turretTF;
    [SerializeField] private GameObject m_turretLight;

    [SerializeField] private LayerMask m_turretScanLayer;

    [SerializeField] private AudioSource audioSource;

    private bool m_turretActive = true;
    private Transform m_playerTF;
    
    private float m_fireTimer;
    private bool m_playerInView;


    private void OnEnable()
    {
        EventManager.I.OnFacilityShutdown += DeactivateTurret;
    }

    private void OnDisable()
    {
        EventManager.I.OnFacilityShutdown -= DeactivateTurret;
    }

    private void Start()
    {
        if(!m_playerTF)
        {
            m_playerTF = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
        
        if(!m_turretTF)
        {
            m_turretTF = transform;
        }
    }
        
    private void Update()
    {
        if(!m_turretActive) return;

        Shoot();
        Look();
    }


    private void Shoot()
    {
        
        m_fireTimer += Time.deltaTime;

        if(!GetPlayerInView()) return;

        if(m_fireTimer < m_firerateInSeconds) return;
        
        foreach(Transform muzzlePoints in m_muzzlePoints)
        {
            muzzlePoints.LookAt(GetPlayerPositionWithoutY());
            Instantiate(m_bullet,muzzlePoints.position, muzzlePoints.rotation);
            
        }
        audioSource.PlayOneShot(audioSource.clip);
        m_fireTimer = 0;

    }

    private void Look()
    {
        m_turretTF.LookAt(GetPlayerPositionWithoutY());
    }

    private bool GetPlayerInView()
    {
        Vector3 directionToPlayer = GetPlayerPositionWithoutY() - m_turretTF.position;

        if(Physics.Raycast(m_turretTF.position, directionToPlayer, out RaycastHit hit, 1000, m_turretScanLayer))
        {
            if(hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        
        return false;

    }

    private Vector3 GetPlayerPositionWithoutY()
    {
        return new Vector3(m_playerTF.position.x, m_turretTF.position.y, m_playerTF.position.z);
    }
    
    public void ActivateTurret()
    {
        m_turretActive = true;
        m_turretLight.SetActive(true);
    }
    
    public void DeactivateTurret()
    {
        m_turretActive = false;
        m_turretLight.SetActive(false);
    }
    
}
