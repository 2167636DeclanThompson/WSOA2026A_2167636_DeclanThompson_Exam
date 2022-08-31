using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float EnemySpeed;
    public bool isFacingRight = true;
    public Rigidbody2D rigidBody;
    public GameManager gameManager;

    private void Update()
    {
        if (isFacingRight == true)
        {
            rigidBody.velocity = new Vector2(EnemySpeed, rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity = new Vector2(-EnemySpeed, rigidBody.velocity.y);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Waypoint" && isFacingRight == false)
        {
            isFacingRight = true;
        }
        else if (collider.gameObject.tag == "Waypoint" && isFacingRight == true)
        {
            isFacingRight = false;
        }
                
    }


    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            
            Destroy(this.gameObject);
        }
    }
}

