using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int numberOfLives = 3;
    private int level = 0;
    public bool bulletRicochet = false;
    public bool bulletPierce = false;
    public bool timeGrenade = false;
    public bool timePulse = false;  
    public bool doubleJump = false;
    public bool blink = true;
    public bool slowTime = true;
    public bool slowDown = false;

    public bool deathBubble = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keeps the GameManager instance across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LevelUp()
    {
        level++;
    }



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        //Need to add code to display game over screen
    }
}
