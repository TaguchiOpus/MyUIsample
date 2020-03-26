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

    public static class CommonSetting
    {
        public static string GetName(this SceneName sceneName)
        {
            var member = sceneName.GetType().GetMember(sceneName.ToString());
            var attributes = member[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            var description = ((DescriptionAttribute)attributes[0]).Description;
            return description;
        }
    }
}