using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CommonSetting;
using UniRx;

public class ProfileIconUI : MonoBehaviour
{
    [SerializeField]
    protected Button mainIcon;
    [SerializeField]
    protected Text mainText;
    [SerializeField]
    protected Image[] rareIcons;
    [SerializeField]
    protected Image[] gladeIcons;

    protected IObjectProfile source = new IObjectProfile();

    #region const
    #endregion

    #region Subscribe
    Subject<IObjectProfile> observePick = new Subject<IObjectProfile>();
    public Subject<IObjectProfile> ObservePick() { return observePick; }
    #endregion

    private void Start()
    {
        mainIcon.OnClickAsObservable().Subscribe(_ =>
        {
            observePick.OnNext(source);
        });
    }

    public void Setup(IObjectProfile profile)
    {
        if (profile == null)
            return;

        source = profile;
        UpdateContents();
        gameObject.SetActive(true);
    }

    public virtual void UpdateContents()
    {
        Color iconColor = Color.white;
        switch ((ObjectCategory)source.GetInt(ProfileKind.Category))
        {
            case ObjectCategory.Character:
                iconColor = Color.cyan;break;
            case ObjectCategory.Weapon:
                iconColor = Color.red; break;
            case ObjectCategory.Amulet:
                iconColor = Color.yellow; break;
            case ObjectCategory.Doragon:
                iconColor = Color.black; break;
        }
        mainIcon.image.color = iconColor;

        mainText.text = source.Name;

        int rare = source.GetInt(ProfileKind.Rarity);
        for(int i = 0; i < rareIcons.Length; i++)
        {
            rareIcons[i].gameObject.SetActive(i < rare);
        }
    }
}
