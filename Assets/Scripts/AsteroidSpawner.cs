using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;
    public float asteroidSpawnRate = 2.0f;
    public float asteroidSpawnDistance = 15.0f;
    public float asteroidTrajectoryVariance = 15.0f;
    public int asteroidSpawnAmount = 1;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.asteroidSpawnRate, this.asteroidSpawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < this.asteroidSpawnAmount; i++)
        {
            Vector3 asteroidSpawnDirection = Random.insideUnitCircle.normalized * this.asteroidSpawnDistance;
            Vector3 asteroidSpawnPoint = this.transform.position + asteroidSpawnDirection;

            float variance = Random.Range(-this.asteroidTrajectoryVariance, this.asteroidTrajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPrefab, asteroidSpawnPoint, rotation);
            asteroid.asteroidSize = Random.Range(asteroid.asteroidMinSize, asteroid.asteroidMaxSize);
            asteroid.SetTrajectory(rotation * -asteroidSpawnDirection);
        }
    }
}
