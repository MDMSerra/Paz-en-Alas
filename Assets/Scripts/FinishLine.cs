using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public RectTransform HUDMenu;
    public RectTransform FinishChart;
    public RectTransform FinishMenu;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            FinishChart.gameObject.SetActive(true);
            StartCoroutine(FinishLineAnimation(collision));
        }
    }

    private IEnumerator  FinishLineAnimation(Collider2D collision)
    {
        yield return new WaitForSeconds(3);
        FinishChart.gameObject.SetActive(false);
        HUDMenu.gameObject.SetActive(false);
        collision.gameObject.SetActive(false);
        FinishMenu.gameObject.SetActive(true);
    }
}
