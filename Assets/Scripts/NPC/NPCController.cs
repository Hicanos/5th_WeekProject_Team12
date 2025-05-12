using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class NPCController : MonoBehaviour
{

    [SerializeField] private SpriteRenderer npcRenderer; //npc 이미지 인스펙터에서 넣게끔
    [SerializeField] private GameObject interactionPopup; //npc 앞에서 F누르라고 안내하는 팝업을 인스펙터에서 관리
    [SerializeField] private Text _dialogText;


    public GameObject DialogImage;

    bool isPlayerEncounter = false;

    string _sceneName = "a";

    private void Awake()
    {
        _sceneName = Helper.GetCurrentSceneName(); //Helper에서 현재 씬 이름을 받아옴
    }

    protected void Update()
    {

        if (isPlayerEncounter && Input.GetKeyDown(KeyCode.F))
        {
            DialogImage.SetActive(true);
            switch (_sceneName) //지금 씬의 위치에 따라 다른 대사 출력
            {
                case "StageSelect":
                    TutorDialog(); //튜터 대사 실행
                    break;

                case "Tutorial":
                    TutorialDialog(); //튜토리얼 대사 실행
                    break;

                default:
                    break;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerEncounter = true;
            interactionPopup.SetActive(true);//f를 눌러 대화하기 팝업 보여주기
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionPopup.SetActive(false); // 플레이어가 벗어나면 팝업 숨김
            isPlayerEncounter = false;
            DialogImage.SetActive(false);
        }
    }

    private void TutorDialog()
    {
        int legacyCount = DataManager.LegacyCount(); //DataManager에서 획득한 유물 수를 불러옴

        switch (legacyCount) //유물 수에 따라 다른 대사 출력
        {
            case 1:

                break;

            case 2:

                break;

            case 3:

                break;

            case 4:

                break;

            case 5:

                break;

            case 6:

                break;

            case 7:

                break;

            case 8:

                break;

            default:
                _dialogText.text = "아직 유물을 가진게 없구나. 유물을 얻고 다시 찾아오렴";
                break;
        }



    }

    private void TutorialDialog()
    {
        //튜토리얼 대사 출력하기.
    }
}
