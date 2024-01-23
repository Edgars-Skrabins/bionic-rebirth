using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_droneGO;
    [SerializeField] private Transform m_spawnPosition;
    [SerializeField] private float m_spawnRateInSeconds;
    private int m_amountOfDronesSpawned;
    private float m_maxDrones;
    private float m_spawnDroneTimer;

    private void Start()
    {
        m_maxDrones = 4 * PlayerPrefs.GetFloat("Difficulty");
    }

    private void Update()
    {
        if (EventManager.I.OnFacilityShutdownHasInvoked)
        {
            return;
        }

        m_spawnDroneTimer += Time.deltaTime;

        if (m_spawnDroneTimer > m_spawnRateInSeconds && m_amountOfDronesSpawned < m_maxDrones)
        {
            SpawnDrone();
        }
    }

    private void SpawnDrone()
    {
        Instantiate(m_droneGO, m_spawnPosition.transform.position, Quaternion.identity);
        m_amountOfDronesSpawned++;
        m_spawnDroneTimer = 0;
    }
}