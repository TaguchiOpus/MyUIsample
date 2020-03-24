using UnityEngine;
using System.Collections;

public class AnimationColorUI : AnimationUI
{
    [SerializeField]
    protected UnityEngine.UI.Image targetImg;
    [SerializeField]
    protected UnityEngine.UI.Text targetText;
    [SerializeField]
    protected Color fromColor;
    [SerializeField]
    protected Color toColor;

    #region Property
    public UnityEngine.UI.Image TargetImg { get { return targetImg; } }
    public UnityEngine.UI.Text TargetText { get { return targetText; } }
    public Color FromColor { get { return fromColor; } }
    public Color ToColor { get { return toColor; } }
    #endregion


    protected override void Load()
    {
        if (targetImg == null)
            targetImg = aniTarget.GetComponent<UnityEngine.UI.Image>();
        if (targetText == null)
            targetText = aniTarget.GetComponent<UnityEngine.UI.Text>();

        uiAnimation = new AnimationColor(aniTarget, aniLength, aniCurve, fromColor, toColor);

    }
}