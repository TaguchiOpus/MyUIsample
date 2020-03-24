using UnityEngine;
using System.Collections;

public class AnimationAngleUI : AnimationUI
{
    [SerializeField]
    Vector3 fromAngle = Vector3.zero;
    [SerializeField]
    Vector3 toAngle = Vector3.zero;

    public Vector3 FromAngle { get { return fromAngle; } }
    public Vector3 ToAngle { get { return toAngle; } }

    protected override void Load()
    {
        uiAnimation = new AnimationAngle(aniTarget, aniLength, aniCurve, fromAngle, toAngle);

    }
}