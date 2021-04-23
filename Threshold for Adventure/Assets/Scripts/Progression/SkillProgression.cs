using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProgression : MonoBehaviour
{
    public string id;
    private void Start()
    {
        SkillEvents.current.onSkillGain += OnSkillgain;
    }
    //This function is called by the event onSkillGain
    void OnSkillgain(string id)
    {
        if (id == this.id)
        {

        }
    }

    private void OnDestroy()
    {
        SkillEvents.current.onSkillGain -= OnSkillgain;
    }
}
