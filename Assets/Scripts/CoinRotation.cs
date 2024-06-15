using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    public float rotationSpeed = 2f; 
    private Vector3 initialScale; 

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        float scaleX = Mathf.Abs(Mathf.Sin(Time.time * rotationSpeed));
        transform.localScale = new Vector3(scaleX * initialScale.x, initialScale.y, initialScale.z);
    }
}
