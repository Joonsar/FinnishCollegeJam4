using RPGCharacterAnims.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{

    public string Name { get; set; }
    public string Description { get; set; }
    public float Damage { get; set; }

    private float currentCooldown;

    private GameObject uiController;

    public float Cooldown { get; set; }

    public int Id { get; set; }

    public int Level { get; set; }
    public ParticleSystem Ps { get; set; }

    public GameObject Gc { get; set; }

    public Skill(int id, string name, float damage, float cooldown, int level, ParticleSystem ps, GameObject gc)
    {
        Level = level;
        Id = id;
        Name = name;
        Cooldown = cooldown;
        Damage = damage;
        currentCooldown = cooldown;
        uiController = GameObject.FindGameObjectWithTag("UiController");
        Gc = gc;
        Ps = ps;

    }

    public void UpdateSkill()
    {
        currentCooldown -= Time.deltaTime;
        uiController.GetComponent<UIController>().ChangeSkillCooldown(Id, currentCooldown / Cooldown);
        if (currentCooldown < 0)
        {
            Gc.GetComponent<GameController>().InvokeSkill(this);
            currentCooldown = Cooldown;


        }
    }

    public virtual void Attack()
    {

    }
}
