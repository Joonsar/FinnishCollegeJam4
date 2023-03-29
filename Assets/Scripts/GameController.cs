using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int maxEnemies = 10;

    public List<GameObject> enemies;

    private AudioController audioController;

    private GameObject player;

    public List<GameObject> enemyPrefabs;

    public Transform navmesh;

    LevelGenerator generator;
    public float spawnClock = 1;


    public GameObject enemyPrefab;
    void Start()

    {
        audioController = FindAnyObjectByType<AudioController>();
        Bounds bounds = navmesh.GetComponent<MeshFilter>().mesh.bounds;
        enemies = new List<GameObject>();
        generator = GetComponent<LevelGenerator>();

        player = GameObject.FindGameObjectWithTag("Player");

        InvokeRepeating("EnemySpawns", 0, spawnClock);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemySpawns()
    {
        maxEnemies = 10 + (int)((600 - GetComponent<Timer>().timeRemaining) * .1f);
        if (enemies.Count < maxEnemies)
        {
            //Valitaan tile, jonka spawn pointteja k�ytet��n
            GameObject chosenTile = generator.map[1, 1];
            foreach (GameObject t in generator.map)
            {
                if (t != null)
                {
                    if (Vector3.Distance(t.transform.position, player.transform.position) < Vector3.Distance(t.transform.position, player.transform.position))
                    {
                        chosenTile = t;
                    }
                }
            }
            TileManager tile = chosenTile.GetComponent<TileManager>();
            GameObject go = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)],
                chosenTile.transform.position + tile.enemySpawnPoints[Random.Range(0, tile.enemySpawnPoints.Count)],
                Quaternion.identity);
            enemies.Add(go);
        }
    }

    public void InvokeSkill(Skill skill)
    {

        if (skill.Ps != null)
        {

        }
        if (skill.Name == "Chain Lightning")
        {
            audioController.PlayAudio(Audios.blackholesound);
            for (int i = 0; i < skill.Level; i++)
            {
                ParticleSystem part = Instantiate(skill.Ps, player.transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), Quaternion.identity) as ParticleSystem;
                Destroy(part.gameObject, 5);
            }
        }

        if (skill.Name == "Lazer Riffle")
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y));
            Vector3 lookDirection = mouseWorldPosition - player.transform.position;
            //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //mousePosition.y = transform.position.y;

            //Vector3 lookDir = mousePosition - transform.position;
            lookDirection.y = 0;
            //Debug.Log(lookDir);
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
            for (int i = 0; i < skill.Level; i++)
            {
                ParticleSystem part = Instantiate(skill.Ps, player.transform.position + new Vector3(0, 1f, 0), lookRotation) as ParticleSystem;
                Destroy(part.gameObject, 2);
            }
            audioController.PlayAudio(Audios.riffleSound);


        }


    }


}
