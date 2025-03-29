using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BackgroundBlur : MonoBehaviour
{
    [SerializeField] private float blurAmount = 5f;

    private void Start()
    {
        // Capture the screen and apply blur
        var image = GetComponent<Image>();
        image.material = new Material(Shader.Find("UI/Blur"));
        image.material.SetFloat("_Size", blurAmount);
        image.color = new Color(1, 1, 1, 0.7f); // Translucent
    }
}
