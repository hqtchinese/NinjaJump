using System;

namespace GameBase
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ResourceAttribute : Attribute
    {
        public string ResPath { get; set; }
        public ResourceAttribute(string _resPath)
        {
            ResPath = _resPath;
        }
    }
}
