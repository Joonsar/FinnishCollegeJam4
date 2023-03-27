using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    
    PlayerScript target;
    [SerializeField] int damage = 40;


    void Start()
    {
        target = FindObjectOfType<PlayerScript>();
    }

    public void AttackHitEvent()
    {
        if (target == null) return;
       // target.GetComponent<PlayerScript>().TakeDamage(damage);
        target.TakeDamage(damage);
        Debug.Log("Bang bang");

    }
}
