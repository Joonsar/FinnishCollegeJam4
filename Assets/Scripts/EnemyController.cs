using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float fireRate = 1f;

    private PlayerScript player;
    private float timeSinceLastShot = 0f;

    void Start()
    {
        player = FindObjectOfType<PlayerScript>();
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= 1 / fireRate)
        {
            Shoot();
            timeSinceLastShot = 0f;
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().velocity = (player.transform.position - transform.position).normalized * projectileSpeed;
        projectile.transform.LookAt(player.transform);
    }
}
