using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MyControllerBase
{
    [SerializeField]
    OpenQuestAnimation openAnimaiton;
    [SerializeField]
    ReleaseQuestAnimation releaseAnimaiton;

    public override IEnumerator OnInitialize()
    {
        return base.OnInitialize();
    }

    public override IEnumerator OnSetup()
    {
        return base.OnSetup();
    }

    public override IEnumerator OnRelease()
    {
        return base.OnRelease();
    }
}
