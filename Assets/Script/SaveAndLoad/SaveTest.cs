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
                SaveSystem.Instance.SaveClass._playerName = "ニッシー";
                SaveSystem.Instance.SaveClass._playerLevel = 1;
                //セーブ処理
                SaveSystem.Instance.Save();
                Debug.Log("セーブ完了");
            }

            if(Input.GetKeyDown(KeyCode.L))
            {
                SaveSystem.Instance.Load();
                Debug.Log("Player Name =" + SaveSystem.Instance.SaveClass._playerName);
                Debug.Log("Player Level =" + SaveSystem.Instance.SaveClass._playerLevel);
            }
        }
    }
}

