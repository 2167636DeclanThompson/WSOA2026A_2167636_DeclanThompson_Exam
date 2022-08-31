using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ScoreScript scoreScript;
    public GameObject platform;
    public GameObject Menu;
    public KeyCode Escape;

    private static GameManager instance;
    public Vector2 lastCheckpoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;            
        }
        else
        {
            Destroy(gameObject);
        }

        Menu.SetActive(false);
    }

    private void Start()
    {
        platform.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(Escape))
        {
            Menu.SetActive(true);
        }
    }
    public void PlayerScored(int playerID)
    {
        scoreScript.PlayerScored(playerID);
    }
}
