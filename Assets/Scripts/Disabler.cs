using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabler : MonoBehaviour
{
    public GameObject player;

    public Transform respawnPoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            collision.gameObject.SetActive(false);
            player.transform.position = respawnPoint.position;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            collision.gameObject.SetActive(false);
            player.transform.position = respawnPoint.position;
        }
    }
}
