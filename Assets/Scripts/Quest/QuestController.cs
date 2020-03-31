using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MyControllerBase
{
    [SerializeField]
    QuestAnimationManager openAnimaiton;

    public override IEnumerator OnInitialize()
    {
        yield return openAnimaiton.Initialize();
    }

    public override IEnumerator OnSetup()
    {
        yield return openAnimaiton.Setup();
        openAnimaiton.StandbyAnimaiton(true);
        gameObject.SetActive(true);

        yield return openAnimaiton.OpenAnimation();
    }

    public override IEnumerator OnRelease()
    {
        openAnimaiton.StandbyAnimaiton(false);
        yield return openAnimaiton.ReleaseAnimation();
        gameObject.SetActive(false);
    }
}
