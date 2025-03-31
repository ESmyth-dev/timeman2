using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;





public class SkillCanvasPopulator : MonoBehaviour
{
    public Skill skill1;
    public Skill skill2;
    public Skill skill3;

    private Color iconColor;
    private Transform Button1;
    private Transform Button2;
    private Transform Button3;

    private Transform description;

    public CloseDoorScript ender;

    // Start is called before the first frame update
    void Start()
    {  
        if (UnityEngine.ColorUtility.TryParseHtmlString("#000000", out Color color))
            {
                iconColor = color;
            }

        description = GameObject.Find("Description").transform;
        ender = FindAnyObjectByType<CloseDoorScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCanvas()
    {
        Button1 = GameObject.Find("ButtonShell").transform;
        Button1.GetComponentInChildren<TMP_Text>().text = skill1.skillName;
        Button1.GetComponent<SkillButton>().skillDescription = skill1.skillDescription;
        Button1.GetComponentInChildren<Image>().sprite = skill1.skillTexture;
        Button1.GetComponentInChildren<Image>().color = iconColor;
        Button1.GetComponentInChildren<Image>().transform.SetAsLastSibling();
        Button1.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            skill1.isUnlocked = true;
            GameManager.instance.skills.Add(skill2);
            GameManager.instance.skills.Add(skill3);
            ender.LoadNextLevel();

        });
        //Button1.GetComponentInChildren<RawImage>().texture = skill1.skillTexture;

        Button2 = GameObject.Find("ButtonShell (1)").transform;
        Button2.GetComponentInChildren<TMP_Text>().text = skill2.skillName;
        Button2.GetComponent<SkillButton>().skillDescription = skill2.skillDescription;
        Button2.GetComponentInChildren<Image>().sprite = skill2.skillTexture;
        Button2.GetComponentInChildren<Image>().color = iconColor;
        Button2.GetComponentInChildren<Image>().transform.SetAsLastSibling();
        Button2.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            skill2.isUnlocked = true;
            GameManager.instance.skills.Add(skill1);
            GameManager.instance.skills.Add(skill3);
            ender.LoadNextLevel();
        });
        //Button2.GetComponentInChildren<RawImage>().texture = skill2.skillTexture;

        Button3 = GameObject.Find("ButtonShell (2)").transform;
        Button3.GetComponentInChildren<TMP_Text>().text = skill3.skillName;
        Button3.GetComponent<SkillButton>().skillDescription = skill3.skillDescription;
        Button3.GetComponentInChildren<Image>().sprite = skill3.skillTexture;
        Button3.GetComponentInChildren<Image>().color = iconColor;
        Button3.GetComponentInChildren<Image>().transform.SetAsLastSibling();
        Button3.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            skill3.isUnlocked = true;
            GameManager.instance.skills.Add(skill1);
            GameManager.instance.skills.Add(skill2);
            ender.LoadNextLevel();
        });
        // Button3.GetComponentInChildren<RawImage>().texture = skill3.skillTexture;
    }
}
