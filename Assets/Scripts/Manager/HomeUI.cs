using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HomeUI : BaseUI
{
    public StageSelect stageSel;

    Button startButton;
    Button exitButton;
    public override UIState GetUIState()
    {
        return UIState.Home;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        stageSel.stageSelect.SetActive(false);
        startButton = transform.Find("StartButton").GetComponent<Button>();
        exitButton = transform.Find("ExitButton").GetComponent<Button>();

        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }
    void OnClickStartButton()
    {
        Debug.Log("Start ¹öÆ° Å¬¸¯µÊ!");
        uiManager.OnClickStart();
        stageSel.ShowStageSelect();
    }

    void OnClickExitButton()
    {
        uiManager.OnClickExit();
    }
}
