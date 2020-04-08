using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssistanceVerticalScrollRect : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.ScrollRect scrollRect;
    [SerializeField]
    UnityEngine.UI.VerticalLayoutGroup verLayout;

    [SerializeField]
    bool isFocus = false;

    RectTransform target = new RectTransform(); // ターゲット(仮)

    private void Start()
    {
        if(!scrollRect)
        {
            scrollRect = GetComponent<UnityEngine.UI.ScrollRect>();
        }
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// スクロール
    /// </summary>
    /// <param name="index"></param>
    void Scroll(int index)
    {
        // 間隔
        float space = verLayout.spacing;
        // スクロール値(計算しやすい様に上下反転)
        float scrollValue = 1.0f - scrollRect.verticalNormalizedPosition;
        // 子要素の数
        float childCount = scrollRect.content.childCount;
        // 描画範囲
        float viewHeigth = scrollRect.viewport.rect.size.y;
        // 描画範囲の半分
        float halfViewHeight = viewHeigth / 2.0f;

        // 子要素のサイズ
        float childHeight = target.rect.size.y + space;

        // 現在の中心座標
        float centerPos = ((childHeight * childCount - viewHeigth) * scrollValue) + halfViewHeight;
        // 上端の座標
        float topPos = centerPos - halfViewHeight;
        // 下端の座標
        float bottomPos = centerPos + halfViewHeight;

        // 選択中の子要素の中心座標
        float targetCenterPos = (childHeight * index) + (childHeight / 2.0f);

        // 選択中の子要素が上側にはみ出している
        if(targetCenterPos < topPos)
        {
            float newScrollValue = (childHeight * index) / ((childHeight * childCount) - viewHeigth);
            scrollRect.verticalScrollbar.value = 1.0f - newScrollValue; // 反転を戻す
        }

        // 選択中の子要素が下側にはみ出している
        if (targetCenterPos > bottomPos)
        {
            float newScrollValue = ((childHeight * (index+1))+space-viewHeigth) / ((childHeight * childCount) - viewHeigth);
            scrollRect.verticalScrollbar.value = 1.0f - newScrollValue; // 反転を戻す
        }
    }
}
