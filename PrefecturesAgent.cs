//ml-agent用スクリプト
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class PrefecturesAgent : Agent
{
    Rigidbody rbody;
    public GameObject[] animals;//どうぶつ取得配列
    public Camera mainCamera;//カメラ取得用変数
    public float pivotHeight = 3;//生成位置の基準

    public static int animalNum = 0;//生成された動物の個数を保管
    public static bool isGameOver = false;//ゲームオーバー判定

    public GameObject geneAnimal;//どうぶつ生成（単品）             #!これprivateから
    public bool isGene;//生成されているか
    public bool isFall;//生成された動物が落下中か

    public static int animalCounter = 0;    //報酬用


    public static int getscore()
    {
        return animalNum;
    }

    private void Start()
    {
        Initialize();
    }

    //初期化時に呼ばれる
    public override void Initialize()
    {
        animalNum = 0;
        isGameOver = false;
        Animal.isMoves.Clear();//移動してる動物のリストを初期化
        StartCoroutine(StateReset());
        //this.rbody = GetComponent<Rigidbody>();
    }



    //エピソード開始時の初期化
    public override void OnEpisodeBegin()
    {
        animalNum = 0;
        isGameOver = false;
        Animal.isMoves.Clear();//移動してる動物のリストを初期化
        StartCoroutine(StateReset());
        //物体が落ちたらリセット
        //if (this.transform.position.y < 0)
        //{
            //位置速度リセット
            //this.rBody.angularVelocity = Vector3.zero;
            //this.rBody.velocity = Vector3.zero;
            //this.transform.localPosition = new Vector3(0, 0.5f, 0);
        //}
    }



    void Update(){

        if (isGameOver)
        {
            AddReward(-10.0f);
            EndEpisode();
            return;//ゲームオーバーならここで止める
        }

        if (CheckMove(Animal.isMoves))
        {
            return;//移動中なら処理はここまで
        }

        if (!isGene)//生成されてるものがない
        {
            StartCoroutine(GenerateAnimal());//生成するコルーチンを動かす
            isGene = true;
            return;
        }

        Vector2 v = new Vector2(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, pivotHeight);

        if (Input.GetMouseButtonUp(0))//もし（マウス左クリックが離されたら）
        {
            if (!RotateButton.onButtonDown)//ボタンをクリックしていたら反応させない
            {
                geneAnimal.transform.position = v;
                geneAnimal.GetComponent<Rigidbody2D>().isKinematic = false;//――――物理挙動・オン
                animalNum++;//どうぶつ生成
                isFall = true;//落ちて、どうぞ
            }
            RotateButton.onButtonDown = false;//マウスが上がったらボタンも離れたと思う
        }
        else if (Input.GetMouseButton(0))//ボタンが押されている間
        {
            geneAnimal.transform.position = v;
        }

        //個数が増えてれば，報酬を与える
        if (animalCounter < animalNum){
            //AddReward(0.1*(animalNum));
            AddReward(1.0f);
            animalCounter = animalNum;
        }
    }




    //環境取得時に呼ぶ
    public override void CollectObservations(VectorSensor sendor)
    {
        
         //agentのxyz座標
        //sensor.AddObservation(this.transform.localPosition);

        // Agentの速度
        //sensor.AddObservation(rBody.velocity.x);
        //sensor.AddObservation(rBody.velocity.z);
    }

    //行動決定時に呼ぶ
    public override void OnActionReceived(float[] vectorAction){
        //

        int action = (int)vectorAction[0];
        //回転させる 落下していないときだけ使えるように
        if (action==1 && !isFall){
            geneAnimal.transform.Rotate(0, 0, -10);//10度ずつ回転
        }
        //x軸移動
        if (action==2 && !isFall){
            geneAnimal.transform.Translate(0.05f, 0, 0);
        }
        if (action==3 && !isFall){
            geneAnimal.transform.Translate(-0.05f, 0, 0);
        }
        //落下させる
        if (action==4){
            geneAnimal.GetComponent<Rigidbody2D>().isKinematic = false;//――――物理挙動・オン
            animalNum++;//どうぶつ生成
            isFall = true;//落ちて、どうぞ
        }


        //個数が増えてれば，報酬を与える
        if (animalCounter < animalNum){
            //AddReward(0.1*(animalNum));
            AddReward(1.0f);
            animalCounter = animalNum;
        }



        //エピソード終了 ゲームオーバーになったら報酬を減らす
        if (isGameOver == true){
            AddReward(-10.0f);
            EndEpisode();
        }


    }


    /// <summary>
    /// 生成・落下状態をリセットするコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator StateReset()
    {
        while (!isGameOver)
        {
            yield return new WaitUntil(() => isFall);//落下するまで処理が止まる
            yield return new WaitForSeconds(0.1f);//少しだけ物理演算処理を待つ（ないと無限ループ）
            isFall = false;
            isGene = false;
        }
    }

    /// <summary>
    /// どうぶつの生成コルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator GenerateAnimal()
    {
        while (CameraController.isCollision)
        {
            yield return new WaitForEndOfFrame();//フレームの終わりまで待つ（無いと無限ループ）
            mainCamera.transform.Translate(0, 0.1f, 0);//カメラを少し上に移動
            pivotHeight += 0.1f;//生成位置も少し上に移動
        }
        geneAnimal = Instantiate(animals[Random.Range(0, animals.Length)], new Vector2(0, pivotHeight), Quaternion.identity);//回転せずに生成
        geneAnimal.GetComponent<Rigidbody2D>().isKinematic = true;//物理挙動をさせない状態にする
    }

    /// <summary>
    /// どうぶつの回転
    /// ボタンにつけて使います
    /// </summary>
    public void RotateAnimal()
    {
        if (!isFall)
            geneAnimal.transform.Rotate(0, 0, -30);//30度ずつ回転
    }

    /// <summary>
    /// リトライボタン
    /// </summary>
    public void Retry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainML");
    }

    /// <summary>
    /// 移動中かチェック
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
                //Debug.Log("移動中(*'ω'*)");
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

