using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AnimationAngle : AnimationBase
{
    Vector3 fromAngle = Vector3.zero;
    Vector3 toAngle = Vector3.zero;

    public Vector3 FromAngle { get { return fromAngle; } }
    public Vector3 ToAngle { get { return toAngle; } }

    public AnimationAngle(AnimationAngle source) : base(source)
    {
        fromAngle = source.fromAngle;
        toAngle = source.toAngle;
    }
    public AnimationAngle(RectTransform target,float length,AnimationCurve curve,Vector3 from_angle,Vector3 to_angle):base(target,length,curve)
    {
        fromAngle = from_angle;
        toAngle = to_angle;
    }

    protected override void PlayingAnimation(float cnt_time)
    {
        aniTarget.localRotation = Quaternion.Euler(Vector3.Lerp(fromAngle, toAngle, cnt_time));
    }
    public override AnimationBase Clone()
    {
        return new AnimationAngle(this);
    }
}