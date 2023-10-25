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
                //�Z�[�u���������ڂƒl������
                SaveSystem.Instance.SaveClass._playerName = "�j�b�V�[";
                SaveSystem.Instance.SaveClass._playerLevel = 1;
                //�Z�[�u����
                SaveSystem.Instance.Save();
                Debug.Log("�Z�[�u����");
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

