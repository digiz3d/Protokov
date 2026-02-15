using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ThumbnailGenerationSettings))]
public class ThumbnailGenerationSettingsEditor : Editor
{
    private const float PREVIEW_SIZE = 200f;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ThumbnailGenerationSettings settings = (ThumbnailGenerationSettings)target;

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Thumbnail Preview", EditorStyles.boldLabel);

        // Generate button
        if (GUILayout.Button("Generate Thumbnail Preview", GUILayout.Height(30)))
        {
            settings.GenerateThumbnailPreview();
        }

        EditorGUILayout.Space(5);

        // Get the thumbnail preview field
        SerializedProperty previewProp = serializedObject.FindProperty("thumbnailPreview");
        
        if (previewProp.objectReferenceValue != null)
        {
            RenderTexture preview = (RenderTexture)previewProp.objectReferenceValue;
            
            // Draw the texture preview
            Rect previewRect = EditorGUILayout.GetControlRect(GUILayout.Height(PREVIEW_SIZE), GUILayout.Width(PREVIEW_SIZE));
            GUI.DrawTexture(previewRect, preview, ScaleMode.ScaleToFit);
            
            EditorGUILayout.HelpBox("Thumbnail preview generated successfully!", MessageType.Info);
        }
        else
        {
            EditorGUILayout.HelpBox("Click 'Generate Thumbnail Preview' to create a preview.", MessageType.Warning);
        }
    }
}
