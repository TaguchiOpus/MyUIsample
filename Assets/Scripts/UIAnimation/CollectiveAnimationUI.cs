using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiveAnimationUI : MonoBehaviour
{
    [SerializeField]
    List<RectTransform> targetList;
    [SerializeField]
    List<float> delayList;
    [SerializeField]
    bool isPlay = false;
    [SerializeField]
    bool isForward = true;
    [SerializeField]
    AnimationBase.PlayType aniPlayType = AnimationBase.PlayType.Once;

    List<AnimationCollector> collectorList = new List<AnimationCollector>();
    List<AnimationUI> animationUIList = new List<AnimationUI>();
    List<AnimationBase> animationList = new List<AnimationBase>();
    DateTime bufTime = DateTime.MinValue;
    bool isReady = true;
    bool isInitialized = false;
    Action onFinished = null;

    public bool IsPlay { get { return isPlay; } }

    bool UIReady { get
        {
            var readies = animationUIList.Select(x => x.IsReady).ToList();
            for(int i = 0; i < readies.Count; i++)
            {
                if (readies[i])
                    return true;
            }
            return false;
        } }

    /*private void Start()
    {
        StartCoroutine(Initialize());
    }*/

    public void SetOnFinished(Action on_finish)
    {
        onFinished = on_finish;
    }

    public void ClearOnFinished()
    {
        onFinished = null;
    }
    public IEnumerator Initialize()
    {
        if (!isInitialized)
        {
            isInitialized = true;
            animationUIList.AddRange(GetComponents<AnimationUI>());
            foreach (var ani in animationUIList)
                ani.Initialize();

            CreateCollector();
                
            //foreach (var ani in collectorList)
                   //ani.StandbyAnimation(isForward, aniPlayType,() => isPlay = false);
            //yield return new WaitForEndOfFrame();
            //isReady = false;
        }
        yield break;
    }

    public IEnumerator Setup()
    {
        isReady = false;
        yield break;
    }

    public void Play()
    {
        enabled = true;
        isPlay = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isPlay&&!isReady)
        {
            if (bufTime == DateTime.MinValue)
            {
                bufTime = DateTime.Now;
                StandbyAnimation();
                StartAnimation();
            }

            isPlay = false;
            float second = (float)((DateTime.Now - bufTime).TotalMilliseconds / 1000.0);
            for (int i = 0; i < collectorList.Count; i++)
            {
                var ani = collectorList[i];
                if (delayList[i] < second)
                {
                    ani.UpdateAnimation();
                }
                if (!ani.IsFinished)
                    isPlay = true;
            }

            if (!isPlay)
                EndAnimation();
        }
    }

    void CreateCollector()
    {
        for (int i = 0; i < animationUIList.Count; i++)
        {
            var animeUI = animationUIList[i];
            animationList.Add(animeUI.UiAnimation);
        }

        if (targetList == null)
            targetList = new List<RectTransform>();
        for (int i = 0; i < targetList.Count; i++)
        {
            AnimationCollector aniCollector = new AnimationCollector(animationList, targetList[i]);
            collectorList.Add(aniCollector);
            if (delayList.Count <= i)
                delayList.Add(0);
        }
    }

    public void StandbyAnimation()
    {
        foreach (var collector in collectorList)
        {
            if (!collector.IsStandby)
                collector.StandbyAnimation();
        }
    }

    void StartAnimation()
    {
        for (int i = 0; i < collectorList.Count; i++)
            collectorList[i].StartAnimation();
    }
    void EndAnimation()
    {
        bufTime = DateTime.MinValue;
        if (onFinished != null)
            onFinished();
    }
}
