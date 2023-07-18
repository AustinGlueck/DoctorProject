using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MerchantItem : MonoBehaviour
{
    public string itemName;
    public int amount;
    private int max;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textNumber;

    private void Start()
    {
        max = MerchantManager.Instance.GetMaxItemAmount();
        textName.text = itemName;
    }

    public void IncrementAmount()
    {
        amount++;
        amount = amount > max ? max : amount;
        textNumber.text = amount.ToString();
        MerchantManager.Instance.AccessOrder()[itemName] = amount;
    }

    public void DecrementAmount()
    {
        amount--;
        amount = amount < 0 ? 0 : amount;
        textNumber.text = amount.ToString();
        MerchantManager.Instance.AccessOrder()[itemName] = amount;
    }
}
