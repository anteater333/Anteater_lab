using System;
using System.Security.Principal;
using System.Diagnostics;
using System.Windows.Forms;

namespace MakeStartup
{
    class Program
    {
        static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();

            if (null != identity)
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return false;
        }

        static int Main(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                Console.Write(args[i] + " ");
            }

            Console.WriteLine();

            /// Wrong Usage
            if (args.Length != 3)
            {
                Console.WriteLine("Wrong Usage!");
                Console.WriteLine("Usage : MakeStartup.exe [AppName] [ExePath] [a|r]");
                Console.WriteLine("Press ANY Key to continue.");
                Console.ReadKey();
                return 1;
            }
            /// Not Administrator
            else if (IsAdministrator() == false)
            {
                try
                {
                    ProcessStartInfo procInfo = new ProcessStartInfo();
                    procInfo.UseShellExecute = true;
                    procInfo.FileName = Application.ExecutablePath;
                    procInfo.WorkingDirectory = Environment.CurrentDirectory;
                    procInfo.Verb = "runas";
                    procInfo.Arguments = args[0] + " " + args[1] + " " + args[2];
                    Process P = Process.Start(procInfo);
                    P.WaitForExit();
                    return P.ExitCode;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.WriteLine("Press ANY Key to continue.");
                    Console.ReadKey();
                    return 1;
                }
            }
            /// Set StartUp
            else
            {
                #region 시작 프로그램 등록 관련 변수
                // 시작 프로그램 레지스트리
                string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
                Microsoft.Win32.RegistryKey startupKey;
                #endregion

                try
                {
                    switch (args[2])
                    {
                        case "a":
                            startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey, true);
                            startupKey.SetValue(args[0], args[1]);
                            startupKey.Close();
                            break;
                        case "r":
                            startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey, true);
                            startupKey.DeleteValue(args[0], false);
                            startupKey.Close();
                            break;
                        default:
                            Console.WriteLine("Wrong Usage!");
                            Console.WriteLine("Press ANY Key to continue.");
                            Console.ReadKey();
                            return 1;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.WriteLine("Press ANY Key to continue.");
                    Console.ReadKey();
                    return 1;
                }
                return 0;
            }
        }
    }
}
