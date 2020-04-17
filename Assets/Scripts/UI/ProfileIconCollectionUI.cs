using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProfileIconCollectionUI : MonoBehaviour
{
    [SerializeField]
    //GameObject[] children;

    ProfileIconUI[] profileIcons;

    public ProfileIconUI[] ProfileIcons { get { return profileIcons; } }

    private void Start()
    {
        //profileIcons = children.Select(x => x.GetComponent<ProfileIconUI>()).ToArray();
    }

    private void Update()
    {
      
    }
}
