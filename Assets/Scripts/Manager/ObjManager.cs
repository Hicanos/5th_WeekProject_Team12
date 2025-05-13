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
    [SerializeField] private float timeLimit = 120f;
    

    private bool isPlayerEncounter = false;
    private bool gotLegacy = false;
    private bool gotAllObjects = false;
    private float clearTime = float.MaxValue;

    private void Start()
    {
        closeDoorObject.SetActive(true);
        openDoorObject.SetActive(false);
    }

     protected void Update()
    {
        if(isPlayerEncounter && Input.GetKeyDown(KeyCode.E))
        {
            clearTime = UIManager.Instance.StopTimer();
            GameManager.Instance.ProcessingStageClear(gotLegacy, gotAllObjects, clearTime, timeLimit);
        }
    }

    public void GetLegacy()
    {
        gotLegacy = true;
        closeDoorObject.SetActive(false);
        openDoorObject.SetActive(true);
    }

    public void GetObject()
    {
        gotAllObjects = true;
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
