using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public Slider levelSlider;
    public TMP_Text levelText;
    public GameObject levelUpPanel;
    public TMP_Text playerHealthText;
    public Image playerHealthBar;
    public List<Button> levelUpButtons = new List<Button>();
    public List<GameObject> skillCooldownButtons = new List<GameObject>();

    Animator levelUpAnim;
    public GameObject levelit;
    private bool levelup = false;

  

    // Start is called before the first frame update
    void Start()
    {
        levelUpAnim = levelit.GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AdjustLevelSlider(float amount)
    {
        levelSlider.value += amount;

    }

    public void ChangeLevelText(string text)
    {
        levelText.text = text;

    
    }

    public void SetLevelSlider(float v)
    {
        levelSlider.value = v;
    }

    public void ChangePlayerHealthbarValue(int health, int maxHealth)
    {
        playerHealthBar.fillAmount = (float)health / maxHealth;

    }

    public void ActivateLevelUpPanel()
    {
     

        levelup = true;
        Debug.Log("LevelUp");
        levelUpAnim.Play("Levelup");
        levelup = false;

        levelUpPanel.SetActive(true);
    }

    public void DisableLevelUpPanel()
    {
        levelUpPanel.SetActive(false);

    }

    public void ChangePlayerHealthText(int health, int maxHealth)
    {
        playerHealthText.text = health + "/" + maxHealth;

    }

    public void ChangeSkillCooldown(int id, float cooldown)
    {


        skillCooldownButtons[id].GetComponent<Image>().fillAmount = cooldown;
    }
}
