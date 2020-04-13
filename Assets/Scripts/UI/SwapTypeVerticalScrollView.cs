using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapTypeVerticalScrollView : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.CustomScrollRect scrollRect;
    [SerializeField]
    UnityEngine.UI.VerticalLayoutGroup verLayout;
    [SerializeField]
    RectTransform childObj;

    int itemCount = 0;
    int ChildCount { get { return scrollRect.content.childCount; } }

    int[] data = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    private void Start()
    {
        if (!scrollRect)
        {
            scrollRect = GetComponent<UnityEngine.UI.CustomScrollRect>();
        }

        foreach (RectTransform child in scrollRect.content)
        {
            int index = child.GetSiblingIndex();
            UpdateItem(child, index);
        }

        scrollRect.onValueChanged.AddListener(SwapScroll);
        scrollRect.UpdateVerticalBerSizeEX(UpdateVerticalBerSize);
        scrollRect.UpdateVerticalBerValueEX(UpdateVerticalBerValue);
    }

    private void Update()
    {
    }

    /// <summary>
    /// スクロール
    /// </summary>
    /// <param name="index"></param>
    void SwapScroll(Vector2 vector2)
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
        // 全要素のサイズ
        float childFullHeight = verLayout.padding.top + verLayout.padding.bottom + (childHeight * childCount) - space;

        // 現在の中心座標
        float centerPos = ((childFullHeight - viewHeigth) * scrollValue) + halfViewHeight;
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
        float lastCenterPos = (childHeight * index) + ((childHeight-space) / 2.0f);
        // 末尾の要素の上端座標
        float lastTopPos = (childHeight * index);
        // 末尾の要素の中心座標
        float lastBottomPos = (childHeight * index) + childHeight;

        // 上方向判定
        if (firstCenterPos > topPos && itemCount > 0)
        {
            ChangeIndex(false);
            /*float newValue = (childHeight + (childHeight / 2.0f)) / ((childHeight * childCount) - viewHeigth);
            scrollRect.verticalNormalizedPosition = 1.0f - newValue;*/
            float contentPos = childHeight + (childObj.rect.size.y / 2.0f);
            scrollRect.content.localPosition = new Vector2(0, contentPos);
            scrollRect.ResetPointer();
        }

        // 下方向判定
        if (lastCenterPos < bottomPos && itemCount + childCount < data.Length)
        {
            ChangeIndex(true);
            /*float newValue = ((childHeight * (childCount - 2)) + (childHeight / 2.0f)) / (childHeight * childCount);
            scrollRect.verticalNormalizedPosition = 1.0f - newValue;*/
            float contentPos = ((childHeight * (childCount - 2)) + (childObj.rect.size.y / 2.0f)) - viewHeigth;
            scrollRect.content.localPosition = new Vector2(0, contentPos);
            scrollRect.ResetPointer();
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
            itemCount++;
            for(int i = 1;i < children.Count; i++)
            {
                children[i].SetSiblingIndex(i - 1);
            }
        }
        else
        {
            itemCount--;
            for(int i = 0;i < children.Count - 1; i++)
            {
                children[i].SetSiblingIndex(i + 1);
            }
        }

        int dataIndex = forward ? itemCount + ChildCount-1 : itemCount;
        UpdateItem(children[targetIndex], dataIndex);

    }

    protected virtual void UpdateItem(RectTransform target,int dataIndex)
    {
        var text = target.GetComponentInChildren<UnityEngine.UI.Text>();
        if (text != null)
            text.text = data[dataIndex].ToString();
    }

    private float UpdateVerticalBerSize(float view_size_y,float offset_y)
    {
        // 間隔
        float space = verLayout.spacing;
        // 子要素のサイズ
        float childHeight = childObj.rect.size.y + space;
        // 全データ数のサイズ
        float childFullHeight = verLayout.padding.top + verLayout.padding.bottom + (childHeight * data.Length) - space;
        return (view_size_y - offset_y) / childFullHeight;
    }

    private float UpdateVerticalBerValue(float view_min_y,float view_size_y,float content_min_y)
    {
        // 間隔
        float space = verLayout.spacing;
        // 子要素のサイズ
        float childHeight = childObj.rect.size.y + space;
        // 全データ数のサイズ
        float contentSizeY = verLayout.padding.top + verLayout.padding.bottom + (childHeight * data.Length) - space;

        float contentMinY = content_min_y - (childHeight * (data.Length - ((itemCount) + ChildCount)));
        return (view_min_y - contentMinY) / (contentSizeY - view_size_y);
    }
}
