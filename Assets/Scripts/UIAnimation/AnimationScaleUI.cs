using UnityEngine;
using System.Collections;

public class AnimationScaleUI : AnimationUI
{
    [SerializeField]
    Vector3 fromScale = Vector3.one;
    [SerializeField]
    Vector3 toScale = Vector3.one;

    protected override void Load()
    {
        uiAnimation = new AnimationScale(aniTarget, aniLength, aniCurve, fromScale, toScale);
    }
}