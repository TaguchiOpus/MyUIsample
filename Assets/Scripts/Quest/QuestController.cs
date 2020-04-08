using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSetting
{
    public enum QuestType
    {
        None = 0,
        Main,
        LimitedEvent,
        Element,
        Weekly,
    }
}

public class QuestController : MyControllerBase
{
    #region SerializeField
    [SerializeField]
    QuestAnimationManager openAnimaiton;
    [SerializeField]
    QuestView questView;
    #endregion

    QuestModel questModel = new QuestModel();


    public override IEnumerator OnInitialize()
    {
        yield break;
    }

    public override IEnumerator OnSetup()
    {
        yield return questModel.Load();
        questView.Setup(questModel.QuestDataList);
        yield return openAnimaiton.Initialize();
        yield return openAnimaiton.Setup();
        openAnimaiton.StandbyAnimaiton(true);
        gameObject.SetActive(true);

        yield return openAnimaiton.OpenAnimation();
    }

    public override IEnumerator OnRelease()
    {
        openAnimaiton.StandbyAnimaiton(false);
        yield return openAnimaiton.ReleaseAnimation();
        gameObject.SetActive(false);
    }
}
