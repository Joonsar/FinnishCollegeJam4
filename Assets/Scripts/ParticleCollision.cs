using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    ParticleSystem ps;


    public float damage = 10;
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

    /*  private void OnTriggerEnter(Collider col)
      {

          if (col.gameObject.CompareTag("Enemy"))
          {
              col.GetComponent<EnemyHealth>().TakeDamage(damage);
          }
      } */

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {

            col.GetComponent<EnemyHealth>().TakeDamage(damage / 100);
        }
    }


}
