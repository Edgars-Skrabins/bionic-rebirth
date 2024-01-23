using UnityEngine;
using UnityEngine.AI;

public class Drone : MonoBehaviour
{
    [Header("Shooting settings")] [Space(5)] [SerializeField]
    private Transform[] m_muzzlePoints;

    [SerializeField] private float m_firerateInSeconds;
    [SerializeField] private GameObject m_bullet;
    [SerializeField] private float m_lookRadius = 10f;
    [SerializeField] private GameObject m_droneLight;
    [SerializeField] private Transform m_droneRaycastTF;

    [SerializeField] private LayerMask m_droneScanLayer;
    private NavMeshAgent agent;

    private bool m_droneActive = true;
    private Transform m_droneTF;

    private float m_fireTimer;
    private bool m_playerInView;
    private Transform m_playerTF;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (!m_droneActive) return;
        Move();
        Look();
    }

    private void OnEnable()
    {
        EventManager.I.OnFacilityShutdown += DeactivateDrone;
    }

    private void OnDisable()
    {
        EventManager.I.OnFacilityShutdown -= DeactivateDrone;
    }

    private void Initialize()
    {
        if (!m_playerTF)
        {
            m_playerTF = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        if (!m_droneTF)
        {
            m_droneTF = transform;
        }

        agent = GetComponent<NavMeshAgent>();
    }

    private void Move()
    {
        float distance = Vector3.Distance(m_playerTF.position, transform.position);

        if (!(distance <= m_lookRadius)) return;
        agent.SetDestination(m_playerTF.position);

        Shoot();
    }

    private void Shoot()
    {
        m_fireTimer += Time.deltaTime;
        if (!IsPlayerInView()) return;
        if (m_fireTimer < m_firerateInSeconds) return;
        foreach (Transform muzzlePoints in m_muzzlePoints)
        {
            muzzlePoints.LookAt(m_playerTF.position);
            Instantiate(m_bullet, muzzlePoints.position, muzzlePoints.rotation);
        }

        m_fireTimer = 0;
    }

    private void Look()
    {
        m_droneTF.LookAt(m_playerTF.position);
    }

    private bool IsPlayerInView()
    {
        return Physics.Raycast(
            m_droneRaycastTF.position,
            m_droneTF.forward,
            out RaycastHit hit,
            1000,
            m_droneScanLayer
        ) && hit.collider.CompareTag("Player");
    }

    public void ActivateDrone()
    {
        m_droneActive = true;
        m_droneLight.SetActive(true);
    }

    private void DeactivateDrone()
    {
        m_droneActive = false;
        m_droneLight.SetActive(false);
    }
}