using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialScript : MonoBehaviour
{
   public TMP_Text tutorialText;
    public GameObject jumpTutorial;    
    public GameObject diagJumpTutorial;
    public GameObject enemyTutorial;
    public GameObject deleteTutorial;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "JumpTutorial")
        {
            tutorialText.text = "Use WASD to aim and Space to shoot a bullet";
            Destroy(jumpTutorial.gameObject);
        }

        if (collider.gameObject.name == "DiagJumpTutorial")
        {
            tutorialText.text = "Shooting diagonally down will make you fly diagonally";
            Destroy(diagJumpTutorial.gameObject);
        }

        if (collider.gameObject.name == "EnemyTutorial")
        {
            tutorialText.text = "Triangles are the enemy";
            Destroy(enemyTutorial.gameObject);
        }

        if (collider.gameObject.name == "DeleteTutorial")
        {
            tutorialText.text = "";
            Destroy(deleteTutorial.gameObject);
            
        }
    }
}
