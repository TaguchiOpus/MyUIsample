using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinforceController : MyControllerBase
{
    [SerializeField]
    ReinforceAnimationManagaer animeManager;

    public override IEnumerator OnInitialize()
    {
        yield return animeManager.Initialize();
        yield break;
    }

    public override IEnumerator OnSetup()
    {
        yield return animeManager.Setup();
        animeManager.StandbyAnimation(true);
        gameObject.SetActive(true);
        yield return animeManager.StartAniamtion();
    }

    public override IEnumerator OnRelease()
    {
        animeManager.StandbyAnimation(false);
        yield return animeManager.StartAniamtion();
        gameObject.SetActive(false);
    }

}
