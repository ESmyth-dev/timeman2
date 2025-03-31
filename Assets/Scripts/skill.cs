using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    //name
    public string skillName;
    //icon
    public Sprite skillTexture;
    //description
    public string skillDescription;
    public bool isUnlocked = false;
    //keybind
    public KeyCode keybind;
}
