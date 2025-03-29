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

    private Transform Button1;
    private Transform Button2;
    private Transform Button3;

    // Start is called before the first frame update
    void Start()
    {
        Button1 = transform.Find("ButtonShell");
        Button1.GetComponentInChildren<TMP_Text>().text = skill1.skillName;
        Button1.GetComponentInChildren<RawImage>().texture = skill1.skillTexture;
        Button1.transform.parent.GetComponentInChildren<TMP_Text>().text = skill1.skillDescription;

        Button2 = transform.Find("ButtonShell2");
        Button2.GetComponentInChildren<TMP_Text>().text = skill2.skillName;
        Button2.GetComponentInChildren<RawImage>().texture = skill2.skillTexture;
        Button2.transform.parent.GetComponentInChildren<TMP_Text>().text = skill2.skillDescription;

        Button3 = transform.Find("ButtonShell3");
        Button3.GetComponentInChildren<TMP_Text>().text = skill3.skillName;
        Button3.GetComponentInChildren<RawImage>().texture = skill3.skillTexture;
        Button3.transform.parent.GetComponentInChildren<TMP_Text>().text = skill3.skillDescription;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
