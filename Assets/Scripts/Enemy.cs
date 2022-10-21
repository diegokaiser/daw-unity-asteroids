using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemySpeed;
    public float enemyAngularSpeed;
    private Vector3 enemyDirection;

    void Start()
    {
        enemyDirection = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100)).normalized;
    }

    void Update()
    {
        transform.Translate(enemyDirection * enemySpeed * Time.deltaTime, Space.World);
    }
}
