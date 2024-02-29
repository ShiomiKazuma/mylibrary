using UnityEngine;
using UnityEditor;

//どのコンポーネントを拡張するのかを指定
[CustomEditor(typeof(DebugLog))]
public class EditorButton : Editor
{
    //インスペクタを変更する
    public override void OnInspectorGUI()
    {
        //元々あったインスペクタを表示
        DrawDefaultInspector();
        
        //targetは、エディタ拡張上のGameObjectを取得
        DebugLog debugLog = (DebugLog)target;
        
        //ボタンが押されたときの処理を追加
        if(GUILayout.Button("ボタン"))
        {
            //ボタンを押したときに行う処理を記入する
            debugLog.DebugAction();
        }
    }
}
