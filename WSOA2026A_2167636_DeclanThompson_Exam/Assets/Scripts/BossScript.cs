using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    public float EnemySpeed;
    private bool isFacingRight = true;
    public Rigidbody2D rigidBody;
    public float Health = 5f;

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

        if (Health <= 0)
        {
            SceneManager.LoadScene(3);
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
            Health = Health - 1;
        }
    }
}
