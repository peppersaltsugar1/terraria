using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    private Vector2 mousePosition;
    [SerializeField] private Tilemap blockTileMap;

    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject bossHp;
    private float lastClickTime = 0f;
    public float clickCooldown = 0.5f;
    float distanceThreshold = 10f;





    void Update()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = blockTileMap.WorldToCell(mousePos);
        Vector3 playerPosition = GameManager.Instance.playerControl.transform.position;
        Vector3 distance = cellPos - playerPosition;
        if (GameManager.Instance.playerControl.isAttack == false)
        {
            if (distance.magnitude < distanceThreshold)
            {
                // 거리가 10보다 작은 경우 실행할 코드 작성
            

                if (Input.GetMouseButton(0) && Time.time > lastClickTime + clickCooldown)
                {

                    lastClickTime = Time.time; // 마지막 클릭 시간 기록
                    TileBase tile = blockTileMap.GetTile(cellPos);
                    if (tile != null)
                    {
                        //플레이어 무기 레벨에따라 캘수있는 블럭을 제한시킴
                        if (GameManager.Instance.playerControl.weaponLevel == 1)
                        {
                            switch (tile.name)
                            {
                                case "SoilRuleTile":
                                    GameManager.Instance.item.TryGetValue("Soil", out GameObject Soil);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Soil, cellPos, Quaternion.identity);
                                    break;

                                case "StoneRuleTile":
                                    GameManager.Instance.item.TryGetValue("Stone", out GameObject Stone);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Stone, cellPos, Quaternion.identity);
                                    break;

                            }
                        }
                        if (GameManager.Instance.playerControl.weaponLevel == 2)
                        {
                            switch (tile.name)
                            {
                                case "SoilRuleTile":
                                    GameManager.Instance.item.TryGetValue("Soil", out GameObject Soil);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Soil, cellPos, Quaternion.identity);
                                    break;

                                case "StoneRuleTile":
                                    GameManager.Instance.item.TryGetValue("Stone", out GameObject Stone);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Stone, cellPos, Quaternion.identity);
                                    break;
                                case "CopperRuleTile":
                                    GameManager.Instance.item.TryGetValue("Copper", out GameObject Copper);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Copper, cellPos, Quaternion.identity);
                                    break;
                            }
                        }
                        if (GameManager.Instance.playerControl.weaponLevel == 3)
                        {
                            switch (tile.name)
                            {
                                case "SoilRuleTile":
                                    GameManager.Instance.item.TryGetValue("Soil", out GameObject Soil);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Soil, cellPos, Quaternion.identity);
                                    break;

                                case "StoneRuleTile":
                                    GameManager.Instance.item.TryGetValue("Stone", out GameObject Stone);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Stone, cellPos, Quaternion.identity);
                                    break;
                                case "CopperRuleTile":
                                    GameManager.Instance.item.TryGetValue("Copper", out GameObject Copper);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Copper, cellPos, Quaternion.identity);
                                    break;
                                case "IronRuleTile":
                                    GameManager.Instance.item.TryGetValue("Iron", out GameObject Iron);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Iron, cellPos, Quaternion.identity);
                                    break;
                            }
                        }
                        if (GameManager.Instance.playerControl.weaponLevel == 4)
                        {
                            switch (tile.name)
                            {
                                case "SoilRuleTile":
                                    GameManager.Instance.item.TryGetValue("Soil", out GameObject Soil);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Soil, cellPos, Quaternion.identity);
                                    break;

                                case "StoneRuleTile":
                                    GameManager.Instance.item.TryGetValue("Stone", out GameObject Stone);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Stone, cellPos, Quaternion.identity);
                                    break;
                                case "CopperRuleTile":
                                    GameManager.Instance.item.TryGetValue("Copper", out GameObject Copper);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Copper, cellPos, Quaternion.identity);
                                    break;
                                case "IronRuleTile":
                                    GameManager.Instance.item.TryGetValue("Iron", out GameObject Iron);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Iron, cellPos, Quaternion.identity);
                                    break;
                                case "TungstenRuleTile":
                                    GameManager.Instance.item.TryGetValue("Tungsten", out GameObject Tungsten);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Tungsten, cellPos, Quaternion.identity);
                                    break;
                            }
                        }
                        if (GameManager.Instance.playerControl.weaponLevel == 5)
                        {
                            switch (tile.name)
                            {
                                case "SoilRuleTile":
                                    GameManager.Instance.item.TryGetValue("Soil", out GameObject Soil);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Soil, cellPos, Quaternion.identity);
                                    break;
                                case "StoneRuleTile":
                                    GameManager.Instance.item.TryGetValue("Stone", out GameObject Stone);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Stone, cellPos, Quaternion.identity);
                                    break;
                                case "CopperRuleTile":
                                    GameManager.Instance.item.TryGetValue("Copper", out GameObject Copper);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Copper, cellPos, Quaternion.identity);
                                    break;
                                case "IronRuleTile":
                                    GameManager.Instance.item.TryGetValue("Iron", out GameObject Iron);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Iron, cellPos, Quaternion.identity);
                                    break;
                                case "GoldRuleTille":
                                    GameManager.Instance.item.TryGetValue("Gold", out GameObject Gold);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Gold, cellPos, Quaternion.identity);
                                    break;
                                case "TungstenRuleTile":
                                    GameManager.Instance.item.TryGetValue("Tungsten", out GameObject Tungsten);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Tungsten, cellPos, Quaternion.identity);
                                    break;
                            }
                        }
                        if (GameManager.Instance.playerControl.weaponLevel == 6)
                        {
                            switch (tile.name)
                            {
                                case "SoilRuleTile":
                                    GameManager.Instance.item.TryGetValue("Soil", out GameObject Soil);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Soil, cellPos, Quaternion.identity);
                                    break;
                                case "StoneRuleTile":
                                    GameManager.Instance.item.TryGetValue("Stone", out GameObject Stone);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Stone, cellPos, Quaternion.identity);
                                    break;
                                case "CopperRuleTile":
                                    GameManager.Instance.item.TryGetValue("Copper", out GameObject Copper);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Copper, cellPos, Quaternion.identity);
                                    break;
                                case "IronRuleTile":
                                    GameManager.Instance.item.TryGetValue("Iron", out GameObject Iron);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Iron, cellPos, Quaternion.identity);
                                    break;
                                case "GoldRuleTille":
                                    GameManager.Instance.item.TryGetValue("Gold", out GameObject Gold);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Gold, cellPos, Quaternion.identity);
                                    break;
                                case "TungstenRuleTile":
                                    GameManager.Instance.item.TryGetValue("Tungsten", out GameObject Tungsten);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Tungsten, cellPos, Quaternion.identity);
                                    break;
                                case "OrichalcumRuleTIle":
                                    GameManager.Instance.item.TryGetValue("Orichalcum", out GameObject Orichalcum);
                                    blockTileMap.SetTile(cellPos, null);
                                    Instantiate(Orichalcum, cellPos, Quaternion.identity);
                                    break;

                            }
                        }
                    }
                    else if (tile == null)
                    {
                        if (Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex] != null)
                        {

                            if (tile == null && Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex].name == "Soil")
                            {
                                blockTileMap.SetTile(cellPos, GameManager.Instance.mapGenerator.ruleSoil);
                                Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex] = null;
                            }
                            else if (tile == null && Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex].name == "Stone")
                            {
                                blockTileMap.SetTile(cellPos, GameManager.Instance.mapGenerator.ruleStone);
                                Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex] = null;
                            }

                        }
                        else
                        {
                            Debug.Log("퀵슬롯비었음");
                        }
                    }
                }
            }
        }
    }
}
