using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAddPosUI : AnimationUI
{
    [SerializeField]
    Vector3 addPos = Vector3.zero;
    protected override void Load()
    {
        uiAnimation = new AnimationAddPosition(aniTarget, aniLength, aniCurve, addPos);
    }
}
