using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SaveData
{
    [System.Serializable]
    //セーブしたい項目をpublicで作る
    //シーン遷移が起こっても消えないようにMonoBehaviourは、継承しない
    public class SaveClass
    {
        public string _playerName = default; //プレイヤーの名前
        public int _playerLevel = 0; //プレイヤーのレベル
        public Transform _playerTransform = default; //プレイヤーの位置
        public int _hp = 0;
    }
}