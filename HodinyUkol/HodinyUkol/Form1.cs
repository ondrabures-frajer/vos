using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Microsoft.Win32;
using Timer = System.Windows.Forms.Timer;

namespace HodinyUkol
{
    public partial class Form1 : Form
    {
        private Timer? timer;
        private bool isRunning = true;
        private bool widgetMode = false;
        private bool showDigital = true;

        private NotifyIcon? notifyIcon;
        private ContextMenuStrip? trayMenu;

        private Point dragOffset;
        private bool dragging = false;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            // výchozí hodnoty
            showDigital = true;
            widgetMode = false;

            // Timer
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();

            // Tray icon + menu
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Otevøít", null, (s, e) => RestoreFromTray());
            trayMenu.Items.Add("Pøepnout widget", null, (s, e) => ToggleWidgetMode());
            trayMenu.Items.Add("Konec", null, (s, e) => { Close(); });

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Application;
            notifyIcon.ContextMenuStrip = trayMenu;
            notifyIcon.Text = "Hodiny";
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += (s, e) => RestoreFromTray();

            // Synchronizace UI se stávajícími hodnotami
            chkShowDigital.Checked = showDigital;
            chkWidgetMode.Checked = widgetMode;
            chkAutostart.Checked = IsInStartup();
            btnStartStop.Text = isRunning ? "Zastavit" : "Spustit";

            // události pro pøetažení formuláøe a pravé tlaèítko
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (isRunning)
                this.Invalidate();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Tick -= Timer_Tick;
                timer.Dispose();
                timer = null;
            }

            if (notifyIcon != null)
            {
                notifyIcon.Visible = false;
                notifyIcon.Dispose();
                notifyIcon = null;
            }

            if (trayMenu != null)
            {
                trayMenu.Dispose();
                trayMenu = null;
            }

            base.OnFormClosed(e);
        }

        private void RestoreFromTray()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Activate();
        }

        private void ToggleWidgetMode()
        {
            chkWidgetMode.Checked = !chkWidgetMode.Checked;
            ApplyWidgetMode(chkWidgetMode.Checked);
        }

        private void ApplyWidgetMode(bool enable)
        {
            widgetMode = enable;
            if (enable)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.TopMost = true;
                this.ShowInTaskbar = false;
                this.Opacity = 0.95;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.TopMost = false;
                this.ShowInTaskbar = true;
                this.Opacity = 1.0;
            }
        }

        private void BtnStartStop_Click(object? sender, EventArgs e)
        {
            isRunning = !isRunning;
            btnStartStop.Text = isRunning ? "Zastavit" : "Spustit";
            if (isRunning)
                timer?.Start();
            else
                timer?.Stop();
        }

        private void ChkWidgetMode_CheckedChanged(object? sender, EventArgs e)
        {
            ApplyWidgetMode(chkWidgetMode.Checked);
        }

        private void ChkShowDigital_CheckedChanged(object? sender, EventArgs e)
        {
            showDigital = chkShowDigital.Checked;
            this.Invalidate();
        }

        private void ChkAutostart_CheckedChanged(object? sender, EventArgs e)
        {
            bool want = chkAutostart.Checked;
            try
            {
                using var rk = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                if (rk == null) return;
                string name = "HodinyUkol";
                if (want)
                {
                    rk.SetValue(name, $"\"{Application.ExecutablePath}\"");
                }
                else
                {
                    rk.DeleteValue(name, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nelze zmìnit autostart: " + ex.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chkAutostart.Checked = !want; // revert
            }
        }

        private bool IsInStartup()
        {
            try
            {
                using var rk = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false);
                if (rk == null) return false;
                var val = rk.GetValue("HodinyUkol") as string;
                return !string.IsNullOrEmpty(val);
            }
            catch { return false; }
        }

        // Pomocná metoda pro získání støedu a polomìru ciferníku
        private void GetClockCenterRadius(out Point center, out int radius)
        {
            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;
            radius = Math.Min(width, height) / 2 - 10;
            if (radius < 10) radius = 10;
            center = new Point(width / 2, height / 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            GetClockCenterRadius(out Point center, out int radius);

            // Ciferník
            using (var circlePen = new Pen(Color.Black, 2))
            {
                g.DrawEllipse(circlePen, center.X - radius, center.Y - radius, radius * 2, radius * 2);
            }

            // Sekundové/minutové znaèky
            using (var smallTickPen = new Pen(Color.Gray, 1))
            {
                for (int i = 0; i < 60; i++)
                {
                    if (i % 5 == 0) // vynechat pozice, kde budou hodinové znaèky
                        continue;

                    double angle = i * Math.PI / 30;
                    int x1 = (int)(center.X + Math.Sin(angle) * (radius - 6));
                    int y1 = (int)(center.Y - Math.Cos(angle) * (radius - 6));
                    int x2 = (int)(center.X + Math.Sin(angle) * radius);
                    int y2 = (int)(center.Y - Math.Cos(angle) * radius);
                    g.DrawLine(smallTickPen, x1, y1, x2, y2);
                }
            }

            // Hodinové znaèky
            using (var markPen = new Pen(Color.Black, 3))
            {
                for (int i = 0; i < 12; i++)
                {
                    double angle = i * Math.PI / 6;
                    int x1 = (int)(center.X + Math.Sin(angle) * (radius - 10));
                    int y1 = (int)(center.Y - Math.Cos(angle) * (radius - 10));
                    int x2 = (int)(center.X + Math.Sin(angle) * radius);
                    int y2 = (int)(center.Y - Math.Cos(angle) * radius);
                    g.DrawLine(markPen, x1, y1, x2, y2);
                }
            }

            // Èíselné hodnoty 1-12
            float numberFontSize = Math.Max(8f, radius * 0.12f);
            using (var numberFont = new Font("Segoe UI", numberFontSize, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                var sfCenter = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                double numberRadius = radius - Math.Max(20, radius / 6.0); // posunutí èísel dovnitø ciferníku
                for (int i = 1; i <= 12; i++)
                {
                    double angle = i * Math.PI / 6;
                    int nx = (int)(center.X + Math.Sin(angle) * numberRadius);
                    int ny = (int)(center.Y - Math.Cos(angle) * numberRadius);
                    g.DrawString(i.ToString(), numberFont, Brushes.Black, nx, ny, sfCenter);
                }
            }

            DateTime now = DateTime.Now;

            // Úhly ruèièek
            double hourAngle = (now.Hour % 12 + now.Minute / 60.0 + now.Second / 3600.0) * Math.PI / 6;
            double minuteAngle = (now.Minute + now.Second / 60.0) * Math.PI / 30;
            double secondAngle = now.Second * Math.PI / 30;

            // Ruèièky (fixní tloušky)
            DrawHand(g, center, hourAngle, radius * 0.5, 6, Color.Black);
            DrawHand(g, center, minuteAngle, radius * 0.75, 4, Color.Black);
            DrawHand(g, center, secondAngle, radius * 0.85, 2, Color.Red);

            // Digitální èas v pravém horním rohu (volitelnì)
            if (showDigital)
            {
                string digital = now.ToString("HH:mm:ss");
                float digitalFontSize = Math.Max(8f, radius * 0.10f);
                using (var digitalFont = new Font("Consolas", digitalFontSize, FontStyle.Regular, GraphicsUnit.Pixel))
                {
                    SizeF txtSize = g.MeasureString(digital, digitalFont);
                    int padding = 8;
                    // pozice v pravém horním rohu
                    RectangleF digitalRect = new RectangleF(
                        this.ClientSize.Width - txtSize.Width - padding,
                        padding,
                        txtSize.Width,
                        txtSize.Height);

                    var sfTopRight = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Near };
                    using (var bgBrush = new SolidBrush(Color.FromArgb(200, Color.White)))
                    {
                        g.FillRectangle(bgBrush, digitalRect);
                    }
                    g.DrawString(digital, digitalFont, Brushes.Black, digitalRect, sfTopRight);
                }
            }
        }

        private void DrawHand(Graphics g, Point center, double angle, double length, int thickness, Color color)
        {
            int x = (int)(center.X + Math.Sin(angle) * length);
            int y = (int)(center.Y - Math.Cos(angle) * length);

            using (var pen = new Pen(color, thickness) { EndCap = System.Drawing.Drawing2D.LineCap.Round })
            {
                g.DrawLine(pen, center.X, center.Y, x, y);
            }
        }

        private void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            if (!widgetMode) return;
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragOffset = new Point(e.X, e.Y);
            }
        }

        private void Form1_MouseMove(object? sender, MouseEventArgs e)
        {
            if (!widgetMode) return;
            if (dragging)
            {
                var screenPos = this.PointToScreen(e.Location);
                this.Location = new Point(screenPos.X - dragOffset.X, screenPos.Y - dragOffset.Y);
            }
        }

        private void Form1_MouseUp(object? sender, MouseEventArgs e)
        {
            // pravé tlaèítko: zobrazit menu pøímo pod ciferníkem
            if (e.Button == MouseButtons.Right && trayMenu != null)
            {
                GetClockCenterRadius(out Point center, out int radius);
                // zobrazit lehce pod ciferníkem (v client coords)
                var menuClientPoint = new Point(center.X, center.Y + radius + 8);
                var screenPoint = this.PointToScreen(menuClientPoint);
                trayMenu.Show(screenPoint);
                return;
            }

            // levé tlaèítko: ukonèit pøetahování
            dragging = false;
        }
    }
}
