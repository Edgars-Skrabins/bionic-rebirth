using System;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private Transform m_laserTF;

    [Header("Laser Damage Settings")]
    [Space(5)]
    [SerializeField] private int m_laserDamage = 1;
    [SerializeField] private float m_damageFrequencyInSeconds = 1f;
    [Space(10)]

    [Header("Checkpoint Settings")]
    [Space(5)]
    [SerializeField] private Transform[] m_checkpoints;
    [SerializeField] private bool m_useCheckpoints;
    [SerializeField] private float m_laserMoveSpeed;
    [Space(10)]

    [SerializeField] private GameObject m_laserGFX;

    private Transform m_currentCheckpoint;
    private int m_currentCheckpointIndex;

    private bool m_laserActive = true;

    private void OnEnable()
    {
        GameEvents.I.OnFacilityShutdown += DeactivateLaser;
    }

    private void OnDisable()
    {
        GameEvents.I.OnFacilityShutdown -= DeactivateLaser;
    }


    private void Start()
    {
        m_laserTF = transform;

        if (m_useCheckpoints)
        {
            if (m_checkpoints.Length > 0)
            {
                m_currentCheckpoint = m_checkpoints[m_currentCheckpointIndex];
            }
        }

    }

    private float m_damageTimer;
    private void Update()
    {
        m_damageTimer += Time.deltaTime;

        if (m_useCheckpoints && m_laserActive)
        {
            CheckCheckpointDistance();
            MoveTowardsCheckpoint();
        }

    }

    private void MoveTowardsCheckpoint()
    {
        m_laserTF.Translate(GetDirectionToCurrentCheckpoint().normalized * (m_laserMoveSpeed * Time.deltaTime));
    }

    private void CheckCheckpointDistance()
    {
        float distanceBetweenCheckpointAndTurret = GetDirectionToCurrentCheckpoint().magnitude;

        if (distanceBetweenCheckpointAndTurret < 0.05f)
        {
            if (m_currentCheckpointIndex + 1 >= m_checkpoints.Length)
            {
                SetNextCheckpoint();
                m_currentCheckpointIndex = 0;
            }
            else
            {
                SetNextCheckpoint();
                m_currentCheckpointIndex += 1;
            }

        }
    }

    private void SetNextCheckpoint()
    {
        m_currentCheckpoint = m_checkpoints[m_currentCheckpointIndex];
    }

    private Vector3 GetDirectionToCurrentCheckpoint()
    {
        Vector3 direction = m_currentCheckpoint.position - m_laserTF.position;
        return direction;
    }

    private void OnTriggerStay(Collider _other)
    {
        if (!m_laserActive)
            return;

        DoDamage(_other);

    }

    private void DoDamage(Collider _other)
    {
        if (_other.TryGetComponent(out Health health))
        {
            if (m_damageTimer >= m_damageFrequencyInSeconds)
            {
                GameEvents.I.InvokeOnPlayerTakeLaserDamageEvent();
                health.TakeDamage(m_laserDamage, false);
                m_damageTimer = 0;
            }
        }
    }

    [ContextMenu("DeactivateLaser")]
    public void DeactivateLaser()
    {
        m_laserActive = false;
        m_laserGFX.SetActive(false);
    }

    [ContextMenu("ActivateLaser")]
    public void ActivateLaser()
    {
        m_laserActive = true;
        m_laserGFX.SetActive(true);
    }

}
