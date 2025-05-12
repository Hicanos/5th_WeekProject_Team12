using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//bestTime만 관리할 Manager 만들기 o
//Awake에서 Playerprefs를 사용해 각 스테이지의 베스트 클리어 타임을 받아옴. ㅇ

public class BestTimeManager : MonoBehaviour
{
    public static BestTimeManager Instance {get; private set;}
    private Dictionary<string, float> _bestTimeList = new()
    {
        {"Tutorial", float.MaxValue},
        {"Stage 1-1", float.MaxValue},
        {"Stage 1-2", float.MaxValue},
        {"Stage 1-3", float.MaxValue},
        {"Stage 2-1", float.MaxValue},
        {"Stage 2-2", float.MaxValue},
        {"Stage 2-3", float.MaxValue},
        {"Stage 3-1", float.MaxValue},
        {"Stage 3-2", float.MaxValue},
        {"Stage 3-3", float.MaxValue},
    };

    private void Awake()
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
        //C#의 foreach는 열거 중에 컬렉션의 구조가 바뀌면 즉시 예외를 던지도록 설계되어 있어요.
        //값을 수정하는 것도 "구조 변경"으로 간주되기 때문에, 반드시 복사본을 사용해야 합니다.
        List<string> keys = new List<string>(_bestTimeList.Keys);

        // PlayerPrefs를 통해 각 스테이지의 베스트 클리어타임을 받아옴 (초기화)
        foreach (string stageKey in keys)
        {
            float savedBestTime = PlayerPrefs.GetFloat(stageKey, float.MaxValue);

            _bestTimeList[stageKey] = savedBestTime;
        }
    }

    public void SavedBestTime(string stageKey, float currentTime)
    {
        //stageKey에 clearTime을 저장
        PlayerPrefs.SetFloat(stageKey, currentTime);

        //_bestTimeList 밸류값도 갱신
        _bestTimeList[stageKey] = currentTime;
    }

    public float GetBestTime(string stageKey)
    {
        float savedTime = _bestTimeList[stageKey];
        if(savedTime >= float.MaxValue)
        return 0;

        return savedTime;
    }
}
