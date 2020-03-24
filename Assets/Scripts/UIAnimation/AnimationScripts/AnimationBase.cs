using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AnimationBase
{
    /// <summary>
    /// アニメーションのタイプ
    /// </summary>
    public enum PlayType
    {
        Once = 1,       // １回のみ
        Loop,           // ループ
        ShuttleRun,     // 往復
        OnceShuttleRun, // 1回のみ往復
    }

    #region Field
    protected RectTransform aniTarget;      // アニメーションするターゲット
    protected float aniLength;              // アニメーションの長さ(秒)
    protected AnimationCurve aniCurve;      // アニメーションカーブ
    protected bool isPlay;                  // 再生中フラグ
    protected bool isForward = true;        // 再生/逆再生フラグ
    protected bool isStandby = true;
    protected PlayType aniPlayType = PlayType.Once;         // 再生タイプ

    protected Action onAnimationFinish = null;      // アニメーション終了時の処理
    protected DateTime bufTime = DateTime.MinValue; // アニメーション開始時間を保持
    protected float cntTime = 0.0f;                 // アニメーションの経過時間
    protected int finishCnt = 0;                    // アニメーション終了数カウント
    #endregion

    #region Property
    public RectTransform AniTarget { get { return aniTarget; } }
    public float AniLength { get { return aniLength; } }
    public AnimationCurve AniCurve { get { return aniCurve; } }
    public PlayType AniPlayType { get { return aniPlayType; } }
    public bool IsForward { get { return isForward; } }
    public bool IsPlay { get { return isPlay; } }

    public bool IsStandby { get { return isStandby; } }
    #endregion

    #region Constructor
    public AnimationBase(RectTransform target,float length, AnimationCurve curve)
    {
        aniTarget = target;
        aniLength = length;
        aniCurve = curve;
        Initialize(aniTarget);
    }
    /// <summary>
    /// コピーコンストラクタ
    /// </summary>
    /// <param name="target"></param>
    public AnimationBase(AnimationBase target)
    {
        aniTarget = target.AniTarget;
        aniLength = target.AniLength;
        aniCurve = target.AniCurve;
        aniPlayType = target.AniPlayType;
        Initialize(aniTarget);
    }
    public AnimationBase(AnimationBase source, RectTransform target)
    {
        aniTarget = target;
        aniLength = source.AniLength;
        aniCurve = source.AniCurve;
        aniPlayType = source.AniPlayType;
        Initialize(aniTarget);
    }
    public AnimationBase(RectTransform target)
    {
        aniTarget = target;
        aniCurve = AnimationCurve.Linear(0, 0, 1, 1);
        Initialize(target);
    }
    #endregion

    public void SetAniTarget(RectTransform target)
    {
        aniTarget = target;
        Initialize(target);
    }

    public void StandbyAnimation(bool forward = true, PlayType play_type = PlayType.Once, Action onFinish = null)
    {
        isStandby = true;
        if (onFinish != null)
            onAnimationFinish = onFinish;
        isForward = forward;
        aniPlayType = play_type;
        finishCnt = 0;
        cntTime = isForward ? 0.0f : aniLength;
        PlayingAnimation(cntTime);
    }

    /// <summary>
    /// アニメーションを再生
    /// </summary>
    /// <param name="forward">再生/逆再生</param>
    /// <param name="onFinish">アニメーション終了時の処理</param>
    public void Play()
    {
        isStandby = false;
        isPlay = true;
    }

    /// <summary>
    /// アニメーション終了後の処理を空にする
    /// </summary>
    public void ClearOnFinishAction()
    {
        onAnimationFinish = null;
    }

    public void UpdateAnimation()
    {
        if (isPlay && !isStandby && aniTarget != null)
        {
            bool isFinish = false;  // アニメーション終了フラグ

            if (bufTime == DateTime.MinValue)
                bufTime = DateTime.Now;

            if (isForward)  // 通常再生
            {
                cntTime = (float)((DateTime.Now - bufTime).TotalMilliseconds / 1000.0);
                if (cntTime >= aniLength)
                {
                    cntTime = aniLength;
                    isFinish = true;
                }
            }
            else            // 逆再生
            {
                cntTime = aniLength - (float)((DateTime.Now - bufTime).TotalMilliseconds / 1000.0);
                if (cntTime <= 0.0f)
                {
                    cntTime = 0.0f;
                    isFinish = true;
                }
            }

            PlayingAnimation(aniCurve.Evaluate(cntTime / aniLength));

            if (isFinish)
                AnimationFinish();
        }
    }

    protected float MyLerp(float from, float to, float by)
    {
        return (from * (1 - by)) + (to * by);
    }

    /// <summary>
    /// アニメーション終了処理
    /// </summary>
    protected void AnimationFinish()
    {
        finishCnt++;
        switch (aniPlayType)
        {
            case PlayType.Loop:
                bufTime = DateTime.Now;
                return;
            case PlayType.ShuttleRun:
                bufTime = DateTime.Now;
                isForward = !isForward;
                return;
            case PlayType.OnceShuttleRun:
                isForward = !isForward;
                if (finishCnt <= 1)
                {
                    bufTime = DateTime.Now;
                    return;
                }
                break;
        }

        isPlay = false;
        finishCnt = 0;
        bufTime = DateTime.MinValue;
        if (onAnimationFinish != null)
            onAnimationFinish();
    }

    public  virtual AnimationBase Clone()
    {
        return new AnimationBase(this);
    }

    /// <summary>
    /// 初期化関数(Start時に呼び出す)
    /// </summary>
    protected virtual void Initialize(RectTransform target) { }
    /// <summary>
    /// アニメーション処理
    /// </summary>
    /// <param name="cnt_time">アニメーションの経過時間</param>
    protected virtual void PlayingAnimation(float cnt_time) { }
}