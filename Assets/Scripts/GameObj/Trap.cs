using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private GameObject Player; //Respawner�� ����־, �ش� ��ġ�� Player�� �ٽ� ��ȯ�ǵ��� �� ����
    protected Vector3 initialPosition; //�ʱ� ���� ������Ʈ�� ��ġ (��/�����/���� ��)

    private void Start()
    {
        //�� �÷��̾��� ������ �ʱⰪ ����
        initialPosition = Player.transform.position;
    }
}
