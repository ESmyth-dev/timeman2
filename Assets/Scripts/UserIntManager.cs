using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserIntManager : MonoBehaviour
{
    // Start is called before the first frame update

    private Image slowAbilityBackground;
    private Image blinkBackground;
    private Image bombBackground;
    private Image pauseMenu;
    private GameObject abilityChoice;


    void Start()
    {
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
        pauseMenu = GameObject.Find("PauseMenuBackground").GetComponent<Image>();
        pauseMenu.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
