# XMaterial - Extend material properties
XMaterial permet d'étendre les assets material avec des propriétés supplémentaire.
# Create XMaterial
Pour créer votre XMaterial, il suffit de selectionner le material que vous souhaitez étendre et de
cliquer sur le bouton "Generate XMaterial properties".
## Use XMaterial properties
Pour utiliser les nouvelles propriétés provenant de XMaterial, il suffit d'executer la méthode suivante

    Material material;
    if(material.GetXMaterialProperties(out XMaterial xmaterial))
    {
        var myProperty = xmaterial.YourPropertyName;
    }
## Add XMaterial properties
Pour ajouter des propriétés supplémentaires, ouvrez le fichier **XMaterial.cs** et ajoutez y vos propriétés :

    public partial class XMaterial
    {
        // Put here your properties
        public string SurfaceType;
        public string GlossRange;
        public string Usage;
    }
Ensuite dans votre code, vous y aurez accès comme ceux-ci :
    
    // Example with SurfaceType
    Material material;
    if(material.TryGetXMaterialProperties(out XMaterial xmaterial))
    {
        string surfaceTypeName = xmaterial.SurfaceType;
    }
    