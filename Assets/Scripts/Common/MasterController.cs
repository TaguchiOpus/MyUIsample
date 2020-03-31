using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using CommonSetting;




public partial class MasterController : MonoBehaviour
{
    [SerializeField]
    HeaderUI headerUI;
    [SerializeField]
    FooterUI footerUI;
    [SerializeField]
    SceneName firstScene = SceneName.None;  // 開始シーン(デバッグ用)
    [SerializeField]
    List<MyControllerBase> controllerList;
    [SerializeField]
    List<SceneName> sceneNameList;

    public static SceneName currentScene = SceneName.None;

    private bool isInitialized = false;
    private bool isChangeScene = false;

    private void Start()
    {
        
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        foreach (var controller in controllerList)
            yield return controller.OnInitialize();

        yield return new WaitForEndOfFrame();

        yield return footerUI.Initialize();
        yield return headerUI.OnInitialize();
        yield return footerUI.Setup();
        yield return headerUI.OnSetup();

        footerUI.OpneFooterUI();

        SceneName scene = firstScene;
        if (scene == SceneName.None)
            scene = SceneName.Mypage;

        yield return ChangeScene(scene);

        yield break;
    }


    public IEnumerator ChangeScene(SceneName scene)
    {
        if (currentScene == scene || isChangeScene)
            yield break;

        isChangeScene = true;

        StartCoroutine(headerUI.ReleaseAnimation());

        int currentIndex = sceneNameList.IndexOf(currentScene);
        if (currentScene != SceneName.None && currentIndex >= 0)
        {
            yield return controllerList[currentIndex].OnRelease();
        }

        yield return new WaitUntil(() => !headerUI.IsPlay);

        currentScene = scene;
        int nextIndex = sceneNameList.IndexOf(scene);
        if (nextIndex < 0)
            yield break;

        StartCoroutine(headerUI.OpenAnimation());

        yield return controllerList[nextIndex].OnSetup();

        yield return new WaitUntil(() => !headerUI.IsPlay);

        isChangeScene = false;
        yield break;
    }
}