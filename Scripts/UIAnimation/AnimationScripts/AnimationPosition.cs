using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AnimationPosition : AnimationBase
{
    public enum LerpType
    {
        Straight=0,
        Arc,
    }
    public enum PosType
    {
        Local = 0,
        World = 1,
    }
    Vector3 fromPos = Vector3.zero;
    Vector3 toPos = Vector3.zero;
    LerpType lerpType = LerpType.Straight;
    PosType posType = PosType.Local;
    bool isFromTarget = false;

    public AnimationPosition(AnimationPosition source) : base(source)
    {
        fromPos = source.fromPos;
        toPos = source.toPos;
        lerpType = source.lerpType;
        posType = source.posType;
        isFromTarget = source.isFromTarget;
    }
    public AnimationPosition(RectTransform target, float length, AnimationCurve curve, Vector3 from_pos, Vector3 to_pos,LerpType lerp_type,PosType pos_type,bool is_from_target) : base(target, length, curve)
    {
        fromPos = from_pos;
        toPos = to_pos;
        lerpType = lerp_type;
        posType = pos_type;
        isFromTarget = is_from_target;
    }

    protected override void Initialize(RectTransform target)
    {
        if(isFromTarget)
            fromPos = posType == AnimationPosition.PosType.Local ? target.localPosition : target.position;
    }

    protected override void PlayingAnimation(float cnt_time)
    {
        switch (posType)
        {
            case PosType.Local:
                aniTarget.localPosition = Vector3.Lerp(fromPos, toPos, cnt_time);
                break;
            case PosType.World:
                aniTarget.position = Vector3.Lerp(fromPos, toPos, cnt_time);
                break;
        }
    }
    public override AnimationBase Clone()
    {
        return new AnimationPosition(this);
    }
}