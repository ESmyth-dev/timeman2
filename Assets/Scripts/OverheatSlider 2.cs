using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverheatSlider : MonoBehaviour
{
    private bool overheated;
    private static Color bar;

    // Start is called before the first frame update
    void Start()
    {
        overheated = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        Slider slider = GameObject.Find("Slider").GetComponent<Slider>();
        float value = slider.value;

        float g_val = 255 * (1f - value);
        float g = Mathf.Round(g_val);
        bar = new Color(255, g, 0, 255);
        bar.a = 1;

        Image fill = GameObject.Find("Fill").GetComponent<Image>();
        Image background = GameObject.Find("SliderBackground").GetComponent<Image>();
        Image handle = GameObject.Find("SliderHandle").GetComponent<Image>();

        if (value >= 1f)
        {
            overheated = true;
        }
        else if (value <= 0.1f)
        {
            overheated = false;
        }

        if (overheated)
        {
            fill.color = new Color(255, 0, 0, 255);
            background.color = new Color(255, 0, 0, 255);
            handle.color = new Color(255, 0, 0, 255);
        }
        else
        {
            fill.color = bar;
            background.color = new Color(255, 255, 255, 255);
            handle.color = new Color(255, 255, 255, 255);
        }

    }
}
