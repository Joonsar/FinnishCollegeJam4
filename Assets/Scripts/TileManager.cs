using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Vector2 size = Vector2.one;
    public List<Vector3> plots;

    public List<GameObject> buildings;
 
    [Header("VehicleSpawns")]
    public List<GameObject> vehicles;
    public int vehiclesMin = 1;
    public int vehiclesMax = 4;
    public List<Vector3> vehicleSpawnLocations;

    [Header("Enemy Spawns")]
    public List<Vector3> enemySpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        SpawnVehicles();
        SpawnBuildings();
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

    void SpawnBuildings()
    {
        //Luodaan rakennukset
        for (int i = 0; i < plots.Count; i++)
        {
            Quaternion rotation = Quaternion.Euler(Vector3.up * 90 * (int)(Random.value * 4));  //Rakennuksen rotaatio
            int buildingID = (int)(Random.value * buildings.Count);
            GameObject building = Instantiate(buildings[buildingID], transform.position + plots[i], rotation);
            building.transform.parent = transform;
        }
    }

    private void OnDrawGizmosSelected()
    {
        foreach(Vector3 plot in plots)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position + plot, 1f);
        }
        foreach (Vector3 loc in vehicleSpawnLocations)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(transform.position + loc, Vector3.one);
        }
        foreach (Vector3 loc in enemySpawnPoints)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position + loc, Vector3.one / 2);
        }
    }
}
