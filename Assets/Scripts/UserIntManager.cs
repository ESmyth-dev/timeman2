using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserIntManager : MonoBehaviour
{
    // Start is called before the first frame update

    private Image slowAbilityBackground;
    private Image blinkBackground;
    private Image bombBackground;
    private Image pauseMenuBackground;
    private Button exitButton;
    private Button backToGameButton;
    private GameObject abilityChoice;
    private GameObject MainMenu;
    private Button playButton;
    private GameObject selectUpgrade;

    private float gameTimeScale;
    public bool menuActive;


    void Start()
    {
        // main menu
        MainMenu = GameObject.Find("MainMenu");
        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        playButton.onClick.AddListener(play);

        if (SceneManager.GetActiveScene().name != "HomePage")
        {
            MainMenu.SetActive(false);
        }    
        else
        {
            Time.timeScale = 0;
            Cursor.visible = true;
        }

        //MainMenu.SetActive(false);

        // ability bar
        slowAbilityBackground = GameObject.Find("SlowInactive").GetComponent<Image>();
        slowAbilityBackground.enabled = false;

        blinkBackground = GameObject.Find("BlinkInactive").GetComponent<Image>();
        blinkBackground.enabled = false;

        bombBackground = GameObject.Find("BombInactive").GetComponent<Image>();
        bombBackground.enabled = false;

        //choose new ability
        abilityChoice = GameObject.Find("SelectNewAbility");
        abilityChoice.SetActive(false);

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



    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !menuActive)
        {
            pauseMenuBackground.enabled = true;
            gameTimeScale = Time.timeScale;
            Time.timeScale = 0;
            exitButton.gameObject.SetActive(true);
            backToGameButton.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            menuActive = true;
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
        Time.timeScale = 1;
        Cursor.visible = false;
        SceneManager.LoadScene("Level1");
    }
}
