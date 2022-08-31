using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiblePlatform : MonoBehaviour
{
    
    
    public GameObject platform;

    private void Start()
    {
        platform.SetActive(false);

        
        
    }

    private void Update()
    {
        
        
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        
        if (collider.gameObject.tag == "Bullet")
        {
            platform.SetActive(true);
        }
    }
}
