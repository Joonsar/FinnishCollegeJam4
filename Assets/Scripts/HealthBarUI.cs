using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarUI : MonoBehaviour
{
    [SerializeField] RectTransform foregroundImage = null;
    [SerializeField] EnemyHealth healthComponent = null;

    private float maxHitPoints;

    void Start()
    {
        maxHitPoints = healthComponent.GetMaxHitPoints();
    }

    private void Update()
    {
        float currentHealth = healthComponent.CurrentHitPoints;
        float healthPercentage = currentHealth / maxHitPoints;
        foregroundImage.localScale = new Vector3(healthPercentage, 1, 1);
    }


}
