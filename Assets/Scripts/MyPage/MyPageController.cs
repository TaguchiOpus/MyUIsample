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
        yield return animatinManager.Setup();
        animatinManager.StandbyAnimation();
        gameObject.SetActive(true);
        yield return animatinManager.OpenAnimaiton();
    }

    public override IEnumerator OnRelease()
    {
        yield return animatinManager.ReleaseAnimation();
        gameObject.SetActive(false);
        yield break;
    }

    private IEnumerator SetupMypage()
    {

        yield break;
    }

}
