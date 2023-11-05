using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchase_Manager : MonoBehaviour/*, IStoreListener*/
{
    //private IStoreController controller;
    //private IExtensionProvider extensions;

    public void OnPurchase()
    {
        CloudOnce.CloudVariables.UnlimitedUndos = true;
        CloudOnce.Cloud.Storage.Save();
    }

    public void RemovePurchase()
    {
        CloudOnce.CloudVariables.UnlimitedUndos = false;
        CloudOnce.Cloud.Storage.Save();
    }

    ///// <summary>
    ///// Called when Unity IAP is ready to make purchases.
    ///// </summary>
    //public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    //{
    //    this.controller = controller;
    //    this.extensions = extensions;
    //    Debug.Log("IT WORRRKJSSDEIJF");
    //}

    ///// <summary>
    ///// Called when Unity IAP encounters an unrecoverable initialization error.
    /////
    ///// Note that this will not be called if Internet is unavailable; Unity IAP
    ///// will attempt initialization until it becomes available.
    ///// </summary>
    //public void OnInitializeFailed(InitializationFailureReason error)
    //{
    //}

    ///// <summary>
    ///// Called when a purchase completes.
    /////
    ///// May be called at any time after OnInitialized().
    ///// </summary>
    //public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    //{
    //    return PurchaseProcessingResult.Complete;
    //}

    ///// <summary>
    ///// Called when a purchase fails.
    ///// </summary>
    //public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    //{
    //}
}
