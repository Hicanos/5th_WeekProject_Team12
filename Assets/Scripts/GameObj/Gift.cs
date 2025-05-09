using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    [SerializeField] public int LegacyID;

    void GetGift()
    {
        //획득한 유물의 ID(번호)가 DataManager의 AquiredLegacy에 없을 경우
        if (!DataManager.AquiredLegacy.Contains(LegacyID))
        {
            //LegacyID를 DataManager의 AquiredLegacy에 추가, 유물이 리스트에 잘 들어갔는지 확인.
            DataManager.AquiredLegacy.Add(LegacyID);
            Debug.Log($"유물획득: {ObjManager.LegacyList[LegacyID].ToString()}");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌체가 개나 고양이인지 확인(둘은 플레이어)
        if (collision.CompareTag("Dog") || collision.CompareTag("Cat"))
        {
            GetGift();
            Destroy(gameObject); //유물을 등록하고 오브젝트는 제거
        }
    }
}
