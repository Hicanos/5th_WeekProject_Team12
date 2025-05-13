using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //레이어 및 Tag 확인 - 중복 카운트 방지
        if (collision.gameObject.layer == LayerMask.NameToLayer("Dog")
            && collision.CompareTag("Player"))
        {
            GetItem();
        }
        //개가 아니면 아무런 효과 없음
    }

    public void GetItem()
    {
        Item.ChangeBoneCount(-1);
        Destroy(gameObject);
    }
}
