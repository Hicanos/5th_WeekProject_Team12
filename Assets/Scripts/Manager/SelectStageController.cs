using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectStageController : MonoBehaviour
{
    private List<StageEnterCard> _stageCardList;

    [Header("Refuse Message")]
    [SerializeField] private GameObject refuseMessage;

    private List<string> _stageNames = new()
    {
        "Tutorial",
        "Stage 1-1",
        "Stage 1-2",
        "Stage 1-3",
        "Stage 2-1",
        "Stage 2-2",
        "Stage 2-3",
        "Stage 3-1",
        "Stage 3-2",
        "Stage 3-3",
    };



    private void Awake()
    {
        _stageCardList = transform.GetComponentsInChildren<StageEnterCard>().ToList();
    }

    private void Start()
    {
        List<StageInfo> stageInfoList = new();

        for (int i = 0; i < _stageNames.Count; i++)
        {
            string stageName = _stageNames[i];
            bool canEnter = GameManager.Instance.IsStageUnlocked(stageName);
            int starCount = DataManager.Instance.GetStars(stageName);

            stageInfoList.Add(new StageInfo(canEnter, stageName, starCount));
        }

        for (int i = 0; i < _stageCardList.Count; i++)
        {
            StageInfo info = stageInfoList[i];
            StageEnterCard card = _stageCardList[i];
            
            card.SetCard(info);
        }
    }

    private void ShowRefuseMessage()
    {
        if (refuseMessage != null)
        {
            refuseMessage.SetActive(true);
            Invoke(nameof(HideRefuseMessage), 2f);
        }
    }
    private void HideRefuseMessage()
    {
        if (refuseMessage != null)
        refuseMessage.SetActive(false);
    }

    private int LegacyIDList(string stageName)
    {
        switch (stageName)
        {
            case "Tutorial": return 0;
            case "Stage 1-1": return 1;          
            case "Stage 1-2": return 2;
            case "Stage 1-3": return 3;
            case "Stage 2-1": return 4;
            case "Stage 2-2": return 5;
            case "Stage 2-3": return 6;
            case "Stage 3-1": return 7;
            case "Stage 3-2": return 8;
            case "Stage 3-3": return 9;
            default: return -1;
        }
    }
}