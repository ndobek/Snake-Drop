using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable_If_Purchased : MonoBehaviour
{
    private void Update()
    {
        gameObject.SetActive(!CloudOnce.CloudVariables.UnlimitedUndos);
    }
}
