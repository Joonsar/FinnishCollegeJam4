using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<Vector3> plots;

    [Header("VehicleSpawns")]
    public List<GameObject> vehicles;
    public int vehiclesMin = 1;
    public int vehiclesMax = 4;
    public List<Vector3> vehicleSpawnLocations;

    [Header("PickupSpawns")]
    public List<GameObject> pickups;
    public int pickupsMin = 0;
    public int pickupsMax = 2;
    public List<Vector3> pickupLocations;

    // Start is called before the first frame update
    void Start()
    {
        SpawnVehicles();
        SpawnPickups();
    } 

    void SpawnVehicles ()
    {
        int vehicleCount = vehiclesMin + (int)(Random.value * (vehiclesMax - vehiclesMin));
        for(int i = 0; i < vehicleCount; i++) {
            int location = (int)(Random.value * vehicleSpawnLocations.Count);
            GameObject vehicle = Instantiate(vehicles[(int)(Random.value * vehicles.Count)], transform.position + vehicleSpawnLocations[location], Quaternion.Euler(Vector3.up * Random.value * 360));
            vehicleSpawnLocations.RemoveAt(location);
        }
    }

    //Spawn Pickups
    void SpawnPickups()
    {
        int pickupCount = pickupsMin + (int)(Random.value * (pickupsMax - pickupsMin));
        for(int i = 0; i < pickupCount;i++)
        {
            int location = (int)(Random.value * pickupLocations.Count);
            GameObject pickup = Instantiate(pickups[(int)(Random.value * pickups.Count)], transform.position + pickupLocations[location], Quaternion.Euler(Vector3.up * Random.value * 360));
            pickupLocations.RemoveAt(location);
        }
    }

    private void OnDrawGizmosSelected()
    {
        foreach(Vector3 plot in plots)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + plot, 1f);
        }
        foreach (Vector3 loc in vehicleSpawnLocations)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(transform.position + loc, Vector3.one);
        }
        foreach (Vector3 loc in pickupLocations)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position + loc, Vector3.one);
        }
    }
}
