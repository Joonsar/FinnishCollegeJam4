using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem test;
    float cooldown = 1f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckCooldown();
    }

    private void CheckCooldown()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            ParticleSystem ps = Instantiate(test, transform.position, transform.rotation) as ParticleSystem;
            ps.transform.SetParent(transform);
            Destroy(ps, 1);
            cooldown = 0.8f;
        }
    }
}
