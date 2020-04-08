using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

public class AnimationCollectorUI : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    RectTransform aniTarget;
    [SerializeField]
    bool isPlay = false;
    [SerializeField]
    bool isForward = true;
    [SerializeField]
    AnimationBase.PlayType playType = AnimationBase.PlayType.Once;
    #endregion

    #region Field
    List<AnimationUI> animationList = new List<AnimationUI>();
    AnimationCollector animationCollector;
    Action onFinished = null;
    bool isInitialized = false;
    bool isReady = true;
    bool isPlaying = false;
    #endregion

    #region Property
    public List<AnimationUI> AnimationList { get { return animationList; } }
    public bool IsForward { get { return isForward; } }
    public bool IsFinished
    {
        get
        {
            foreach (var ani in animationList)
            {
                if (ani.IsPlay)
                    return false;
            }
            return true;
        }
    }
    public bool IsPlay { get { return isPlaying || isPlay; } }
    #endregion

    public void ClearOnFinished()
    {
        onFinished = null;
    }

    public void SetOnFinished(Action on_finish)
    {
        onFinished = on_finish;
    }

    public void StandbyAnimation()
    {
        if (!animationCollector.IsStandby)
            animationCollector.StandbyAnimation(isForward,onFinish:()=>EndAnimation());
    }

    public void StandbyAnimation(bool forward)
    {
        isForward = forward;
        if (!animationCollector.IsStandby)
            animationCollector.StandbyAnimation(isForward, onFinish: () => EndAnimation());
    }

    public void StartAnimation()
    {
        isPlaying = true;
        animationCollector.StartAnimation();
    }

    public void Initialize(GameObject target)
    {
        if (!isInitialized)
        {
            isInitialized = true;
            aniTarget = target.GetComponent<RectTransform>();
            var anime = target.GetComponents<AnimationUI>();
            animationCollector = new AnimationCollector(anime.Select(x => x.UiAnimation).ToList(), aniTarget);
            animationList = new List<AnimationUI>();
            animationList.AddRange(anime);
        }
    }

    public IEnumerator Initialize()
    {
        if (!isInitialized)
        {
            isInitialized = true;
            if (aniTarget == null)
                aniTarget = GetComponent<RectTransform>();
            var anime = GetComponents<AnimationUI>();
            animationList = new List<AnimationUI>();
            animationList.AddRange(anime);
            foreach (var ani in animationList)
            {
                ani.Initialize();
            }
            animationCollector = new AnimationCollector(anime.Select(x => x.UiAnimation).ToList(), aniTarget);
        }
        isReady = false;
        yield break;
    }

    public  IEnumerator Setup()
    {
        isReady = false;
        yield break;
    }

    public void Play()
    {
        isPlay = true;
        enabled = true;
    }
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Initialize());
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay&&!isReady)
        {
            StandbyAnimation();
            StartAnimation();
            isPlay = false;
        }
        if (isPlaying)
        {
            animationCollector.UpdateAnimation();
        }
    }

    private void EndAnimation()
    {
        isPlaying = false;
        if (onFinished != null)
            onFinished();
    }
}