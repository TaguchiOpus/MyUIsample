using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyController : MyControllerBase
{
    [SerializeField]
    PartyAnimationManager animationManager;

    public override IEnumerator OnInitialize()
    {
        yield return animationManager.Initialize();
        yield break;

    }

    public override IEnumerator OnSetup()
    {
        yield return animationManager.Setup();
        animationManager.StandbyAnimation(true);
        gameObject.SetActive(true);
        yield return animationManager.StartAnimation();
        yield break;
    }

    public override IEnumerator OnRelease()
    {
        animationManager.StandbyAnimation(false);
        yield return animationManager.StartAnimation();
        gameObject.SetActive(false);
        yield break;
    }

}
