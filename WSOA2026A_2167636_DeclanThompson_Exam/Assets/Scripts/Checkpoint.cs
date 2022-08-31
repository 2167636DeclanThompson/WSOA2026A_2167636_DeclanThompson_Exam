using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Checkpoint : MonoBehaviour
{
    // Blackthronprod. "HOW TO MAKE CHECKPOINTS IN UNITY - EASY TUTORIAL," YouTube. June 20, 2018. [Video file] Available at: https://www.youtube.com/watch?v=ofCLJsSUom0

    private GameManager gameManager;
    public TMP_Text checkpointText;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.lastCheckpoint = transform.position;
            checkpointText.text = "Checkpoint Reached";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            checkpointText.text = "";
        }
    }
}
