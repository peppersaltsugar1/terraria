using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject bossHp;
    private List<GameObject> monsterList;
    private float spawnTime= 30;
    private List<bool> aliveMonster;
    private bool maxMonsterSpawn;
    public int maxMonsterNum = 10;
    private int index = 0;
    int randomPosition;
    private void Awake()
    {
        monsterList = new List<GameObject>();
        aliveMonster = new List<bool>();
        for (int i = 0; i< maxMonsterNum; i++)
        {
            monsterList.Add(Instantiate(monsterPrefab, transform.position, Quaternion.identity));
            aliveMonster.Add(false);
            monsterList[i].SetActive(false);
        }
        StartCoroutine("MonsterSpawn_co");

    }

    private void Update()
    {
        if (Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex] != null)
        {
            Debug.Log(Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex]);
            if (Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex].name == "BossSpawner")
            {
                if (Input.GetMouseButton(0))
                {
                    Debug.Log("보스스폰");
                    boss.SetActive(true);
                    bossHp.SetActive(true);
                    StopCoroutine("MonsterSpawn_co");
                }
            }
        }
    }

    private IEnumerator MonsterSpawn_co()
    {
        WaitForSeconds wfs = new WaitForSeconds(spawnTime);

        while (true)
        {
            for(int i = 0; i < aliveMonster.Count; i++)
            {
                if (aliveMonster[i] == true)
                {
                    maxMonsterSpawn = true;
                    continue;
                }
                if (aliveMonster[i] == false)
                {
                    maxMonsterSpawn = false;
                    break;
                }
            }
            if (!maxMonsterSpawn)
            {
                
                int randomNum = Random.Range(1,3);
                switch (randomNum)
                {
                    case 1: randomPosition = 1;break;
                    case 2: randomPosition = -1;break;

                }
                float positionX = Random.Range(transform.position.x+100, transform.position.x + 150)*randomPosition;
                float positionY = Random.Range(transform.position.y+30, transform.position.y + 50);
                Vector3 position = new Vector3(positionX, positionY , 0f);
                monsterList[index].transform.position = position;
                monsterList[index].SetActive(true);
                aliveMonster[index] = true;
                index++;
                if (index >= monsterList.Count)
                {
                    index = 0; //리스트 순서대로 생성
                }
            }
            yield return wfs;

        }
    }



}
