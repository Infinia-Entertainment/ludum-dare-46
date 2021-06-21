using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWaveMonsterUICell : MonoBehaviour
{
    [SerializeField] SpriteRenderer monsterSpriteRenderer;
    [SerializeField] SpriteRenderer elementTypeSpriteRenderer;
    [SerializeField] GameObject bossIconObj;

    public void InitializeCell(Sprite monsterSprite, Sprite elementTypeSprite,bool isABossMonster)
    {
        monsterSpriteRenderer.sprite = monsterSprite;
        elementTypeSpriteRenderer.sprite = elementTypeSprite;
        bossIconObj.SetActive(isABossMonster);
    }
}
