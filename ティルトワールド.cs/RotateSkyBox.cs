using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyBox : MonoBehaviour
{

    //　回転スピード
    [SerializeField]
    private float RotateSpeed = 0.5f;
    //　スカイボックスのマテリアル
    private Material SkyboxMaterial;

    // Use this for initialization
    void Start()
    {
        //　Lighting Settingsで指定したスカイボックスのマテリアルを取得
        SkyboxMaterial = RenderSettings.skybox;
    }

    // Update is called once per frame
    void Update()
    {
        //　スカイボックスマテリアルのRotationを操作して角度を変化させる
        SkyboxMaterial.SetFloat("_Rotation", Mathf.Repeat(SkyboxMaterial.GetFloat("_Rotation") + RotateSpeed * Time.deltaTime, 360f));
    }
}