using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GaramCharacterDog : Characterbase
{
    protected override void Awake()
    {
        enumChar = CHAR.DOG;
        base.Awake();
        ControlKey();
    }

    private void Update()
    {

        HandleInput();         // �̵� �Է� ����
        HandleAnimation();     // �̵� �ִϸ��̼� ó��
        HeadSpriteFlip();    // ĳ���� �¿� ����

        Vector2 input = new Vector2(moveX, moveY);

        Move(input); // BaseController�� �̵� ó�� ȣ��
        HandleJump();   // ���� �Է� ó��
        CheckLanding();
    }
}



