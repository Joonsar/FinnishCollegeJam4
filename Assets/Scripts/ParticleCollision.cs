using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    ParticleSystem ps;


    public int damage = 10;
    void Start()
    {

        ps = GetComponent<ParticleSystem>();

    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("hit by blackhole");
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.CompareTag("Enemy"))
        {
            col.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.GetComponent<EnemyHealth>().TakeDamage(0.1f);
        }
    }


}
