using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    // Alexander Zotov. "How to make your character fire a bullet in 2D Unity game | Unity 2D tutorial" YouTube. January 12, 2017. [Video file] Available at: https://www.youtube.com/watch?v=1QOsUrXWMWY

    public float xVelocity = 5f;
    public float yVelocity = 0f;
    public Rigidbody2D rigidBody;
    public MoveScript moveScript;    
    public GameObject explosion;
    Vector2 bulletExplosion;

    private void Update()
    {
        rigidBody.velocity = new Vector2(xVelocity, yVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {        

      StartCoroutine(Destroy());
        
       
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.1f);
        bulletExplosion = transform.position;
        Instantiate(explosion, bulletExplosion, Quaternion.identity);
        Destroy(this.gameObject);
    }

}

   

