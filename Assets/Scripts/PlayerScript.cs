using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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
    public float shotgunDamage = 15;
    public float shotgunCooldown = 1.35f;
    public float flamethrowerDamage = 1;
    public float flamethrowerCooldown = 3f;
    public float blackholeDamage = 30;
    public float blackholeCooldown = 5f;

    public ParticleSystem chainLightningPs;

    public ParticleSystem LazerRifflePs;

    public ParticleSystem flameThrowerPs;

    [SerializeField] GameObject explosionPrefab;

    private Vector3 mousePosition;
    private List<Skill> skills;
    private Animator animator;

    public GameObject uiController;

    public Camera followCam;

    private GameObject gc;

    private int skillIndex = 0;

    private AudioController audioController;

    public bool isAlive = true;
    public float levelCap = 1f;
 

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController");
        audioController = GameObject.FindObjectOfType<AudioController>();
        rb = GetComponent<Rigidbody>();
        skills = new List<Skill>();
        animator = GetComponent<Animator>();

        skills.Add(new Skill(skillIndex, "Lazer Riffle", shotgunDamage, shotgunCooldown, 1, LazerRifflePs, gc));
        skillIndex++;
        skills.Add(new Skill(skillIndex, "Flamethrower", flamethrowerDamage, flamethrowerCooldown, 1, flameThrowerPs, gc));  
        skillIndex++;
        skills.Add(new Skill(skillIndex, "Chain Lightning", blackholeDamage, blackholeCooldown, 1, chainLightningPs, gc));

        uiController.GetComponent<UIController>().ChangePlayerHealthText(health, maxHealth);
      
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
        if (isAlive)
        {
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement);
            //AddExperience(0.01f);
        }
    }



    public void TakeDamage(int amount)
    {

        health -= amount;
        uiController.GetComponent<UIController>().ChangePlayerHealthbarValue(health, maxHealth);
        uiController.GetComponent<UIController>().ChangePlayerHealthText(health, maxHealth);
        CheckDeath();
    }

    public void CheckDeath()
    {
        if (health <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Die();
        }
    }

    public void Die()
    {
        isAlive = false;
        StartCoroutine("WaitAfterDead");
    }


    IEnumerator WaitAfterDead()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("GameOver");

    }

    public void AddExperience(float amount)
    {
        exp += amount;
        if (exp >= levelCap)
        {
            levelCap = levelCap * 1.3f;
            level++;
            exp = 0f;
            uiController.GetComponent<UIController>().ChangeLevelText("Level " + level);
            uiController.GetComponent<UIController>().SetLevelSlider(0f);
            uiController.GetComponent<UIController>().ActivateLevelUpPanel();
            audioController.PlayAudio(Audios.levelupsound);
            Time.timeScale = 0;
        }
        uiController.GetComponent<UIController>().AdjustLevelSlider((exp / 1f) / (levelCap / 1f));
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Enemy"))
        {
            coll.gameObject.GetComponent<EnemyHealth>().TakeDamage((float)damage);
            //Debug.Log(coll.gameObject.name + " took " + damage + " damage");
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

    public void LevelUp(int passive)
    {
        switch (passive)
        {
            case 0: //Movement speed
                Resume();
                break;
            case 1: //Cooldown rate
                foreach (Skill s in skills)
                {
                    s.Cooldown -= 0.1f;
                }
                Resume();
                break;
            case 2: //Max health
                health += 150;              
                if (health > maxHealth)
                {
                    health = maxHealth;                 
                }
                uiController.GetComponent<UIController>().ChangePlayerHealthbarValue(health, maxHealth);
                uiController.GetComponent<UIController>().ChangePlayerHealthText(health, maxHealth);
                Resume();
                break;
            case 3: //Health regen
                Resume();
                break;
            case 4: //Damage
                foreach (Skill s in skills)
                {
                    s.Damage +=  2;
                }
                Resume();
                break;
            case 5:
                foreach(Skill s in skills)
                {
                    if(s.Name == "Lazer Riffle")
                    {
                        s.Level++;
                    }
                    
                }
                health -= 200;
                uiController.GetComponent<UIController>().ChangePlayerHealthbarValue(health, maxHealth);
                uiController.GetComponent<UIController>().ChangePlayerHealthText(health, maxHealth); 
                Resume();
                break;
            case 6:
                foreach (Skill s in skills)
                {
                    if (s.Name == "Chain Lightning")
                    {
                        if(s.Level < 4)
                        {
                            s.Level++;
                        }
                        else
                        {
                            s.Damage += 2;
                        }
                        
                    }
                    
                }
                health -= 200;
                uiController.GetComponent<UIController>().ChangePlayerHealthbarValue(health, maxHealth);
                uiController.GetComponent<UIController>().ChangePlayerHealthText(health, maxHealth); 
                Resume();
                break;
            case 7:
                foreach (Skill s in skills)
                {
                    if (s.Name == "Flamethrower")
                    {
                        s.Damage++;
                    }
                }
                health -= 200;
                uiController.GetComponent<UIController>().ChangePlayerHealthbarValue(health, maxHealth);
                uiController.GetComponent<UIController>().ChangePlayerHealthText(health, maxHealth);
                Resume();
                break;
        }
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
