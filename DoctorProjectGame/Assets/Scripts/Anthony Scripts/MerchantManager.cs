using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantManager : MonoBehaviour
{
    public static MerchantManager Instance { get; private set; }

    [SerializeField] private Image merchantSprite;
    [SerializeField] private Canvas merchantCanvas;

    //Items
    private List<string> products = new List<string>();
    [SerializeField] private GameObject itemListParent;
    [SerializeField] private GameObject itemObjPrefab;
    private List<GameObject> itemObjectList = new List<GameObject>();
    [SerializeField] private int maxItemAmount = 10;
    public int GetMaxItemAmount() { return maxItemAmount; }

    //Order
    private Dictionary<string, int> myOrder = new Dictionary<string, int>();
    public Dictionary<string, int> AccessOrder() { return myOrder; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        //merchantSprite.gameObject.SetActive(false);
        //merchantCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        //For Testing
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            SetupMerchant();
        }*/
    }

    public void SetupMerchant()
    {
        //temp update products list in merchant
        List<string> list = new List<string>();
        list.Add("apple"); list.Add("banana"); list.Add("orange");
        products = list;
        
        //UI
        ToggleMerchantUI(true);
    }

    private void ToggleMerchantUI(bool b)
    {
        //merchantSprite.gameObject.SetActive(!merchantSprite.gameObject.activeSelf);
        merchantCanvas.gameObject.SetActive(b);

        if (b)
        {
            foreach (string itemName in products)
            {
                GameObject shopItem = Instantiate(itemObjPrefab, itemListParent.transform);
                shopItem.name = itemName;
                MerchantItem merchantItem= shopItem.GetComponent<MerchantItem>();
                merchantItem.itemName = itemName;
                itemObjectList.Add(shopItem);
                myOrder.Add(itemName, 0);
            }
        }
    }

    public void ResetMerchant()
    {
        products.Clear();
        foreach (GameObject item in itemObjectList)
        {
            Destroy(item);
        }
        itemObjectList.Clear();
        myOrder.Clear();

        //UI
        ToggleMerchantUI(false);
    }

    public void ConfirmPurchase()
    {
        AddToInventory();
        //ResetMerchant();
    }

    //
    private void AddToInventory()
    {
        //temp till link to inventory is made
        foreach (KeyValuePair<string, int> kvp in myOrder)
        {
            print(kvp.Value + " " + kvp.Key);
        }
    }
}
