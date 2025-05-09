using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    public GameObject stageSelect;

    public void ShowStageSelect()
    {
        
        stageSelect.SetActive(true);
    }

    public void HideStageSelect()
    {
        stageSelect.SetActive(false);
    }
}
