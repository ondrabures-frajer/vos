using System;
using System.Drawing;
using System.Windows.Forms;

namespace HodinyUkol
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // ovládací prvky
        private Button btnStartStop;
        private CheckBox chkWidgetMode;
        private CheckBox chkAutostart;
        private CheckBox chkShowDigital;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnStartStop = new Button();
            chkWidgetMode = new CheckBox();
            chkAutostart = new CheckBox();
            chkShowDigital = new CheckBox();
            SuspendLayout();
            // 
            // btnStartStop
            // 
            btnStartStop.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnStartStop.Location = new Point(185, 592);
            btnStartStop.Name = "btnStartStop";
            btnStartStop.Size = new Size(90, 30);
            btnStartStop.TabIndex = 0;
            btnStartStop.Text = "Zastavit";
            btnStartStop.UseVisualStyleBackColor = true;
            btnStartStop.Click += BtnStartStop_Click;
            // 
            // chkWidgetMode
            // 
            chkWidgetMode.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chkWidgetMode.AutoSize = true;
            chkWidgetMode.Location = new Point(20, 628);
            chkWidgetMode.Name = "chkWidgetMode";
            chkWidgetMode.Size = new Size(96, 19);
            chkWidgetMode.TabIndex = 1;
            chkWidgetMode.Text = "Widget režim";
            chkWidgetMode.UseVisualStyleBackColor = true;
            chkWidgetMode.CheckedChanged += ChkWidgetMode_CheckedChanged;
            // 
            // chkAutostart
            // 
            chkAutostart.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chkAutostart.AutoSize = true;
            chkAutostart.Location = new Point(331, 628);
            chkAutostart.Name = "chkAutostart";
            chkAutostart.Size = new Size(117, 19);
            chkAutostart.TabIndex = 3;
            chkAutostart.Text = "Autostart (HKCU)";
            chkAutostart.UseVisualStyleBackColor = true;
            chkAutostart.CheckedChanged += ChkAutostart_CheckedChanged;
            // 
            // chkShowDigital
            // 
            chkShowDigital.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chkShowDigital.AutoSize = true;
            chkShowDigital.Location = new Point(156, 628);
            chkShowDigital.Name = "chkShowDigital";
            chkShowDigital.Size = new Size(135, 19);
            chkShowDigital.TabIndex = 2;
            chkShowDigital.Text = "Zobrazit digitální čas";
            chkShowDigital.UseVisualStyleBackColor = true;
            chkShowDigital.CheckedChanged += ChkShowDigital_CheckedChanged;
            // 
            // Form1
            // 
            ClientSize = new Size(507, 659);
            Controls.Add(btnStartStop);
            Controls.Add(chkWidgetMode);
            Controls.Add(chkShowDigital);
            Controls.Add(chkAutostart);
            MinimumSize = new Size(220, 220);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hodiny";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
