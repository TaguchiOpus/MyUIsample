using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorsCollectorUI : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Image parentImg;
    [SerializeField]
    List<UnityEngine.UI.Image> childrenImg;
    [SerializeField]
    List<UnityEngine.UI.Text> childrenText;

    Color bufColor = Color.white;

    bool IsChnaged { get
        {
            if (parentImg)
            {
                if(bufColor != parentImg.color)
                {
                    bufColor = parentImg.color;
                    return true;
                }
            }
            return false;
        } }

    // Start is called before the first frame update
    void Start()
    {
        if (parentImg == null)
        {
            parentImg = GetComponent<UnityEngine.UI.Image>();
            if (parentImg != null)
                bufColor = parentImg.color;
        }
        if (childrenImg == null)
            childrenImg = GetComponentsInChildren<UnityEngine.UI.Image>().ToList();
        if (childrenText == null)
            childrenText = GetComponentsInChildren<UnityEngine.UI.Text>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsChnaged)
        {
            foreach (var child in childrenImg)
                child.color = bufColor;
            foreach (var child in childrenText)
                child.color = bufColor;
        }
    }
}
