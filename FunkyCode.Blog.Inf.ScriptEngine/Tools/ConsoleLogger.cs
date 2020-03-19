using System;
using System.Collections.Generic;
using System.Text;
using FunkyCode.Blog.App.Core;
using YamlDotNet.Serialization;

namespace FunkyCode.Blog.ScriptEngine.Tools
{
    public class ConsoleLogger<T> : IBlogEngineLogger<T>
    {
        private readonly Serializer _yamlSerializer = new Serializer();

        public void Info(string info)
        {
            Console.WriteLine(info);
        }

        public void Exception(Exception exc)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(exc.Message);
            Console.ResetColor();
        }

        public void Warning(string warning)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(warning);
            Console.ResetColor();
        }

        public void Error(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        public void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void Object(string objName, object obj)
        {
            Console.WriteLine(objName);
            var yaml = _yamlSerializer.Serialize(obj);
            Console.WriteLine(yaml);
        }
    }
}
