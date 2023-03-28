using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    
    PlayerScript target;
    [SerializeField] int damage = 40;
    public GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;

    void Start()
    {
        target = FindObjectOfType<PlayerScript>();
    }

    public void AttackHitEvent()
    {
        if (target == null) return;
        target.TakeDamage(damage);
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.transform.parent = transform.Find("ProjectileSpawnPoint");
        projectile.transform.localPosition = Vector3.zero;
        projectile.transform.localRotation = Quaternion.identity;
        projectile.GetComponent<Projectile>().speed = projectileSpeed;
        Debug.Log("Bang bang");

    }
}
