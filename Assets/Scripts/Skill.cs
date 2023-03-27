using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
  
    public string Name { get; set; }
    public string Description { get; set; }
    public int Damage { get; set; }

    public Skill(string name, string description, int damage, int type) 
    {
        Name = name;
        Description = description;
        Damage = damage;
    }
}
