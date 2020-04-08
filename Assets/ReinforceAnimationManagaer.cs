using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinforceAnimationManagaer : MonoBehaviour
{
    [SerializeField]
    CollectiveAnimationUI openAnimation;
    [SerializeField]
    CollectiveAnimationUI releaseAnimation;

    bool isPlay = false;
    bool isOpen = true;
    
    public IEnumerator Initialize()
    {
        yield return openAnimation.Initialize();
        yield break;
    }

    public IEnumerator Setup()
    {
        yield return openAnimation.Setup();
        openAnimation.SetOnFinished(() => isPlay = false);
        releaseAnimation.SetOnFinished(() => isPlay = false);
        yield break;
    }

    public void StandbyAnimation(bool open)
    {
        isOpen = open;
        if (isOpen)
        {
            openAnimation.StandbyAnimation();
        }
        else
        {
            releaseAnimation.StandbyAnimation();
        }
    }

    public IEnumerator StartAniamtion()
    {
        if (isPlay)
            yield break;

        isPlay = true;
        if (isOpen)
        {
            openAnimation.Play();
            yield return new WaitUntil(() => !isPlay);
            yield return releaseAnimation.Initialize();
        }
        else
        {
            releaseAnimation.Play();
            yield return new WaitUntil(() => !isPlay);
        }

        yield break;
    }
}
