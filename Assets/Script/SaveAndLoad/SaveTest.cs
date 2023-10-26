using OpenCover.Framework.Model;
using System.IO;
using UnityEngine;

namespace SaveData
{
    public class SaveTest : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                //セーブしたい項目と値を書く
                //SaveSystem.Instance._saveClass._playerName = "ニッシー";
                //SaveSystem.Instance._saveClass._playerLevel = 1;
                //セーブ処理
                SaveSystem.Instance.Save();
                Debug.Log("セーブ完了");
            }

            if(Input.GetKeyDown(KeyCode.L))
            {
                SaveSystem.Instance.Load();
                Debug.Log("ロードしました");
                
            }

            if(Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("レベルを表示");
                Debug.Log("Player Level =" + SaveSystem.Instance._saveClass._playerLevel);
            }

            if(Input.GetKeyDown(KeyCode.U))
            {
                SaveSystem.Instance._saveClass._playerLevel++;
                Debug.Log("レベルアップ");
                Debug.Log("Player Level =" + SaveSystem.Instance._saveClass._playerLevel);
            }
        }
    }
}

