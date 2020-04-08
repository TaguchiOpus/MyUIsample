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
        if (MasterController.NonTitleScene(sceneName))
            return;

        titleImg.color = sceneName.GetColor();
        titleText.text = sceneName.GetName();
        titleImg.gameObject.SetActive(true);
    }

    public void OpenTitle(SceneName sceneName)
    {
        if (MasterController.NonTitleScene(sceneName))
            return;

        animation.ClearOnFinished();
        animation.StandbyAnimation(true);
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
        animation.StandbyAnimation(false);
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
