using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        //레이어 및 Tag 확인 - 중복 카운트 방지
        if(collision.gameObject.layer == LayerMask.NameToLayer("Cat")
            && collision.CompareTag("Player"))
        {
            GetItem();
        }
        //고양이가 아니면 아무런 효과 없음
    }

    public void GetItem()
    {
        Item.ChangeFishCount(-1);
        Destroy(gameObject);
    }
}
