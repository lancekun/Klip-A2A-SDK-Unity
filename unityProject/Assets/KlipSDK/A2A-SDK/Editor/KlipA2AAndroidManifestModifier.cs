using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEditor.Android;

public class KlipSchemeData
{
    public string host;
    public string scheme;

    public KlipSchemeData(string scheme, string host)
    {
        this.scheme = scheme;
        this.host   = host;
    }
}

public class PatchKlipSchemeUnityAndroidAppManifest : IPostGenerateGradleAndroidProject
{
    public int callbackOrder { get { return 0; } }
    private string _manifestFilePath;

    public void OnPostGenerateGradleAndroidProject(string basePath)
    {
        var androidManifest = new AndroidManifest(GetManifestPath(basePath));

        List<KlipSchemeData> schemeDatas = new List<KlipSchemeData>();
        schemeDatas.Add(new KlipSchemeData("klipdemo", "request"));
        
        androidManifest.SetDeepLinkScheme(schemeDatas);
        androidManifest.Save();
    }

    private string GetManifestPath(string basePath)
    {
        if (string.IsNullOrEmpty(_manifestFilePath))
        {
            var pathBuilder = new StringBuilder(basePath);
            pathBuilder.Append(Path.DirectorySeparatorChar).Append("src");
            pathBuilder.Append(Path.DirectorySeparatorChar).Append("main");
            pathBuilder.Append(Path.DirectorySeparatorChar).Append("AndroidManifest.xml");
            _manifestFilePath = pathBuilder.ToString();
        }
        return _manifestFilePath;
    }
}

internal class AndroidXmlDocument : XmlDocument
{
    private string m_Path;
    protected XmlNamespaceManager nsMgr;
    public readonly string AndroidXmlNamespace = "http://schemas.android.com/apk/res/android";
    public AndroidXmlDocument(string path)
    {
        m_Path = path;
        using (var reader = new XmlTextReader(m_Path))
        {
            reader.Read();
            Load(reader);
        }
        nsMgr = new XmlNamespaceManager(NameTable);
        nsMgr.AddNamespace("android", AndroidXmlNamespace);
    }

    public string Save()
    {
        return SaveAs(m_Path);
    }

    public string SaveAs(string path)
    {
        using (var writer = new XmlTextWriter(path, new UTF8Encoding(false)))
        {
            writer.Formatting = Formatting.Indented;
            Save(writer);
        }
        return path;
    }
}


internal class AndroidManifest : AndroidXmlDocument
{
    private readonly XmlElement ApplicationElement;

    public AndroidManifest(string path) : base(path)
    {
        ApplicationElement = SelectSingleNode("/manifest/application") as XmlElement;
    }

    private XmlAttribute CreateAndroidAttribute(string key, string value)
    {
        XmlAttribute attr = CreateAttribute("android", key, AndroidXmlNamespace);
        attr.Value = value;
        return attr;
    }

    internal XmlNode GetActivityWithLaunchIntent()
    {
        return SelectSingleNode("/manifest/application/activity[intent-filter/action/@android:name='android.intent.action.MAIN' and " +
                "intent-filter/category/@android:name='android.intent.category.LAUNCHER']", nsMgr);
    }

    internal void SetDeepLinkScheme(List<KlipSchemeData> schemeDatas)
    {
        var unityActivity = GetActivityWithLaunchIntent();
        XmlElement interntFilter = CreateElement("intent-filter");
        
        XmlElement   viewAction = CreateElement("action");
        XmlAttribute viewAttribute = CreateAndroidAttribute("name", "android.intent.action.VIEW");
        viewAction.Attributes.Append(viewAttribute);
        interntFilter.AppendChild(viewAction);
        
        XmlElement   catDef = CreateElement("category");
        XmlAttribute catDefAttribute = CreateAndroidAttribute("name", "android.intent.category.DEFAULT");
        catDef.Attributes.Append(catDefAttribute);
        interntFilter.AppendChild(catDef);
        
        XmlElement   catBro = CreateElement("category");
        XmlAttribute catBroAttribute = CreateAndroidAttribute("name", "android.intent.category.BROWSABLE");
        catBro.Attributes.Append(catBroAttribute);
        interntFilter.AppendChild(catBro);
        
        foreach( var data in schemeDatas)
        {
            XmlElement   schemeData = CreateElement("data");
            XmlAttribute schemeAttribute = CreateAndroidAttribute("scheme", data.scheme);
            schemeData.Attributes.Append(schemeAttribute);
            XmlAttribute hostAttribute = CreateAndroidAttribute("host", data.host);
            schemeData.Attributes.Append(hostAttribute);
            interntFilter.AppendChild(schemeData);
        }
        
        unityActivity.AppendChild(interntFilter);
    }
}