using Newtonsoft.Json.Linq;
using Octokit;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;

namespace PokeViewer.NET.SubForms
{
    partial class TimeViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            BackwardButton = new Button();
            ForwordButton = new Button();
            ResetButton = new Button();
            HourNum = new NumericUpDown();
            FowardMinute = new Button();
            BackwardMinute = new Button();
            MinutesNum = new NumericUpDown();
            SetNTPTime = new Button();
            SetCurrentTime = new Button();
            ReadClock = new Button();
            ((System.ComponentModel.ISupportInitialize)HourNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MinutesNum).BeginInit();
            SuspendLayout();
            // 
            // BackwardButton
            // 
            BackwardButton.Location = new Point(12, 41);
            BackwardButton.Name = "BackwardButton";
            BackwardButton.Size = new Size(70, 23);
            BackwardButton.TabIndex = 0;
            BackwardButton.Text = "Backward";
            BackwardButton.UseVisualStyleBackColor = true;
            BackwardButton.Click += Backward_Click;
            // 
            // ForwordButton
            // 
            ForwordButton.Location = new Point(12, 12);
            ForwordButton.Name = "ForwordButton";
            ForwordButton.Size = new Size(70, 23);
            ForwordButton.TabIndex = 1;
            ForwordButton.Text = "Forward";
            ForwordButton.UseVisualStyleBackColor = true;
            ForwordButton.Click += Forward_Click;
            // 
            // ResetButton
            // 
            ResetButton.Location = new Point(88, 41);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(69, 23);
            ResetButton.TabIndex = 2;
            ResetButton.Text = "Reset";
            ResetButton.UseVisualStyleBackColor = true;
            ResetButton.Click += Reset_Click;
            // 
            // HourNum
            // 
            HourNum.Location = new Point(88, 14);
            HourNum.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            HourNum.Name = "HourNum";
            HourNum.Size = new Size(69, 23);
            HourNum.TabIndex = 6;
            HourNum.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // FowardMinute
            // 
            FowardMinute.Location = new Point(12, 79);
            FowardMinute.Name = "FowardMinute";
            FowardMinute.Size = new Size(92, 23);
            FowardMinute.TabIndex = 7;
            FowardMinute.Text = "Forward Min";
            FowardMinute.UseVisualStyleBackColor = true;
            FowardMinute.Click += FowardMinute_Click;
            // 
            // BackwardMinute
            // 
            BackwardMinute.Location = new Point(12, 108);
            BackwardMinute.Name = "BackwardMinute";
            BackwardMinute.Size = new Size(92, 23);
            BackwardMinute.TabIndex = 8;
            BackwardMinute.Text = "Backward Min";
            BackwardMinute.UseVisualStyleBackColor = true;
            BackwardMinute.Click += BackwardMinute_Click;
            // 
            // MinutesNum
            // 
            MinutesNum.Location = new Point(110, 79);
            MinutesNum.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            MinutesNum.Name = "MinutesNum";
            MinutesNum.Size = new Size(69, 23);
            MinutesNum.TabIndex = 9;
            MinutesNum.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // SetNTPTime
            // 
            SetNTPTime.Location = new Point(12, 174);
            SetNTPTime.Name = "SetNTPTime";
            SetNTPTime.Size = new Size(102, 23);
            SetNTPTime.TabIndex = 10;
            SetNTPTime.Text = "ResetNTPTime";
            SetNTPTime.UseVisualStyleBackColor = true;
            SetNTPTime.Click += SetTime_Click;
            // 
            // SetCurrentTime
            // 
            SetCurrentTime.Location = new Point(12, 137);
            SetCurrentTime.Name = "SetCurrentTime";
            SetCurrentTime.Size = new Size(114, 23);
            SetCurrentTime.TabIndex = 11;
            SetCurrentTime.Text = "Set Current Time";
            SetCurrentTime.UseVisualStyleBackColor = true;
            SetCurrentTime.Click += SetCurrentTime_Click;
            // 
            // ReadClock
            // 
            ReadClock.Location = new Point(132, 137);
            ReadClock.Name = "ReadClock";
            ReadClock.Size = new Size(114, 23);
            ReadClock.TabIndex = 12;
            ReadClock.Text = "Read Switch Clock";
            ReadClock.UseVisualStyleBackColor = true;
            ReadClock.Click += ReadClock_Click;
            // 
            // TimeViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(271, 209);
            Controls.Add(ReadClock);
            Controls.Add(SetCurrentTime);
            Controls.Add(SetNTPTime);
            Controls.Add(MinutesNum);
            Controls.Add(BackwardMinute);
            Controls.Add(FowardMinute);
            Controls.Add(HourNum);
            Controls.Add(ResetButton);
            Controls.Add(ForwordButton);
            Controls.Add(BackwardButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TimeViewer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TimeViewer";
            ((System.ComponentModel.ISupportInitialize)HourNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)MinutesNum).EndInit();
            ResumeLayout(false);
        }
        #endregion

        private Button BackwardButton;
        private Button ForwordButton;
        private Button ResetButton;
        private NumericUpDown HourNum;
        private Button FowardMinute;
        private Button BackwardMinute;
        private NumericUpDown MinutesNum;
        private Button SetNTPTime;
        private Button SetCurrentTime;
        private Button ReadClock;
    }
}