#define XMATERIAL
using System;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
using XEditor.Editor;
#endif

namespace XMaterial
{
    public partial class XMaterial : ScriptableObject
    {
        [HideInInspector]
        [SerializeField]
        private Material _material;

        public Material material => _material;

        private void OnEnable()
        {
            
        }
#if UNITY_EDITOR
        public static XMaterial CreateInstance(Material material)
        {
            XMaterial xMaterial = ScriptableObject.CreateInstance<XMaterial>();
            xMaterial._material = material;
            return xMaterial;
        }
        public void OnValidate()
        {
            if (material == null)
            {
                Debug.LogWarning("Warning, if you want create a XMaterial Asset use XMaterial.CreateInstance(Material material)");
                #if UNITY_EDITOR
                                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(this));
                #endif
            }
            var xMaterialsData = Resources.Load<XMaterialsData>("XMaterials");
            if (xMaterialsData == null)
            {
                CreateXMaterialsData();
                xMaterialsData = Resources.Load<XMaterialsData>("XMaterials");
            }
            if(!xMaterialsData.xMaterials.Contains(this))
                xMaterialsData.xMaterials.Add(this);
        }
        void CreateXMaterialsData()
        {
            string path = $"Assets/XMaterials/Resources/XMaterials.asset";
            
            var assetXMaterialData = AssetDatabase.LoadAssetAtPath<XMaterialsData>(path);
            if (!string.IsNullOrEmpty(path) && assetXMaterialData == null )
            {
                AssetDatabase.CreateAsset(CreateInstance<XMaterialsData>(), path);
                AssetDatabase.Refresh();
            }
        }
#endif
       
    }
}
