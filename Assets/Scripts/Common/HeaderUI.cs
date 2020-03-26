using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonSetting;

public class HeaderUI : MonoBehaviour
{
    [SerializeField]
    HeaderTitleUI titleUI;

    public bool IsPlay { get; set; }

    public  IEnumerator OnInitialize()
    {
        yield return titleUI.Initialize();
        yield break;
    }

    public IEnumerator OnSetup()
    {
        yield break;
    }

    public IEnumerator OpenAnimation()
    {
        IsPlay = true;
        
        titleUI.OpenTitle(MasterController.currentScene);
        yield return new WaitUntil(() => !titleUI.IsPlay);

        IsPlay = false;
        yield break;
    }

    public IEnumerator ReleaseAnimation()
    {
        IsPlay = true;
        titleUI.CloseTitle();
        yield return new WaitUntil(() => !titleUI.IsPlay);
        IsPlay = false;
        yield break;
    }

    public void ChangeTitle(SceneName sceneName)
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
