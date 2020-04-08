using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyAnimationManager : MonoBehaviour
{
    [SerializeField]
    CollectiveAnimationUI equipmentsAnimation;
    [SerializeField]
    AnimationCollectorUI partyHeaderAnimation;
    [SerializeField]
    AnimationCollectorUI bgImgAnimation;
    [SerializeField]
    CollectiveAnimationUI releaseAnimation;

    bool isPlay = false;
    bool isOpen = true;

    public IEnumerator Initialize()
    {
        yield return equipmentsAnimation.Initialize();
        yield return partyHeaderAnimation.Initialize();
        yield return bgImgAnimation.Initialize();
        yield break;
    }

    public IEnumerator Setup()
    {
        yield return equipmentsAnimation.Setup();
        equipmentsAnimation.SetOnFinished(() => isPlay = false);
        yield return partyHeaderAnimation.Setup();
        yield return bgImgAnimation.Setup();
        releaseAnimation.SetOnFinished(() => isPlay = false);
        yield break;
    }

    public void StandbyAnimation(bool open)
    {
        isOpen = open;
        if (isOpen)
        {
            equipmentsAnimation.StandbyAnimation();
            partyHeaderAnimation.StandbyAnimation();
            bgImgAnimation.StandbyAnimation(isOpen);
        }
        else
        {
            releaseAnimation.StandbyAnimation();
            bgImgAnimation.StandbyAnimation(isOpen);
        }
    }

    public IEnumerator StartAnimation()
    {
        if (isPlay)
            yield break;

        isPlay = true;

        if (isOpen)
        {
            equipmentsAnimation.Play();
            partyHeaderAnimation.Play();
            bgImgAnimation.Play();
            yield return new WaitUntil(() => !isPlay);
            yield return releaseAnimation.Initialize();
            yield return releaseAnimation.Setup();
        }
        else
        {
            releaseAnimation.Play();
            bgImgAnimation.Play();
            yield return new WaitUntil(() => !isPlay);
        }

        yield break;
    }
}
