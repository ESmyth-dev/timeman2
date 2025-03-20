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
    public bool blink = false;
    public bool slowTime = false;

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

    public void levelUp()
    {
        level++;
    }

    public void gainLife()
    {
        numberOfLives++;
        //Need to add code to repair clock on UI
    }

    public void loseLife()
    {
        numberOfLives--;
        //Need to add code to break clock on UI
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
