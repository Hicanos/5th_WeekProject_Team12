using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door Objects")]
    [SerializeField] private GameObject openDoorObject;
    [SerializeField] private GameObject closeDoorObject;

    [Header("UI Elements")]
    [SerializeField] private GameObject interactionPopup;
    [SerializeField] private GameObject refuseMessage;

    private bool isPlayerNear = false;
    private void Awake()
    {
        openDoorObject = GameObject.FindGameObjectWithTag("OpenDoor");
        closeDoorObject = GameObject.FindGameObjectWithTag("ClosedDoor");
        interactionPopup = GameObject.FindGameObjectWithTag("inter");
        refuseMessage = GameObject.FindGameObjectWithTag("refu ");
    }
    private void Start()
    {
        Close();

        if (refuseMessage != null)
            refuseMessage.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!ObjManager.Instance.HasGotLegacy())
            {
                if (refuseMessage != null)
                {
                    refuseMessage.SetActive(true);
                    Invoke(nameof(HideRefuseMessage), 2f);
                }
                return;
            }

            float clearTime = UIManager.Instance.StopTimer();
            GameManager.Instance.ProcessingStageClear(
                gotLegacy: true,
                gotAllObjects: ObjManager.CGO(),
                clearTime: clearTime,
                timeLimit: ObjManager.Instance.TimeLimit
            );
        }
    }

    private void HideRefuseMessage()
    {
        if (refuseMessage != null)
            refuseMessage.SetActive(false);
    }

    public void Open()
    {
        closeDoorObject.SetActive(false);
        openDoorObject.SetActive(true);
    }

    public void Close()
    {
        closeDoorObject.SetActive(true);
        openDoorObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("접근");
            isPlayerNear = true;
            if (interactionPopup != null)
                interactionPopup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (interactionPopup != null)
                interactionPopup.SetActive(false);
        }
    }
}
