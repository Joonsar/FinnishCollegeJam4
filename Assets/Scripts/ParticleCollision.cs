using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    ParticleSystem ps;
    public int damage = 5;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
}
