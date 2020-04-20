using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CommonSetting;
using UniRx;
using UniRx.Async;
using System.Linq;

public class PartyScrollViewObjects : MonoBehaviour
{
    [SerializeField]
    SwapScrollRectProfileUI swapScroll;
    [SerializeField]
    ToggleGroup tabToggles;
    [SerializeField]
    ObjectCategory[] tabCategorizes;
    [SerializeField]
    Text countText;
    [SerializeField]
    Button closeBtn;

    #region Const
    const int HAVE_MAX = 300;
    #endregion

    ObjectCategory currentTab = ObjectCategory.Character;
    PartyModel modelParty;

    bool isInitialized = false;

    private void Start()
    {
    }

    public async void Open()
    {

    }

    public async void Release()
    {

    }

    public async void Initialize(PartyModel model)
    {
        modelParty = model;
        await swapScroll.Initialize(modelParty.GetDataList(currentTab));

        tabToggles.EnsureValidState();

        if (!isInitialized)
        {
            isInitialized = true;
            
            // タブを押したときの処理
            tabToggles.ObserveEveryValueChanged(group => group.ActiveToggles().FirstOrDefault())
                .Select(toggle =>
                {
                    return toggle.GetComponent<RectTransform>().GetSiblingIndex();
                })
                .Subscribe(index => ChangeToggle(index));
            closeBtn.OnClickAsObservable().First().Repeat().Subscribe(_ => Release());
            // アイコンを選択したときの処理
            swapScroll.ObservePick()
                .Subscribe(profile =>
                {
                    
                });
        }

        UpdateContents();

    }

    public async void Setup()
    {
    }

    private void ChangeToggle(int index)
    {
        currentTab = tabCategorizes[index];
        swapScroll.Source = modelParty.GetDataList(currentTab);
        swapScroll.Reposition();
        UpdateContents();
    }

    private void UpdateContents()
    {
        var data = modelParty.GetDataList(currentTab);

        if (countText)
            countText.text = string.Format("{0}/{1}", data.Count, HAVE_MAX);
    }
}
