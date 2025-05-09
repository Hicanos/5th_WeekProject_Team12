using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 넘어가도 유지
        }
        else
        {
            Destroy(gameObject);
        }
        //SoundManager.Instance.PlayBGM();
        //UIManager.Instance.InitUI();
        //MapManager.Instance.LoadMap("스테이지 선택창");
        //DataManager.Instance.LoadData();//점수와 같은 정보 호출
    }

}

