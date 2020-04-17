using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UniRx;

public class SwapScrollRectProfileUI : MonoBehaviour
{
    [SerializeField]
    CustomScrollRect scrollRect;

    SwapTypeVerticalScrollView swapScroll = new SwapTypeVerticalScrollView();
    List<IObjectProfile> source = new List<IObjectProfile>();

    public List<IObjectProfile> Source 
    { 
        get { return source; }
        set
        {
            source = value;
            swapScroll.DataCount = GetDataCount();
        }
    }

    public void Reposition()
    {
        swapScroll.Reset();
    }

    public IEnumerator Initialize(List<IObjectProfile> data)
    {
        source = data;
        int dataCount = GetDataCount();
        swapScroll.Inisialize(scrollRect, dataCount, UpdateItem);
        yield break;
    }

    private int GetDataCount()
    {
        int res = 0;
        var collection = scrollRect.GetChild().gameObject.GetComponent<ProfileIconCollectionUI>();
        if (collection != null)
        {
            int itemCount = collection.ProfileIcons.Length;
            res = source.Count / itemCount;
            if (source.Count % itemCount > 0)
                res++;
        }

        return res;
    }
    private void Start()
    {
        //swapScroll.Inisialize(scrollRect, source.Count, UpdateItem);
    }

    private void UpdateItem(RectTransform target,int index)
    {
        var collection = target.GetComponent<ProfileIconCollectionUI>();
        if(collection != null)
        {
            int startIndex = index * collection.ProfileIcons.Length;
            if (startIndex < source.Count)
            {
                for (int i = 0; i < collection.ProfileIcons.Length; i++)
                {
                    int sourceIndex = startIndex + i;
                    if (source.Count > sourceIndex)
                        collection.ProfileIcons[i].Setup(source[sourceIndex]);
                    else
                        collection.ProfileIcons[i].gameObject.SetActive(false);
                }
                collection.gameObject.SetActive(true);
            }
            else
            {
                collection.gameObject.SetActive(false);
            }
        }
    }

    
}
