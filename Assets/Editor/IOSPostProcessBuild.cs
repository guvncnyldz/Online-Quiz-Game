using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class IOSPostProcessBuild : MonoBehaviour
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string path)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            //Modify PList File and add AdMob ID
            string plistPath = path + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            PlistElementDict rootDict = plist.root;

            string buildKey = "CFBundleVersion";
            rootDict.SetString(buildKey, "2.2");
            rootDict.SetString("CFBundleShortVersionString", "0.2");

            rootDict["GADApplicationIdentifier"] = new PlistElementString("ca-app-pub-8277769580123099~5627625490");

            PlistElementDict nsAppTransportSecurityDict = rootDict["NSAppTransportSecurity"].AsDict();
            nsAppTransportSecurityDict["NSAllowsArbitraryLoads"] = new PlistElementBoolean(true);

            rootDict["NSAppTransportSecurity"] = nsAppTransportSecurityDict;

            File.WriteAllText(plistPath, plist.WriteToString());


            //Add AdSupport.framework
            string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";

            PBXProject proj = new PBXProject();
            proj.ReadFromString(File.ReadAllText(projPath));

            string target = proj.GetUnityMainTargetGuid();

            proj.AddFrameworkToProject(target, "AdSupport.framework", true);

            File.WriteAllText(projPath, proj.WriteToString());
        }
    }
}