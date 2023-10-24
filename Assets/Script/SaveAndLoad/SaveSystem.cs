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
        
        private SaveSystem() { }
        //Assetにデータの保存場所を作る
        public string Path => Application.dataPath + "/data.json";
        //セーブデータをインスタンスする
        private SaveClass _saveclass = new SaveClass();

        public void Save()
        {
            //インスタンスを文字列にする
            string jsonData = JsonUtility.ToJson(_saveclass);
            //ファイルへの書き込み
            StreamWriter writer = new StreamWriter(Path,false);
            writer.WriteLine(jsonData);
            //書き残しがあれば書き出す
            writer.Flush();
            //ファイルを閉じる
            writer.Close();
        }
    }
}

