using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [Header("��ȣ�ۿ� ������Ʈ")] public GameObject gameObject;
    //���� ������Ʈ�� Collider ����, Collision = ���ڸ� �ν��ؼ� ��ư�� ������ �����
    //������ Tag => Box�� ����
    //SetActive�� false�� true, true�� false�� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
