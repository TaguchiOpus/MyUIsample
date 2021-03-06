﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CommonSetting
{
    public enum SceneName
    {
        [Description("―")]
        None = -1,
        [Description("マイページ")]
        Mypage = 0,
        [Description("クエスト")]
        Quest,
        [Description("強化")]
        Reinforce,
        [Description("パーティ")]
        Party,
        [Description("聖城")]
        Castle,
        [Description("召喚")]
        Summon
    }

    public enum ProfileKind
    {
        [Description("➖")]
        None = -1,
        [Description("なし")]
        Empty = 0,
        [Description("ID")]
        Master,
        [Description("登録")]
        Unique,
        [Description("名前")]
        Name,
        [Description("カテゴリ")]
        Category,
        [Description("レベル")]
        Level,
        [Description("希少度")]
        Rarity,
        [Description("武器種")]
        Job,
        [Description("属性")]
        Element,
    }

    public enum ObjectCategory
    {
        [Description("➖")]
        None = -1,
        [Description("なし")]
        Empty = 0,
        [Description("キャラ")]
        Character,
        [Description("武器")]
        Weapon,
        [Description("装飾品")]
        Amulet,
        [Description("ドラゴン")]
        Doragon,
        [Description("アイテム")]
        Item,
    }

    public static class CommonSetting
    {
        public static string GetName(this SceneName sceneName)
        {
            var member = sceneName.GetType().GetMember(sceneName.ToString());
            var attributes = member[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            var description = ((DescriptionAttribute)attributes[0]).Description;
            return description;
        }

        public static Color GetColor(this SceneName sceneName)
        {
            Color res = Color.white;

            switch (sceneName)
            {
                case SceneName.Quest:
                    res = ColorPalette.Quest;
                    break;
                case SceneName.Reinforce:
                    res = ColorPalette.Reinforce;
                    break;
                case SceneName.Party:
                    res = ColorPalette.Party;
                    break;
                case SceneName.Castle:
                    res = ColorPalette.Castle;
                    break;
                case SceneName.Summon:
                    res = ColorPalette.Summon;
                    break;
            }

            return res;
        }
    }
}
