using System.Reflection;

[assembly: AssemblyTitle("MouseClickTool")]
[assembly: AssemblyProduct("MouseClickTool")]
[assembly: AssemblyCopyright("Copyright (C) 2025 qipai360.cn")]

public static class Program
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(true);
        Application.Run(new MouseClickTool.MainForm());
    }
}