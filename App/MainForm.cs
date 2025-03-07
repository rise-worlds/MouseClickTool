using System.Globalization;
using System.Runtime.InteropServices;

namespace MouseClickTool
{
    public partial class MainForm : Form
    {
        private Input m;
        private int wait = 3;
        private TaskCompletionSource<int>? z;
        private bool startApp = false;
        private string[] cfg = ["F1", "1000", "0", "600", string.Empty, "开始", "停止", "点击次数(Count):", "程序路径(Path):", string.Empty];
        private string fCfg;

        public MainForm()
        {
            InitializeComponent();
            for (int i = 1; i < 13; i++)
            {
                hk.Items.Add($"F{i}");
            }

            fCfg = Path.Combine(Path.GetTempPath(), $"MouseClickTool.ini");
            if (File.Exists(fCfg))
            {
                var tCfg = File.ReadAllLines(fCfg);
                cfg = (tCfg.Length == cfg.Length) ? tCfg : cfg;
            }

            int.TryParse(cfg[2], NumberStyles.Integer, null, out int ctv);
            hk.SelectedItem = cfg[0];
            dv.Text = cfg[1];
            ct.SelectedIndex = ctv;
#if WINDOWS
            Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
            Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
#endif
            Application.EnableVisualStyles();
            var dark = false;
#if WINDOWS
            try
            {
                dark = (int)Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", "AppsUseLightTheme", -1) == 0;
                SetProcessDPIAware();
            }
            catch
            {
            }
#endif
        }

        [Flags]
        private enum MouseEventFlag
        {
            MOUSEEVENTF_LEFTDOWN = 0x0002,
            MOUSEEVENTF_LEFTUP = 0x0004,
            MOUSEEVENTF_RIGHTDOWN = 0x0008,
            MOUSEEVENTF_RIGHTUP = 0x0010,
            MOUSEEVENTF_WHEEL = 0x0800,
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x84)
            {
                m.Result = (IntPtr)0x2;
            }
            else if (m.Msg == 0x0312)
            {
                wait = 0;
                bs.PerformClick();
            }
        }

        private static void ProcessStart(string path)
        {
            new Thread(() =>
            {
                try
                {
                    System.Diagnostics.Process.Start(path);
                }
                catch
                {
                }
            }).Start();
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, Keys vk);

        // 参考：https://stackoverflow.com/questions/5094398/how-to-programmatically-mouse-move-click-right-click-and-keypress-etc-in-winfo
        [DllImport("user32.dll")]
        private static extern int SendInput(int nInputs, ref Input pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private void UpdateText()
        {
            bs.Text = $"{(z == null ? cfg[5] : cfg[6])}({cfg[0]})";
            bs.Enabled = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                File.WriteAllLines(fCfg, cfg);
            }
            catch
            {
            }

            Hide();
            z?.TrySetCanceled();
            Application.Exit();
        }

        private void bh_Click(object sender, EventArgs e)
        {
            ProcessStart("https://rise-worlds.github.io");
        }

        private void bs_Click(object sender, EventArgs e)
        {
            bs.Enabled = false;
            if (ct.Enabled && z == null)
            {
                if (int.TryParse(dv.Text, out int delay) && delay > -1)
                {
                    dv.Enabled = ct.Enabled = pk.Enabled = pv.Enabled = false;
                    var downFlag = MouseEventFlag.MOUSEEVENTF_LEFTDOWN;
                    var upFlag = MouseEventFlag.MOUSEEVENTF_LEFTUP;
                    if ((ct.SelectedIndex & 1) == 1)
                    {
                        downFlag = MouseEventFlag.MOUSEEVENTF_RIGHTDOWN;
                        upFlag = MouseEventFlag.MOUSEEVENTF_RIGHTUP;
                    }

                    var mouseWheel = ct.SelectedIndex > 3;
                    if (mouseWheel)
                    {
                        downFlag = MouseEventFlag.MOUSEEVENTF_WHEEL;
                        int sc = 600;
                        m.mi.mouseData = ct.SelectedIndex > 4 ? -sc : sc;
                    }

                    var longPress = ct.SelectedIndex > 1;
                    Task.Run(async () =>
                    {
                        for (int i = 1; i < wait; i++)
                        {
                            Invoke(() => bs.Text = $"{wait - i}");
                            await Task.Delay(1000);
                        }

                        var pressed = false;
                        var size = Marshal.SizeOf(m);
                        z = new();
                        Invoke(() => UpdateText());
                        var tg = pk.Value < DateTime.Now;
                        uint.TryParse(pv.Text.Trim(), NumberStyles.Integer, null, out uint num);
                        for (ulong count = 0; num < 1 || count < num; count++)
                        {
                            if (z?.Task.IsCanceled == true)
                            {
                                break;
                            }

                            if (tg)
                            {
                                if (startApp)
                                {
                                    if (File.Exists(pv.Text))
                                    {
                                        ProcessStart(pv.Text);
                                    }

                                    break;
                                }
                                else
                                {
                                    if (!pressed || mouseWheel)
                                    {
                                        m.mi.dwFlags = downFlag;
                                        _ = SendInput(1, ref m, size);
                                    }

                                    if (!longPress)
                                    {
                                        m.mi.dwFlags = upFlag;
                                        _ = SendInput(1, ref m, size);
                                    }
                                    else
                                    {
                                        m.mi.dwFlags = upFlag;
                                        pressed = true;
                                    }
                                }
                            }
                            else
                            {
                                tg = pk.Value < DateTime.Now;
                            }

                            if (delay != 0)
                            {
                                await Task.WhenAny(Task.Delay(delay), z?.Task);
                            }
                        }

                        if (longPress && !mouseWheel)
                        {
                            _ = SendInput(1, ref m, size);
                        }

                        await Task.Delay(delay == 0 ? 5 : 0);
                        wait = 3;
                        z = null;
                        Invoke(() =>
                        {
                            dv.Enabled = ct.Enabled = pk.Enabled = pv.Enabled = true;
                            pk.Value = DateTime.Now;
                            UpdateText();
                        });
                    });
                }
                else
                {
                    MessageBox.Show("鼠标点击间隔必须是一个自然数", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                z?.TrySetCanceled();
            }
        }

        private void hk_SelectedIndexChanged(object sender, EventArgs e)
        {
            const int hotkeyId = 0x233;
            UnregisterHotKey(Handle, hotkeyId);
            if (Enum.TryParse(hk.Text, out Keys key))
            {
                RegisterHotKey(Handle, hotkeyId, 0x4000, key);
                cfg[0] = hk.Text;
                UpdateText();
            }
        }

        private void ct_SelectedIndexChanged(object sender, EventArgs e)
        {
            cfg[2] = $"{ct.SelectedIndex}";
            startApp = ct.SelectedIndex == ct.Items.Count - 1;
            if (startApp)
            {
                pv.Text = cfg[9];
                wb1.Text = cfg[8];
            }
            else
            {
                pv.Text = cfg[4];
                wb1.Text = cfg[7];
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Input
        {
            public int type;
            public MouseInput mi;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MouseInput
        {
            public int dx;
            public int dy;
            public int mouseData;
            public MouseEventFlag dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }
    }
}