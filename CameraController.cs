using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static bool isCollision;//�Փˌ��o�ϐ�

    /// <summary>
    /// �Փ˒���`���郁�\�b�h
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        isCollision = true;
        //Debug.Log(isCollision);
    }

    /// <summary>
    /// �ՓˏI����`���郁�\�b�h
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        isCollision = false;
    }
}