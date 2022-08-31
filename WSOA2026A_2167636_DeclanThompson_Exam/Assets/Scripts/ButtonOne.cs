using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOne : MonoBehaviour
{
    public GameObject Button2;

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            Button2.SetActive(true);
        }
    }
}
