using Behaviours.Grid;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(MapTileLayoutSettings))]
    public class MapTileLayoutSettingsEditor : UnityEditor.Editor
    {
        private MapTileLayoutSettings data;

        private TileType currentBrush = TileType.Road;

        private const float CellSize = 30;

        private void OnEnable()
        {
            data = (MapTileLayoutSettings)target;

            if (data.Tiles == null ||
                data.Tiles.Length != data.Width * data.Height)
            {
                data.Resize();
            }
        }

        public override void OnInspectorGUI()
        {
            DrawSizeControls();

            EditorGUILayout.Space();

            currentBrush = (TileType)EditorGUILayout.EnumPopup(
                "Brush",
                currentBrush);

            EditorGUILayout.Space();

            DrawGrid();
        }

        private void DrawSizeControls()
        {
            EditorGUI.BeginChangeCheck();

            int width = EditorGUILayout.IntField("Width", data.Width);
            int height = EditorGUILayout.IntField("Height", data.Height);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(data, "Resize Grid");

                data.Width = Mathf.Max(1, width);
                data.Height = Mathf.Max(1, height);

                data.Resize();

                EditorUtility.SetDirty(data);
            }
        }

        private void DrawGrid()
        {
            for (int y = 0; y < data.Height; y++)
            {
                EditorGUILayout.BeginHorizontal();

                for (int x = 0; x < data.Width; x++)
                {
                    DrawCell(x, y);
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawCell(int x, int y)
        {
            int index = data.GetIndex(x, y);

            Color oldColor = GUI.backgroundColor;
            GUI.backgroundColor = GetColor(data.Tiles[index]);

            if (GUILayout.Button(
                    "",
                    GUILayout.Width(CellSize),
                    GUILayout.Height(CellSize)))
            {
                Undo.RecordObject(data, "Paint Cell");

                data.Tiles[index] = currentBrush;

                EditorUtility.SetDirty(data);
            }

            GUI.backgroundColor = oldColor;
        }

        private Color GetColor(TileType type)
        {
            return type switch
            {
                TileType.Road => Color.yellow,
                TileType.Ground => Color.green,
                TileType.EnemySpawner => Color.red,
                TileType.Center => Color.blue,
                _ => Color.white
            };
        }
    }
}