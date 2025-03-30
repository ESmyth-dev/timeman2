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
    private Image blinkBackground;
    private Image bombBackground;

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


    void Start()
    {

        if (SceneManager.GetActiveScene().name == "HomePage")
        {
            MainMenu = GameObject.Find("MainMenu");
            playButton = GameObject.Find("PlayButton").GetComponent<Button>();
            playButton.onClick.AddListener(play);
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
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
        slowAbilityBackground.enabled = false;

        blinkBackground = GameObject.Find("BlinkInactive").GetComponent<Image>();
        blinkBackground.enabled = false;

        bombBackground = GameObject.Find("BombInactive").GetComponent<Image>();
        bombBackground.enabled = false;

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
