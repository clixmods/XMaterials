using UnityEngine;

namespace XMaterial
{
    public static class XMaterialUtility
    {
        public static bool TryGetXMaterialProperties(this Material material, out XMaterial xMaterial)
        {
            if (XMaterialsData.XMaterialsDictionary.TryGetValue(material, out xMaterial))
            {
                return true;
            }
            return false;
        }
        public static XMaterial XMaterialProperties(this Material material)
        {
            if (material.TryGetXMaterialProperties(out XMaterial xMaterial))
            {
                return xMaterial;
            }
            return xMaterial;
        }
        
    }
}