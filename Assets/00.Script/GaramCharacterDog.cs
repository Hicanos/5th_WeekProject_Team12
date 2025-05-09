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

        HandleInput();         // 이동 입력 감지
        HandleAnimation();     // 이동 애니메이션 처리
        HeadSpriteFlip();    // 캐릭터 좌우 반전

        Vector2 input = new Vector2(moveX, moveY);

        Move(input); // BaseController의 이동 처리 호출
        HandleJump();   // 점프 입력 처리
        CheckLanding();
    }
}



