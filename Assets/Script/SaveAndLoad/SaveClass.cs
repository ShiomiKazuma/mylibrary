using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SaveData
{
    [System.Serializable]
    //�Z�[�u���������ڂ�public�ō��
    //�V�[���J�ڂ��N�����Ă������Ȃ��悤��MonoBehaviour�́A�p�����Ȃ�
    public class SaveClass
    {
        public string _playerName = default; //�v���C���[�̖��O
        public int _playerLevel = 0; //�v���C���[�̃��x��
        public Transform _playerTransform = default; //�v���C���[�̈ʒu
        public int _hp = 0;
    }
}