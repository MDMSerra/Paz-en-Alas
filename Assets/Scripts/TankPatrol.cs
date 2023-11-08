using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPatrol : MonoBehaviour
{
    public ParticleSystem dust;
    private Weapon _weapon;

    // follow player
    public Transform player;
    private Vector3 _playerDirection;
    private float _angle;
    //necesarias
    public float speed = 1f;
    public float wallAware = 0.5f;
    public LayerMask groundLayer;

    private Rigidbody2D _rigidbody;
    private bool _facingRight;
    private AudioSource _audio;

    public float aimingTime = 0.5f;
    public float shootingTime = 0.5f;

    private bool _isAttacking;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _weapon = GetComponentInChildren<Weapon>();
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        //determina a que lado mira en el inicio
        if (transform.localScale.x < 0f)
        {
            _facingRight = false;
        }
        else if (transform.localScale.x > 0f)
        {
            _facingRight = true;
        }
    }

    private void Update()
    {
        HandleWeaponRotation();

        Vector2 direction = Vector2.right;

        if (_facingRight == false)
        {
            direction = Vector2.left;
        }

        if (_isAttacking == false)
        {
            if (Physics2D.Raycast(transform.position, direction, wallAware, groundLayer))
            {
                Flip();
                CreateDust();
            }
        }
    }

    private void FixedUpdate()
    {
        float horizontalVelocity = speed;
        if (_facingRight == false)
        {
            horizontalVelocity = horizontalVelocity * -1f;
        }

        if (_isAttacking)
        {
            horizontalVelocity = 0f;
        }
        _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isAttacking == false && collision.CompareTag("Player"))
        {
            StartCoroutine(AimAndShoot());
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.localScale = new Vector3(transform.localScale.x*-1f, transform.localScale.y, transform.localScale.z);
        _weapon.transform.localScale = new Vector3(_weapon.transform.localScale.x*-1f, _weapon.transform.localScale.y, _weapon.transform.localScale.z);
    }

    private IEnumerator AimAndShoot()
    {
        _isAttacking = true;
        yield return new WaitForSeconds(aimingTime);
        _weapon.Shoot();
        _audio.Play();
        yield return new WaitForSeconds(shootingTime);
        _isAttacking = false;
    }

    void CreateDust()
    {
        dust.Play();
    }

    private void OnEnable()
    {
        _isAttacking = false;
    }

    private void OnDisable()
    {
        StopCoroutine(AimAndShoot());
        _isAttacking = false;
    }

    void HandleWeaponRotation()
    {
        _playerDirection = (player.position - _weapon.transform.position).normalized;
        _weapon.transform.right = (player.position - _weapon.transform.position).normalized;
        _angle = Mathf.Atan2(_playerDirection.y, _playerDirection.x) * Mathf.Rad2Deg;
        _weapon.transform.rotation = Quaternion.Euler(0.0f,0.0f, _angle);
    }

}
