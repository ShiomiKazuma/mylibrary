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
                //SaveSystem.Instance._saveClass._playerName = "�j�b�V�[";
                //SaveSystem.Instance._saveClass._playerLevel = 1;
                //�Z�[�u����
                SaveSystem.Instance.Save();
                Debug.Log("�Z�[�u����");
            }

            if(Input.GetKeyDown(KeyCode.L))
            {
                SaveSystem.Instance.Load();
                Debug.Log("���[�h���܂���");
                
            }

            if(Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("���x����\��");
                Debug.Log("Player Level =" + SaveSystem.Instance._saveClass._playerLevel);
            }

            if(Input.GetKeyDown(KeyCode.U))
            {
                SaveSystem.Instance._saveClass._playerLevel++;
                Debug.Log("���x���A�b�v");
                Debug.Log("Player Level =" + SaveSystem.Instance._saveClass._playerLevel);
            }
        }
    }
}

