using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace SaveData
{
    public class SaveSystem
    {
        //�V���O���g������
        private static SaveSystem _instance = new SaveSystem();
        public static SaveSystem Instance => _instance;
        
        private SaveSystem() { }
        //Asset�Ƀf�[�^�̕ۑ��ꏊ�����
        public string Path => Application.dataPath + "/data.json";
        //�Z�[�u�f�[�^���C���X�^���X����
        private SaveClass _saveclass = new SaveClass();

        public void Save()
        {
            //�C���X�^���X�𕶎���ɂ���
            string jsonData = JsonUtility.ToJson(_saveclass);
            //�t�@�C���ւ̏�������
            StreamWriter writer = new StreamWriter(Path,false);
            writer.WriteLine(jsonData);
            //�����c��������Ώ����o��
            writer.Flush();
            //�t�@�C�������
            writer.Close();
        }
    }
}

