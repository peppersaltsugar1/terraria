using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerControl playerControl;
    public GameObject[] itemPrefabs;
    public ItemSlot itemslot;
    public MoveSlot moveSlot;
    public MapGenerator mapGenerator;
    public SlotToolTip slotToolTip;
    public PlayerLevel playerLevel;

    public Dictionary<string, GameObject> item = new Dictionary<string, GameObject>();

    void Start()
    {
        // 각 타일 종류와 매칭될 아이템을 Dictionary에 등록
        item.Add("Soil", itemPrefabs[0]);
        item.Add("Stone", itemPrefabs[1]);
        item.Add("Copper", itemPrefabs[2]);
        item.Add("Iron", itemPrefabs[3]);
        item.Add("Gold", itemPrefabs[4]);
        item.Add("Tungsten", itemPrefabs[5]);
        item.Add("Orichalcum", itemPrefabs[6]);
        item.Add("CopperBar", itemPrefabs[7]);
        item.Add("IronBar", itemPrefabs[8]);
        item.Add("GoldBar", itemPrefabs[9]);
        item.Add("TungstenBar", itemPrefabs[10]);
        item.Add("OrichalcumBar", itemPrefabs[11]);
        item.Add("itemNull", itemPrefabs[12]);
        item.Add("Core", itemPrefabs[13]);
        item.Add("BossSpawner", itemPrefabs[14]);
        item.Add("BossCore", itemPrefabs[15]);



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
}
