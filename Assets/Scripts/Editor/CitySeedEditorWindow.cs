using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CitySeedEditorWindow : EditorWindow {

	private static CitySeed city;
    private Vector2 offset;
    private float menuSize = 200;
    private Vector2 drag;
    public Vector2 scrollPos = Vector2.zero;

	[MenuItem("Component/City Editor")]
    private static void OpenWindow(){
        
        GameObject cityObject = GameObject.FindWithTag("CG_CitySeed");
        if(cityObject != null){
            city = cityObject.GetComponent(typeof(CitySeed)) as CitySeed;
            city.Start();
        }
        CitySeedEditorWindow window = GetWindow<CitySeedEditorWindow>();
        window.titleContent = new GUIContent("City Editor");
    }

    private void OnEnable(){
    }

 	private void OnGUI(){

        Rect windowRect = new Rect(0, 0, menuSize, position.height);
        //scrollPos = GUI.BeginScrollView(new Rect(0, 0, position.width, position.height), scrollPos, new Rect(0, 0, 500, 500));
        Handles.BeginGUI();
		DrawGrid(20, 0.2f, Color.black);
        DrawGrid(100, 0.4f, Color.black);  
        if(city != null){
            DrawCity();
        }
        BeginWindows();
        windowRect = GUILayout.Window(1, windowRect, DoWindow, "Hi There");
        EndWindows();
        Handles.EndGUI();
        //GUI.EndScrollView();
    }

    private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        offset += drag * 0.5f;
        Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

        for (int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
        }

        for (int j = 0; j < heightDivs; j++)
        {
            Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
        }

        Handles.color = Color.white;
    }

    private void DrawCity(){

        Rect cityRect = new Rect(new Vector2(position.width / 2 + (menuSize - 30) / 2, position.height / 2 - 20), new Vector2(30, 30));
        EditorGUI.DrawRect(cityRect, new Color(0f, 0.5f, 0f, 0.3f));

        foreach(BuildingNode b in city.getCityMap()){
            Debug.Log(cityRect.position.x + b.getLocation().x + "\t" + cityRect.position.y + b.getLocation().y);
            EditorGUI.DrawRect(new Rect(new Vector2(cityRect.position.x + b.getLocation().x, cityRect.position.y + b.getLocation().y), 
                                new Vector2(15, 15)), new Color(0.5f, 0.5f, 0f, 0.3f));
        }
    }

    private void DoWindow(int unusedWindowID){
        GUILayout.Button("" + position.width + "");
    }
}