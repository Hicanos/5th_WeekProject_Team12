using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //다른 스크립트에서 사용하게 될 변수
    public int LastStageStarCount { get; private set; } = 0;

    

    void Awake()
    {
        Application.targetFrameRate = 60;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //저장된 정보 초기화
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();

    }
    

    public void ProcessingStageClear(bool gotLegacy, bool gotAllObjects, float clearTime, float timeLimit) //클리어시 별을 체크하는 메서드
    {
        int starCount = 0;
        if (gotLegacy) starCount++;
        if (gotAllObjects) starCount++;
        if (clearTime <= timeLimit) starCount++;
        
        //별 갯수를 저장할 변수
        LastStageStarCount = starCount;

        string stage = MapManager.Instance.CurrentStage;
        DataManager.Instance.UpdateStarCount(stage, starCount);
        UIManager.Instance.DisplayStars(starCount);
    }

    public bool IsStageUnlocked(string stageName)
    {
        int legacyCount = DataManager.LegacyCount();
        if(stageName == "SelectStage") return true;
        if(stageName == "Tutorial") return true;
        if(stageName == "Stage 1-1" && legacyCount >=1) return true;
        if(stageName == "Stage 1-2" && legacyCount >=2) return true;
        if(stageName == "Stage 1-3" && legacyCount >=3) return true;
        if(stageName == "Stage 2-1" && legacyCount >=4) return true;
        if(stageName == "Stage 2-2" && legacyCount >=5) return true;
        if(stageName == "Stage 2-3" && legacyCount >=6) return true;
        if(stageName == "Stage 3-1" && legacyCount >=7) return true;
        if(stageName == "Stage 3-2" && legacyCount >=8) return true;
        if(stageName == "Stage 3-3" && legacyCount >=9) return true;
        if(stageName == "EndingScene" && legacyCount >=10) return true;

        return false;
    }
}

