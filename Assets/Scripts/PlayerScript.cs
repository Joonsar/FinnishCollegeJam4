using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    public float speed = 6f;
    public float rotateSpeed = 10f;
    public int maxHealth = 100;
    public int health = 100;
    public int level = 1;
    public float exp = 0f;
    public int damage = 4;
    private Rigidbody rb;
    private Vector3 movement;

    public ParticleSystem chainLightningPs;

    public ParticleSystem LazerRifflePs;

    private Vector3 mousePosition;
    private List<Skill> skills;
    private Animator animator;

    public GameObject uiController;

    public Camera followCam;

    private GameObject gc;

    private int skillIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController");

        rb = GetComponent<Rigidbody>();
        skills = new List<Skill>();
        animator = GetComponent<Animator>();


        skills.Add(new Skill(skillIndex, "Chain Lightning", 10, Random.Range(2f, 20f), 5, chainLightningPs, gc));
        skillIndex++;
        skills.Add(new Skill(skillIndex, "Lazer Riffle", 40, Random.Range(1f, 2f), 5, LazerRifflePs, gc));
        skillIndex++;
        skills.Add(new Skill(skillIndex, "Lazer Riffle", 40, Random.Range(1f, 2f), 5, LazerRifflePs, gc));


    }

    // Update is called once per frame
    void Update()
    {
        foreach (Skill sk in skills)
        {
            sk.UpdateSkill();
        }
        var moveX = Input.GetAxis("Horizontal");
        var moveZ = Input.GetAxis("Vertical");
        movement.x = moveX;
        movement.z = moveZ;
        //  Vector3 mousePosition = Input.mousePosition;
        //  Vector3 mouseWorldPosition = followCam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, followCam.transform.position.y));
        //  Vector3 lookDirection = mouseWorldPosition - transform.position;
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePosition.y = transform.position.y;

        //Vector3 lookDir = mousePosition - transform.position;
        //  lookDirection.y = 0;
        //Debug.Log(lookDir);
        //  Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        //  transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * Time.fixedDeltaTime);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
        // transform.rotation = lookRotation;



    }



    void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement);
        //AddExperience(0.01f);
    }



    public void TakeDamage(int amount)
    {
        health -= amount;
        uiController.GetComponent<UIController>().ChangePlayerHealthbarValue(amount);
        uiController.GetComponent<UIController>().ChangePlayerHealthText(health, maxHealth);
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
        exp += amount;
        if (exp >= 1.0f)
        {
            level++;
            exp = 0f;
            uiController.GetComponent<UIController>().ChangeLevelText("Level " + level);
            uiController.GetComponent<UIController>().SetLevelSlider(0f);
            uiController.GetComponent<UIController>().ActivateLevelUpPanel();
            Time.timeScale = 0;
        }


        uiController.GetComponent<UIController>().AdjustLevelSlider(amount);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Enemy"))
        {
            coll.gameObject.GetComponent<EnemyHealth>().TakeDamage((float)damage);
            Debug.Log(coll.gameObject.name + " took " + damage + " damage");
            //Destroy(coll.gameObject);
        }
    }

    public void TestEvent()
    {
        Debug.Log("test");
    }

    public void IncreaseMoveSpeed(float amount)
    {
        speed += amount;
        Resume();

    }

    public void IncreaseMaxHealth(int amount)
    {
        Debug.Log("testi");
        maxHealth += amount;
        uiController.GetComponent<UIController>().ChangePlayerHealthText(health, maxHealth);
        Resume();

    }

    private void Resume()
    {
        uiController.GetComponent<UIController>().DisableLevelUpPanel();
        Time.timeScale = 1f;
    }
}
