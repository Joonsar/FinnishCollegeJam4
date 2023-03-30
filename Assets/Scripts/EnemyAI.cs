using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 15f;
    [SerializeField] float turnSpeed = 5f;

    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    private ParticleSystem pb;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    bool isShooting = false; // boolean flag to check if already shooting a projectile

    private Animator animator;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        pb = GetComponent<ParticleSystem>();
    }


    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }

    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            animator.SetBool("IsMoving", true);
            ChaseTarget();
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {

            AttackTarget();
        }

    }


    private void ChaseTarget()
    {
        animator.SetBool("IsShooting", false);
        pb.Stop();
        navMeshAgent.SetDestination(target.position);

    }

    private void AttackTarget()
    {
        animator.SetBool("IsShooting", true);
        pb.Play();

        if (!isShooting) // check if not already shooting a projectile
        {
            isShooting = true;

            // Instantiate the projectile prefab at the projectile spawn point after 3 seconds
            Invoke("ShootProjectile", 0.75f);
        }

    }
    private void ShootProjectile()
    {
        // Instantiate the projectile prefab at the projectile spawn point
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        // Get the Projectile component of the instantiated prefab
        Projectile projectileComponent = projectile.GetComponent<Projectile>();

        // Set the speed of the projectile
        projectileComponent.speed = 15f;

        // Set the direction of the projectile towards the player
        Vector3 direction = (target.position + new Vector3(0, 1f, 0) - projectileSpawnPoint.position).normalized;
        projectile.transform.rotation = Quaternion.LookRotation(direction);

        // Destroy the projectile after a certain amount of time to prevent cluttering
        Destroy(projectile, 10f);

        isShooting = false; // set the flag to false so another projectile can be shot
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
