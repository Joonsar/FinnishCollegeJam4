using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 10f;
    public int maxHealth = 100;
    public int health = 100;
    public int level = 1;
    public float exp = 0;
    private Rigidbody rb;
    private Vector3 movement;

    private Vector3 mousePosition;
    private List<Skill> skills;

    public GameObject uiController;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        skills = new List<Skill>();

    }

    // Update is called once per frame
    void Update()
    {
        var moveX = Input.GetAxis("Horizontal");
        var moveZ = Input.GetAxis("Vertical");
        movement.x = moveX;
        movement.z = moveZ;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.y = transform.position.y;

        Vector3 lookDir = mousePosition - transform.position;
        lookDir.y = 0;
        //Debug.Log(lookDir);
        Quaternion rotation = Quaternion.LookRotation(lookDir.normalized);
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
        UpdateAnimator();


    }



    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        AddExperience(0.01f);



    }

    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        CheckDeath();
    }

    public void CheckDeath()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //game over
    }

    public void AddExperience(float amount)
    {
        if (exp >= 1f)
        {
            level++;
            exp = 0f;
            uiController.GetComponent<UIController>().ChangeLevelText("Level " + level);
            uiController.GetComponent<UIController>().SetLevelSlider(0f);
        }


        uiController.GetComponent<UIController>().AdjustLevelSlider(amount);
    }
}
