
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateButton : MonoBehaviour, IPointerDownHandler{

    public static bool onButtonDown;//�{�^�����N���b�N����Ă邩�𔻒�I

    /// <summary>
    /// �{�^���������ꂽ�ƌ��o
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        onButtonDown = true;
    }
}