using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌체의 레이어 확인
        int charLayer = collision.gameObject.layer;

        //레이어 및 Tag 확인 - 중복 카운트 방지
        if(charLayer == LayerMask.NameToLayer("Cat")&& collision.CompareTag("Player"))
        {
            GetItem();
        }
        //고양이가 아니면 아무런 효과 없음
    }

    public void GetItem()
    {
        Item.FishCount--;
        Destroy(gameObject);
    }
}
