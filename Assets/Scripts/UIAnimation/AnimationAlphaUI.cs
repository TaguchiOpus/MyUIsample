using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAlphaUI : AnimationUI
{
    [SerializeField]
    protected UnityEngine.UI.Image targetImg;
    [SerializeField]
    protected UnityEngine.UI.Text targetText;
    [SerializeField]
    [Range(0.0f, 1.0f)] protected float fromAlpha = 0.0f;
    [SerializeField]
    [Range(0.0f, 1.0f)] protected float toAlpha = 1.0f;

    #region Property
    public UnityEngine.UI.Image TargetImg { get { return targetImg; } }
    public UnityEngine.UI.Text TargetText { get { return targetText; } }
    public float FromAlpha { get { return fromAlpha; } }
    public float ToAlpha { get { return toAlpha; } }
    #endregion

    protected override void Load()
    {
        if (targetImg == null)
            targetImg = aniTarget.GetComponent<UnityEngine.UI.Image>();
        if (targetText == null)
            targetText = aniTarget.GetComponent<UnityEngine.UI.Text>();

        uiAnimation = new AnimationAlpha(aniTarget, aniLength, aniCurve, fromAlpha, toAlpha);

    }

}