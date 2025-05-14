using UnityEngine;
using UnityEngine.UI;

public class StageSelectButton : MonoBehaviour
{
    [SerializeField] private string stageName;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (MapManager.Instance != null)
            {
                MapManager.Instance.LoadStage(stageName);
            }
        });
    }
}
