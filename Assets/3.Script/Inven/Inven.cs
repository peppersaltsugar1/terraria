using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inven : MonoBehaviour
{
    public static Inven Instance;
    [SerializeField] private GameObject inven;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject invenSlot;
    [SerializeField] private GameObject quickSlotPrefab;
    [SerializeField] private GameObject quick_Slot;

    bool activeInven;
    public List<Item> inventory;
    private int invenSlotMax = 40;
    int firstNullIndex = -1;
    public bool invenUse;
    public Item[] quickSlot;
    public bool[] useQuickSlot;
    public int useQuickSlotIndex = -1;
    private void Start()
    {
        inventory = new List<Item>(invenSlotMax);
        quickSlot = new Item[10];
        useQuickSlot = new bool[10];
        inven.SetActive(activeInven);
        for (int i = 0; i < invenSlotMax; i++)
        {
            inventory.Add(null);
            GameObject itemSlot = Instantiate(itemSlotPrefab);
            itemSlot.transform.SetParent(invenSlot.transform, false);
            itemSlot.GetComponent<ItemSlot>().InputIndex(i);
        }
        for(int i = 0; i < quickSlot.Length; i++)
        {
            quickSlot[i] = null;
            GameObject quickSlotBox = Instantiate(quickSlotPrefab);
            quickSlotBox.transform.SetParent(quick_Slot.transform, false);
            quickSlotBox.GetComponent<QuickSlot>().InputIndex(i);

        }
        

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            activeInven = !activeInven;
            inven.SetActive(activeInven);
        }
        InvenCheck();
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddInven(string itemName)
    {
        GameManager.Instance.item.TryGetValue(itemName, out GameObject item);
        Item inputItem = item.GetComponent<Item>();
        if(invenUse == true)
        {
            inventory[firstNullIndex]=inputItem;
        }
    }
    public void ChangeInven(int index,string itemName)
    {
        GameManager.Instance.item.TryGetValue(itemName, out GameObject item);
        Item inputItem = item.GetComponent<Item>();
        inventory[index] = inputItem;
    }
    public void ChangeQuickSlot(int index, string itemName)
    {
        GameManager.Instance.item.TryGetValue(itemName, out GameObject item);
        Item inputItem = item.GetComponent<Item>();
        quickSlot[index] = inputItem;
    }
    public void InvenCheck()
    {
        firstNullIndex = -1;
        for (int i = 0; i < invenSlotMax; i++)
        {
            if (inventory[i] == null)
            {
                firstNullIndex = i;
                invenUse = true;
                break;
            }
        }
        if (firstNullIndex == -1)
        {
            invenUse = false;
        }

    }
    public void UseQuickSlotIndexCheck()     
    {
        for(int i = 0; i < useQuickSlot.Length; i++)
        {
            if (useQuickSlot[i])
            {
                useQuickSlotIndex = i;
            }
        }
    }


}
