using UnityEngine;
using UnityEditor;

public class AnimationAddPosition : AnimationBase
{
    Vector3 addPos = Vector3.zero;
    Vector3 bufPos = Vector3.zero;

    public Vector3 AddPos { get { return addPos; } }
    public Vector3 BufPos { get { return bufPos; } }


    public AnimationAddPosition(AnimationAddPosition source) : base(source)
    {
        addPos = source.addPos;
        bufPos = source.BufPos;
    }

    public AnimationAddPosition(RectTransform target, float length, AnimationCurve curve, Vector3 add_pos) : base(target, length, curve)
    {
        addPos = add_pos;
    }

    protected override void Initialize(RectTransform target)
    {
        bufPos = new Vector3(target.localPosition.x, target.localPosition.y, target.localPosition.z);
    }
    protected override void PlayingAnimation(float cnt_time)
    {
        aniTarget.localPosition = Vector3.Lerp(bufPos, bufPos + addPos, cnt_time);
    }
    public override AnimationBase Clone()
    {
        return new AnimationAddPosition(this);
    }
}