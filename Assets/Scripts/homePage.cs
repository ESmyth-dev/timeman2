using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class homePage : MonoBehaviour
{

    Button b;
    Button easy;
    Button medium;
    Button hard;

    Image easyImg;
    Image mediumImg;
    Image hardImg;

    public Sprite button;
    public Sprite buttonHover;
    public Sprite buttonClick;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        b = GameObject.Find("PlayButton").GetComponent<Button>();
        b.onClick.AddListener(startClick);

        easy = GameObject.Find("EasyButton").GetComponent<Button>();
        medium = GameObject.Find("MediumButton").GetComponent<Button>();
        hard = GameObject.Find("HardButton").GetComponent<Button>();

        easyImg = easy.GetComponent<Image>();
        mediumImg = medium.GetComponent<Image>();
        hardImg = hard.GetComponent<Image>();

        easy.onClick.AddListener(easyClick);
        medium.onClick.AddListener(mediumClick);
        hard.onClick.AddListener(hardClick);

        mediumClick();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startClick()
    {
        b.image.sprite = buttonClick;
        SceneManager.LoadScene("Level1");
    }

    void easyClick()
    {
        easy.image.sprite = buttonClick;
        medium.image.sprite = button;
        hard.image.sprite = button;

        GameManager.instance.enemyPercentage = 0.5f;
    }
    void mediumClick()
    {
        easy.image.sprite = button;
        medium.image.sprite = buttonClick;
        hard.image.sprite = button;

        GameManager.instance.enemyPercentage = 0.75f;
    }
    void hardClick()
    {
        easy.image.sprite = button;
        medium.image.sprite = button;
        hard.image.sprite = buttonClick;

        GameManager.instance.enemyPercentage = 1f;
    }
}
