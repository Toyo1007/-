using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyBox : MonoBehaviour
{

    //�@��]�X�s�[�h
    [SerializeField]
    private float RotateSpeed = 0.5f;
    //�@�X�J�C�{�b�N�X�̃}�e���A��
    private Material SkyboxMaterial;

    // Use this for initialization
    void Start()
    {
        //�@Lighting Settings�Ŏw�肵���X�J�C�{�b�N�X�̃}�e���A�����擾
        SkyboxMaterial = RenderSettings.skybox;
    }

    // Update is called once per frame
    void Update()
    {
        //�@�X�J�C�{�b�N�X�}�e���A����Rotation�𑀍삵�Ċp�x��ω�������
        SkyboxMaterial.SetFloat("_Rotation", Mathf.Repeat(SkyboxMaterial.GetFloat("_Rotation") + RotateSpeed * Time.deltaTime, 360f));
    }
}