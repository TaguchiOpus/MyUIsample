using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CommonSetting;
using UniRx;

public class PartyListViewObjects : MonoBehaviour
{
    [SerializeField]
    SwapScrollRectProfileUI swapScroll;
    [SerializeField]
    ToggleGroup tabToggles;
    [SerializeField]
    ObjectCategory[] tabCategorizes;
    [SerializeField]
    Text countText;

    ObjectCategory currentTab = ObjectCategory.Character;
    PartyModel modelParty;

    public IEnumerator Initialize(PartyModel model)
    {
        modelParty = model;
        yield return swapScroll.Initialize(modelParty.GetDataList(currentTab));
        yield break;
    }

    public IEnumerator Setup()
    {
        
        yield break;
    }

    private void ChnageToggle(int index)
    {

    }

    private void UpdateContents()
    {
        swapScroll.Source = modelParty.GetDataList(currentTab);
        swapScroll.Reposition();


    }
}
