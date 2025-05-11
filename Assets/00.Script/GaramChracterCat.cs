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
    /// �Է¸ż���� ���⿡
    /// </summary>
    private void Update()
    {
        HandleInput();         // �̵� �Է� 
        SkillCall(); //��ų Ű �Է�
        JumpCall();   // ���� �Է� 
    }
    /// <summary>
    /// �ൿ�ż���� ���⿡
    /// </summary>
    private void FixedUpdate()
    {
        JumpAtivate();//���� ����
        HandleJumpAnim();//���� �ִϸ��̼�
        CheckLanding();//���� ����
        
        Move(moveInput); // �̵� ȣ�� 
        HeadSpriteFlip();    // ĳ���� �¿� ����
        HandleMoveAnim();     // �̵� �ִϸ��̼� 
        SkillActivate(); //��ų�ߵ�
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