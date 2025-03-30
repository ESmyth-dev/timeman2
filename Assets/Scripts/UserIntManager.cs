using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserIntManager : MonoBehaviour
{
    // Start is called before the first frame update

    //abilities
    private Image slowAbilityBackground;
    private GameObject slow;
    private Image blinkBackground;
    private GameObject blink;
    private Image bombBackground;
    private GameObject bomb;

    //pause menu
    private Image pauseMenuBackground;
    private Button exitButton;
    private Button backToGameButton;

    //main menu
    private GameObject MainMenu;
    private Button playButton;

    //
    private GameObject p;

    private float gameTimeScale;
    public bool menuActive;

    private List<Skill> skills;
    public bool hideBlind;
    public bool hideTime;
    public bool hideBomb;

    CanvasGroup s;
    CanvasGroup bl;
    CanvasGroup bo;
    GameManager gameMan;

    void Start()
    {
        hideBlind = false;
        hideBomb = false;
        hideTime = false;

        gameMan = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        if (SceneManager.GetActiveScene().name == "HomePage")
        {
            //MainMenu = GameObject.Find("MainMenu");
            //playButton = GameObject.Find("PlayButton").GetComponent<Button>();
            //playButton.onClick.AddListener(play);
            //Time.timeScale = 0;
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
        }    
        else
        {
            //MainMenu = GameObject.Find("MainMenu");
            //MainMenu.SetActive(false);
            //playButton.gameObject.SetActive(false);
        }

        //MainMenu.SetActive(false);

        // ability bar
        slowAbilityBackground = GameObject.Find("SlowInactive").GetComponent<Image>();
        slowAbilityBackground.enabled = true;
        slow = GameObject.Find("SlowTime");
        s = slow.GetComponent<CanvasGroup>();
        s.alpha = 0;


        blinkBackground = GameObject.Find("BlinkInactive").GetComponent<Image>();
        blinkBackground.enabled = true;
        blink = GameObject.Find("Blink");
        bl = blink.GetComponent<CanvasGroup>();
        bl.alpha = 0;

        bombBackground = GameObject.Find("BombInactive").GetComponent<Image>();
        bombBackground.enabled = true;
        bomb = GameObject.Find("BabyBomb");
        bo = bomb.GetComponent<CanvasGroup>();
        bo.alpha = 0;

        // pause menu
        menuActive = false;
        pauseMenuBackground = GameObject.Find("PauseMenuBackground").GetComponent<Image>();
        pauseMenuBackground.enabled = false;

        exitButton = GameObject.Find("ExitGame").GetComponent<Button>();
        backToGameButton = GameObject.Find("BackToGame").GetComponent<Button>();

        exitButton.onClick.AddListener(exitClick);
        backToGameButton.onClick.AddListener(backClick);
        Debug.Log("back listen");

        exitButton.gameObject.SetActive(false);
        backToGameButton.gameObject.SetActive(false);

        //p = GameObject.Find("PauseMenu");
        //p.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
        //skills = GameManager.instance.skills;
        foreach (Skill sk in gameMan.skillPersist)
        {
            if (sk.isUnlocked && sk.skillName == "Blink")
            {
                //hideBlind = true;
                bl.alpha = 1;
            }

            if (sk.isUnlocked && sk.skillName == "Slow Time")
            {
                //hideTime = true;
                s.alpha = 1;
            }

            if (sk.isUnlocked && sk.skillName == "Baby Time Bomb")
            {
                hideBomb = true;
                bo.alpha = 1;
            }

            
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !menuActive)
        {
            pauseMenuBackground.enabled = true; // show pause menu background
            gameTimeScale = Time.timeScale;     // save time scale
            Time.timeScale = 0;                 // pause game
            exitButton.gameObject.SetActive(true);  // show exit button
            backToGameButton.gameObject.SetActive(true);    // show back button
            Cursor.lockState = CursorLockMode.None; // allow cursor to move
            Cursor.visible = true;  // show cursor
            menuActive = true;  // tracks if the pause menu is active
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuActive)
        {
            pauseMenuBackground.enabled = false;
            Time.timeScale = gameTimeScale;
            exitButton.gameObject.SetActive(false);
            backToGameButton.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            menuActive = false;
        }
    }

    void exitClick()
    {
        QuitGame();
    }
    void backClick()
    {
        Debug.Log("back");
        pauseMenuBackground.enabled = false;
        Time.timeScale = gameTimeScale;
        exitButton.gameObject.SetActive(false);
        backToGameButton.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menuActive = false;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quit");
    }

    public void play()
    {
        Debug.Log("click play");
        MainMenu.SetActive(false);
        playButton.gameObject.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Level1");
    }
}
