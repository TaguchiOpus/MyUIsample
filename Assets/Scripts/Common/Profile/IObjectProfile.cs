using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonSetting;

public class IObjectProfile
{
    public int _id;
    public int _master_id;
    public int _category;
    public int _rarity;
    public string _name;

    public int UniqueID { get { return _id; } }
    public int MasterID { get { return _master_id; } }
    public int Category { get { return _category; } }
    public int Rarity { get { return _rarity; } }
    public string Name { get { return _name; } }


    public IObjectProfile() { }
    public IObjectProfile(IObjectProfile source)
    {
        _id = source._id;
        _master_id = source._master_id;
        _category = source._category;
        _rarity = source._rarity;
        _name = source._name;
    }
    public IObjectProfile(int id,int master,int category,int rarity,string name)
    {
        _id = id;
        _master_id = master;
        _category = category;
        _rarity = rarity;
        _name = name;
    }

    public virtual int GetInt(ProfileKind kind)
    {
        int res = 0;

        switch (kind)
        {
            case ProfileKind.Unique:
                res = _id;break;
            case ProfileKind.Master:
                res = _master_id; break;
            case ProfileKind.Category:
                res = _category; break;
            case ProfileKind.Rarity:
                res = _rarity; break;
        }
        return res;
    }

    public virtual string GetString(ProfileKind kind)
    {
        string res = string.Empty;
        switch (kind)
        {
            case ProfileKind.Name:
                res = _name; break;
        }
        return res;
    }
}
