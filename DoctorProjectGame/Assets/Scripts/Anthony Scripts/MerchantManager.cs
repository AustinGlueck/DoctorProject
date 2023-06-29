using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantManager : MonoBehaviour
{
    public static MerchantManager Instance { get; private set; }

    [SerializeField] private Merchant merchant;
    [SerializeField] private Image merchantSprite;
    [SerializeField] private GameObject merchantUI;
    private List<string> productsToSell = new List<string>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        //merchantSprite.gameObject.SetActive(false);
        merchantUI.SetActive(false);
    }

    public void SetupMerchant()
    {
        //temp update products list in merchant
        List<string> list = new List<string>();
        list.Add("apple"); list.Add("banana"); list.Add("orange");
        productsToSell = list;
        SetMerchantProducts(productsToSell);
        
        //UI
        ToggleMerchantUI(true);
    }

    private void SetMerchantProducts(List<string> products)
    {
        foreach (string str in products)
        {
            merchant.SetProducts(str);
        }
    }

    private void ToggleMerchantUI(bool b)
    {
        //merchantSprite.gameObject.SetActive(!merchantSprite.gameObject.activeSelf);
        merchantUI.SetActive(b);

        if (b)
        {
            foreach (string product in productsToSell)
            {
                print(product);
            }
        }
    }

    public void ResetMerchant()
    {
        //Merchant
        merchant.ClearProducts();

        //UI
        ToggleMerchantUI(false);
    }

    private void MenuNavigation()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {

        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {

        }
    }

    public void ButtonSelect()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {

        }
    }

    public void ButtonConfirm()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {

        }
    }
}
