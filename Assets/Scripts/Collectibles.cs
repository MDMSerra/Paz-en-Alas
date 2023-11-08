using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public static GameObject obj;
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _animator.GetCurrentAnimatorStateInfo(0).IsTag("CanEat"))
        {
            _animator.SetTrigger("Eat");
            collision.SendMessageUpwards("AddEnergy", 100);
        }
    }
}
