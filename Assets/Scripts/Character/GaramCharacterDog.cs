using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
//���¸ӽ�, ����ü��� , ��������Ʈ ��� 
public class GaramCharacterDog : Characterbase
{
    [Header("������ ����")]
    
        [SerializeField] private LayerMask destroyLayer;//�ı��� ������Ʈ ���̾�
        [SerializeField] private float dashSpeed = 30f;//���� �ӵ�(��)
        [SerializeField] private float dashDuration = 0.2f;//�������� �ð�
    

    protected override void Awake()
    {
        enumChar = CHAR.DOG;//ĳ���� �з�
        base.Awake();
        ControlKey();//����Ű�Ҵ�
    }
    /// <summary>
    /// �Է¸ż���� ���⿡
    /// </summary>
    private void Update()
    {  //��� �� �������� ���� ���̱� ������  �Է� ����( ȣ���� ���� true���� bool���� false�� �ٲ��� ���ϰ� �� )
        if (isDash)
        {
            return;
        }
        else
        {
            InstantSkillCall();
            JumpCall();
            MoveCall();
        }
    }
    /// <summary>
    /// �ൿ�ż���� ���⿡
    /// </summary>
    private void FixedUpdate()
    {
        HandleJumpAnim();
        CheckLanding();
        //����߿� rb.velocity���� ������ �ֱ⶧���� Update�ܰ迡�� ���� 
        if (isDash)
        {
            return;
        }
        else
        {
            JumpAtivate();//���� ����
            MoveActivate(moveInput); // �̵� ȣ�� 
        }

        SpriteFlip();    // ĳ���� �¿� ����
        HandleMoveAnim();     // �̵� �ִϸ��̼� 
        InstantSkillActivate(); //��ų�ߵ�
        HandleSkillAnim();//��ų �ִϸ��̼�
    }
    /*------------------------------------------------------------------------------*/
    // ������ ��ų�ߵ����� ������ �Ұ�
    private bool isDash = false;
    //���������϶��� ��ų ��� ���� �ϰ� �ϱ� ���� ������0
    protected override void InstantSkillCall()
    {

        if (IsGrounded() && currentJumpCount == 0)
            base.InstantSkillCall();

    }
    //��ų ������
    protected override void InstantSkill()
    {
        //������ ������� ���Ͱ� moveX�̿��ϸ� �̵����� �ƴҶ� 0�̶� ������.���߿� �ϵ��ڵ� ���ϰ� �غ��� 
        Vector2 currentLook; 
        if (this.spriteRenderer.flipX == false)
        {
            currentLook = new Vector2(1, 0);
        }
        else
        {
            currentLook = new Vector2(-1, 0);
        }
        rb.velocity = Vector2.zero; //  �ӵ� �ʱ�ȭ
        rb.position -= currentLook * 0.1f;// �� �پ ����� �� Stay���¶� �ı����� �����Ƿ� Stay �ϳ��� ����� �� ���� ����� ó���ϱ�� ��
        //�ڷ�ƾ ���� �ڿ� �Ű����� 2�� �ν�����â ���� ����
        StartCoroutine(DashDuration(currentLook, dashDuration, dashSpeed));

    }

    //��� ������ ���� �ڷ�ƾ 
    private IEnumerator DashDuration(Vector2 direction, float duration, float dashSpeed)
    {
        isDash = true;
        
        yield return new WaitForSeconds(0.1f);
        // ���������� ���� �ӵ� ����
        rb.velocity = direction * dashSpeed;  

        // �� duration��ŭ�� �̵�
        yield return new WaitForSeconds(duration); 

        // ��� ���߱�
        rb.velocity = Vector2.zero;
        isDash = false;
    }

    //���̾� �����Ͽ� �浹 ������Ʈ �ı��ϴ� �Լ�
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���� ���� �ƴ� ���� ����
        if (!isDash) return;

        // ���̾� üũ: ������ ���̾ �ش��ϴ��� Ȯ��
        if (((1 << other.gameObject.layer) & destroyLayer) != 0)
        {
            Destroy(other.gameObject);
            HandleCrashAnim();
           //�Ѱ��� ������Ʈ �浹�Ͽ� �ı��Ͽ��� �� ��� ���߰� �ϱ����� ���� 
            isDash = false;
            rb.velocity = Vector2.zero;

            Debug.Log(" �������� �ı���: " + other.name);
        }
    }
    //�������� ������ ���� ������
    protected override void HandleSkillAnim()
    {
        bool isSkill = isDash;
        Anim.SetSkill(isSkill);
    }

}




