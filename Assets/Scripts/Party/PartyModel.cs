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
    const string AmuletDataFilePath = "/Data/Common/AmuletData.json";
    const string DragonDataFilePath = "/Data/Common/DragonData.json";
    #endregion

    #region Field
    private List<CharacterProfile> charaDataList;
    private List<WeaponProfile> weaponDataList;
    private List<AmuletProfile> amuletDataList;
    private List<DragonProfile> dragonDataList;
    #endregion

    #region Property
    public List<CharacterProfile> CharaDataList { get { return charaDataList; } }
    public List<WeaponProfile> WeaponDataList { get { return weaponDataList; } }
    public List<DragonProfile> DoragonDataList { get { return dragonDataList; } }
    public List<AmuletProfile> AmuletDataList { get { return amuletDataList; } }
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
            case CommonSetting.ObjectCategory.Amulet:
                res = amuletDataList.Cast<IObjectProfile>().ToList(); break;
            case CommonSetting.ObjectCategory.Doragon:
                res = dragonDataList.Cast<IObjectProfile>().ToList(); break;
        }

        return res;
    }

    public IEnumerator Load()
    {
        if (charaDataList == null)
            yield return LoadCharaData();
        if (weaponDataList == null)
            yield return  LoadWeaponData();
        if (amuletDataList == null)
            yield return LoadAmuletData();
        if (dragonDataList == null)
            yield return LoadDragonData();

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

    private IEnumerator LoadAmuletData()
    {
        List<AmuletProfile> res = new List<AmuletProfile>();

        StreamReader reader = new StreamReader(Application.dataPath + AmuletDataFilePath);
        string str = reader.ReadToEnd();
        reader.Close();

        var data = JsonUtility.FromJson<AmuletDatas>(str);
        if (data != null)
            res = data.amulet_list.ToList();

        amuletDataList = res;

        yield break;
    }

    private IEnumerator LoadDragonData()
    {
        List<DragonProfile> res = new List<DragonProfile>();

        StreamReader reader = new StreamReader(Application.dataPath + DragonDataFilePath);
        string str = reader.ReadToEnd();
        reader.Close();

        var data = JsonUtility.FromJson<DragonDatas>(str);
        if (data != null)
            res = data.dragon_list.ToList();

        dragonDataList = res;

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

[System.Serializable]
public class AmuletDatas
{
    public AmuletProfile[] amulet_list;
}

[System.Serializable]
public class DragonDatas
{
    public DragonProfile[] dragon_list;
}

