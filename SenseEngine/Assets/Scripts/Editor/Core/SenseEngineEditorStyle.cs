using UnityEngine;
using UnityEditor;

namespace DenizYanar.SenseEngine
{
    public static class SenseEngineEditorStyle
    {
        public static void DrawHeader(ref bool expanded,int senseId, string title, Color headerColor, System.Action<int> removeSense)
		{
            Event e = Event.current;

            // Initialize Rects
            var backgroundRect = GUILayoutUtility.GetRect(1f, 34f);

 
            var labelRect = backgroundRect;
            labelRect.xMin += 20f;
            labelRect.xMax -= 20f;


            

            Vector2 deleteRectSize = Vector2.one * 10;
            float deleteRectYPos = backgroundRect.yMin + (backgroundRect.height / 2) - (deleteRectSize.y / 2);
            var deleteRect = new Rect(labelRect.xMax + 4f ,deleteRectYPos, deleteRectSize.x, deleteRectSize.y);


            var colorRect = new Rect(labelRect.xMin, labelRect.yMin, 5f, 17f);
            colorRect.xMin = 0f;
            colorRect.xMax = 5f;

            // Background rect should be full-width
            backgroundRect.xMin = 0f;
            backgroundRect.width += 4f;

            EditorGUI.DrawRect(backgroundRect, headerColor);

            EditorGUI.LabelField(deleteRect, "X");


            // Title ----------------------------------------------------------------------------------------------------

            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold};

            EditorGUI.LabelField(labelRect, title, style);

            // Events

            if(e.type == EventType.MouseDown && e.button == 0 && deleteRect.Contains(e.mousePosition))
			{
                expanded = false;
                removeSense.Invoke(senseId);
                e.Use();
			}

            if (e.type == EventType.MouseDown && labelRect.Contains(e.mousePosition) && e.button == 0)
            {
                expanded = !expanded;
                e.Use();
            }

        }
    }
}
