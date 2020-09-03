using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LineManager : MonoBehaviour {
    /// <summary> 描く線のコンポーネント </summary>
    private LineRenderer lineRenderer;

    /// <summary> 描く線のオブジェクトリスト </summary>
    private List<GameObject> lineObject;

    /// <summary> 描く線のコンポーネントリスト </summary>
    private List<LineRenderer> lineRendererList;

    /// <summary> 描く線のコリジョンリスト </summary>
    private List<EdgeCollider2D> lineEdgeCollider2DList;

    /// <summary> 描く線のマテリアル </summary>
    public Material lineMaterial;

    /// <summary> 描く線の色 </summary>
    public Color lineColor;

    /// <summary> 描く線の太さ </summary>
    [Range(0, 10)] public float lineWidth;

    /// <summary> 描く線の開始位置 </summary>
    private Vector3 startLinePos;

    /// <summary> 描く線の終了位置 </summary>
    private Vector3 endLinePos;

    void Start() {
        lineObject = new List<GameObject>();
        lineRendererList = new List<LineRenderer>();
        lineEdgeCollider2DList = new List<EdgeCollider2D>();

        // コンポーネントを取得する
        this.lineRenderer = GetComponent<LineRenderer>();

        // 線の幅を決める
        this.lineRenderer.startWidth = lineWidth;
        this.lineRenderer.endWidth = lineWidth;

        // 頂点の数を決める
        this.lineRenderer.positionCount = 2;
    }

    void Update() {

        // ボタンが押された時に線オブジェクトの追加を行う
        if (Input.GetMouseButtonDown(0)) {

            // 追加予定位置表示の初期化
            this.GhostLineInit();

            // 線オブジェクトの追加
            this.AddLineObject();

            // 位置情報の更新
            this.LinePositionUpdata();

        }

        // ボタンが押されている時、線オブジェクトの追加予定位置更新
        if (Input.GetMouseButton(0)) {

            this.GhostLineUpdata();
        }

        // ボタンを離した時、
        if (Input.GetMouseButtonUp(0)) {

            // 当たり判定追加
            this.AddEdgeCollider();

            // 位置情報の更新
            this.LinePositionUpdata();

            // 追加予定位置表示の初期化
            this.GhostLineInit();
        }

    }

    /// <summary>
    /// 線オブジェクトの追加予定位置表示の更新
    /// </summary>
    private void GhostLineUpdata() {
        // 座標の変換を行いマウス位置を取得
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 1.0f);
        endLinePos = Camera.main.ScreenToWorldPoint(screenPosition);

        // 追加した頂点の座標を設定
        this.lineRenderer.SetPosition(lineRenderer.positionCount - 2, endLinePos);


    }

    /// <summary>
    /// 描く線のコンポーネントリストに位置情報の更新
    /// </summary>
    private void LinePositionUpdata() {

        // 座標の変換を行いマウス位置を取得
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 1.0f);
        endLinePos = Camera.main.ScreenToWorldPoint(screenPosition);

        // 描く線のコンポーネントリストを更新
        lineRendererList.Last().SetPosition(lineRendererList.Last().positionCount - 2, endLinePos);
        lineRendererList.Last().SetPosition(lineRendererList.Last().positionCount - 1, startLinePos);

    }

    /// <summary>
    /// 線オブジェクトの追加予定位置表示の初期化
    /// </summary>
    private void GhostLineInit() {
        // 座標の変換を行いマウス位置を取得
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 1.0f);
        startLinePos = Camera.main.ScreenToWorldPoint(screenPosition);

        // 追加した頂点の座標を設定
        this.lineRenderer.SetPosition(lineRenderer.positionCount - 1, startLinePos);
        this.lineRenderer.SetPosition(lineRenderer.positionCount - 2, startLinePos);

    }

    /// <summary>
    /// 線オブジェクトの当たり判定追加
    /// </summary>
    private void AddEdgeCollider() {
        Vector2[] points = new Vector2[2];

        lineObject.Last().AddComponent<EdgeCollider2D>();
        lineEdgeCollider2DList.Add(lineObject.Last().GetComponent<EdgeCollider2D>());

        // 位置情報取得
        points[0] = startLinePos;
        points[1] = endLinePos;

        // 位置と範囲設定
        lineEdgeCollider2DList.Last().points = points;
        lineEdgeCollider2DList.Last().edgeRadius = this.lineWidth / 2;
    }


    /// <summary>
    /// 線オブジェクトの追加
    /// </summary>
    private void AddLineObject() {

        // 追加するオブジェクトをインスタンス
        lineObject.Add(new GameObject());

        // オブジェクトにLineRendererを取り付ける
        lineObject.Last().AddComponent<LineRenderer>();

        // 描く線のコンポーネントリストに追加する
        lineRendererList.Add(lineObject.Last().GetComponent<LineRenderer>());

        // 線と線をつなぐ点の数を設定
        lineRendererList.Last().positionCount = 2;

        // マテリアルを設定
        lineRendererList.Last().material = this.lineMaterial;

        // 線の色を設定
        lineRendererList.Last().material.color = this.lineColor;

        // 線の太さを設定
        lineRendererList.Last().startWidth = this.lineWidth;
        lineRendererList.Last().endWidth = this.lineWidth;

    }
    /// <summary>
    /// 線オブジェクトの削除
    /// </summary>
    public void DeleteLineObject() {

        for (int i = 0; i < lineRendererList.Count; i++) {
            Destroy(lineRendererList[i]);
        }
        lineRendererList.Clear();

        for (int i = 0; i < lineEdgeCollider2DList.Count; i++) {
            Destroy(lineEdgeCollider2DList[i]);
        }
        lineEdgeCollider2DList.Clear();

        for (int i = 0; i < lineObject.Count; i++) {
            Destroy(lineObject[i]);
        }
        lineObject.Clear();
    }
}