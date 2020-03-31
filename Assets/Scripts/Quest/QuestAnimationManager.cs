using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAnimationManager : MonoBehaviour
{
    [SerializeField]
    CollectiveAnimationUI scrollViewChildren;
    [SerializeField]
    AnimationCollectorUI scrollViewBer;

    bool isPlay = false;
    bool isOpen = true;

    public IEnumerator Initialize()
    {
        yield return scrollViewChildren.Initialize();
        yield return scrollViewBer.Initialize();
        yield break;
    }

    public IEnumerator Setup()
    {
        yield return scrollViewChildren.Setup();
        scrollViewChildren.SetOnFinished(() => isPlay = false);
        yield return scrollViewBer.Setup();
        yield break;
    }

    public void StandbyAnimaiton(bool is_open)
    {
        scrollViewChildren.StandbyAnimation(is_open);
        scrollViewBer.StandbyAnimtion(is_open);
    }

    public IEnumerator OpenAnimation()
    {
        if (isPlay)
            yield break;
        isPlay = true;

        scrollViewChildren.Play();
        scrollViewBer.Play();

        yield return new WaitUntil(() => !isPlay);

        yield break;
    }

    public IEnumerator ReleaseAnimation()
    {
        if (isPlay)
            yield break;
        isPlay = true;

        scrollViewChildren.Play();
        scrollViewBer.Play();

        yield return new WaitUntil(() => !isPlay);

        yield break;
    }
}
