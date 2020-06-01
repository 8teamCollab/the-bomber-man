using UnityEditor;
using UnityEngine;


public class TimeScaler : EditorWindow
{
    [MenuItem("Tools/TimeScaler &T")]


    public static void TimeScale()
    {
        TimeScaler window = GetWindow<TimeScaler>();
    }


     public void OnGUI() => Time.timeScale = EditorGUILayout.Slider("Time Scale", Time.timeScale, 0f, 2f);
}
