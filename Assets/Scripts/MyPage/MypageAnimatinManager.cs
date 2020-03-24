using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MypageAnimatinManager : MonoBehaviour
{
    public CollectiveAnimationUI anounceManaUI;
    public CollectiveAnimationUI commandUI;
    public AnimationCollectorUI missionAnnounceUI;
    public AnimationCollectorUI changeViewBtn;
    public CollectiveAnimationUI releaseAnimationUI;

    bool isPlay = false;
    bool isOpen = true;

    public IEnumerator Initialize()
    {
        yield return anounceManaUI.Initialize();
        yield return commandUI.Initialize();
        yield return missionAnnounceUI.Initialize();
        yield return changeViewBtn.Initialize();
        yield break;
    }

    public IEnumerator Setup()
    {
        yield return anounceManaUI.Setup();
        yield return commandUI.Setup();
        commandUI.SetOnFinished(() => missionAnnounceUI.Play());
        yield return missionAnnounceUI.Setup();
        missionAnnounceUI.SetOnFinished(() => { if (isOpen) changeViewBtn.Play(); });
        yield return changeViewBtn.Setup();
        changeViewBtn.SetOnFinished(() => isPlay = false);
        yield break;
    }

    private void StandbyAnimation()
    {
        anounceManaUI.StandbyAnimation();
        commandUI.StandbyAnimation();
        missionAnnounceUI.StandbyAnimtion(true);
        changeViewBtn.StandbyAnimtion();
    }

    public IEnumerator OpenAnimaiton(Action onFinish = null)
    {
        if (isPlay)
            yield break;

        isOpen = true;
        StandbyAnimation();

        isPlay = true;

        anounceManaUI.Play();
        commandUI.Play();

        yield return new WaitUntil(() => !isPlay);

        if (onFinish != null)
            onFinish();

        yield break;
    }

    public IEnumerator ReleaseAnimation()
    {
        if (isPlay)
            yield break;

        isOpen = false;

        yield return releaseAnimationUI.Initialize();
        yield return releaseAnimationUI.Setup();
        releaseAnimationUI.SetOnFinished(() => isPlay = false);
        missionAnnounceUI.StandbyAnimtion(false);

        releaseAnimationUI.Play();
        missionAnnounceUI.Play();
    }
}
