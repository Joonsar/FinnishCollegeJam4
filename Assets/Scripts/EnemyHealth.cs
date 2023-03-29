using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    public float CurrentHitPoints
    {
        get { return hitPoints; }
    }

    [SerializeField] GameObject explosionPrefab;
    
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
     
    }


    public float GetMaxHitPoints()
    {
        return hitPoints;
    }

    // create public method which reduses hitpoints by the amount of damage
    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Debug.Log("Enemy took damage");
            player.GetComponent<PlayerScript>().AddExperience(0.1f);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
