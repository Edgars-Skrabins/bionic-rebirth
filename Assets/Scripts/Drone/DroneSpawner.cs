using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject droneGO;
    [SerializeField] private Transform spawnpos;
    [SerializeField] private float spawnRateInSeconds;
    private float spawnDroneTimer;
    private int amountOfDronesSpawned;
    private float maxDrones;

    private void Start()
    {
        maxDrones = 4 * PlayerPrefs.GetFloat("Difficulty");
    }

    // Update is called once per frame
    void Update()
    {

        // if(amountOfDronesSpawned > maxDrones)
        // {
        //     Destroy(gameObject);
        // }
        
        if(EventManager.I.OnFacilityShutdownHasInvoked) { return; }
        spawnDroneTimer += Time.deltaTime;
        
        if(spawnDroneTimer > spawnRateInSeconds && amountOfDronesSpawned < maxDrones)
        {
            SpawnDrone();
        }
        
    }
    public void SpawnDrone()
    {
        Instantiate(droneGO, spawnpos.transform.position, Quaternion.identity);
        amountOfDronesSpawned++;
        spawnDroneTimer = 0;
    }
}
