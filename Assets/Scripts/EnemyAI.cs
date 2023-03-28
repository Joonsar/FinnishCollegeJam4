using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 15f;
    [SerializeField] float turnSpeed = 5f;


    private ParticleSystem pb;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

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
        //  GetComponent<Animator>().SetBool("attack", true);
        
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
