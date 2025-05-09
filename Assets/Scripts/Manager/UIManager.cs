using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum UIState
{
    Home,
    Game,
}

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    UIState currentState = UIState.Home;

    HomeUI homeUI = null;

    GameUI gameUI = null;


    private void Awake()
    {
        instance = this;

        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI?.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        if (gameUI != null)  // gameUI가 null이 아니라면
        {
            gameUI.Init(this);  // gameUI의 Init() 메서드를 호출하면서 UIManager 객체(this)를 전달
        }

        ChangeState(UIState.Home);
    }


    public void ChangeState(UIState state)//현재UI상태 변경
    {
        currentState = state;// 현재 UI 상태를 저장하는 변수
        homeUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
    }

    public void OnClickStart()//시작 버튼 클릭 시 호출되는 함수
    {
        ChangeState(UIState.Game);
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
