using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MypageAnnounceManagerUI : MonoBehaviour
{
    public CollectiveAnimationUI collectiveAnimation;
    public MyPageController mypageController;

    public void Initialize()
    {
        //collectiveAnimation.Initialize();
        collectiveAnimation.SetOnFinished(() =>
        {
        });
    }
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
