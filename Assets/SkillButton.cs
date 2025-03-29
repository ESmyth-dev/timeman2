using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject descriptionBox;
    private GameObject nameBox;
    private Image skillImage;
    [SerializeField]public string skillName;
    [TextArea][SerializeField] public string skillDescription;

    private void Start()
    {
        descriptionBox = FindChildWithTag(this.gameObject, "skillDescription");
        nameBox = FindChildWithTag(this.gameObject, "skillName");
        descriptionBox.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionBox.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionBox.SetActive(false);
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
