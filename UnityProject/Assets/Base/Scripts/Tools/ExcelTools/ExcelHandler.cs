using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace GameBase.Tool
{
    public class ExcelHandler 
    {
        public static void ExportExcel(object obj)
        {
            if(obj == null)
            {
                Debug.LogWarning("The object is null");
                return;
            }

            if(obj is ValueType)
            {
                Debug.LogWarning("Can't export value type");
                return;
            }

            XSSFWorkbook book = new XSSFWorkbook();
            
            using(FileStream fs = File.Create("H://test.xlsx"))
            {
                book.Write(fs);
                fs.Close();
            }
        }

        public static void ExportListToExcel(IList list,string path)
        {
            XSSFWorkbook book = new XSSFWorkbook();
            CreateSheetByList(book,list);
            using(FileStream fs = File.Create(path))
            {
                book.Write(fs);
                fs.Close();
                Debug.Log("Export Finish !");
            }
        }

        private static void CreateSheetByObj(XSSFWorkbook book,object obj,Dictionary<FieldInfo,ISheet> sheetMap)
        {
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields();

            ISheet sheet = book.CreateSheet(type.Name);
            IRow headerRow = sheet.CreateRow(0);
            IRow typeRow = sheet.CreateRow(1);
            IRow dataRow = sheet.CreateRow(2);

            int colCount = 0;
            //创建header和相关sheet
            for (int i = 0; i < fields.Length; i++)
            {
                if(fields[i].IsPublic)
                {
                    object value = fields[i].GetValue(obj);
                    Debug.Log(value.GetType().Name);
                    if(value is int)
                    {
                        ICell headerCell = headerRow.CreateCell(colCount + 1);
                        headerCell.SetCellValue(fields[i].Name);
                        ICell typeCell = typeRow.CreateCell(colCount + 1);
                        typeCell.SetCellValue("int");
                        ICell dataCell = dataRow.CreateCell(colCount + 1);
                        dataCell.SetCellValue((int)value);
                        colCount++;
                    }
                    else if(value is long)
                    {
                        ICell headerCell = headerRow.CreateCell(colCount + 1);
                        headerCell.SetCellValue(fields[i].Name);
                        ICell typeCell = typeRow.CreateCell(colCount + 1);
                        typeCell.SetCellValue("long");
                        ICell dataCell = dataRow.CreateCell(colCount + 1);
                        dataCell.SetCellValue((long)value);
                        colCount++;
                    }
                    else if(value is float)
                    {
                        ICell headerCell = headerRow.CreateCell(colCount + 1);
                        headerCell.SetCellValue(fields[i].Name);
                        ICell typeCell = typeRow.CreateCell(colCount + 1);
                        typeCell.SetCellValue("float");
                        ICell dataCell = dataRow.CreateCell(colCount + 1);
                        dataCell.SetCellValue((float)value);
                        colCount++;
                    }
                    else if(value is double)
                    {
                        ICell headerCell = headerRow.CreateCell(colCount + 1);
                        headerCell.SetCellValue(fields[i].Name);
                        ICell typeCell = typeRow.CreateCell(colCount + 1);
                        typeCell.SetCellValue("double");
                        ICell dataCell = dataRow.CreateCell(colCount + 1);
                        dataCell.SetCellValue((double)value);
                        colCount++;
                    }
                    else if(value is string)
                    {
                        ICell headerCell = headerRow.CreateCell(colCount + 1);
                        headerCell.SetCellValue(fields[i].Name);
                        ICell typeCell = typeRow.CreateCell(colCount + 1);
                        typeCell.SetCellValue("string");
                        ICell dataCell = dataRow.CreateCell(colCount + 1);
                        dataCell.SetCellValue(value as string);
                        colCount++;
                    }
                    else if(value is bool)
                    {
                        ICell headerCell = headerRow.CreateCell(colCount + 1);
                        headerCell.SetCellValue(fields[i].Name);
                        ICell typeCell = typeRow.CreateCell(colCount + 1);
                        typeCell.SetCellValue("bool");
                        ICell dataCell = dataRow.CreateCell(colCount + 1);
                        dataCell.SetCellValue(((bool)value) ? "true" : "false");
                        colCount++;
                    }
                    else if(value is IList)
                    {
                        ICell headerCell = headerRow.CreateCell(colCount + 1);
                        headerCell.SetCellValue(fields[i].Name);
                        ICell typeCell = typeRow.CreateCell(colCount + 1);
                        typeCell.SetCellValue("List");
                        ICell dataCell = dataRow.CreateCell(colCount + 1);
                        dataCell.SetCellValue("Link");
                        if(sheetMap.TryGetValue(fields[i],out ISheet listSheet))
                        {

                        }
                        colCount++;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

        }

        private static void CreateSheetByList(XSSFWorkbook book,IList list)
        {
            if(list.Count == 0)
            {
                Debug.LogWarning("The List didn't contains any element");
                return;
            }
            object obj = list[0];
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields();

            ISheet sheet = book.CreateSheet(type.Name);
            CreateHeaderRow(sheet,type);

            int rowCount = 2;
            foreach (var item in list)
            {
                IRow dataRow = sheet.CreateRow(rowCount++);
                int colCount = 0;
                for (int i = 0; i < fields.Length; i++)
                {
                    if(fields[i].IsPublic)
                    {
                        object value = fields[i].GetValue(item);
                        if(value is int)
                        {
                            ICell dataCell = dataRow.CreateCell(colCount++);
                            dataCell.SetCellValue((int)value);
                        }
                        else if(value is long)
                        {
                            ICell dataCell = dataRow.CreateCell(colCount++);
                            dataCell.SetCellValue((long)value);
                        }
                        else if(value is float)
                        {
                            ICell dataCell = dataRow.CreateCell(colCount++);
                            dataCell.SetCellValue((float)value);
                        }
                        else if(value is double)
                        {
                            ICell dataCell = dataRow.CreateCell(colCount++);
                            dataCell.SetCellValue((double)value);
                        }
                        else if(value is bool)
                        {
                            ICell dataCell = dataRow.CreateCell(colCount++);
                            dataCell.SetCellValue((bool)value);
                        }
                        else if(value is string)
                        {
                            ICell dataCell = dataRow.CreateCell(colCount++);
                            dataCell.SetCellValue(value as string);
                        }
                        else if(value is Enum)
                        {
                            ICell dataCell = dataRow.CreateCell(colCount++);
                            dataCell.SetCellValue(Enum.GetName(value.GetType(),value));
                        }
                        else if(value == null)
                        {
                            colCount++;
                        }
                    }
                }
            }

        }

        private static void CreateHeaderRow(ISheet sheet,Type type)
        {
            IRow headerRow = sheet.CreateRow(0);
            IRow typeRow = sheet.CreateRow(1);
            FieldInfo[] fields = type.GetFields();

            int colCount = 0;
            for (int i = 0; i < fields.Length; i++)
            {
                if(fields[i].IsPublic)
                {
                    Type valueType = fields[i].FieldType;
                    if(valueType == typeof(int))
                    {
                        ICell headerCell = headerRow.CreateCell(colCount);
                        headerCell.SetCellValue(fields[i].Name);
                        ICell typeCell = typeRow.CreateCell(colCount);
                        typeCell.SetCellValue("int");
                        colCount++;
                    }
                    else if(valueType == typeof(long))
                    {
                        ICell headerCell = headerRow.CreateCell(colCount);
                        headerCell.SetCellValue(fields[i].Name);
                        ICell typeCell = typeRow.CreateCell(colCount);
                        typeCell.SetCellValue("long");
                        colCount++;
                    }
                    else if(valueType == typeof(float))
                    {
                        ICell headerCell = headerRow.CreateCell(colCount);
                        headerCell.SetCellValue(fields[i].Name);
                        ICell typeCell = typeRow.CreateCell(colCount);
                        typeCell.SetCellValue("float");
                        colCount++;
                    }
                    else if(valueType == typeof(double))
                    {
                        ICell headerCell = headerRow.CreateCell(colCount);
                        headerCell.SetCellValue(fields[i].Name);
                        ICell typeCell = typeRow.CreateCell(colCount);
                        typeCell.SetCellValue("double");
                        colCount++;
                    }
                    else if(valueType == typeof(string))
                    {
                        ICell headerCell = headerRow.CreateCell(colCount);
                        headerCell.SetCellValue(fields[i].Name);
                        ICell typeCell = typeRow.CreateCell(colCount);
                        typeCell.SetCellValue("string");
                        colCount++;
                    }
                    else if(valueType == typeof(bool))
                    {
                        ICell headerCell = headerRow.CreateCell(colCount);
                        headerCell.SetCellValue(fields[i].Name);
                        ICell typeCell = typeRow.CreateCell(colCount);
                        typeCell.SetCellValue("bool");
                        colCount++;
                    }
                    else if(valueType.IsEnum)
                    {
                        ICell headerCell = headerRow.CreateCell(colCount);
                        headerCell.SetCellValue(fields[i].Name);
                        ICell typeCell = typeRow.CreateCell(colCount);
                        typeCell.SetCellValue("enum");
                        colCount++;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        public static void LoadExcelToList(string path,IList list)
        {
            using (FileStream fs = new FileStream(path,FileMode.Open))
            {
                XSSFWorkbook book = new XSSFWorkbook(fs);
                ISheet sheet = book.GetSheetAt(0);

                Type[] types = list.GetType().GenericTypeArguments;
                if(types.Length < 1)
                    return;

                Type itemType = types[0];
                FieldInfo[] itemFields = itemType.GetFields();
                
                IRow headerRow = sheet.GetRow(0);
                List<ICell> cellList = headerRow.Cells;

                //添加field和对应row的映射
                Dictionary<string,int> colDict = new Dictionary<string, int>();
                for (int i = 0; i < cellList.Count; i++)
                {
                    string fieldName = cellList[i].StringCellValue;
                    colDict.Add(fieldName,cellList[i].ColumnIndex);
                }

                if(sheet.LastRowNum < 2)
                {
                    Debug.Log("Can't find data in the sheet");
                    return;
                }

                for (int i = 2; i < sheet.LastRowNum + 1; i++)
                {
                    object obj = Activator.CreateInstance(itemType);
                    for (int j = 0; j < itemFields.Length; j++)
                    {
                        if(itemFields[j].IsPublic)
                        {
                            if(colDict.TryGetValue(itemFields[j].Name, out int colIndex))
                            {
                                string typeStr = sheet.GetRow(1).GetCell(colIndex).StringCellValue;
                                IRow curRow = sheet.GetRow(i);
                                ICell curCell = curRow.GetCell(colIndex);
                                if(curCell == null)
                                    continue;
                                
                                if(typeStr == "int")
                                {
                                    double numValue = curCell.NumericCellValue;
                                    itemFields[j].SetValue(obj,(int)numValue);
                                }
                                else if(typeStr == "long")
                                {
                                    double numValue = curCell.NumericCellValue;
                                    itemFields[j].SetValue(obj,(long)(numValue));
                                }
                                else if(typeStr == "float")
                                {
                                    double numValue = curCell.NumericCellValue;
                                    itemFields[j].SetValue(obj,(float)(numValue));
                                }
                                else if(typeStr == "double")
                                {
                                    double numValue = curCell.NumericCellValue;
                                    itemFields[j].SetValue(obj,numValue);
                                }
                                else if(typeStr == "bool")
                                {
                                    bool boolValue = curCell.BooleanCellValue;
                                    itemFields[j].SetValue(obj,boolValue);
                                }
                                else if(typeStr == "string")
                                {
                                    string strValue = curCell.StringCellValue;
                                    itemFields[j].SetValue(obj,strValue);
                                }
                                else if(typeStr == "enum")
                                {
                                    string strValue = curCell.StringCellValue;
                                    itemFields[j].SetValue(obj,Enum.Parse(itemFields[j].FieldType,strValue));
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    list.Add(obj);
                }
                Debug.Log("Load Finish !");
            }
        }

    }

}
