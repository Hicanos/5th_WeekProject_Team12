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
        if (gameUI != null)  // gameUI�� null�� �ƴ϶��
        {
            gameUI.Init(this);  // gameUI�� Init() �޼��带 ȣ���ϸ鼭 UIManager ��ü(this)�� ����
        }

        ChangeState(UIState.Home);
    }


    public void ChangeState(UIState state)//����UI���� ����
    {
        currentState = state;// ���� UI ���¸� �����ϴ� ����
        homeUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
    }

    public void OnClickStart()//���� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    {
        ChangeState(UIState.Game);
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }
}
