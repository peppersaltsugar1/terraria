using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpView : MonoBehaviour
{
    [SerializeField] private Slider slierHp;
    [SerializeField] private Boss boss;
    [SerializeField] private Image bossIcon;
    [SerializeField] private Sprite bossIcon2;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out slierHp);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(boss.monsterMaxHp);
        Debug.Log(boss.monsterCurrenHp);
        Debug.Log("°è»ê"+boss.monsterCurrenHp / boss.monsterMaxHp);
        slierHp.value = (float)boss.monsterCurrenHp / boss.monsterMaxHp;

        if (slierHp.value <= 0.5)
        {
            bossIcon.sprite = bossIcon2;
        }
    }
}
