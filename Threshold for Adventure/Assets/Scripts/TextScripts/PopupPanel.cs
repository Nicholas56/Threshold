using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupPanel : MonoBehaviour
{
    public TMP_Text descriptionText;

    static PopupPanel instance;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public static void Show(TextData textData)
    {
        instance.descriptionText.text = textData.Description;
        instance.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        instance.gameObject.SetActive(false);
    }
}
