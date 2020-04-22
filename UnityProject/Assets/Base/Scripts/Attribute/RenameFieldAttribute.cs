using System;
using UnityEngine;
using UnityEditor;

namespace GameBase.Editor
{
    [AttributeUsage(AttributeTargets.Field)]
    public class RenameFieldAttribute : PropertyAttribute
    {
        public string FieldName { get; set; }
        
        public RenameFieldAttribute(string _name)
        {
            FieldName = _name;
        }
    }
}


