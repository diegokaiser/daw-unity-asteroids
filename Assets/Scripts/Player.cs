using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public GameObject _gameManager;
    private GameManager _gameManagerScript;
    Animator _animator;
    public Bullet bulletPrefab;
    private bool _thrusting;
    private float _turnDirection;
    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _gameManagerScript = _gameManager.GetComponent<GameManager>();
    }

    private void Update() {
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            _animator.SetBool("Ignite", false);
            _animator.SetBool("IgniteTurnRight", false);
            _animator.SetBool("IgniteRight", true);
        }
        else
        {
            _animator.SetBool("IgniteRight", false);
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            _animator.SetBool("Ignite", false);
            _animator.SetBool("IgniteTurnLeft", false);
            _animator.SetBool("IgniteLeft", true);
        }
        else
        {
            _animator.SetBool("IgniteLeft", false);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _animator.SetBool("IgniteTurnRight", true);
            _turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _animator.SetBool("IgniteTurnLeft", true);
            _turnDirection = -1.0f;
        }
        else
        {
            if (_animator.GetBool("IgniteTurnLeft") == true)
            {
                _animator.SetBool("IgniteTurnLeft", false);
            } else
            {
                _animator.SetBool("IgniteTurnRight", false);
            }
            _turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void FixedUpdate() {
        if (_thrusting)
        {
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed);
            _animator.SetBool("Ignite", true);
        }
        else
        {
            _animator.SetBool("Ignite", false);
        }
        if (_turnDirection != 0.0f)
        {
            _rigidbody.AddTorque(_turnDirection * this.turnSpeed);
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            _gameManagerScript.PlayerDied();
        }
    }
}
