using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiController : MonoBehaviour
{
    // Start is called before the first frame update

    private Image slowAbilityBackground;
    private Image blinkBackground;


    void Start()
    {
        // ability bar
        slowAbilityBackground = GameObject.Find("SlowInactive").GetComponent<Image>();
        slowAbilityBackground.enabled = false;

        blinkBackground = GameObject.Find("BlinkInactive").GetComponent<Image>();
        blinkBackground.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
