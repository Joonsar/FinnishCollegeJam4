using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int maxEnemies = 50;

    private List<GameObject> enemies;

    private GameObject player;

    public Terrain terrain;



    public GameObject enemyPrefab;
    void Start()

    {
        Bounds bounds = terrain.terrainData.bounds;
        enemies = new List<GameObject>();


        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < maxEnemies; i++)
        {
            Vector3 randomPoint = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            bounds.max.y,
            Random.Range(bounds.min.z, bounds.max.z)
    );
            GameObject go = Instantiate(enemyPrefab, randomPoint, Quaternion.identity) as GameObject;
            enemies.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
