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
        //シングルトン処理
        private static SaveSystem _instance = new SaveSystem();
        public static SaveSystem Instance => _instance;
        //インスタンスが生成された時に必ずロードされる
        private SaveSystem() { Load(); }
        //Assetにデータの保存場所を作る
        public string Path => Application.dataPath + "/data.json";
        public  SaveClass _saveClass { get; private set; }

        public void Save()
        {
            //インスタンスを文字列にする
            string jsonData = JsonUtility.ToJson(_saveClass);
            //ファイルへの書き込み
            StreamWriter writer = new StreamWriter(Path,false);
            writer.WriteLine(jsonData);
            //書き残しがあれば書き出す
            writer.Flush();
            //ファイルを閉じる
            writer.Close();
        }

        //ロード処理
        public void Load()
        {
            //ファイルが見つからない場合の処理
            if (!File.Exists(Path))
            {
                Debug.Log("初回起動");
                //セーブファイルを作る
                _saveClass = new SaveClass();
                Save();
                return;
            }
            //ファイルを読み込む
            StreamReader reader = new StreamReader(Path);
            string jsonData = reader.ReadToEnd();
            _saveClass = JsonUtility.FromJson<SaveClass>(jsonData);
            //ファイルを閉じる
            reader.Close();
        }
    }
}

