using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlphaCollectorUI : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Image parentImg;
    [SerializeField]
    List<UnityEngine.UI.Image> childrenImg;
    [SerializeField]
    List<UnityEngine.UI.Text> childrenText;

    float bufAlpha = 1;

    NotificationObject<float> notification;

    bool IsChnaged
    {
        get
        {
            if (parentImg)
            {
                if (bufAlpha != parentImg.color.a)
                {
                    bufAlpha = parentImg.color.a;
                    return true;
                }
            }
            return false;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (parentImg == null)
        {
            parentImg = GetComponent<UnityEngine.UI.Image>();
            if (parentImg != null)
                bufAlpha = parentImg.color.a;
        }
        if (childrenImg == null)
            childrenImg = GetComponentsInChildren<UnityEngine.UI.Image>().ToList();
        if (childrenText == null)
            childrenText = GetComponentsInChildren<UnityEngine.UI.Text>().ToList();

        notification = new NotificationObject<float>(parentImg.color.a);
        notification.action += UpdateAlpha;
    }

    private void Start()
    {
        UpdateAlpha(parentImg.color.a);
    }

    void UpdateAlpha(float alpha)
    {
        bufAlpha = alpha;
        foreach (var child in childrenImg)
            child.color = new Color(child.color.r, child.color.g, child.color.b, bufAlpha);
        foreach (var child in childrenText)
            child.color = new Color(child.color.r, child.color.g, child.color.b, bufAlpha);
    }

    // Update is called once per frame
    void Update()
    {
        if (parentImg && IsChnaged)
        {
            bufAlpha = parentImg.color.a;
            foreach (var child in childrenImg)
                child.color = new Color(child.color.r, child.color.g, child.color.b,bufAlpha);
            foreach (var child in childrenText)
                child.color = new Color(child.color.r, child.color.g, child.color.b, bufAlpha); 
        }
    }
}
