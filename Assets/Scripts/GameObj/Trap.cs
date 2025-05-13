using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private GameObject Player; //Respawner를 집어넣어서, 해당 위치에 Player가 다시 소환되도록 할 것임
    protected Vector3 initialPosition; //초기 게임 오브젝트의 위치 (개/고양이/상자 등)

    private void Start()
    {
        //각 플레이어의 포지션 초기값 세팅
        initialPosition = Player.transform.position;
    }
}
