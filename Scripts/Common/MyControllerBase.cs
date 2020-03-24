using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyControllerBase : MonoBehaviour
{

    protected bool isInitialized = false;

    protected virtual void OnStart()
    {

    }

    private IEnumerator Ready()
    {
        yield return new WaitForEndOfFrame();
        yield return OnInitialize();
        yield return OnSetup();
    }

    public virtual IEnumerator OnInitialize()
    {
        yield break;
    }

    public virtual IEnumerator OnSetup()
    {
        yield break;
    }

    public virtual IEnumerator OnRelease()
    {
        yield break;
    }


    public IEnumerator OpenAnimaiton()
    {
        yield break;
    }

    public IEnumerator CloseAnimation()
    {
        yield break;
    }

    protected virtual IEnumerator ChangeSceneProcess(SceneName sceneName)
    {
        yield break;
    }

    // Start is called before the first frame update
    void Start()
    {
        //OnStart();
        //StartCoroutine(Ready());
    }

    // Update is called once per frame
    void Update()
    {
    }
}
