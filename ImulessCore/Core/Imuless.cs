using System;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using Umbraco.Core.Logging;
using System.Configuration;

using Imuless.Models;

namespace Imuless.Core
{
    public static class ImulessCore
    {
        private static string outputDirectory = ConfigurationManager.AppSettings["imuless:outputDirectory"];
        private static string workingDirectory = ConfigurationManager.AppSettings["imuless:workingDirectory"];
        private static string varsDirectory = ConfigurationManager.AppSettings["imuless:domainVarsDirectory"];
        private static string rootLessFile = ConfigurationManager.AppSettings["imuless:rootLessFile"];

        public static void Compile(ImulessModel imulessModel, IEnumerable<string> domainList)
        {
            _compile(imulessModel, domainList, outputDirectory, workingDirectory);
        }

        public static void Compile(ImulessModel imulessModel, IEnumerable<string> domainList, string outputDirectory)
        {
            _compile(imulessModel, domainList, outputDirectory, workingDirectory);
        }

        public static void Compile(ImulessModel imulessModel, IEnumerable<string> domainList, string outputDirectory, string workingDirectory)
        {
            _compile(imulessModel, domainList, outputDirectory, workingDirectory);
        }

        private static void _compile(ImulessModel imulessModel, IEnumerable<string> domainList, string outputDirectory, string workingDirectory)
        {
            Debugger("Begin compile method...");

            var context = HttpContext.Current;

            if (context == null || imulessModel == null || !domainList.Any())
                return;

            //go thru each hostname for this tree
            foreach (var domain in domainList)
            {
                Debugger("Handling: " + domain);

                //figure selected theme path
                var themeFilePath = context.Server.MapPath(workingDirectory + "/" + imulessModel.Theme + "/" + rootLessFile);

                //write an override file
                var overrideFilePath = context.Server.MapPath(workingDirectory + "/" + varsDirectory + "/" + domain + ".less");
                File.WriteAllText(overrideFilePath, GetOverrideVariables(imulessModel), Encoding.UTF8);
             
                //get selected theme application.less file
                
                if (File.Exists(themeFilePath))
                {
                    //add import to the selected theme 
                    var fileLines = File.ReadAllLines(themeFilePath);

                    if (fileLines.Last().Contains(varsDirectory))
                    {
                        var cleanedFile = fileLines.ToList().GetRange(0, fileLines.Length - 2);
                        cleanedFile.Add("");
                        cleanedFile.Add(string.Format("@import '../{0}/{1}.less';", varsDirectory, domain));

                        File.WriteAllLines(themeFilePath, cleanedFile);
                    }
                    else
                    {
                        File.AppendAllText(themeFilePath, string.Format("@import '../{0}/{1}.less';", varsDirectory, domain));
                    }

                    //run compile   
                    var outputFilePath = context.Server.MapPath(outputDirectory + "/" + domain + ".css");

                    RunCommand(themeFilePath, outputFilePath);
                }
                else
                {
                    Debugger("Could not find theme: " + themeFilePath);
                    return;
                }
            }
        }

        private static string GetOverrideVariables(ImulessModel model)
        {
            if (!model.Vars.Any())
            {
                return "";
            }

            var sb = new StringBuilder();

            foreach (var _var in model.Vars)
            {
                if (!string.IsNullOrWhiteSpace(_var.Alias) && !string.IsNullOrWhiteSpace(_var.Value))
                {
                    sb.AppendFormat(@"@{0}: {1};" + Environment.NewLine, _var.Alias, _var.Value);
                }
            }

            return sb.ToString();
        }

        private static void Debugger(string message)
        {
            if (ConfigurationManager.AppSettings["imuless:debug"] == "true")
            {
                LogHelper.Info<ImulessModel>(message);
            }
        }

        private static void RunCommand(string inputFilePath, string outputFilePath)
        {
            try
            {
                var process = new Process();

                process.StartInfo.FileName = ConfigurationManager.AppSettings["imuless:pathToLessc"];
                process.StartInfo.Arguments = string.Format("\"{0}\" > \"{1}\"", inputFilePath, outputFilePath);

                Debugger(process.StartInfo.Arguments);

                process.StartInfo.RedirectStandardOutput = false;
                process.StartInfo.UseShellExecute = true;

                process.Start();
            }
            catch (Exception ex)
            {
                LogHelper.Info<ImulessModel>(ex.Message);
            }
        }
    }
}
