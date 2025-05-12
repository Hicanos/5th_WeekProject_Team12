using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option: MonoBehaviour
{
    public GameObject gameOption;
    public void openOption()
    {
        gameOption.SetActive(true);
    }
    public void closeOption()
    {
        gameOption.SetActive(false);
    }
}
