using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int maxEnemies = 50;

    private List<GameObject> enemies;

    private GameObject player;

    public List<GameObject> enemyPrefabs;

    public Transform navmesh;





    public GameObject enemyPrefab;
    void Start()

    {
        Bounds bounds = navmesh.GetComponent<MeshFilter>().mesh.bounds;
        enemies = new List<GameObject>();


        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < maxEnemies; i++)
        {
            Vector3 randomPoint = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            bounds.max.y,
            Random.Range(bounds.min.z, bounds.max.z)
    );
            GameObject go = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], randomPoint, Quaternion.identity) as GameObject;
            enemies.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InvokeSkill(Skill skill)
    {

        if (skill.Ps != null)
        {

        }
        if (skill.Name == "Chain Lightning")
        {
            ParticleSystem part = Instantiate(skill.Ps, player.transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), Quaternion.identity) as ParticleSystem;
            Destroy(part.gameObject, 5);


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
            ParticleSystem part = Instantiate(skill.Ps, player.transform.position, lookRotation) as ParticleSystem;
            Destroy(part.gameObject, 2);


        }


    }


}
