using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;
    public ParticleSystem explosionEffect;

    public float livingTime = 2f;
    public Color initialColor = Color.white;
    public Color finalColor = Color.red;

    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidbody;
    private float _startingTime;

    public void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //iniciar temporizador para cambiar color sprite desde su instancia durante su living time hasta su destroy
        _startingTime = Time.time;
        Destroy(this.gameObject, livingTime);
        _rigidbody.velocity = transform.right * speed;
    }

    void Update()
    {
        //cambiar color de la bullet
        float _timeSinceStarted = Time.time - _startingTime;
        float _percentageCompleted = _timeSinceStarted / livingTime;
        _renderer.color = Color.Lerp(initialColor, finalColor, _percentageCompleted);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            CreateExplosion();
        }
    }
    void CreateExplosion()
    {
        explosionEffect.Play();
    }
}
