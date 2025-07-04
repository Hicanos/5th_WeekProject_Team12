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
         Debug.Log($"SetCard 호출됨: {info.StageName}");
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
        for (int i = 0; i < starImages.Count; i++)
        {
            Image starImage = starImages[i];

            starImage.sprite = i < starCount ? star : emptyStar;
        }
    }

    private void OnButtonPressed()
    {
            UIManager.Instance.CR.SetActive(false);
            UIManager.Instance.MainCanvas.SetActive(true);
      
         UIManager.Instance.tBtn.SetActive(false); 
       UIManager.Instance.bBtn.SetActive(true);
        UIManager.Instance.StartTimer();

        Debug.Log($"스테이지 불러오기 : {_stageName}");
        
         MapManager.Instance.LoadStageByName(_stageName);
    }
}