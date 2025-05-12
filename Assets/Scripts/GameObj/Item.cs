using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    //아이템/Trap: 뼈/생선 : 각각 Trigger가 개인지 고양이인지 확인 = 태그 변경
    //알맞은 충돌 체에서 점수가 올라감. 아니라면 파괴되지 않음
    //Gift: 유물. 획득하면 true. 각 스테이지 별로 3개를 모으면 도전과제 달성
    // : 데이터 매니저, 오브젝트 매니저와 연계
    // Gift 0번은 튜토리얼 아이템
    [Header("비교할 태그")] public string compareTag;
    public GameObject Player;
    int BonusCount = 0;

    public string GetTargetTagName()
    {
        return compareTag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetTargetTagName();
        if (collision.CompareTag(compareTag))
        {
            GetItem();

        }
    }
    void GetItem()
    {   // 아이템 획득 시 = BonusCount(점수)증가 후 파괴
        BonusCount++;
        Destroy(this);
    }
}
