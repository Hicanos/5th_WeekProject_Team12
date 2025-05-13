using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    public static ObjManager Instance { get; private set; }

    [Header("Door Objects")]
    [SerializeField] private GameObject openDoorObject;
    [SerializeField] private GameObject closeDoorObject;

    [Header("UI Elements")]
    [SerializeField] private GameObject interactionPopup;
    [SerializeField] private GameObject refuseMessage;

    [Header("Clear Condition")]
    [SerializeField] private float timeLimit = 120f;

    private bool isPlayerNear = false;
    private bool gotLegacy = false;
    private static bool gotAllObjects = false;
    private float clearTime = float.MaxValue;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
        {
            Instance = this;
           
        }
    }

    private void Start()
    {
        closeDoorObject.SetActive(true);
        openDoorObject.SetActive(false);
        refuseMessage.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!gotLegacy)
            {
                refuseMessage.SetActive(true);
                Invoke(nameof(HideRefuseMessage), 2f);
                return;
            }

            clearTime = UIManager.Instance.StopTimer();
            GameManager.Instance.ProcessingStageClear(gotLegacy, gotAllObjects, clearTime, timeLimit);
        }
    }

    private void HideRefuseMessage()
    {
        refuseMessage.SetActive(false);
    }

    public void CollectLegacy(int legacyID)
    {
        if (!DataManager.AquiredLegacy.Contains(legacyID))
        {
            DataManager.AquiredLegacy.Add(legacyID);
            Debug.Log($"유물 획득: {DataManager.LegacyList[legacyID]}");
        }

        gotLegacy = true;
        closeDoorObject.SetActive(false);
        openDoorObject.SetActive(true);
    }

    public static bool CheckGetObject(bool allCollected)
    {
        gotAllObjects = allCollected;
        Debug.Log(gotAllObjects ? "아이템 다 모았음!" : "아이템 아직 있음");
        return gotAllObjects;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            interactionPopup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactionPopup.SetActive(false);
        }
    }
}
