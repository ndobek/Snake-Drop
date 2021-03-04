using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectOnPlatform : MonoBehaviour
{
    public GameObject objectToActivate;
    public bool ActiveOnStandalone;
    public bool ActiveOnAndroid;
    public bool ActiveOnIOS;

    public void Awake()
    {

#if UNITY_STANDALONE
        objectToActivate.SetActive(ActiveOnStandalone);

#elif UNITY_ANDROID
        objectToActivate.SetActive(ActiveOnAndroid);

#elif UNITY_IOS
        objectToActivate.SetActive(ActiveOnIOS);
#endif

    }

}