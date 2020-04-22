using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using GameBase.Tool;
using GameBase.Util;

namespace GameBase
{
    public class ToolsCenter : EditorWindow
    {
        private string keyText = "Player";
        private string excelPath = "D://test.xlsx";
        private string excelDir = "D://sss";

        private ScriptableObject obj;
        private bool fold = true;
        private List<ScriptableObject> assetList = new List<ScriptableObject>();

        [MenuItem ("MyHelpers/ToolsWindow")]
        public static void ShowWindow()
        {       
            //创建窗口
            ToolsCenter window = (ToolsCenter)EditorWindow.GetWindow(typeof(ToolsCenter),true,"ToolsCenter");
            window.Show();
            
            string path = CustomDataUtil.GetVal("ExcelPath");
            if(path != null)
                window.excelPath = path;

            string dir = CustomDataUtil.GetVal("ExcelDir");
            if(dir != null)
                window.excelDir = dir;

            string paths = CustomDataUtil.GetVal("AutoExcelPaths");
            window.assetList = new List<ScriptableObject>();
            if(paths != null && paths.Length > 0)
            {
                string[] pathArray = paths.Split('@');
                for (int i = 0; i < pathArray.Length; i++)
                {
                    ScriptableObject sobj = Resources.Load<ScriptableObject>(pathArray[i]);
                    if(sobj != null)
                    {
                        window.assetList.Add(sobj);
                    }
                }
            }
        }
        
        public void OnDestroy() 
        {
            CustomDataUtil.SetKV("ExcelPath",excelPath);
            CustomDataUtil.SetKV("ExcelDir",excelDir);

            string saveStr = "";
            for (int i = 0; i < assetList.Count; i++)
            {
                if(assetList[i] != null)
                {
                    string assetPath = AssetDatabase.GetAssetPath(assetList[i].GetInstanceID());
                    int startIndex = assetPath.IndexOf("Resources/") + 10;
                    int endIndex = assetPath.LastIndexOf('.');
                    saveStr += assetPath.Substring(startIndex,endIndex - startIndex);
                    if(i < assetList.Count - 1)
                    {
                        saveStr += "@";
                    }
                }
            }
            CustomDataUtil.SetKV("AutoExcelPaths",saveStr);
            Debug.Log(saveStr);

            CustomDataUtil.SaveData();
        }

        public void OnGUI()
        {
            GUILayout.Label("Local Data");
            DrawSaveDataGUI();

            EditorGUILayout.Separator();
            GUILayout.Space(30);

            GUILayout.Label("Excel");
            DrawExcelFuncGUI();
        }

        public void DrawSaveDataGUI()
        {
            keyText = EditorGUILayout.TextField("Key",keyText,GUILayout.Width(300));
            GUILayout.BeginHorizontal();
            if(GUILayout.Button("Read Key",GUILayout.Width(100)))
            {
                ReadKeyInfo(keyText);
            }

            if(GUILayout.Button("Delete Key",GUILayout.Width(100)))
            {
                DeleteKey(keyText);
            }

            if(GUILayout.Button("Delete All Key",GUILayout.Width(100)))
            {
                DeleteAllKey();
            }
            GUILayout.EndHorizontal();
        }

        public void DrawExcelFuncGUI()
        {
            obj = (ScriptableObject)EditorGUILayout.ObjectField("Asset",obj,typeof(ScriptableObject),true);
            excelPath = EditorGUILayout.TextField("Path",excelPath,GUILayout.Width(300));

            if(GUILayout.Button("ExportExcel"))
            {
                ExportExcel(obj,excelPath);
            }

            if(GUILayout.Button("ImportExcel"))
            {
                ImportExcel(excelPath,obj);
            }
            
            fold = EditorGUILayout.Foldout(fold, "Settings");
            if(fold)
            {
                EditorGUI.indentLevel = 1;
                for (int i = 0; i < assetList.Count; i++)
                {
                    assetList[i] = (ScriptableObject)EditorGUILayout.ObjectField("Asset",assetList[i],typeof(ScriptableObject),true);
                }
                
                excelDir = EditorGUILayout.TextField("Dir",excelDir,GUILayout.Width(300));
                if(GUILayout.Button("Add"))
                {
                    assetList.Add(null);
                }
                if(GUILayout.Button("Export"))
                {
                    ExportAllExcel(excelDir);
                }
                if(GUILayout.Button("Import"))
                {
                    ImportAllExcel(excelDir);
                }
                EditorGUI.indentLevel = 0;
            }
        }

        public void ExportExcel(ScriptableObject sobj,string path)
        {
            Type type = sobj.GetType();
            FieldInfo[] fields = type.GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                if(fields[i].IsPublic)
                {
                    object value = fields[i].GetValue(sobj);
                    if(value is IList)
                    {
                        ExcelHandler.ExportListToExcel(value as IList,path);
                        break;
                    }
                }
            }
        }

        public void ImportExcel(string path,ScriptableObject sobj)
        {
            Type type = sobj.GetType();
            FieldInfo[] fields = type.GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                if(fields[i].IsPublic)
                {
                    object value = fields[i].GetValue(sobj);
                    if(value is IList)
                    {
                        try
                        {
                            IList tempList = Activator.CreateInstance(value.GetType()) as IList;
                            IList list = value as IList;
                            ExcelHandler.LoadExcelToList(path,tempList);
                            list.Clear();
                            for (int j = 0; j < tempList.Count; j++)
                            {
                                list.Add(tempList[j]);
                                
                            }
                            EditorUtility.SetDirty(sobj);
                        }
                        catch (System.Exception e)
                        {
                            Debug.LogError(e);
                            break;
                        }
                        break;
                    }
                }
            }
            
            AssetDatabase.SaveAssets();
        }

        public void ExportAllExcel(string dir)
        {
            for (int i = 0; i < assetList.Count; i++)
            {
                if(assetList[i] == null)
                    continue;
                
                if(!dir.EndsWith("/"))
                {
                    dir += '/';
                }
                string path = dir + assetList[i].name + ".xlsx";

                Debug.Log($"开始导出{assetList[i].name},目录{path}");
                ExportExcel(assetList[i],path);
            }
        }

        public void ImportAllExcel(string dir)
        {
            for (int i = 0; i < assetList.Count; i++)
            {
                if(assetList[i] == null)
                    continue;
                
                if(!dir.EndsWith("/"))
                {
                    dir += '/';
                }
                string path = dir + assetList[i].name + ".xlsx";

                Debug.Log($"开始导入{assetList[i].name},目录:{path}");
                ImportExcel(path,assetList[i]);
            }
        }

        public void ReadKeyInfo(string key)
        {
            object obj = SerializeUtil.LoadFromPlayerPref(key);
            if(obj == null)
            {
                Debug.Log("The value of this key is null");
                return;
            }
            if(obj is ValueType)
            {
                Debug.Log($"{key} : {obj}");
                return;
            }

            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                if(fields[i].IsPublic)
                {
                    object val = fields[i].GetValue(obj);
                    
                    if(val == null)
                        Debug.Log($"{fields[i].Name} : null");
                    else if(val is ICollection)
                        Debug.Log($"{fields[i].Name} : has {(val as ICollection).Count} elements");
                    else
                        Debug.Log($"{fields[i].Name} : {val}");
                }
            }
        }

        public void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
            Debug.Log($"Delete key '{key}' success");
        }

        public void DeleteAllKey()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Delete success");
        }
    }


}