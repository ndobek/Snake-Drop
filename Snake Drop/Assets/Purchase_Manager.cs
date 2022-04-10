using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchase_Manager : MonoBehaviour
{
    public void OnPurchase()
    {
        CloudOnce.CloudVariables.UnlimitedUndos = true;
        CloudOnce.Cloud.Storage.Save();
    }
}
