using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartyController : MyControllerBase
{
    [SerializeField]
    PartyAnimationManager animationManager;
    [SerializeField]
    SwapScrollRectProfileUI swapScroll;

    public PartyModel partyModel = new PartyModel();

    public override IEnumerator OnInitialize()
    {
        yield return animationManager.Initialize();
        yield break;

    }

    public override IEnumerator OnSetup()
    {
        yield return partyModel.Load();
        yield return swapScroll.Initialize(partyModel.WeaponDataList.Cast<IObjectProfile>().ToList());
        swapScroll.gameObject.SetActive(true);
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
