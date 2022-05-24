using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//どうぶつが動いているか判定する
public class Animal : MonoBehaviour
{
    //移動している動物がいないかチェックするリスト
    public static List<Moving> isMoves = new List<Moving>();

    Rigidbody2D rigid;
    Moving moving = new Moving();   //移動チェック変数

    /// <summary>
    /// リストに追加&Rigidbody2D取得
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        isMoves.Add(moving);
    }

    /// <summary>
    /// 固定フレームレートで移動チェック
    /// </summary>

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rigid.velocity.magnitude > 0.01f)//少しでも移動していれば動いてると判定
        {
            //Debug.Log("動いてる");
            moving.isMove = true;
        }
        else
        {
            //Debug.Log("動いてねぇッピ！");
            moving.isMove = false;
        }
    }
}

/// <summary>
/// 移動チェッククラス
/// </summary>
public class Moving
{
    public bool isMove;
}
