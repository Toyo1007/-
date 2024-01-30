using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;  // Except

/// <summary>
/// カメラと対象との間の遮蔽物(Cover)を透明化します。
/// カメラに付加してください。
/// 透明にする遮蔽物は Renderer コンポーネントを付加している必要があります。
/// </summary>
public class SCCameraCoverTransparent : MonoBehaviour
{


    /// <summary>
    /// 被写体を指定してください。
    /// </summary>
    [SerializeField]
    private Transform subject_;

    /// <summary>
    /// 遮蔽物のレイヤー名のリスト。
    /// </summary>
    [SerializeField]
    private List<string> coverLayerNameList_;

    /// <summary>
    /// 遮蔽物とするレイヤーマスク。
    /// </summary>
    private int layerMask_;

    /// <summary>
    /// 今回の Update で検出された遮蔽物の Renderer コンポーネント。
    /// </summary>
    public List<Renderer> rendererHitsList_ = new List<Renderer>();

    /// <summary>
    /// 前回の Update で検出された遮蔽物の Renderer コンポーネント。
    /// 今回の Update で該当しない場合は、遮蔽物ではなくなったので Renderer コンポーネントを有効にする。
    /// </summary>
    public Renderer[] rendererHitsPrevs_;


    // Use this for initialization
    void Start()
    {
        // 遮蔽物のレイヤーマスクを、レイヤー名のリストから合成する。
        layerMask_ = 0;
        foreach (string _layerName in coverLayerNameList_)
        {
            layerMask_ |= 1 << LayerMask.NameToLayer(_layerName);
        }

    }


    // Update is called once per frame
    void Update()
    {
        // カメラと被写体を結ぶ ray を作成
        Vector3 _difference = (subject_.transform.position - this.transform.position);
        Vector3 _direction = _difference.normalized;
        Ray _ray = new Ray(this.transform.position, _direction);

        // 前回の結果を退避してから、Raycast して今回の遮蔽物のリストを取得する
        RaycastHit[] _hits = Physics.RaycastAll(_ray, _difference.magnitude, layerMask_);


        rendererHitsPrevs_ = rendererHitsList_.ToArray();
        rendererHitsList_.Clear();
        // 遮蔽物は一時的にすべて描画機能を無効にする。
        foreach (RaycastHit _hit in _hits)
        {
            // 遮蔽物が被写体の場合は例外とする
            if (_hit.collider.gameObject == subject_)
            {
                continue;
            }

            // 遮蔽物の Renderer コンポーネントを無効にする
            Renderer _renderer = _hit.collider.gameObject.GetComponent<Renderer>();
            if (_renderer != null)
            {
                rendererHitsList_.Add(_renderer);
                Color color = _renderer.material.color;
                _renderer.material.color = new Color(color.r,color.g,color.b,0.3f);
                // _renderer.enabled = false;
            }
          
        }

        // 前回まで対象で、今回対象でなくなったものは、表示を元に戻す。
        foreach (Renderer _renderer in rendererHitsPrevs_.Except<Renderer>(rendererHitsList_))
        {
            // 遮蔽物でなくなった Renderer コンポーネントを有効にする
            if (_renderer != null)
            {
                Color color = _renderer.material.color;
                _renderer.material.color = new Color(color.r, color.g, color.b, 1.0f);
                //_renderer.enabled = true;
            }
        }

    }
}
