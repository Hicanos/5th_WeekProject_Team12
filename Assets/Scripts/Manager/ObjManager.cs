using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    //여기서 다뤄야 할 것들...
    //일단 유물 획득하면 문 열리게끔
    //문에 따라 다른 곳으로 갈 수 있게끔?
    //gotLegacy, gotAllObjects, cleartime을 넘겨줘야 함....
    //문에 들어가면 게임클리어 되면서 GameManager.Instance.ProcessingStageClear(bool gotLegacy, bool gotAllObjects, float clearTime, float timeLimit) 실행되게끔

    [SerializeField] private GameObject openDoorObject;
    [SerializeField] private GameObject closeDoorObject;
    [SerializeField] private GameObject interactionPopup; //E를 눌러 상호작용 하라고 알려주기
    [SerializeField] private GameObject refuseMessage;
    [SerializeField] private float timeLimit = 120f;

    public static ObjManager Instance { get; private set; }

    private bool isPlayerEncounter = false;
    private bool gotLegacy = false;
    private static bool gotAllObjects = false;
    private float clearTime = float.MaxValue;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        closeDoorObject.SetActive(true);
        openDoorObject.SetActive(false);
    }

    protected void Update()
    {
        if (isPlayerEncounter && Input.GetKeyDown(KeyCode.E))
        {
            if (!gotLegacy)
            {
                refuseMessage.SetActive(true);
                return;
            }
            clearTime = UIManager.Instance.StopTimer();
            GameManager.Instance.ProcessingStageClear(gotLegacy, gotAllObjects, clearTime, timeLimit);
        }
    }

    public void CollectLegacy(int LegacyID)
    {
        if (!DataManager.AquiredLegacy.Contains(LegacyID))
        {
            DataManager.AquiredLegacy.Add(LegacyID);
            Debug.Log($"유물 {DataManager.LegacyList[LegacyID]} 획득");
        }
        gotLegacy = true;
        closeDoorObject.SetActive(false);
        openDoorObject.SetActive(true);
    }

    public static bool CheckGetObject(bool check)
    {
        if(check == true)
        {
            Debug.Log("아이템 다 먹었어요");
            return gotAllObjects = true;
        }
        else
        {
            Debug.Log("아이템 있어요");
            return gotAllObjects = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerEncounter = true;
            interactionPopup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionPopup.SetActive(false); // 플레이어가 벗어나면 팝업 숨김
            isPlayerEncounter = false;
        }
    }
}
