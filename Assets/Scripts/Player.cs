using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    Animator animator;
    public float speed = 49;
    public float rotationSpeed = -140;
    public float offsetBullet;
    public GameObject bulletPrefab;
    public float shootRate = 0.5f;
    private bool canShoot = true;

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
            animator.SetBool("TurnRight", false);
            animator.SetBool("IgniteRight", true);
        } else
        {
            animator.SetBool("IgniteRight", false);
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("ignite", false);
            animator.SetBool("TurnLeft", false);
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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        float horizontal = Input.GetAxis("Horizontal");
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, horizontal * rotationSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        if (canShoot)
        {
            StartCoroutine(FireRate());
        }
        
    }
    private IEnumerator FireRate()
    {
        canShoot = false;
        var pos = transform.up * offsetBullet + transform.position;
        var bullet = Instantiate(
            bulletPrefab, pos, transform.rotation
        );
        Destroy(bullet, 6);
        yield return new WaitForSeconds(shootRate);
        canShoot = true;
    }

    public void Lose()
    {
        //animator.SetBool("Explotion", true);
        rigidbody.velocity = Vector3.zero;
        transform.position = Vector3.zero;
    }
}
