using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Back : MonoBehaviour
{
    public void OnClickExitStageSelect()
    {
        //Debug.Log("Back ¹öÆ° Å¬¸¯µÊ!");
        SceneManager.LoadScene("StageSelect");
    }
}
