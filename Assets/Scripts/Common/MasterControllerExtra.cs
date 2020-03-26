using System.Collections;
using System.Collections.Generic;
using CommonSetting;
using UnityEngine;

public partial class MasterController : MonoBehaviour
{
    public static bool NonTitleScene(SceneName scene)
    {
        switch (scene)
        {
            case SceneName.Mypage:
                return true;
        }
        return false;
    }
}
