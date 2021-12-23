using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;

public class InfoPlistUpdater : IPostprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }
    
    public void OnPostprocessBuild(BuildReport report)
    {
        BuildTarget buildTarget = report.summary.platform;
        string pathToBuiltProject = report.summary.outputPath;

        if (buildTarget == BuildTarget.iOS)
        {
            {
                SetPlist(pathToBuiltProject);
            }
        }
    }
     public void SetPlist(string path)
    {
        string plistPath = path + "/Info.plist";
        PlistDocument plist = new PlistDocument();
        plist.ReadFromString(File.ReadAllText(plistPath));
        
        PlistElementArray lsApplicationQueriesSchemes = plist.root.CreateArray("LSApplicationQueriesSchemes");
        {
            lsApplicationQueriesSchemes.AddString("kakaotalk");
            lsApplicationQueriesSchemes.AddString("itms-apps");
        }
        
        File.WriteAllText(plistPath, plist.WriteToString());
    }
}


