using UnityEditor;
using UnityEngine;

public class GroupEditor : MonoBehaviour
{
    [MenuItem("Editor/Group %g")]
    
    private static void Grouping()
    {
        //選択されているゲームオブジェクトを取得
        var gameObjects = Selection.gameObjects;
        //１個も選択されていない場合は、処理をしない
        if (gameObjects.Length <= 0)
            return;

        var group = new GameObject("Group");
        //親オブジェクトを生成
        Undo.RegisterCreatedObjectUndo(group, "Group");
        foreach(var obj in gameObjects)
        {
            //選択したものを子オブジェクトにする
            Undo.SetTransformParent(obj.transform, group.transform, "Group");
        }
        Selection.activeGameObject = group;
    }

    [MenuItem("Edit/Group %g", true)]
    private static bool CanGrouping()
    {
        return 0 < Selection.transforms.Length;
    }
}
