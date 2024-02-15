using System.Collections.Generic;
using FEngine;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
namespace FEngineEditor
{
   
    
    public class SvnTools : EditorWindow
    {
        [MenuItem("Window/SVN Tools")]
        private static void ShowWindow()
        {
            var window = GetWindow<SvnTools>("SVN Tools");
            window.Show();
        }

      
        public static string SVNProjectPath = Application.dataPath + "/../";
        
        //TODO:F5如何绑定？现在不行..........
        [MenuItem(" F5 keys")]
        public void Commit()
        {
            Debug.Log("F5 keys");
            //RunSVNCommand("commit", "F5 commit", path);
        }
        
        // [MenuItem(" _F6")]
        // public void PullAll()
        // {
        //     RunSVNCommand("update", "F6 pull all", path);
        // }
        //
        // [MenuItem(" _F7")]
        // public void PushAll()
        // {
        //     RunSVNCommand("commit --depth=infinity --message \"F7 push all\"", "F7 push all", path);
        // }
        
        
        [MenuItem("SVN/提交整个项目",false,1)]
        static void SVNCommit(){
            List<string> pathList = new List<string> ();
            // 工程路径
            string projectPath = Application.dataPath;//C:/Users/侯超孟/UnityProject/ECSDemo/Assets
            //Packages路径
            string packagesPath = Application.dataPath + "/../Packages";//C:/Users/侯超孟/UnityProject/ECSDemo/Assets/../Packages
            
            pathList.Add (projectPath);
            pathList.Add (packagesPath);
            string commitPath = string.Join ("*", pathList.ToArray ());
            ProcessCommand ("TortoiseProc.exe", "/command:commit /path:"+ commitPath);
        }
        [MenuItem("SVN/提交FEngine",false,1)]
        static void SVNCommitFEngine(){

            ProcessCommand ("TortoiseProc.exe", "/command:commit /path:"+ Application.dataPath+"/FEngine/");
        }
        [MenuItem("SVN/更新FEngine",false,1)]
        static void SVNUpdateFEngine(){

            ProcessCommand ("TortoiseProc.exe", "/command:update /path:"+ Application.dataPath+"/FEngine/");
        }
        [MenuItem("SVN/更新",false,2)]
        static void SVNUpdate(){
            ProcessCommand ("TortoiseProc.exe", "/command:update /path:" + SVNProjectPath + " /closeonend:0");
        }
	
        [MenuItem("SVN/", false, 3)]
        static void Breaker () { }
	
        [MenuItem("SVN/清理缓存",false,4)]
        static void SVNCleanUp(){
            ProcessCommand ("TortoiseProc.exe", "/command:cleanup /path:" + SVNProjectPath);
        }
	
        [MenuItem("SVN/打开日志",false,5)]
        static void SVNLog(){
            ProcessCommand ("TortoiseProc.exe", "/command:log /path:" + SVNProjectPath);
        }
        //SVN提交
        public static void ProcessCommand(string command, string argument)
        {
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(command);
            info.Arguments = argument;
            info.CreateNoWindow = false;
            info.ErrorDialog = true;
            info.UseShellExecute = true;

            if (info.UseShellExecute)
            {
                info.RedirectStandardOutput = false;
                info.RedirectStandardError = false;
                info.RedirectStandardInput = false;
            }
            else
            {
                info.RedirectStandardOutput = true;
                info.RedirectStandardError = true;
                info.RedirectStandardInput = true;
                info.StandardOutputEncoding = System.Text.UTF8Encoding.UTF8;
                info.StandardErrorEncoding = System.Text.UTF8Encoding.UTF8;
            }

            System.Diagnostics.Process process = System.Diagnostics.Process.Start(info);

            if (!info.UseShellExecute)
            {
                UnityEngine.Debug.Log(process.StandardOutput);
                UnityEngine.Debug.Log(process.StandardError);
            }

            process.WaitForExit();
            process.Close();
        }
    }
}