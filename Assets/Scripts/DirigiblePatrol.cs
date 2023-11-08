using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirigiblePatrol : MonoBehaviour
{
    public ParticleSystem dust;
    public GameObject pointA;
    public GameObject pointB;

    private Weapon _weapon;
    private Transform _currentPoint;

    // follow player
    public Transform player;
    private Vector3 _playerDirection;
    private float _angle;
   
    //necesarias
    public float speed = 1f;

    private Rigidbody2D _rigidbody;
    private AudioSource _audio;

    public float aimingTime = 0.5f;
    public float shootingTime = 0.5f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _weapon = GetComponentInChildren<Weapon>();
        _audio = GetComponent<AudioSource>();
        _currentPoint = pointB.transform;
        CreateDust();
       
    }

    private void Update()
    {
        Vector2 point = _currentPoint.position - transform.position;

        if (_currentPoint == pointB.transform)
        {
            //_rigidbody.velocity = new Vector2(speed, 0);
            transform.position = Vector2.MoveTowards(transform.position, pointB.transform.position, speed*Time.deltaTime);
        }else
        {
            //_rigidbody.velocity = new Vector2(-speed, 0);
            transform.position = Vector2.MoveTowards(transform.position, pointA.transform.position, speed * Time.deltaTime);
        }

        if(Vector2.Distance(transform.position, _currentPoint.position) < 0.2f && _currentPoint == pointB.transform)
        {
            Flip();
            _currentPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position, _currentPoint.position) < 0.2f && _currentPoint == pointA.transform)
        {
            Flip();
            _currentPoint = pointB.transform;
        }

        HandleWeaponRotation();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(AimAndShoot());
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

        Vector3 weaponLocalScale = _weapon.transform.localScale;
        weaponLocalScale.x *= -1;
        _weapon.transform.localScale = weaponLocalScale;
    }

    private IEnumerator AimAndShoot()
    {
        yield return new WaitForSeconds(aimingTime);
        _weapon.Shoot();
        _audio.Play();
        yield return new WaitForSeconds(shootingTime);
    }

    void CreateDust()
    {
        dust.Play();
    }


    private void OnDisable()
    {
        StopCoroutine(AimAndShoot());  
    }

    void HandleWeaponRotation()
    {
        _playerDirection = (player.position - _weapon.transform.position).normalized;
        _weapon.transform.right = (player.position - _weapon.transform.position).normalized;
        _angle = Mathf.Atan2(_playerDirection.y, _playerDirection.x) * Mathf.Rad2Deg;
        _weapon.transform.rotation = Quaternion.Euler(0.0f,0.0f, _angle);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

}
