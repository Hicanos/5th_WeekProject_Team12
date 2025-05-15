﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //아이템: 뼈/생선 : 각각 Trigger가 개인지 고양이인지 확인 = Layer로 구분 
    //알맞은 충돌 체에서 점수가 올라감. 아니라면 파괴되지 않음
    //Gift: 유물. 획득하면 true. 각 스테이지 별로 3개를 모으면 도전과제 달성
    
    //여기서 총 아이템 갯수가 0 = 별 1개
    public GameObject FishObj;
    public GameObject BoneObj;
    public static int FishCount;
    public static int BoneCount;


    //이벤트 시스템으로 Fish와 Bone의 카운트 변화가 있을 때마다 호출
    public static event Action OnItemCountChanged;

    private void Awake()
    {
        //Fish, Bone 오브젝트 아래에 있는 자식들(Fish 클론들) 카운트)
        BoneCount = BoneObj.transform.childCount;
        FishCount = FishObj.transform.childCount;
        OnItemCountChanged?.Invoke();
    }

    public static void ChangeFishCount(int delta)
    {
        FishCount += delta;
        OnItemCountChanged?.Invoke();
    }

    public static void ChangeBoneCount(int delta)
    {
        BoneCount += delta;
        OnItemCountChanged?.Invoke();
    }

    // 아이템 획득 시 = 해당 아이템과 부합하는 개수 차감 후 파괴
    //    FishCount--;
    //    BoneCount--;
    //    Destroy(this);
}
