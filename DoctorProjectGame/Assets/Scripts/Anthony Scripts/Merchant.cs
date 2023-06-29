using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    private Dictionary<string, int> products = new Dictionary<string, int>();

    public void SetProducts(string product)
    {
        products.Add(product, 0);
    }

    public void ClearProducts() { products.Clear(); }

    public void SetProductAmounts(string product, int amount)
    {
        if (products.ContainsKey(product))
        {
            products[product] = amount;
        }
    }

    public void MakePurchase()
    {

    }

    public void ConfirmPurchase()
    {

    }
}
