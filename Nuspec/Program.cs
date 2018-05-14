using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Nuspec
{
    class Program
    {
        static int Main(string[] args)
        {
            var targetFolder = ConfigurationManager.AppSettings["TargetFolder"];
            if (string.IsNullOrEmpty(targetFolder))
            {
                Console.WriteLine("缺失TargetFolder配置，请检查app.config");
                return -1;
            }
            Console.WriteLine("TargetFolder is " + targetFolder);

            if (args == null || args.Length == 0)
            {
                Console.WriteLine("请指定需要处理的nuspec文件");
                return -1;
            }
            var path = args[0];

            XElement root = XElement.Load(path);

            string id = root.Element("metadata").Element("id").Value;
            var version = root.Element("metadata").Element("version").Value;

            var targetPath = Path.Combine(targetFolder, id + "_" + version + ".nuspec");

            if (File.Exists(targetPath))
            {
                Console.WriteLine("文件已存在，正在进行覆盖。");
                File.Copy(path, targetPath, true);
            }
            else
            {
                Console.WriteLine("正在拷贝文件。");
                File.Copy(path, targetPath);
            }

            return 0;
        }
    }
}
