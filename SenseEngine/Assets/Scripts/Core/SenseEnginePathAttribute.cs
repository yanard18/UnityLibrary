using System;
using System.Linq;

namespace DenizYanar.External.Sense_Engine.Scripts.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SenseEnginePathAttribute : Attribute
    {
        public string SensePath;
        public string SenseName;

        public SenseEnginePathAttribute(string path)
        {
            SensePath = path;
            SenseName = path.Split('/').Last();
        }

        static public string GetSenseName(Type type)
        {
            SenseEnginePathAttribute attribute = type.GetCustomAttributes(false).OfType<SenseEnginePathAttribute>().FirstOrDefault();
            return attribute != null 
                ? attribute.SenseName 
                : type.Name;
        }

        static public string GetSensePath(Type type)
        {
            SenseEnginePathAttribute attribute = type.GetCustomAttributes(false).OfType<SenseEnginePathAttribute>().FirstOrDefault();
            return attribute != null 
                ? attribute.SensePath 
                : type.Name;
        }
    }
}
