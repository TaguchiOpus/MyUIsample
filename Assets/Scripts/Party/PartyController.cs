using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx.Async;

public class PartyController : MyControllerBase
{
    [SerializeField]
    PartyAnimationManager animationManager;
    [SerializeField]
    PartyScrollViewObjects viewObjects;

    public PartyModel partyModel = new PartyModel();

    public override IEnumerator OnInitialize()
    {
        yield return animationManager.Initialize();
        yield break;

    }

    public override IEnumerator OnSetup()
    {
        yield return partyModel.Load();
        viewObjects.Initialize(partyModel);
        viewObjects.gameObject.SetActive(true);
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
