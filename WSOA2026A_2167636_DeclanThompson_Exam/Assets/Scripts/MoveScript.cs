using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MoveScript : MonoBehaviour
{
    // Binary Lunar. "Wall Interactions in Unity : Grab, Climb, Slide and Jump (2D Platformer Tutorial Part5)" YouTube. March 23, 2020. [Video file] Available at: https://www.youtube.com/watch?v=YeHhVlDMVKY

    private GameManager gameManager;
    public float Lives = 3f;
    public float Speed;
    public Rigidbody2D rigidBody;    
    public KeyCode Shoot;
    public ShootScript shootScript;
    public float jumpSpeed = 200f;    
    public float horizontalMovement = 20f;
    public bool onPlatform = false;
    public TMP_Text Life;

    public LayerMask groundLayers;
    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public float collisionRadius;
    public Vector2 groundOffset;
    public Vector2 rightOffset;
    public Vector2 leftOffset;
    public Color gizmoColor = Color.red;
    public int side;
    
    public GameObject Bullet;
    Vector2 bulletPosition;
    public float fireRate = 0.5f;
    float nextFire = 0.0f;

    public float Knock;
    public float KnockCount;
    public float KnockLength;
    public bool KnockFromRight;
    public bool WallKnock;       

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        Life.text = Lives.ToString();

    }

    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + groundOffset, collisionRadius, groundLayers);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayers)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayers);
        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayers);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayers);
        side = onRightWall ? 1 : -1;

        if (onGround == true || onPlatform == true)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            Vector2 direction = new Vector2(x, y);
            Move(direction);
        }
        else
        {

        }


        if (KnockCount <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        nextFire = Time.time + fireRate;
                        FireLeftUp();
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        nextFire = Time.time + fireRate;
                        FireRightUp();
                    }
                    else
                    {
                        nextFire = Time.time + fireRate;
                        FireUp();
                    }
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        nextFire = Time.time + fireRate;
                        FireLeftDown();
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        nextFire = Time.time + fireRate;
                        FireRightDown();
                    }
                    else
                    {
                        nextFire = Time.time + fireRate;
                        FireDown();
                    }
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    nextFire = Time.time + fireRate;
                    FireLeft();
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    nextFire = Time.time + fireRate;
                    FireRight();
                }

            }



        }
        else
        {
            if (KnockFromRight == true && WallKnock == false)
            {
                rigidBody.velocity = new Vector2(-Knock, Knock);
            }
            else if (KnockFromRight == false && WallKnock == false)
            {
                rigidBody.velocity = new Vector2(Knock, Knock);
            }
            KnockCount -= Time.deltaTime;

            if (KnockFromRight == true && WallKnock == true)
            {
                rigidBody.velocity = new Vector2(Knock, 0);
                StartCoroutine(Wall());
                
            }
            else if (KnockFromRight == false && WallKnock == true)
            {
                rigidBody.velocity = new Vector2(-Knock, 0);
                StartCoroutine(Wall());

            }
            KnockCount -= Time.deltaTime;
        }

        if (Lives <= 0)
        {
            SceneManager.LoadScene(2);
        }

    }

    public void Move(Vector2 dir)
    {
        rigidBody.velocity = new Vector2(dir.x * Speed, rigidBody.velocity.y);
    }

    private IEnumerator Wall()
    {
        yield return new WaitForSeconds(1f);
        WallKnock = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere((Vector2)transform.position + groundOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Enemy") 
        {
            transform.position = gameManager.lastCheckpoint;
            Lives = Lives - 1;
            Life.text = Lives.ToString();
        }

        if (collider.gameObject.name == "MovingPlatform")
        {
            onPlatform = true;
            this.transform.parent = collider.transform;
        }

        if (collider.gameObject.tag == "OneWayPlatform")
        {
            onPlatform = true;            
        }

        
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.name == "MovingPlatform")
        {
            onPlatform = false;
            this.transform.parent = null;
        }

        if (collider.gameObject.tag == "OneWayPlatform")
        {
            onPlatform = false;            
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Death")
        {
            transform.position = gameManager.lastCheckpoint;
            Lives = Lives - 1;
            Life.text = Lives.ToString();
        }

        
    }

    void FireLeft()
    {
        bulletPosition = transform.position;
        bulletPosition += new Vector2(-1f, 0f);
        shootScript.xVelocity = -6f;
        shootScript.yVelocity = 0f;
        Instantiate(Bullet, bulletPosition, Quaternion.identity);

        if (onLeftWall == true)
        {
            KnockCount = KnockLength;
            KnockFromRight = true;
            WallKnock = true;
            
        }
    }

    void FireRight()
    {
        bulletPosition = transform.position;
        bulletPosition += new Vector2(+1f, 0f);
        shootScript.xVelocity = 6f;
        shootScript.yVelocity = 0f;
        Instantiate(Bullet, bulletPosition, Quaternion.identity);

        if (onRightWall == true)
        {
            KnockCount = KnockLength;
            KnockFromRight = false;
            WallKnock = true;            
        }
    }

    void FireUp()
    {
        bulletPosition = transform.position;
        bulletPosition += new Vector2(0f, +1f);
        shootScript.xVelocity = 0f;
        shootScript.yVelocity = 6f;
        Instantiate(Bullet, bulletPosition, Quaternion.identity);
    }

    void FireDown()
    {
        bulletPosition = transform.position;
        bulletPosition += new Vector2(0f, -1f);
        shootScript.xVelocity = 0f;
        shootScript.yVelocity = -6f;
        Instantiate(Bullet, bulletPosition, Quaternion.identity);

        if(onGround == true || onPlatform == true)
        {
            rigidBody.AddForce(new Vector2(0, jumpSpeed));
        }
        else
        {

        }
       
    }

    void FireLeftUp()
    {
        bulletPosition = transform.position;
        bulletPosition += new Vector2(-1f, +1f);
        shootScript.xVelocity = -6f;
        shootScript.yVelocity = 6f;
        Instantiate(Bullet, bulletPosition, Quaternion.identity);
    }

    void FireRightUp()
    {
        bulletPosition = transform.position;
        bulletPosition += new Vector2(+1f, +1f);
        shootScript.xVelocity = 6f;
        shootScript.yVelocity = 6f;
        Instantiate(Bullet, bulletPosition, Quaternion.identity);
    }

    void FireLeftDown()
    {
        bulletPosition = transform.position;
        bulletPosition += new Vector2(-1f, -1f);
        shootScript.xVelocity = -6f;
        shootScript.yVelocity = -6f;
        Instantiate(Bullet, bulletPosition, Quaternion.identity);

        if (onGround == true || onPlatform == true)
        {
            KnockCount = KnockLength;
            KnockFromRight = false;

        }
        else
        {

        }

    }

    void FireRightDown()
    {
        bulletPosition = transform.position;
        bulletPosition += new Vector2(+1f, -1f);
        shootScript.xVelocity = 6f;
        shootScript.yVelocity = -6f;
        Instantiate(Bullet, bulletPosition, Quaternion.identity);

        if (onGround == true || onPlatform == true)
        {
            KnockCount = KnockLength;
            KnockFromRight = true;

        }
        else
        {

        }

    }

    

}
    

    

