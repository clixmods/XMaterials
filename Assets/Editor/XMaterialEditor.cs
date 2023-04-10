using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using XEditor.Editor;

namespace XMaterial.Editor
{ [CustomEditor(typeof (Material))]
public class XMaterialEditor : MaterialEditor
{
    private XMaterial _xMaterial;
    private static string[] tagsMaterialsType;
    private UnityEditor.Editor _editor;
    private bool _foldSO;
    public override void OnEnable()
    {
        UpdateTags();
        var xMaterials = GetXMaterialFromMaterial(target.name);
        if (xMaterials.Length != 0)
        {
            _xMaterial = xMaterials[0];
        }
        base.OnEnable();
    }

    public static void UpdateTags()
    { 
        tagsMaterialsType = XEditorUtility.GetTags("MaterialType/");
    }

    public override void OnInspectorGUI()
    {
        if (_xMaterial != null)
        {
            var serialize = new SerializedObject(_xMaterial);
            _foldSO = EditorGUILayout.InspectorTitlebar(_foldSO, _xMaterial, false);
            if (_foldSO)
            {
                CreateCachedEditor(_xMaterial, null, ref _editor);
                _editor.OnInspectorGUI();
                serialize.ApplyModifiedProperties();
            }
        }
        else
        {
            if (GUILayout.Button("Generate XMaterial properties"))
            {
                XMaterial mySO = XMaterial.CreateInstance((Material)target);
                string assetPath = AssetDatabase.GetAssetPath(target);
                if (assetPath != String.Empty)
                {
                    string parentFolder = Path.GetDirectoryName(assetPath);
                    Debug.Log(AssetDatabase.GetAssetPath(target));
                    AssetDatabase.CreateAsset(mySO, $"Assets/XMaterials/{target.name}.asset");
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
        }
        // Afficher le contenu par défaut de l'inspecteur Unity
        base.OnInspectorGUI();
    }
    
    
    public static XMaterial[] GetXMaterialFromMaterial(string name)
    {
        string searchFilter = $"t:{nameof(XMaterial)} {name}";
        string[] assetGUIDs = AssetDatabase.FindAssets(searchFilter);
        int count = assetGUIDs.Length;
        XMaterial[] a = new XMaterial[count];
        for (int i = 0; i < count; i++)         //probably could get optimized 
        {
            string path = AssetDatabase.GUIDToAssetPath(assetGUIDs[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<XMaterial>(path);
        }

        return a;
    }


    private static void DrawXMaterialProperty(SerializedObject serialize, string propertyPath)
    {
        var tagProp = serialize.FindProperty(propertyPath);
        int selectedTag = XEditorUtility.GetIndexFromNameTag(tagsMaterialsType, tagProp.stringValue);
        selectedTag = EditorGUILayout.Popup(propertyPath, selectedTag, tagsMaterialsType);
        if (selectedTag == tagsMaterialsType.Length - 1) // Is Add Tag field
        {
            EditorWindow.GetWindow<AddTagWindow>();
            UpdateTags();
        }
        else
        {
            if (selectedTag == 0)
                tagProp.stringValue = string.Empty;
            else
                tagProp.stringValue = tagsMaterialsType[selectedTag];
        }
    }
}

}