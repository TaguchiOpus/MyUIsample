using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class  SwapTypeVerticalScrollView
{
    #region SerializeField
    [SerializeField]
    UnityEngine.UI.CustomScrollRect scrollRect;
    #endregion


    #region Field
    UnityEngine.UI.VerticalLayoutGroup verLayout;   // content内のレイアウト
    RectTransform childObj;                         // 使用する子要素
    int swapCount = 0;  // 入れ替えた回数
    int dataCount = 0;  // データ数
    Action<RectTransform, int> updateItem;
    #endregion

    #region Property
    int ChildCount { get { return scrollRect.content.childCount; } }
    public int DataCount { get { return dataCount; }set { dataCount = value; Review(); } }
    public Action<RectTransform, int> UpdateItem { set { updateItem = value; } }
    #endregion

    public void Inisialize(UnityEngine.UI.CustomScrollRect scroll,int data_count, Action<RectTransform, int> on_swap = null)
    {
        updateItem = on_swap;
        scrollRect = scroll;
        verLayout = scrollRect.content.GetComponent<UnityEngine.UI.VerticalLayoutGroup>();
        childObj = scrollRect.GetChild();
        dataCount = data_count;
        Reset();
        scrollRect.onValueChanged.AddListener(SwapScroll);
        scrollRect.UpdateVerticalBerSizeEX(UpdateVerticalBerSize);
        scrollRect.UpdateVerticalBerValueEX(UpdateVerticalBerValue);
        scrollRect.SetVerticalNormalizePositionEX(VerticalNorNormalizedPosition);
    }

    /// <summary>
    /// 表示を更新
    /// </summary>
    public void Review()
    {
        foreach (RectTransform child in scrollRect.content)
        {
            int index = child.GetSiblingIndex() + swapCount;
            updateItem(child, index);
        }
    }

    /// <summary>
    /// スクロール位置をリセット
    /// </summary>
    public void Reset()
    {
        swapCount = 0;
        scrollRect.content.localPosition = Vector2.zero;
        scrollRect.Rebuild(UnityEngine.UI.CanvasUpdate.PostLayout);
        Review();
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
        if (firstCenterPos > topPos && swapCount > 0)
        {
            ChangeIndex(false);
            /*float newValue = (childHeight + (childHeight / 2.0f)) / ((childHeight * childCount) - viewHeigth);
            scrollRect.verticalNormalizedPosition = 1.0f - newValue;*/
            float contentPos = childHeight + ((childObj.rect.size.y + space) / 2.0f);
            scrollRect.content.localPosition = new Vector2(0, contentPos);
            scrollRect.ResetPointer();
        }

        // 下方向判定
        if (lastCenterPos < bottomPos && swapCount + childCount < dataCount)
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
            swapCount++;
            for(int i = 1;i < children.Count; i++)
            {
                children[i].SetSiblingIndex(i - 1);
            }
        }
        else
        {
            swapCount--;
            for(int i = 0;i < children.Count - 1; i++)
            {
                children[i].SetSiblingIndex(i + 1);
            }
        }

        int dataIndex = forward ? swapCount + ChildCount-1 : swapCount;
        UpdateContents(children[targetIndex], dataIndex);

    }

    protected void UpdateContents(RectTransform target,int dataIndex)
    {
        if (updateItem != null)
        {
            if (dataIndex < dataCount) 
            {
                target.gameObject.SetActive(true);
                updateItem(target, dataIndex);
            }
            else
                target.gameObject.SetActive(false);
        }
    }

    private float UpdateVerticalBerSize(float view_size_y,float offset_y)
    {
        // 間隔
        float space = verLayout.spacing;
        // 子要素のサイズ
        float childHeight = childObj.rect.size.y + space;
        // 全データ数のサイズ
        float childFullHeight = verLayout.padding.top + verLayout.padding.bottom + (childHeight * dataCount) - space;
        return (view_size_y - offset_y) / childFullHeight;
    }

    private float UpdateVerticalBerValue(float view_min_y,float view_size_y,float content_min_y)
    {
        // 間隔
        float space = verLayout.spacing;
        // 子要素のサイズ
        float childHeight = childObj.rect.size.y + space;
        // 全データ数のサイズ
        float contentSizeY = verLayout.padding.top + verLayout.padding.bottom + (childHeight * dataCount) - space;

        float contentMinY = content_min_y - (childHeight * (dataCount - ((swapCount) + ChildCount)));
        return (view_min_y - contentMinY) / (contentSizeY - view_size_y);
    }

    private float VerticalNorNormalizedPosition(float view_min_y, float view_size_y, float content_min_y,float value)
    {
        // 間隔
        float space = verLayout.spacing;
        // 子要素のサイズ
        float childHeight = childObj.rect.size.y + space;
        // 全データ数のサイズ
        float contentSizeY = verLayout.padding.top + verLayout.padding.bottom + (childHeight * dataCount) - space;
        // 現在のcontentの底辺座標
        float contentMinY = content_min_y - (childHeight * (dataCount - ((swapCount) + ChildCount)));

        // How much the content is larger than the view.
        float hiddenLength = contentSizeY - view_size_y;
        // Where the position of the lower left corner of the content bounds should be, in the space of the view.
        float contentBoundsMinPosition = view_min_y - value * hiddenLength;
        // The new content localPosition, in the space of the view.
        float newLocalPosition = scrollRect.content.localPosition[1] + contentBoundsMinPosition - contentMinY;

        return newLocalPosition;
    }
}
