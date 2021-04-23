using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractiveText : MonoBehaviour, IPointerClickHandler
{
    TMP_Text interactiveTxt;
    private void Awake()
    {
        interactiveTxt = GetComponent<TMP_Text>();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        StopAllCoroutines();
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(interactiveTxt, Input.mousePosition, null);
        if (linkIndex > -1)
        {
            var linkInfo = interactiveTxt.textInfo.linkInfo[linkIndex];
            var linkId = linkInfo.GetLinkID();

            var textData = FindObjectOfType<TextDataController>().Get(linkId);

            PopupPanel.Show(textData);
            StartCoroutine(ClosePopup());
        }
    }

    IEnumerator ClosePopup()
    {
        yield return new WaitForSeconds(3);
        PopupPanel.Hide();
    }
}
