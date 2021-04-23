using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DisplayStatus : MonoBehaviour, IDragHandler,IBeginDragHandler
{
    public TMP_Text nameTxt;
    public TMP_Text healthTxt;
    public TMP_Text armorTxt;
    public TMP_Text statusTxt;
    public TMP_Text weaponTxt;

    Vector3 initialPos;
    Quaternion initialRot;
    public Transform charCamera;

    public void DisplayStatusMenu()
    {
        nameTxt.text = SaveData.current.profile.playerName;
        weaponTxt.text = "Weapon: " + SaveData.current.profile.weapon;
        healthTxt.text = "Health: " + SaveData.current.profile.health.ToString("n0");
        armorTxt.text = "Armor: " + SaveData.current.profile.armor.ToString("n0");
        statusTxt.text = "Status: healthy";
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPos = Input.mousePosition;
        initialRot = charCamera.rotation;
    }

    public void OnDrag(PointerEventData eventData)
    {
        charCamera.rotation = Quaternion.Euler(charCamera.eulerAngles.x, initialRot.eulerAngles.y-(initialPos.x-Input.mousePosition.x)*0.2f, charCamera.eulerAngles.z);
    }
    private void Start()
    {
        GameEvents.current.onDisplayMenu += DisplayStatusMenu;
    }
    private void OnDestroy()
    {
        GameEvents.current.onDisplayMenu -= DisplayStatusMenu;
    }
}
