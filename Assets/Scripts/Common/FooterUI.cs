using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FooterUI : MonoBehaviour
{
    [SerializeField]
    AnimationCollectorUI openAnimation;
    [SerializeField]
    List<UnityEngine.UI.Button> buttonList;
    [SerializeField]
    List<SceneName> sceneNameList;
    [SerializeField]
    MasterController masterController;

    public SceneName GetSceneName(int index)
    {
        return sceneNameList[index];
    }

    public IEnumerator Initialize()
    {
        yield return openAnimation.Initialize();
    }

    public IEnumerator Setup()
    {
        buttonList[0].onClick.AddListener(() => {
            StartCoroutine(masterController.ChangeScene(GetSceneName(0))); 
        });
        for (int i = 1; i < buttonList.Count; i++)
        {
            var sceneName = GetSceneName(i);
            buttonList[i].onClick.AddListener(() => { StartCoroutine(masterController.ChangeScene(sceneName)); });
        }

        yield return openAnimation.Setup();
    }

    public void OpneFooterUI()
    {
        openAnimation.StandbyAnimtion();
        openAnimation.Play();
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
