using System.Collections;
using System.Collections.Generic;
using CommonSetting;
using UnityEngine;

public class HeaderTitleUI : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Image titleImg;
    [SerializeField]
    UnityEngine.UI.Text titleText;
    [SerializeField]
    AnimationCollectorUI animation;

    public bool IsPlay { get { return animation.IsPlay; } }

    public IEnumerator Initialize()
    {
        yield return animation.Initialize();
    }

    public void SetTitle(SceneName sceneName)
    {
        switch (sceneName)
        {
            case SceneName.Quest:
                titleImg.color = ColorPalette.Quest;
                break;
            case SceneName.Reinforce:
                titleImg.color = ColorPalette.Reinforce;
                break;
            case SceneName.Party:
                titleImg.color = ColorPalette.Party;
                break;
            case SceneName.Castle:
                titleImg.color = ColorPalette.Castle;
                break;
            case SceneName.Summon:
                titleImg.color = ColorPalette.Summon;
                break;
            default:
                titleImg.gameObject.SetActive(false);
                return;
        }
        titleText.text = sceneName.GetName();
        titleImg.gameObject.SetActive(true);
    }

    public void OpenTitle(SceneName sceneName)
    {
        if (MasterController.NonTitleScene(sceneName))
            return;

        animation.ClearOnFinished();
        animation.StandbyAnimtion(true);
        SetTitle(sceneName);
        if (!animation.gameObject.activeSelf)
            return;
        animation.Play();
    }

    public void CloseTitle()
    {
        if (!animation.gameObject.activeSelf)
            return;

        animation.SetOnFinished(() => {
            titleImg.gameObject.SetActive(false);
            });
        animation.StandbyAnimtion(false);
        animation.Play();
    }


    // Start is called before the first frame update
    void Awake()
    {
        if (animation == null)
            animation = GetComponent<AnimationCollectorUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
