using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;


    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy Hit player!");
            // Damage the player and destroy the projectile
            collision.gameObject.GetComponent<PlayerScript>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            // Destroy the projectile if it collides with anything else
            Destroy(gameObject);
        }
    }
}
