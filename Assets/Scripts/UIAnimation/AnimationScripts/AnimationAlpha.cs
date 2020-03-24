using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAlpha : AnimationBase
{
    protected UnityEngine.UI.Image targetImg;
    protected UnityEngine.UI.Text targetText;
    [Range(0.0f, 1.0f)] protected float fromAlpha = 0.0f;
    [Range(0.0f, 1.0f)] protected float toAlpha = 1.0f;

    #region Property
    public  UnityEngine.UI.Image TargetImg { get { return targetImg; } }
    public UnityEngine.UI.Text TargetText { get { return targetText; } }
    public float FromAlpha { get { return fromAlpha; } }
    public float ToAlpha { get { return toAlpha; } }
    #endregion

    public AnimationAlpha(AnimationAlpha source):base(source)
    {
        targetImg = source.targetImg;
        targetText = source.targetText;
        fromAlpha = source.fromAlpha;
        toAlpha = source.toAlpha;
    }

    public AnimationAlpha(RectTransform target):base(target)
    {
    }

    public AnimationAlpha(RectTransform target, float length, AnimationCurve curve,float from_alpha,float to_alpha) : base(target, length, curve)
    {
        fromAlpha = from_alpha;
        toAlpha = to_alpha;
    }

    protected override void Initialize(RectTransform target)
    {
        if (target != null)
        {
            targetImg = target.GetComponent<UnityEngine.UI.Image>();
            targetText = target.GetComponent<UnityEngine.UI.Text>();
        }
    }

    protected override void PlayingAnimation(float cnt_time)
    {
        if (targetImg == null && targetText == null)
            return;

        
        float alpha = MyLerp(fromAlpha, toAlpha, cnt_time);
        if (targetImg != null)
        {
            var color = targetImg.color;
            targetImg.color = new Color(color.r, color.g, color.b, alpha);
        }
        if (targetText != null)
        {
            var color = targetText.color;
            targetText.color = new Color(color.r, color.g, color.b, alpha);
        }
    }

    public override AnimationBase Clone()
    {
        return new AnimationAlpha(this);
    }
}