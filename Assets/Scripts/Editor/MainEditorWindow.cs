using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
    
public class MainEditorWindow : EditorWindow {

	//CitySeedEditorWindow cityWindow;
    
    public Vector2 scrollPos = Vector2.zero;

    [MenuItem("Test/GUIWindow Demo")]
    private static void OpenWindow(){
        MainEditorWindow window = GetWindow<MainEditorWindow>();
        window.titleContent = new GUIContent("City Editor");
    }

    void OnGUI(){
        
        Rect windowRect = new Rect(100, 100, 200, 200);
        scrollPos = GUI.BeginScrollView(new Rect(0, 0, position.width, position.height), scrollPos, new Rect(0, 0, 1000, 1000));
        BeginWindows();
        // All GUI.Window or GUILayout.Window must come inside here
        windowRect = GUILayout.Window(1, windowRect, DoWindow, "Hi There");
        EndWindows();
        GUI.EndScrollView();
    }

    void DoWindow(int unusedWindowID){
        GUILayout.Button("" + position.width + "");
    }
}

