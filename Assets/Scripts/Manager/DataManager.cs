using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public static HashSet<int> AquiredLegacy = new HashSet<int>(); // 획득한 유물의 Key값만 저장, HashSet사용으로 중복제거

    public static int LegacyCount() //유물을 몇개 획득했는지 정리
    {
        return AquiredLegacy.Count;
    }

    public static Dictionary<int, string> LegacyList = new Dictionary<int, string>()
    {
            {0, "야구공"},
            {1, "스카라베"}, {2, "황금 지팡이"}, {3, "호루스의 눈"},
            {4, "페르세포네의 석류"}, {5, "삼지창"}, {6, "헤르메스의 팬티"},
            {7, "메긴요르드"}, {8, "묠니르"}, {9, "궁그닐"},
    };



    

}
