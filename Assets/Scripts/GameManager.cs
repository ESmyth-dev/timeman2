using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int numberOfLives = 3;
    public int levelsCompleted = 0;

    public Skill blinkSkill = new Skill();
    public Skill slowTimeSkill = new Skill();
    public Skill doubleJumpSkill = new Skill();
    public Skill timeGrenadeSkill = new Skill();
    public Skill gunCooldownSkill = new Skill();
    public Skill ricochetSkill = new Skill();
    public List<Skill> skills = new List<Skill>();
    public List<Skill> skillPersist = new List<Skill>();

    public float enemyPercentage = 100f;

    public bool deathBubble = false;



    public List<string> levels;

    private string[] GetLevelsInBuild()
    {
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        string[] scenes = new string[sceneCount];


        // get current scene name
        string currentSceneName = SceneManager.GetActiveScene().name;
        // remove current scene from the list
        Debug.Log("Current scene: " + currentSceneName);




        Debug.Log("Scene count in build settings: " + sceneCount);
        for (int i = 0; i < sceneCount; i++)
        {
            scenes[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
        }
        return scenes;
    }



    void Awake()
    {
        levels = GetLevelsInBuild()
         .Where(level => level != "HomePage")
         .ToList<string>();
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

    private void OnEnable()
    {
        levels.Remove(SceneManager.GetActiveScene().name);
        Debug.Log($"removed level: {SceneManager.GetActiveScene().name}");
    }


    // Start is called before the first frame update
    void Start()
    {
        // Skills initialisation
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

        gunCooldownSkill.skillName = "Faster Gun Cooldown";
        gunCooldownSkill.skillDescription = "Increase the rate at which your weapon cools down by creating a small function quantum time flucuation.";
        skills.Add(gunCooldownSkill);

        doubleJumpSkill.skillName = "Double Jump";
        doubleJumpSkill.skillDescription = "Bend space time itself using powerful time magic to allow you to propel yourself while in mid air!";
        skills.Add(doubleJumpSkill);

        ricochetSkill.skillName = "Bullet Richochet";
        ricochetSkill.skillDescription = "Use time fracture potential energy to allow your bullets to ricochet off a surface.";
        skills.Add(ricochetSkill);

        int length = skills.Count;
        Skill[] myArray = skills.ToArray();
        Skill[] skillarray = new Skill[length];
        Array.Copy(myArray, skillarray, length);
        skillPersist = new List<Skill>(skillarray);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GameOver()
    {
        //Need to add code to display game over screen
        // I think we have this, redundant method ???
    }
}
