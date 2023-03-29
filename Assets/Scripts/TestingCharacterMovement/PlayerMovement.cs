using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public Camera followCam;

    private PlayerScript ps;

    private Animator animator;

    private Rigidbody rb;
    void Start()
    {
        ps = GetComponent<PlayerScript>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.isAlive)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
            float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
            movementDirection.Normalize();
            Vector3 mousePosition = Input.mousePosition;
            Vector3 mouseWorldPosition = followCam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, followCam.transform.position.y));
            Vector3 lookDirection = mouseWorldPosition - transform.position;
            //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //mousePosition.y = transform.position.y;

            //Vector3 lookDir = mousePosition - transform.position;
            lookDirection.y = 0;
            //Debug.Log(lookDir);
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.fixedDeltaTime);
            transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

            if (movementDirection != Vector3.zero)
            {

                animator.SetBool("Run", true);
                // Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

                // transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Run", false);
            }
        }
    }

}
