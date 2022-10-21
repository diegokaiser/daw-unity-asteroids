using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    public GameObject _gameManager;
    private GameManager _gameManagerScript;

    public float asteroidSize = 1.4f;
    public float asteroidMinSize = 0.7f;
    public float asteroidMaxSize = 2.1f;
    public float asteroidSpeed = 50.0f;
    public float asteroidLifeTime = 30.0f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager");
        _gameManagerScript = _gameManager.GetComponent<GameManager>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.asteroidSize;
        this.gameObject.tag = "Asteroid";
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.asteroidSpeed);
        Destroy(this.gameObject, this.asteroidLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (this.asteroidSize * 0.5f >= this.asteroidMinSize)
            {
                CreateSplit();
            }
            _gameManagerScript.AsteroidDestroyed(this);
            Destroy(this.gameObject);
        }
    }

    private void CreateSplit()
    {
        int randomInt = Random.Range(0, 2);

        for(int i = 0; i <= randomInt; i ++)
        {
            Vector2 position = this.transform.position;
            position += Random.insideUnitCircle * 0.5f;

            Asteroid half = Instantiate(this, position, this.transform.rotation);
            half.asteroidSize = this.asteroidSize * 0.5f;
            half.SetTrajectory(Random.insideUnitCircle.normalized * this.asteroidSpeed);
        }
    }
}
