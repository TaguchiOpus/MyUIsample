using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationButtonScaleUI : AnimationUI
{
    [SerializeField]
    UnityEngine.UI.Button targetButton;
    [SerializeField]
    Vector2 fromScale = Vector2.one;
    [SerializeField]
    Vector2 toScale = new Vector2(0.8f, 0.8f);

    protected override void Load()
    {
        if (targetButton == null)
            targetButton = GetComponent<UnityEngine.UI.Button>();

        targetButton.onClick.AddListener(() => isPlay = true);
        uiAnimation = new AnimationScale(aniTarget, aniLength, aniCurve, fromScale, toScale);
    }
}
