using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationColor : AnimationBase
{
    protected UnityEngine.UI.Image targetImg;
    protected UnityEngine.UI.Text targetText;
    protected Color fromColor;
    protected Color toColor;

    #region Property
    public UnityEngine.UI.Image TargetImg { get { return targetImg; } }
    public UnityEngine.UI.Text TargetText { get { return targetText; } }
    public Color FromColor { get { return fromColor; } }
    public Color ToColor { get { return toColor; } }
    #endregion

    public AnimationColor(AnimationColor source) : base(source)
    {
        targetImg = source.targetImg;
        targetText = source.targetText;
        fromColor = source.fromColor;
        toColor = source.toColor;
    }

    public AnimationColor(RectTransform target) : base(target)
    {
    }

    public AnimationColor(RectTransform target, float length, AnimationCurve curve, Color from_color, Color to_color) : base(target, length, curve)
    {
        fromColor = from_color;
        toColor = to_color;
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

        Color color = Color.Lerp(fromColor, toColor, cnt_time);
        if (targetImg != null)
            targetImg.color = color;
        if (targetText != null)
            targetText.color = color;
    }

    public override AnimationBase Clone()
    {
        return new AnimationColor(this);
    }
}