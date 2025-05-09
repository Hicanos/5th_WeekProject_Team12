using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static Dictionary<int, string> LegacyList = new Dictionary<int, string>()
        {
            {1, "유물이름1"}, {2, "유물이름2"}, {3, "유물이름3"}, 
            {4, "유물이름4"}, {5, "유물이름5"}, {6, "유물이름6"}, 
            {7, "유물이름7"}, {8, "유물이름8"},
        };

    public static HashSet<int> AquiredLegacy = new HashSet<int>(); // 획득한 유물의 Key값만 저장, HashSet사용으로 중복제거

    public static int LegacyCount() //유물을 몇개 획득했는지 정리
    {
        return AquiredLegacy.Count;        
    }

}
