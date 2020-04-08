using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;

public class QuestModel
{
    #region Const
    const string QuestDataFilePath = "/Data/Quest/QuestDataFile.json";
    #endregion

    #region Field
    private List<QuestData> questDataList;
    #endregion

    #region Property
    public List<QuestData> QuestDataList { get { return questDataList; } }
    #endregion

    public IEnumerator Load()
    {
        questDataList = GetQuestData();
        yield break;
    }

    private List<QuestData> GetQuestData()
    {
        List<QuestData> res = new List<QuestData>();

        StreamReader reader = new StreamReader(Application.dataPath + QuestDataFilePath);
        string str = reader.ReadToEnd();
        reader.Close();

        var data = JsonUtility.FromJson<QuestDatas>(str);
        if (data != null)
            res = data.quest_datas.ToList();

        return res;
    }
}

[System.Serializable]
public class QuestDatas
{
    public QuestData[] quest_datas;
}

[System.Serializable]
public class QuestData
{
    public int quest_type;
    public int quest_id;
    public int quest_sub_id;
    public string title;
    public string sub_title;
}
