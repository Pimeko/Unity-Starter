using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;

public class IAPData
{
    [SerializeField]
    string id;
    public string Id { get { return id; } }
    [SerializeField]
    ProductType type;
    public ProductType Type { get { return type; } }
}

public class IAPController : MonoBehaviour, IStoreListener
{
    [SerializeField, LabelText("All IAPs")]
    IAPVariableList allIAPs;
    [SerializeField]
    GameEventPurchaseEventArgs onPurchaseEvent;

    IStoreController storeController;
    IExtensionProvider storeExtensionProvider;

    bool isInitialized { get { return storeController != null && storeExtensionProvider != null; } }

    void Start()
    {
        InitializeProducts();
    }

    void InitializeProducts()
    {
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        foreach (IAPVariable iapVariable in allIAPs.Value)
            builder.AddProduct(iapVariable.Id, iapVariable.Type);

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        storeExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason _initializationFailureReason)
    {
        Debug.Log(_initializationFailureReason.ToString());
    }

    public void OnPurchaseFailed(Product _product, PurchaseFailureReason _purchaseFailureReason)
    {
        Debug.Log(_purchaseFailureReason.ToString());
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        #region security
        /*
        bool validPurchase = true;

        Security check: not working because no tangle
        #if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX

                validPurchase = false;

                CrossPlatformValidator crossPlatformValidator = new CrossPlatformValidator(GooglePlayTangle.Data(),
                    AppleTangle.Data(), Application.identifier);

                try
                {
                    // On Google Play, result has a single product ID.
                    // On Apple stores, receipts contain multiple products.
                    IPurchaseReceipt[] purchaseReceipts = crossPlatformValidator.Validate(_purchaseEventArgs.purchasedProduct.receipt);

                    // For informational purposes, we list the receipt(s)
                    foreach (IPurchaseReceipt purchaseReceipt in purchaseReceipts)
                    {
                        if (_purchaseEventArgs.purchasedProduct.definition.id == purchaseReceipt.productID)
                        {
                            validPurchase = true;
                            break;
                        }
                    }
                }
                catch (IAPSecurityException)
                {
                    Debug.Log("Invalid receipt, not unlocking content");
                }
        #endif

        if (validPurchase)
        */
        #endregion

        onPurchaseEvent?.Raise(e);
        allIAPs.GetById(e.purchasedProduct.definition.id).OnBuy?.Raise();

        return PurchaseProcessingResult.Complete;
    }

    public void OnBuyIAP(IAPVariable iapToBuy)
    {
        if (!isInitialized)
        {
            Debug.Log("IAP not initialized ...");
            return;
        }

        Product product = storeController.products.WithID(iapToBuy.Id);
        if (product == null || !product.availableToPurchase)
        {
            Debug.Log("Product not available to purchase ...");
            return;
        }

        storeController.InitiatePurchase(product);
    }

    public void OnRestore()
    {
        storeExtensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(_ => { });
    }
}