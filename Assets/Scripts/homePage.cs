using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class homePage : MonoBehaviour
{

    Button b;

    // Start is called before the first frame update
    void Start()
    {
        b = GameObject.Find("PlayButton").GetComponent<Button>();
        b.onClick.AddListener(startClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startClick()
    {
        SceneManager.LoadScene("Level1");
    }
}
