using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserIntManager : MonoBehaviour
{
    // Start is called before the first frame update

    private Image slowAbilityBackground;
    private Image blinkBackground;
    private GameObject abilityChoice;


    void Start()
    {
        // ability bar
        slowAbilityBackground = GameObject.Find("SlowInactive").GetComponent<Image>();
        slowAbilityBackground.enabled = false;

        blinkBackground = GameObject.Find("BlinkInactive").GetComponent<Image>();
        blinkBackground.enabled = false;

        //choose new ability
        abilityChoice = GameObject.Find("SelectNewAbility");
        abilityChoice.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
