using UnityEngine;

public class FixedSizeImage : MonoBehaviour
{
    [SerializeField]
    private Vector2 fixedSize = new Vector2(100f, 100f);

    void Start()
    {
        AdjustSize();
    }

    void AdjustSize()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        if (rectTransform != null)
        {
            rectTransform.sizeDelta = fixedSize;
        }
        else
        {
            Debug.LogError("El objeto no tiene un RectTransform.");
        }
    }
}
