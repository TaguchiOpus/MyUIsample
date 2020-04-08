using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTypeSelectChildUI : MonoBehaviour
{
    [SerializeField]
    RectTransform myChild;
    [SerializeField]
    UnityEngine.UI.Image baseImg;
    [SerializeField]
    UnityEngine.UI.Text titleText;
    [SerializeField]
    UnityEngine.UI.Text subText;

    public RectTransform MyChild { get { return myChild; } }

    QuestData source = new QuestData();

    public void Setup(Color base_color,string title,string sub_title)
    {
        if (baseImg)
            baseImg.color = base_color;
        if (titleText)
            titleText.text = title;
        if (subText)
            subText.text = sub_title;
    }

    public void Setup(QuestData data)
    {
        source = data;

        if (baseImg)
            baseImg.color = Color.grey;
        if (titleText)
            titleText.text = source.title;
        if (subText)
            subText.text = source.sub_title;
    }
}
