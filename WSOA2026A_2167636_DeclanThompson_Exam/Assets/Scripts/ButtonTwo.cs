using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTwo : MonoBehaviour
{
    public GameObject Button3;

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            Button3.SetActive(true);
        }
    }
}
