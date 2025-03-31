using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Victory : MonoBehaviour
{
    Button b;

    // Start is called before the first frame update
    void Start()
    {
        b = GameObject.Find("PlayAgainButton").GetComponent<Button>();
        b.onClick.AddListener(replayClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void replayClick()
    {
        //b.image.sprite = buttonClick;
        SceneManager.LoadScene("homePage");
    }
}
