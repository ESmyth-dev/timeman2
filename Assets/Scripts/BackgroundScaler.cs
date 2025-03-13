using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SC_BackgroundScaler : MonoBehaviour
{
    public RawImage backgroundRawImage; // Changed from Image to RawImage
    public RectTransform rt;
    float ratio;

    // Start is called before the first frame update
    void Start()
    {
        // Get the RawImage component instead of Image
        //backgroundRawImage = GetComponent<RawImage>();
        //rt = backgroundRawImage.rectTransform;

        // Calculate the aspect ratio based on the texture's width and height
        if (backgroundRawImage.texture != null)
        {
            ratio = (float)backgroundRawImage.texture.width / backgroundRawImage.texture.height;
        }
        else
        {
            Debug.LogError("RawImage texture is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!rt || backgroundRawImage.texture == null)
            return;

        // Scale image proportionally to fit the screen dimensions, while preserving aspect ratio
        if (Screen.height * ratio >= Screen.width)
        {
            rt.sizeDelta = new Vector2(Screen.height * ratio, Screen.height);
        }
        else
        {
            rt.sizeDelta = new Vector2(Screen.width, Screen.width / ratio);
        }
    }
}