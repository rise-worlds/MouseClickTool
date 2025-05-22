using System;
using System.Reflection;
using System.Windows.Forms;

[assembly: AssemblyTitle("MouseClickTool")]
[assembly: AssemblyProduct("MouseClickTool")]
[assembly: AssemblyCopyright("Copyright (C) 2025 qipai360.cn")]
[assembly: AssemblyVersion("1.0.0")]
[assembly: AssemblyFileVersion("1.0.0")]

public static class Program
{
    [STAThread]
    public static void Main()
    {
#if WINDOWS
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(true);
        Application.Run(new MouseClickTool.MainForm());
#else
        MessageBox.Show("This application requires Windows Vista or later.");
#endif
    }
}