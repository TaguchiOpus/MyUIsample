using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapTypeVerticalScrollView : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.ScrollRect scrollRect;
    [SerializeField]
    UnityEngine.UI.VerticalLayoutGroup verLayout;
    [SerializeField]
    RectTransform childObj;

    int ChildCount { get { return scrollRect.content.childCount; } }

    int[] data = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    private void Start()
    {
        if (!scrollRect)
        {
            scrollRect = GetComponent<UnityEngine.UI.ScrollRect>();
        }
    }

    private void Update()
    {
       SwapScroll();
    }

    /// <summary>
    /// スクロール
    /// </summary>
    /// <param name="index"></param>
    void SwapScroll()
    {
        // 間隔
        float space = verLayout.spacing;
        // スクロール値(計算しやすい様に上下反転)
        float scrollValue = 1.0f - scrollRect.verticalNormalizedPosition;
        // 子要素の数
        int childCount = ChildCount;
        // 描画範囲
        float viewHeigth = scrollRect.viewport.rect.size.y;
        // 描画範囲の半分
        float halfViewHeight = viewHeigth / 2.0f;

        // 子要素のサイズ
        float childHeight = childObj.rect.size.y + space;

        // 現在の中心座標
        float centerPos = ((childHeight * childCount - viewHeigth) * scrollValue) + halfViewHeight;
        // 上端の座標
        float topPos = centerPos - halfViewHeight;
        // 下端の座標
        float bottomPos = centerPos + halfViewHeight;

        // 0番の要素
        int index = 0;
        // 0番の要素の中心座標
        float firstCenterPos = (childHeight * index) + (childHeight / 2.0f);
        // 0番の要素の上端座標
        float firstTopPos = (childHeight * index);
        // 0番の要素の中心座標
        float firstBottomPos = (childHeight * index) + childHeight;

        // 末尾の要素
        index = childCount-1;
        // 末尾の要素の中心座標
        float lastCenterPos = (childHeight * index) + (childHeight / 2.0f);
        // 末尾の要素の上端座標
        float lastTopPos = (childHeight * index);
        // 末尾の要素の中心座標
        float lastBottomPos = (childHeight * index) + childHeight;

        // 上方向判定
        if (firstCenterPos > topPos)
        {
            ChangeIndex(false);
            float newValue = (childHeight + (childHeight / 2.0f)) / ((childHeight * childCount) - viewHeigth);
            scrollRect.verticalNormalizedPosition = 1.0f - newValue;
        }

        // 下方向判定
        if (lastCenterPos < bottomPos)
        {
            ChangeIndex(true);
            /*float newValue = ((childHeight * (childCount - 2)) + (childHeight / 2.0f)) / (childHeight * childCount);
            scrollRect.verticalNormalizedPosition = 1.0f - newValue;*/
        }
    }

    void ChangeIndex(bool forward)
    {
        int targetIndex = forward ? 0 : ChildCount - 1;
        int toIndex = forward ? ChildCount - 1 : 0;
        List<RectTransform> children = new List<RectTransform>();
        foreach(RectTransform child in scrollRect.content.transform)
            children.Add(child);

        children[targetIndex].SetSiblingIndex(toIndex);
        if (forward)
        {
            for(int i = 1;i < children.Count; i++)
            {
                children[i].SetSiblingIndex(i - 1);
            }
        }
        else
        {
            for(int i = 0;i < children.Count - 1; i++)
            {
                children[i].SetSiblingIndex(i + 1);
            }
        }

    }
}
