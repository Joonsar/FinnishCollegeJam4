using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5f;
    public int maxHealth = 100;
    public int health = 100;
    public int level = 1;
    public int exp = 0;
    private Rigidbody rb;
    private Vector3 movement;
    private List<Skill> skills;


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
        
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // transform.Translate(new Vector3(moveX, 0, moveZ) * speed * Time.deltaTime);
        var movement = new Vector3(moveX, 0, moveZ).normalized;
    }

    void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    void MoveCharacter(Vector3 direction)
    {
        rb.velocity = speed * Time.fixedDeltaTime * direction;
    }
}
