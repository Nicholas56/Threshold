using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillEvents : MonoBehaviour
{
    public static SkillEvents current;

    private void Awake()
    {
        current = this;
    }

    //Event reciever delegates functions to this action
    public event Action<string> onSkillGain;
    //Event transmitter call function below
    public void SkillGain(string id)
    {
        if (onSkillGain != null)
        {
            onSkillGain(id);
        }
    }
}
