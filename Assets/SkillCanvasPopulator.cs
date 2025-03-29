using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;





public class SkillCanvasPopulator : MonoBehaviour
{
    public Skill skill1;
    public Skill skill2;
    public Skill skill3;

    public Transform Button1;
    public Transform Button2;
    public Transform Button3;

    // Start is called before the first frame update
    void Start()
    {
        Button1 = transform.Find("ButtonShell");
        Button1.GetComponentInChildren<TMP_Text>().text = skill1.skillName;
        Button1.GetComponentInChildren<RawImage>().texture = skill1.skillTexture;
        Button1.transform.parent.GetComponentInChildren<TMP_Text>().text = skill1.skillDescription;

        Button2 = transform.Find("ButtonShell2");
        Button1.GetComponentInChildren<TMP_Text>().text = skill2.skillName;
        Button1.GetComponentInChildren<RawImage>().texture = skill2.skillTexture;
        Button1.transform.parent.GetComponentInChildren<TMP_Text>().text = skill2.skillDescription;

        Button3 = transform.Find("ButtonShell3");
        Button1.GetComponentInChildren<TMP_Text>().text = skill3.skillName;
        Button1.GetComponentInChildren<RawImage>().texture = skill3.skillTexture;
        Button1.transform.parent.GetComponentInChildren<TMP_Text>().text = skill3.skillDescription;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
