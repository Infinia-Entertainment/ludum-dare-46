using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWaveMonsterUICell : MonoBehaviour
{
    [SerializeField] SpriteRenderer monsterSpriteRenderer;
    [SerializeField] SpriteRenderer elementTypeSpriteRenderer;

    void Start()
    {

    }
    public void InitializeCell(Sprite monsterSprite, Sprite elementTypeSprite)
    {
        monsterSpriteRenderer.sprite = monsterSprite;
        elementTypeSpriteRenderer.sprite = elementTypeSprite;
    }
}
