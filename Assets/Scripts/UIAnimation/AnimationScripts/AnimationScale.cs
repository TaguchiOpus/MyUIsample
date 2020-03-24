using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AnimationScale : AnimationBase
{
    Vector3 fromScale= Vector3.one;
    Vector3 toScale= Vector3.one;

    public Vector3 FromScale { get { return fromScale; } }
    public Vector3 ToScale { get { return toScale; } }

    public AnimationScale(AnimationScale source) : base(source)
    {
        fromScale = source.fromScale;
        toScale = source.toScale;
    }
    public AnimationScale(RectTransform target, float length, AnimationCurve curve, Vector3 from_scale, Vector3 to_scale) : base(target, length, curve)
    {
        fromScale = from_scale;
        toScale = to_scale;
    }

    protected override void PlayingAnimation(float cnt_time)
    {
        aniTarget.localScale = Vector3.Lerp(fromScale, toScale, cnt_time);
    }
    public override AnimationBase Clone()
    {
        return new AnimationScale(this);
    }
}
