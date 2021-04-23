using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    PlayerProfile profile;
    public CharacterHolder holder;
    public Transform charCamera;
    // Start is called before the first frame update
    void Start()
    {
        profile = SaveData.current.profile;
        SetupCharModel();
    }

    void SetupCharModel()
    {
        if (holder.charChoices == null) { Debug.Log("There is no character model"); }
        GameObject model = Instantiate(holder.charChoices[profile.modelChoice], transform);
        model.tag = "Player";
        model.AddComponent<PlayerMovement>();
        model.AddComponent<PlayerCombat>();
        model.AddComponent<PlayerCameraFollow>();
        WeaponManager.current.Equip(profile.weapon);
        model.transform.position = profile.lastPos;
        charCamera.position = profile.lastPos;

        charCamera.SetParent(model.transform);
    }
}
