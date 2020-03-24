using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public enum SceneName
{
    None = -1,
    Mypage = 0,
    Quest,
    Reinforce,
    Party,
    Castle,
    Summon
}


public class MasterController : MonoBehaviour
{
    [SerializeField]
    FooterUI footerUI;
    [SerializeField]
    List<MyControllerBase> controllerList;
    [SerializeField]
    List<SceneName> sceneNameList;

    public static SceneName currentScene = SceneName.None;

    private bool isInitialized = false;

    private void Start()
    {
        
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        foreach (var controller in controllerList)
            yield return controller.OnInitialize();

        yield return footerUI.Initialize();
        yield return footerUI.Setup();

        yield return new WaitForEndOfFrame();

        footerUI.OpneFooterUI();

        yield return ChangeScene(SceneName.Mypage);

        yield break;
    }


    public IEnumerator ChangeScene(SceneName scene)
    {
        if (currentScene == scene)
            yield break;

        int currentIndex = sceneNameList.IndexOf(currentScene);
        if (currentScene != SceneName.None && currentIndex >= 0)
            yield return controllerList[currentIndex].OnRelease();

        currentScene = scene;
        int nextIndex = sceneNameList.IndexOf(scene);
        if (nextIndex < 0)
            yield break;

        yield return controllerList[nextIndex].OnSetup();
        yield break;
    }
}