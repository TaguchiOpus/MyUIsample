using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestView : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    QuestTypeSelectChildUI childObj;
    [SerializeField]
    GameObject parentObj;
    [SerializeField]
    CollectiveAnimationUI animaiton;
    #endregion

    List<QuestData> source = new List<QuestData>();
    List<QuestTypeSelectChildUI> typeSelectList = new List<QuestTypeSelectChildUI>();

    public List<QuestTypeSelectChildUI> QuestTypeSelectList { get { return typeSelectList; } }

    public List<RectTransform> GetAnimaitonObjectList()
    {
        List<RectTransform> res = new List<RectTransform>();
        foreach (RectTransform obj in parentObj.transform)
            res.Add(obj);    
        
        return res.ToList();
    }

    public List<float> GetAnimationDelayList()
    {
        List<float> res = new List<float>();
        var count = GetAnimaitonObjectList().Count;
        for (int i = 0; i < count; i++)
            res.Add(0.1f * i);

        return res;
    }

    public void Setup(List<QuestData> data)
    {
        source = data;

        CreateQuestList(source);

        var list = GetAnimaitonObjectList();
        var aniTargets = list.Select(x => (RectTransform)x.transform.GetChild(0)).ToList();
        animaiton.SetTarget(aniTargets, GetAnimationDelayList());
    }

    private void CreateQuestList(List<QuestData> data_list)
    {
        ResetQuestList();
        for(int i = 0; i < data_list.Count; i++)
        {
            var data = data_list[i];
            if(typeSelectList.Count > i)
            {
                SetQuestChild(data, typeSelectList[i]);
            }
            else
            {
                AddQuestChild(data, i);
            }
        }
        SortQuestTypeList();
    }

    private void SetQuestChild(QuestData data, QuestTypeSelectChildUI child)
    {
        child.Setup(data);
        child.gameObject.SetActive(true);
    }

    private void AddQuestChild(QuestData data, int index)
    {
        var clone = GameObject.Instantiate(childObj) as QuestTypeSelectChildUI;
        if (clone != null)
        {
            clone.transform.SetParent(parentObj.transform);
            clone.transform.localScale = Vector3.one;
            typeSelectList.Add(clone);
            SetQuestChild(data, clone);
        }
    }

    private void SortQuestTypeList()
    {
        var source = GetAnimaitonObjectList();
        var eventTitle = source[1];
        var mainQuest = source[2];
        if (eventTitle.name == "EventQuestTitle")
        {
            eventTitle.SetSiblingIndex(2);
            mainQuest.SetSiblingIndex(1);
        }
    }

    private void ResetQuestList()
    {
        foreach (var list in typeSelectList)
            list.gameObject.SetActive(false);
    }
}
