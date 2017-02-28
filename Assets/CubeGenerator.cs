using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour {
    //キューブのPrefab
    public GameObject cubePrefab;

    //時間計測用の変数
    private float delta = 0;

    //キューブの生成感覚
    private float span = 1.0f;

    //キューブの生成位置:x座標
    private float genPosX = 12;

    //キューブの生成位置オフセット、キューブを出現させる高さ
    private float offsetY = 0.3f;
    //キューブの縦方向の感覚、キューブ同士の縦の
    private float spaceY = 6.9f;

    //キューブの生成位置オフセット
    private float offsetX = 0.5f;
    //キューブの横方向の感覚
    private float spaceX = 0.4f;

    //キューブの生成個数の上限
    private int maxBlockNum = 4;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.delta += Time.deltaTime;

        //span秒以上の時間が経過したかを調べる
        if (this.delta > this.span) {
            this.delta = 0;
            //生成するキューブ数をランダムに決める。「+1」するのは、そうしないと「maxBlockNum未満」で決められるから
            int n = Random.Range(1, maxBlockNum + 1);

            //指定した数だけキューブを生成するための「i」
            //1 を足さないと i < n の間という終了条件が満たされず無限にその for 文の中が繰り返される
            for (int i = 0; i < n; i++) {
                //キューブの生成
                GameObject go = Instantiate(cubePrefab) as GameObject;
                //offsetYが高さ、spaceYが生成されるキューブ毎の感覚
                //１ループ目は i が 0 なので「offsetY(0.3) + i(0) * spaceY(6.9)」、2ループ目で生成
                go.transform.position = new Vector2(this.genPosX, this.offsetY + i * this.spaceY);
            }
            //次のキューブまでの生成時間を決める
            //this.delta と比較するための this.span の値を求めている
            //n (キューブの生成数）が 1 なら「this.offsetX (0.5) + this.spaceX (0.4) * n (1)」
            this.span = this.offsetX + this.spaceX * n;
        }
	}
}
