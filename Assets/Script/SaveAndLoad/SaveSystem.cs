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
        //�C���X�^���X���������ꂽ���ɕK�����[�h�����
        private SaveSystem() { Load(); }
        //Asset�Ƀf�[�^�̕ۑ��ꏊ�����
        public string Path => Application.dataPath + "/data.json";
        public  SaveClass _saveClass { get; private set; }

        public void Save()
        {
            //�C���X�^���X�𕶎���ɂ���
            string jsonData = JsonUtility.ToJson(_saveClass);
            //�t�@�C���ւ̏�������
            StreamWriter writer = new StreamWriter(Path,false);
            writer.WriteLine(jsonData);
            //�����c��������Ώ����o��
            writer.Flush();
            //�t�@�C�������
            writer.Close();
        }

        //���[�h����
        public void Load()
        {
            //�t�@�C����������Ȃ��ꍇ�̏���
            if (!File.Exists(Path))
            {
                Debug.Log("����N��");
                //�Z�[�u�t�@�C�������
                _saveClass = new SaveClass();
                Save();
                return;
            }
            //�t�@�C����ǂݍ���
            StreamReader reader = new StreamReader(Path);
            string jsonData = reader.ReadToEnd();
            _saveClass = JsonUtility.FromJson<SaveClass>(jsonData);
            //�t�@�C�������
            reader.Close();
        }
    }
}
