using RPGCharacterAnims.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
  
    public string Name { get; set; }
    public string Description { get; set; }
    public int Damage { get; set; }

    private float currentCooldown;

    private GameObject uiController;

    public float Cooldown { get; set; }

    public int Id { get; set; }

    public Skill(int id, string name, int damage, float cooldown) 
    {

        Id = id;
        Name = name;
        Cooldown = cooldown;
        Damage = damage;
        currentCooldown = cooldown;
        uiController = GameObject.FindGameObjectWithTag("UiController");
    }

    public void UpdateSkill()
    {
        currentCooldown -= Time.deltaTime;
        uiController.GetComponent<UIController>().ChangeSkillCooldown(Id, currentCooldown/Cooldown);
        if (currentCooldown < 0 )
        {
            Attack();
            currentCooldown = Cooldown;
           
            
        }
    }

    public virtual void Attack()
    {
        
    }
}
