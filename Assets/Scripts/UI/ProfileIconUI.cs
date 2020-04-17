using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CommonSetting;

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

    protected IObjectProfile souce = new IObjectProfile();

    #region const
    #endregion

    public void Setup(IObjectProfile profile)
    {
        if (profile == null)
            return;

        souce = profile;
        UpdateContents();
        gameObject.SetActive(true);
    }

    public virtual void UpdateContents()
    {
        Color iconColor = Color.white;
        switch ((ObjectCategory)souce.GetInt(ProfileKind.Category))
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

        mainText.text = souce.Name;

        int rare = souce.GetInt(ProfileKind.Rarity);
        for(int i = 0; i < rareIcons.Length; i++)
        {
            rareIcons[i].gameObject.SetActive(i < rare);
        }
    }
}
