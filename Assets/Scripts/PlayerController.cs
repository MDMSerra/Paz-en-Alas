using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.025f;
    public int flyCost = 40;
    public Slider slider;

    public RectTransform gameOverMenu;
    public RectTransform HUDMenu;
    public Transform respawnPoint;


    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private Vector2 _movement;
    private bool _isGrounded;
    private bool _facingRight = true;
    public bool _isDeath;

    //Energy bar
    public float maxEnergy = 500f;
    public float currentEnergy;
    public EnergyBar energyBar;

    //distance bar
    public TextMeshProUGUI distanceProgrese;
    float distanceUnit = 0;
    
    //distance bar 2
    private float dirX;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();       
    }
    
    void Start()
    {
        //Energy bar
        currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);
    }

     void Update()
    {
        if (!_isDeath)
        {
                _rigidbody.constraints = RigidbodyConstraints2D.None;
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

                //Movimiento
                float horizontalInput = Input.GetAxisRaw("Horizontal");
                float verticalInput = Input.GetAxisRaw("Vertical");
                _movement = new Vector2(horizontalInput, verticalInput);

                //Is flying?
                if (!_isGrounded)
                {
                    DrainEnergy(flyCost);
                }

                //distanceBar 2
                dirX = Input.GetAxisRaw("Horizontal") * speed;

                //Is grounded?
                _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

                //No Energy?
                if (currentEnergy <= 0f)
                {
                    speed = 0.5f;
                }
                else
                { 
                    speed = 2f;
                }
        }
    }

    private void FixedUpdate()
    {
        if (!_isDeath)
        {
            float horizontalVelocity = _movement.normalized.x * speed;
            float verticalVelocity = _movement.normalized.y * speed;
            _rigidbody.velocity = new Vector2(horizontalVelocity, verticalVelocity);

            //movement for flip
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            _movement = new Vector2(horizontalInput, 0f);

            //flip character
            if (horizontalInput < 0f && _facingRight == true)
            {
                Flip();
            }
            else if (horizontalInput > 0f && _facingRight == false)
            {
                Flip();
            }
            _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
        }
    }

    private void LateUpdate()
    {
        _animator.SetBool("Idle", _isGrounded);
        _animator.SetBool("Walk", !_isGrounded);
    }

    //Energy Bar
    public void DrainEnergy(int energyDrain)
    {
        currentEnergy -= energyDrain * Time.deltaTime;
        if (currentEnergy < 0) { currentEnergy = 0; }
        energyBar.SetEnergy(currentEnergy);
    }

    public void AddEnergy(int energy)
    {
            currentEnergy += energy;
            if (currentEnergy > slider.maxValue) { currentEnergy = slider.maxValue; }
            energyBar.SetEnergy(currentEnergy);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX *= -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _isDeath = true;
            _rigidbody.velocity = new Vector2(0, -1);
            _animator.SetTrigger("Death");
            Physics.gravity = new Vector3(0f, -50f, 0f);
            ChangeTag("Death");
        }
 
    }
    private void OnEnable()
    {
        _isDeath = false;
        ChangeTag("Player");
        AddEnergy(500);
        this.transform.position = respawnPoint.position;
        HUDMenu.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        gameOverMenu.gameObject.SetActive(true);
        HUDMenu.gameObject.SetActive(false);
        AddEnergy(500);
    }

    void ChangeTag(string tag)
    {
        this.gameObject.tag = tag;
    }
    public bool IsDeath { get { return _isDeath; } }
}
