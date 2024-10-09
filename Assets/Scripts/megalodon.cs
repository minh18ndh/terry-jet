using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class megalodon : MonoBehaviour
{
    [SerializeField] private float moveForce;
    [SerializeField] private Rigidbody2D rb;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity += new Vector2(0, moveForce);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.velocity -= new Vector2(0, moveForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Barrier")
            MegalodonGameManager.Instance.endGame();
    }
}
