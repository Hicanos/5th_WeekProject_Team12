using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GaramCharacterCat : Characterbase
{
    protected override void Awake()
    {
        enumChar = CHAR.CAT;
        base.Awake();
        ControlKey();
    }
    /// <summary>
    /// 입력매서드는 여기에
    /// </summary>
    private void Update()
    {
        HandleInput();         // 이동 입력 
        SkillCall(); //스킬 키 입력
        JumpCall();   // 점프 입력 
    }
    /// <summary>
    /// 행동매서드는 여기에
    /// </summary>
    private void FixedUpdate()
    {
        JumpAtivate();//점프 실행
        HandleJumpAnim();//점프 애니메이션
        CheckLanding();//착지 판정
        
        Move(moveInput); // 이동 호출 
        HeadSpriteFlip();    // 캐릭터 좌우 반전
        HandleMoveAnim();     // 이동 애니메이션 
        SkillActivate(); //스킬발동
    }

    
        
    protected override void Skill()
    {


        base.Skill();
    }
     protected bool IsWallClimb()
    {
        RaycastHit2D hit = Physics2D.Raycast(WallCheck.position, Vector2.right, groundRayRange, groundLayer);
        return hit.collider != null;
    }
    protected void OnDrawGizmosSelected()
    {
        if (WallCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(WallCheck.position, WallCheck.position + Vector3.right * wallRayRange);
    }



}