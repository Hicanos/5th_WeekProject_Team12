using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static HashSet<int> AquiredLegacy = new HashSet<int>(); // 획득한 유물의 Key값만 저장, HashSet사용으로 중복제거

    public static int LegacyCount() //유물을 몇개 획득했는지 정리
    {
        return AquiredLegacy.Count;        
    }

}
