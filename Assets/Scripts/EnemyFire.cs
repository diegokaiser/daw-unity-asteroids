using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public float TimeBetweenShotsInSeconds;
    public float AngleOffset;
    private float timer;
    public GameObject enemyLaser;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= TimeBetweenShotsInSeconds)
        {
            timer = 0;
            var direction = Aim();
            Fire(direction);
        }
    }

    private Vector3 Aim()
    {
        var direction = player.transform.position - transform.position;
        return Quaternion.AngleAxis(Random.Range(-AngleOffset, AngleOffset), Vector3.up) * direction;
    }

    private void Fire(Vector3 direction)
    {
        Instantiate(enemyLaser, transform.position, Quaternion.LookRotation(direction));
    }
}
