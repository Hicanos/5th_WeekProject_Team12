using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInfo
{
    public bool   CanEnter;
    public string StageName;
    public int    StarCount;

    public StageInfo(bool canEnter, string stageName, int starCount)
    {
        CanEnter = canEnter;
        StageName = stageName;
        StarCount = starCount;
    }
}

public class StageEnterCard : MonoBehaviour
{
    public Button btnEnter;
    public List<Image> starImages;
    public Image dim;

    public Sprite emptyStar;
    public Sprite star;

    private string _stageName;

    public void SetCard(StageInfo info)
    {
        _stageName = info.StageName;
        
        if (info.CanEnter == false)
        {
            dim.gameObject.SetActive(true);
            SetStarImage(0);
        }
        else
        {
            dim.gameObject.SetActive(false);
            SetStarImage(info.StarCount);
            btnEnter.onClick.AddListener(OnButtonPressed);
        }
    }

    private void SetStarImage(int starCount)
    {
        for (var i = 0; i < starImages.Count; i++)
        {
            Image starImage = starImages[i];

            starImage.sprite = i < starCount ? star : emptyStar;
        }
    }

    private void OnButtonPressed()
    {
        Debug.Log($"스테이지 불러오기 : {_stageName}");
        
        // MapManager.Instance.LoadStageByName(_stageName);
    }
}