using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pistol : MonoBehaviour
{

    [Header("References")]
    [Space(5)]
    [SerializeField] private Transform m_pistolTF;
    [SerializeField] private Transform m_pistolMuzzleTF;
    [SerializeField] private Transform m_playerCameraTF;
    [Space(10)]
    
    [Header("Pistol shooting settings")]
    [Space(5)]
    [SerializeField] private float m_fireRateInSeconds;
    [SerializeField] private int m_pistolDamage;
    [SerializeField] private LayerMask m_gunLayerMask;
    [Space(10)]

    [Header("Pistol animation settings")]
    [Space(5)]
    [SerializeField] private float m_pistolRotationSpeed;
    [SerializeField] private float m_pistolLookAtActivationDistance;
    [SerializeField] private Animation m_gunAnimation;

    [Space(5)]
    [SerializeField] private float m_swayMultiplier;
    [SerializeField] private float m_SwayRoughness;

    [Space(5)]
    [SerializeField] private GameObject m_muzzleVFX;
    [SerializeField] private GameObject m_lineVFX;

    private float m_fireTimer;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        m_fireTimer += Time.deltaTime;
        
        RotateGunTowardsCameraCenter();

        if(Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if(m_fireTimer >= m_fireRateInSeconds)
        {
            m_fireTimer = 0;
            if(Physics.Raycast(m_playerCameraTF.position, m_playerCameraTF.forward,out RaycastHit hit,1000f,m_gunLayerMask))
            {
                if(hit.collider.TryGetComponent(out Health health))
                {
                    float damage = m_pistolDamage * PlayerPrefs.GetFloat("Difficulty");
                    health.TakeDamage(damage, false);
                }
            }

            PlayMuzzleVFX();
            GameEvents.I.InvokeOnPlayerShootGun();
            audioSource.PlayOneShot(audioSource.clip);
            m_gunAnimation.Stop();
            m_gunAnimation.CrossFade("Pistol_Shoot", 0);
        }
    }

    private void PlayMuzzleVFX()
    {
        var muzzleVFX = Instantiate(m_muzzleVFX, m_pistolMuzzleTF.position, Quaternion.identity);
        muzzleVFX.transform.SetParent(m_pistolTF);
        
        Instantiate(m_lineVFX, m_pistolMuzzleTF.position, m_pistolMuzzleTF.rotation);
    }

    private void RotateGunTowardsCameraCenter()
    {

        if(!Physics.Raycast(m_playerCameraTF.position, m_playerCameraTF.forward,out RaycastHit hit,1000f,m_gunLayerMask)) return;
        
        Vector3 pistolToHitPoint = hit.point - m_pistolTF.position;

        if(pistolToHitPoint.magnitude <= m_pistolLookAtActivationDistance) return;

        Quaternion lookRotation = Quaternion.LookRotation(pistolToHitPoint.normalized);
            
        m_pistolTF.rotation = Quaternion.Slerp(m_pistolTF.rotation, lookRotation, Time.deltaTime / m_pistolRotationSpeed);
            
    }

}
