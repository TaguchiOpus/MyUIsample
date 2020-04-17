using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class PartyModel
{
	#region Const
	const string CharaDataFilePath = "/Data/Common/CharaData.json";
    const string WeaponDataFilePath = "/Data/Common/WeaponData.json";
    #endregion

    #region Field
    private List<CharacterProfile> charaDataList;
    private List<WeaponProfile> weaponDataList;
    #endregion

    #region Property
    public List<CharacterProfile> CharaDataList { get { return charaDataList; } }
    public List<WeaponProfile> WeaponDataList { get { return weaponDataList; } }
    #endregion

    public List<IObjectProfile> GetDataList(CommonSetting.ObjectCategory category)
    {
        List<IObjectProfile> res = new List<IObjectProfile>();
        switch (category)
        {
            case CommonSetting.ObjectCategory.Character:
                res = charaDataList.Cast<IObjectProfile>().ToList();break;
            case CommonSetting.ObjectCategory.Weapon:
                res = weaponDataList.Cast<IObjectProfile>().ToList(); break;

        }

        return res;
    }

    public IEnumerator Load()
    {
        if (charaDataList == null)
            yield return LoadCharaData();
        if (weaponDataList == null)
            yield return  LoadWeaponData();

        yield break;
    }

    private IEnumerator LoadCharaData()
    {
        List<CharacterProfile> res = new List<CharacterProfile>();

        StreamReader reader = new StreamReader(Application.dataPath + CharaDataFilePath);
        string str = reader.ReadToEnd();
        reader.Close();

        var data = JsonUtility.FromJson<CharaDatas>(str);
        if (data != null)
            res = data.chara_list.ToList();

        charaDataList = res;

        yield break;
    }

    private IEnumerator LoadWeaponData()
    {
        List<WeaponProfile> res = new List<WeaponProfile>();

        StreamReader reader = new StreamReader(Application.dataPath + WeaponDataFilePath);
        string str = reader.ReadToEnd();
        reader.Close();

        var data = JsonUtility.FromJson<WeaponDatas>(str);
        if (data != null)
            res = data.weapon_list.ToList();

        weaponDataList = res;

        yield break;
    }
}

[System.Serializable]
public class CharaDatas
{
    public CharacterProfile[] chara_list;
}

[System.Serializable]
public class WeaponDatas
{
    public WeaponProfile[] weapon_list;
}

