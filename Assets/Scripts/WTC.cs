using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WTC : MonoBehaviour
{
    [SerializeField] float speed = 5f; // Speed of the WTC movement
    private float leftEdge;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;  // Set the left edge based on the camera's viewport
    }

    private void Update()
    {      
        transform.position += speed * Time.deltaTime * Vector3.left;  // Move the WTC to the left
     
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);  // Destroy the WTC object if it goes off-screen
        }
    }
}
