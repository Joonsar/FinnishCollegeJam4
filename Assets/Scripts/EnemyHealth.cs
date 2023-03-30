using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 50f;

    public float CurrentHitPoints
    {
        get { return hitPoints; }
    }

    [SerializeField] GameObject explosionPrefab;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("CheckDistance", 0, 4f);
    }

    public void CheckDistance()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > 35f)
        {
            print("Enemy was too far!");
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().enemies.Remove(gameObject);
            Destroy(gameObject);
        }
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
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
