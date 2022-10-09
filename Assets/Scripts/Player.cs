using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Animator animator;
    public float speed = 49;
    public float rotationSpeed = -140;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        if (vertical > 0)
        {
            rigidbody.AddForce(transform.up * vertical * speed * Time.deltaTime);
            animator.SetBool("ignite", true);            
        } else
        {
            animator.SetBool("ignite", false);
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("ignite", false);
            animator.SetBool("IgniteRight", true);
        } else
        {
            animator.SetBool("IgniteRight", false);
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("ignite", false);
            animator.SetBool("IgniteLeft", true);
        } else
        {
            animator.SetBool("IgniteLeft", false);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("TurnRight", true);            
        }
        else
        {
            animator.SetBool("TurnRight", false);            
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("TurnLeft", true);
        }
        else
        {
            animator.SetBool("TurnLeft", false);
        }
        float horizontal = Input.GetAxis("Horizontal");
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, horizontal * rotationSpeed * Time.deltaTime);
    }
}
