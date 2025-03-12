using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int numberOfLives = 3;
    private int level = 0;
    public boolean bulletRicochet = false;
    public boolean bulletPierce = false;
    public boolean timeGrenade = false;
    public boolean timePulse = false;
    public boolean doubleJump = false;
    public boolean blink = false;
    public boolean slowTime = false;

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
