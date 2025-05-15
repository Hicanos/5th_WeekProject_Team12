using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catnip : Trap
{    private void OnTriggerEnter2D(Collider2D collision)
    {
        //레이어 및 Tag 확인 - 중복 카운트 방지
        if (collision.gameObject.layer == LayerMask.NameToLayer("Cat")
            && collision.CompareTag("Player"))
        {
            collision.transform.position = initialPosition; //플레이어의 position을 Respawner의 포지션으로 변경
        }
        //고양이가 아니면 아무런 효과 없음
    }
}
