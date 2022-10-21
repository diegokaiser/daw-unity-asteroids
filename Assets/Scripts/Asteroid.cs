using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    Animator animator;
    public float speed;
    public GameObject[] subAsteroids;
    public int numberOfAsteroids;
    private bool isDestroyed = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rigidbody.drag = 0;
        rigidbody.angularDrag = 0;
        rigidbody.velocity = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            0
        ).normalized * speed;
        rigidbody.angularVelocity = Random.Range(-50f, 50f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (isDestroyed)
        {
            return;
        }
        if (collider.CompareTag("Bullet"))
        {
            isDestroyed = true;
            animator.SetBool("AsteroidDestroy", true);
            Destroy(gameObject);
            Destroy(collider.gameObject);
            for (var i = 0; i < numberOfAsteroids; i++)
            {
                Instantiate(
                    subAsteroids[Random.Range(0, subAsteroids.Length)],
                    transform.position,
                    Quaternion.identity
                );
            }
        }
        if (collider.CompareTag("Player"))
        {
            var asteroids = FindObjectsOfType<Asteroid>();
            for (var i = 0; i < asteroids.Length; i++)
            {
                Destroy(asteroids[i].gameObject);
            }
            collider.GetComponent<Player>().Lose();
        }
    }
}
