using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalGenerator : MonoBehaviour
{
    public GameObject[] animals;//�ǂ��Ԃ擾�z��
    public Camera mainCamera;//�J�����擾�p�ϐ�
    public float pivotHeight = 3;//�����ʒu�̊

    public static int animalNum = 0;//�������ꂽ�����̌���ۊ�
    public static bool isGameOver = false;//�Q�[���I�[�o�[����

    private GameObject geneAnimal;//�ǂ��Ԃ����i�P�i�j
    public bool isGene;//��������Ă��邩
    public bool isFall;//�������ꂽ��������������

    public static int getscore()
    {
        return animalNum;
    }

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// ����������
    /// </summary>
    void Init()
    {
        animalNum = 0;
        isGameOver = false;
        Animal.isMoves.Clear();//�ړ����Ă铮���̃��X�g��������
        StartCoroutine(StateReset());
    }

    // ���t���[���Ăяo�����(60fps��������1�b�Ԃ�60��)
    void Update()
    {
        

        if (isGameOver)
        {
            return;//�Q�[���I�[�o�[�Ȃ炱���Ŏ~�߂�
        }

        if (CheckMove(Animal.isMoves))
        {
            return;//�ړ����Ȃ珈���͂����܂�
        }

        if (!isGene)//��������Ă���̂��Ȃ�
        {
            StartCoroutine(GenerateAnimal());//��������R���[�`���𓮂���
            isGene = true;
            return;
        }

        Vector2 v = new Vector2(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, pivotHeight);

        if (Input.GetMouseButtonUp(0))//�����i�}�E�X���N���b�N�������ꂽ��j
        {
            if (!RotateButton.onButtonDown)//�{�^�����N���b�N���Ă����甽�������Ȃ�
            {
                geneAnimal.transform.position = v;
                geneAnimal.GetComponent<Rigidbody2D>().isKinematic = false;//�\�\�\�\���������E�I��
                animalNum++;//�ǂ��Ԃ���
                isFall = true;//�����āA�ǂ���
            }
            RotateButton.onButtonDown = false;//�}�E�X���オ������{�^�������ꂽ�Ǝv��
        }
        else if (Input.GetMouseButton(0))//�{�^����������Ă����
        {
            geneAnimal.transform.position = v;
        }
    }

    /// <summary>
    /// �����E������Ԃ����Z�b�g����R���[�`��
    /// </summary>
    /// <returns></returns>
    IEnumerator StateReset()
    {
        while (!isGameOver)
        {
            yield return new WaitUntil(() => isFall);//��������܂ŏ������~�܂�
            yield return new WaitForSeconds(0.1f);//���������������Z������҂i�Ȃ��Ɩ������[�v�j
            isFall = false;
            isGene = false;
        }
    }

    /// <summary>
    /// �ǂ��Ԃ̐����R���[�`��
    /// </summary>
    /// <returns></returns>
    IEnumerator GenerateAnimal()
    {
        while (CameraController.isCollision)
        {
            yield return new WaitForEndOfFrame();//�t���[���̏I���܂ő҂i�����Ɩ������[�v�j
            mainCamera.transform.Translate(0, 0.1f, 0);//�J������������Ɉړ�
            pivotHeight += 0.1f;//�����ʒu��������Ɉړ�
        }
        geneAnimal = Instantiate(animals[Random.Range(0, animals.Length)], new Vector2(0, pivotHeight), Quaternion.identity);//��]�����ɐ���
        geneAnimal.GetComponent<Rigidbody2D>().isKinematic = true;//���������������Ȃ���Ԃɂ���
    }

    /// <summary>
    /// �ǂ��Ԃ̉�]
    /// �{�^���ɂ��Ďg���܂�
    /// </summary>
    public void RotateAnimal()
    {
        if (!isFall)
            geneAnimal.transform.Rotate(0, 0, -30);//30�x����]
    }

    /// <summary>
    /// ���g���C�{�^��
    /// </summary>
    public void Retry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// �ړ������`�F�b�N
    /// </summary>
    /// <param name="isMoves"></param>
    /// <returns></returns>
    bool CheckMove(List<Moving> isMoves)
    {
        if (isMoves == null)
        {
            return false;
        }
        foreach (Moving b in isMoves)
        {
            if (b.isMove)
            {
                //Debug.Log("�ړ���(*'��'*)");
                //Debug.Log(geneAnimal.transform.position.y);
                //if (geneAnimal.transform.position.y < -5)
                //{
                //    Destroy(geneAnimal);
                //    b.isMove = false;
                //    return false;
                //}
                return true;
            }
        }

        if (transform)
        {

        }

        return false;
    }
}