# WORK IN PROGRESS #

# XMaterial - Extend material properties
XMaterial allows you to extend material assets with additional properties.
# Create XMaterial
To create your XMaterial, simply select the material you want to extend and click the "Generate XMaterial properties" button.
## Use XMaterial properties
To use the new properties from XMaterial, execute the following method:

    Material material;
    if(material.TryGetXMaterialProperties(out XMaterial xmaterial))
    {
        var myProperty = xmaterial.YourPropertyName;
    }
## Add XMaterial properties
To add additional properties, open the XMaterial.cs file and add your properties:

    public partial class XMaterial
    {
        // Put here your properties
        public string SurfaceType;
        public string GlossRange;
        public string Usage;
    }
Then, in your code, you can access them like this:
    
    // Example with SurfaceType
    Material material;
    if(material.TryGetXMaterialProperties(out XMaterial xmaterial))
    {
        string surfaceTypeName = xmaterial.SurfaceType;
    }
    