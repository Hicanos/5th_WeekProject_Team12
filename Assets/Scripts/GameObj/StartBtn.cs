using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtn : MonoBehaviour
{
   
    public void OnClickExitStageSelect()
    {
        SceneManager.LoadScene("SelectStage");
        UIManager.Instance.tBtn.SetActive(true);
    }
}
