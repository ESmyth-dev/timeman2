using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegainLife : MonoBehaviour
{
    public void RegainPlayerLife()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.GainLife();
        }
        else
        {
            Debug.LogError("GameManager instance is not set.");
        }
    }
}
