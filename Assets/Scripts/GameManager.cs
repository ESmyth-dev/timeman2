using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    public Skill blinkSkill = new Skill();
    public Skill slowTimeSkill = new Skill();
    public Skill doubleJumpSkill = new Skill();
    public Skill timeGrenadeSkill = new Skill();
    public List<Skill> skills = new List<Skill>();


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
        blinkSkill.skillName = "Blink";
        blinkSkill.skillDescription = "Teleport a short distance in the direction you are currently walking using the power of time magic.";
        // add blink texture here
        skills.Add(blinkSkill);
        slowTimeSkill.skillName = "Slow Time";
        slowTimeSkill.skillDescription = "Slow down time to a crawl while maintaining your own ability to move at full speed with the power of time magic.";
        skills.Add(slowTimeSkill);
        timeGrenadeSkill.skillName = "Baby Time Bomb";
        timeGrenadeSkill.skillDescription = "Send out a sphere of pure time force, sending all those it touches back in time, turning them into small incapacitated children.";
        skills.Add(timeGrenadeSkill);
        Debug.Log(skills.Count);
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
