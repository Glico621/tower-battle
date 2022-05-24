using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ǂ��Ԃ������Ă��邩���肷��
public class Animal : MonoBehaviour
{
    //�ړ����Ă��铮�������Ȃ����`�F�b�N���郊�X�g
    public static List<Moving> isMoves = new List<Moving>();

    Rigidbody2D rigid;
    Moving moving = new Moving();   //�ړ��`�F�b�N�ϐ�

    /// <summary>
    /// ���X�g�ɒǉ�&Rigidbody2D�擾
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        isMoves.Add(moving);
    }

    /// <summary>
    /// �Œ�t���[�����[�g�ňړ��`�F�b�N
    /// </summary>

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rigid.velocity.magnitude > 0.01f)//�����ł��ړ����Ă���Γ����Ă�Ɣ���
        {
            //Debug.Log("�����Ă�");
            moving.isMove = true;
        }
        else
        {
            //Debug.Log("�����Ă˂��b�s�I");
            moving.isMove = false;
        }
    }
}

/// <summary>
/// �ړ��`�F�b�N�N���X
/// </summary>
public class Moving
{
    public bool isMove;
}
