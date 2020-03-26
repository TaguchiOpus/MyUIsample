using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MypageController : MyControllerBase
{
    public MypageAnnounceManagerUI announceManager;
    public MypageAnimatinManager animatinManager;

    public GameObject infoWindow;

    public MissionAnnounceUI missionBtn;
    public CommandUI commandUI;

    public UnityEngine.UI.Button chagneViewBtn;

    public override IEnumerator OnInitialize()
    {
        yield return base.OnInitialize();
        yield return animatinManager.Initialize();
    }

    public override IEnumerator OnSetup()
    {
        yield return base.OnSetup();
        yield return animatinManager.Setup();
        yield return animatinManager.OpenAnimaiton();
    }

    public override IEnumerator OnRelease()
    {
        yield return animatinManager.ReleaseAnimation();
        yield break;
    }

    private IEnumerator SetupMypage()
    {

        yield break;
    }

}
