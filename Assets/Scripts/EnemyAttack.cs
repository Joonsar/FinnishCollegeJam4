using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    
    PlayerScript target;
    [SerializeField] float damage = 40f;


    void Start()
    {
        target = FindObjectOfType<PlayerScript>();
    }

    public void AttackHitEvent()
    {

       
    }
}
