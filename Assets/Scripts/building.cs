using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    private float leftEdge;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * Vector3.left;
         
        if (transform.position.x < leftEdge) 
        {
            Destroy(gameObject);
        }
    }
}
