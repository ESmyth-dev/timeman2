using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int numberOfLives = 3;
    private int level = 0;
    public bool bulletRicochet = false;
    public bool bulletPierce = false;
    public bool timeGrenade = false;
    public bool timePulse = false;
    public bool doubleJump = false;
    public bool blink = false;
    public bool slowTime = false;

    public void levelUp()
    {
        level++;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
