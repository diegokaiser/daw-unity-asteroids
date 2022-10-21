using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public Transform gameOverPanel;
    public float respawnTime = 3.0f;
    public float respawnInvulnerabilityTime = 3.0f;
    public int lives = 3;
    public int score = 0;
    public bool gameOver = false;

    public ParticleSystem explotion;

    private void Update()
    {
        livesText.text = lives.ToString();
        scoreText.text = score.ToString();
        timeText.text = Time.time.ToString("00:00");
        if (gameOver)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene("Asteroids");
            }
        }
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        //_animator.SetBool("AsteroidExplotion", true);
        this.explotion.transform.position = asteroid.transform.position;
        this.explotion.Play();
        if (asteroid.asteroidSize > 1.7f)
        {
            this.score += 25;
        } else if (asteroid.asteroidSize > 1.2f && asteroid.asteroidSize < 1.7)
        {
            this.score += 75;
        } else
        {
            this.score += 100;
        }
        this.scoreText.text = this.score.ToString() + " POINTS";
    }

    public void PlayerDied()
    {
        //_animator.SetBool("Explotion", true);
        this.explotion.transform.position = this.player.transform.position;
        this.explotion.Play();
        this.lives--;

        if (this.lives <= 0)
        {
            GameOver();
        } else
        {
            this.livesText.text = this.lives.ToString();
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    private void Respawn()
    {
        //_animator.SetBool("Explotion", false);
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), this.respawnInvulnerabilityTime);
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        gameOverPanel.gameObject.SetActive(true);
        gameOver = true;
    }
}
