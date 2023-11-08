using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DistanceToCheckPoint : MonoBehaviour
{
    //image bar
    public Slider slider;
    public Image fill;

    // Reference to checkpoint position
    [SerializeField]
    private Transform checkpoint;
    [SerializeField]
    private Transform inicialCheckpoint;

    // Reference to UI text that shows the distance value
    [SerializeField]
    public TextMeshProUGUI distanceProgrese;

    // Calculated distance value
    private float distance;
    private float totalDistance;
    private float actualDistance;

    private void Awake()
    {
        totalDistance = checkpoint.transform.position.x - inicialCheckpoint.transform.position.x;
        slider.maxValue = totalDistance;
        slider.value = 0;
    }

    private void Update()
    {
        distance = (checkpoint.transform.position.x - transform.position.x);
        actualDistance = totalDistance - distance;
        slider.value = actualDistance;

        distanceProgrese.text = "Distance: " + distance.ToString("F1") + " meters";

        if (distance <= 0)
        {
            distanceProgrese.text = "Finish!";
        }
    }

    public void SetMaxDistance(int maxDistance)
    {
        slider.maxValue = maxDistance;
        slider.value = maxDistance;
    }
    public void SetDistance(int distance)
    {
        slider.value = distance;
    }
}
