﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class MyTestEditorWindow : EditorWindow
{
    [MenuItem("Window/My Test Editor Window")]
    
    //ウィンドウの表示
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MyTestEditorWindow));
    }

    private void OnGUI()
    {
        //実際のウィンドウのコードはここに書く

    }
}
