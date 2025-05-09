using UnityEngine;                         
using UnityEngine.SceneManagement;         
using UnityEngine.UI;                      

public class SceneTimer : MonoBehaviour
{
    public Text timerText;                 
    public float timeInScene = 300f;         // 현재 씬에서 경과한 시간 (초 단위)
    public bool isTiming = false;          // 시간 측정 활성화 여부를 나타내는 플래그

    
    
    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;     // 씬이 로드될 때 호출할 메서드 등록
        SceneManager.sceneUnloaded += OnSceneUnloaded; // 씬이 언로드될 때 호출할 메서드 등록
    }

   
    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;     // 씬 로드 이벤트 구독 해제
        SceneManager.sceneUnloaded -= OnSceneUnloaded; // 씬 언로드 이벤트 구독 해제
    }

    // 매 프레임마다 호출되는 메서드
    public void Update()
    {
        if (isTiming) // 시간 측정이 활성화된 경우
        {
            timeInScene -= Time.deltaTime;

            if (timerText != null) // 텍스트 UI가 연결되어 있다면
            {
                timerText.text = $"{timeInScene:F1}";
            }
        }
    }

    // 씬이 로드될 때 자동으로 호출되는 메서드
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        timeInScene = 300f;  // 씬 시작 시 시간 초기화
        isTiming = true;   // 시간 측정 시작
    }

    // 씬이 언로드될 때 자동으로 호출되는 메서드
    public void OnSceneUnloaded(Scene current)
    {
        isTiming = false;  // 시간 측정 종료
        timeInScene = 0f;  // 시간 초기화
    }
}
