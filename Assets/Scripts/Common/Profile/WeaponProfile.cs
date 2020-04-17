using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonSetting;

[System.Serializable]
public class WeaponProfile : IObjectProfile
{
    public int _lv;
    public int _job;

    public override int GetInt(ProfileKind kind)
    {
        int res = 0;

        switch (kind)
        {
            case ProfileKind.Unique:
                res = _id; break;
            case ProfileKind.Master:
                res = _master_id; break;
            case ProfileKind.Category:
                res = _category; break;
            case ProfileKind.Rarity:
                res = _rarity; break;
            case ProfileKind.Level:
                res = _lv; break;
            case ProfileKind.Job:
                res = _job; break;
        }
        return res;
    }
}
