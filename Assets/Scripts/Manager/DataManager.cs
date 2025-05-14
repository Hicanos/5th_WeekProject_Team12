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
            LoadLegacyData(); //획득한 유물 데이터 불러오기
        }
    }
    //-----------------------------------------------------------------------------------------------------
    //별 저장 로직
    private const string StarKeyPrefix = "Stars_"; //PlayerPrefs 저장 키
    private Dictionary<string, int> _stageStars = new(); //스테이지별로 별갯수를 저장할 딕셔너리

    public void UpdateStarCount(string stageName, int starCount) //stage이름과 별 갯수를 매개변수로 하는 메서드
    {
        int saved = GetStars(stageName);                        //이미 저장된 별 개수 가져오기 (없으면 0 반환)
        if (starCount > saved)                                    //만약 전에 기록된 별의 갯수(기본값0)보다 많다면
        {
            _stageStars[stageName] = starCount;                     //더 많은 별의 갯수를 stageName key에 저장
            PlayerPrefs.SetInt(StarKeyPrefix + stageName, starCount);
            PlayerPrefs.Save();
        }
    }

    public int GetStars(string stageName)           //stage이름을 매개변수로 하는 메서드
    {
        if (_stageStars.ContainsKey(stageName))         //이미 불러온 적 있다면 캐시에서 반환
            return _stageStars[stageName];                 

        int saved = PlayerPrefs.GetInt(StarKeyPrefix + stageName, 0);   //저장된 기록이 없다면 기본값 0 저장
        _stageStars[stageName] = saved;                                 //비교를 위한 saved변수에 위에서 key-value 페어링된 값을 집어넣고
        return saved;                                                   //saved를 반환
    }
    //-----------------------------------------------------------------------------------------------------
    //유물 저장 로직
    private const string LegacyKeyPrefix = "Legacy_";
    public static HashSet<int> AquiredLegacy = new HashSet<int>(); // 획득한 유물의 Key값만 저장, HashSet사용으로 중복제거

    public static void AddLegacy(int legacyID)
    {
       
            if(!AquiredLegacy.Contains(legacyID)) //만약 HashSet에 그 legacyID가 포함되어 있지 않는다면
            {
                AquiredLegacy.Add(legacyID);          //LegacyID를 추가하고
                PlayerPrefs.SetInt(LegacyKeyPrefix + legacyID, 1); //bool값을 저장할 수 없기때문에 1로 true를 표시
                PlayerPrefs.Save();
            }
        
    }

    public static void LoadLegacyData()
    {
        AquiredLegacy.Clear(); //게임을 다시 실행할 때 획득유물 리스트를 초기화
        foreach (int id in LegacyList.Keys) //LegacyList에 있는 Keys들을 순회
        {
            if(PlayerPrefs.GetInt(LegacyKeyPrefix + id, 0) == 1) //PlayerPrefs에 저장된 id번의 밸류값이 1이라면
            {
                AquiredLegacy.Add(id);                           //획득유물 HashSet에 그 legacyid를 추가
            }
        }
    }


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
