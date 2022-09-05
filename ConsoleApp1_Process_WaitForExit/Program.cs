using System;
using System.Diagnostics;

namespace ConsoleApp1_Process_WaitForExit
{
    class Program
    {
        static void Main(string[] args)
        {
            string result = Execute(@"d:\dev\repos\BaseballLogger\Output\bin\Debug\Synergy.BaseballLogger.Win32App\vidsdk\ffprobe.exe",
                                    @"-hide_banner -show_format -show_streams -pretty C:\Users\tamas\Downloads\IMG_0632.MOV");

            //string result = Execute(@"d:\dev\!vsprojs\!PoC\ConsoleApp1_StdErr\bin\Debug\net5.0\ConsoleApp1_StdErr.exe", "");
        }

        private static string Execute(string exePath, string parameters)
        {
            string result = string.Empty;
            string err = string.Empty;

            using (Process p = new Process())
            {
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.FileName = exePath;
                p.StartInfo.Arguments = parameters;
                p.ErrorDataReceived += (sender, args) => err += args.Data != null ? args.Data + Environment.NewLine : ""; // use StringBuilder
                p.OutputDataReceived += (sender, args) => result += args.Data != null ? args.Data + Environment.NewLine : ""; // use StringBuilder
                p.Start();

                p.BeginErrorReadLine();
                p.BeginOutputReadLine();

                if (!p.WaitForExit(5000))
                {
                    Console.WriteLine("timeout!!");
                    p.Kill();
                }
                else
                {
                    //msdn:
                    //When standard output has been redirected to asynchronous event handlers, it is possible that output processing will not have completed
                    //when this method returns. To ensure that asynchronous event handling has been completed, call the WaitForExit() overload that takes no parameter
                    //after receiving a true from this overload.
                    p.WaitForExit();
                }

                if (p.ExitCode != 0)
                {
                    throw new Exception(err);
                }
                
            }

            return result;
        }
    }
}
