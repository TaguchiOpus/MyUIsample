using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

namespace DebugSetting
{
    public static class DebugSetting
    {
        public static void MyDebugLog(string text)
        {
            Debug.Log(string.Format("<color=magenta>{0}</color>", text));
        }
    }
}
