using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text descriptionBox;
    private TMP_Text nameBox;
    private Image skillImage;
    [SerializeField]public string skillName;
    [TextArea][SerializeField] public string skillDescription;

    private void Start()
    {
        descriptionBox = transform.parent.GetComponentInChildren<TMP_Text>();
        nameBox = GetComponentInChildren<TMP_Text>();
        nameBox.text = skillName;
        descriptionBox.text = "";
        RawImage image = GetComponentInChildren<RawImage>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionBox.text = skillDescription;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionBox.text = "";
    }

    // Call this when button is clicked
    public void SelectSkill()
    {
        Debug.Log($"Selected skill: {skillName}");
        // Add your skill selection logic here
    }

    GameObject FindChildWithTag(GameObject parent, string tag)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }
        }
        return null; // If no child with the tag was found
    }
}
