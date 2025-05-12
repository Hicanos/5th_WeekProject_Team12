using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }



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
        
        //UIManager.Instance.InitUI();
        //MapManager.Instance.LoadMap("스테이지 선택창");
        //DataManager.Instance.LoadData();//점수와 같은 정보 호출
    }

}

