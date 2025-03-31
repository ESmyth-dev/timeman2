using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class homePage : MonoBehaviour
{

    Button b;
    Button easy;
    Button medium;
    Button hard;

    Image easyImg;
    Image mediumImg;
    Image hardImg;

    // Start is called before the first frame update
    void Start()
    {
        b = GameObject.Find("PlayButton").GetComponent<Button>();
        b.onClick.AddListener(startClick);

        easy = GameObject.Find("Easy").GetComponent<Button>();
        medium = GameObject.Find("Medium").GetComponent<Button>();
        hard = GameObject.Find("Hard").GetComponent<Button>();

        easyImg = easy.GetComponent<Image>();
        mediumImg = medium.GetComponent<Image>();
        hardImg = hard.GetComponent<Image>();

        easy.onClick.AddListener(easyClick);
        medium.onClick.AddListener(mediumClick);
        hard.onClick.AddListener(hardClick);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startClick()
    {
        SceneManager.LoadScene("Level1");
    }

    void easyClick()
    {
        easyImg.color = Color.blue;
        mediumImg.color = Color.white;
        hardImg.color = Color.white;
        GameManager.instance.enemyPercentage = 0.5f;
    }
    void mediumClick()
    {
        easyImg.color = Color.white;
        mediumImg.color = Color.blue;
        hardImg.color = Color.white;
        GameManager.instance.enemyPercentage = 0.75f;
    }
    void hardClick()
    {
        easyImg.color = Color.white;
        mediumImg.color = Color.white;
        hardImg.color = Color.blue;
        GameManager.instance.enemyPercentage = 1f;
    }
}
