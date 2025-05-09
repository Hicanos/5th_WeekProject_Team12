using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : BaseUI
{
    public override UIState GetUIState()
    {
        return UIState.Game;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }
}
