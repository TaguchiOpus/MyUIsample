using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCollector
{
    #region SerializeField
    bool isPlay = false;
    bool isForward = true;
    #endregion

    #region Field
    List<AnimationBase> animationList = new List<AnimationBase>();
    Action onFinished = null;
    bool isStandby = false;
    #endregion

    #region Property
    public List<AnimationBase> AnimationList { get { return animationList; } }
    public bool IsForward { get { return isForward; } }
    public bool IsFinished { get
        {
            foreach(var ani in animationList)
            {
                if (ani != null && ani.IsPlay)
                    return false;
            }
            return true;
        } }
    public bool IsStandby { get { return isStandby; } }
    #endregion

    public AnimationCollector(List<AnimationBase> animations,RectTransform target)
    {
        foreach (var ani in animations)
        {
            var clone = ani.Clone();
            clone.SetAniTarget(target);
            animationList.Add(clone);
        }
    }

    /*public AnimationCollector(List<AnimationUI> animations,RectTransform target)
    {
        foreach (var ani in animations)
            animationList.Add
    }*/

    public AnimationCollector(AnimationCollector source)
    {
        isForward = source.IsForward;
        foreach (var ani in source.AnimationList)
            animationList.Add(new AnimationBase(ani));
    }

    public void SetAniTarget(RectTransform target)
    {
        foreach (var ani in animationList)
            ani.SetAniTarget(target);
    }

    public void StandbyAnimation(bool forward = true, AnimationBase.PlayType play_type = AnimationBase.PlayType.Once, Action onFinish = null)
    {
        isStandby = true;
        onFinished = onFinish;
        for (int i = 0; i < animationList.Count; i++)
        {
            var anime = animationList[i];
            if (anime != null)
                anime.StandbyAnimation(forward, play_type);
        }
    }

    public void StartAnimation()
    {
        isPlay = true;
        isStandby = false;
        for (int i = 0;i< animationList.Count; i++)
        {
            var anime = animationList[i];
            if (anime!=null)
                anime.Play();
        }
    }

    public void UpdateAnimation()
    {
        if (isPlay && !isStandby)
        {
            for (int i = 0; i < animationList.Count; i++)
            {
                var ani = animationList[i];
                if (ani != null)
                {
                    ani.UpdateAnimation();
                    if (IsFinished)
                        EndAnimation();
                }
            }
        }
    }

    private void EndAnimation()
    {
        isPlay = false;
        if (onFinished != null)
            onFinished();
    }
}
