using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    public static ObjManager Instance { get; private set; }

    [Header("Door Reference")]
    [SerializeField] private Door door;

    [Header("Clear Condition")]
    [SerializeField] private float timeLimit = 120f;
    public float TimeLimit => timeLimit;

    private bool gotLegacy = false;
    private static bool gotAllObjects = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        door.Close();
    }

    public void CollectLegacy(int legacyID)
    {
        if (!DataManager.AquiredLegacy.Contains(legacyID))
        {
            DataManager.AddLegacy(legacyID); // DataManager에서 저장로직을 구현했기 때문에 그 함수만 실행
            Debug.Log($"유물 획득: {DataManager.LegacyList[legacyID]}");
        }

        gotLegacy = true;
        door.Open();
    }

    public static bool CheckGetObject(bool allCollected)
    {
        gotAllObjects = allCollected;
        Debug.Log(gotAllObjects ? "아이템 다 모았음!" : "아이템 아직 있음");
        return gotAllObjects;
    }
    public static bool CGO()
    {
        return gotAllObjects;
    }
    public bool HasGotLegacy() => gotLegacy;
}
