using Newtonsoft.Json.Linq;
using Octokit;

namespace PokeViewer.NET.SubForms
{
    partial class MoneyViewer
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
            MoneyLabel = new System.Windows.Forms.Label();
            MoneyGroup = new GroupBox();
            LeaguePayLabel = new System.Windows.Forms.Label();
            LPValue = new NumericUpDown();
            MoneyValue = new NumericUpDown();
            LPbutton = new Button();
            Moneybutton = new Button();
            Readbutton = new Button();
            BlueberryGroup = new GroupBox();
            BlueberryPointLabel = new System.Windows.Forms.Label();
            BPValue = new NumericUpDown();
            BPbutton = new Button();
            MoneyGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LPValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MoneyValue).BeginInit();
            BlueberryGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BPValue).BeginInit();
            SuspendLayout();
            // 
            // MoneyLabel
            // 
            MoneyLabel.AutoSize = true;
            MoneyLabel.Location = new Point(15, 22);
            MoneyLabel.Name = "MoneyLabel";
            MoneyLabel.Size = new Size(44, 15);
            MoneyLabel.TabIndex = 0;
            MoneyLabel.Text = "Money";
            // 
            // MoneyGroup
            // 
            MoneyGroup.Controls.Add(LeaguePayLabel);
            MoneyGroup.Controls.Add(LPValue);
            MoneyGroup.Controls.Add(MoneyValue);
            MoneyGroup.Controls.Add(LPbutton);
            MoneyGroup.Controls.Add(MoneyLabel);
            MoneyGroup.Controls.Add(Moneybutton);
            MoneyGroup.Location = new Point(12, 11);
            MoneyGroup.Margin = new Padding(3, 2, 3, 2);
            MoneyGroup.Name = "MoneyGroup";
            MoneyGroup.Padding = new Padding(3, 2, 3, 2);
            MoneyGroup.Size = new Size(243, 85);
            MoneyGroup.TabIndex = 12;
            MoneyGroup.TabStop = false;
            MoneyGroup.Text = "Money";
            // 
            // LeaguePayLabel
            // 
            LeaguePayLabel.AutoSize = true;
            LeaguePayLabel.Location = new Point(15, 49);
            LeaguePayLabel.Name = "LeaguePayLabel";
            LeaguePayLabel.Size = new Size(20, 15);
            LeaguePayLabel.TabIndex = 10;
            LeaguePayLabel.Text = "LP";
            // 
            // LPValue
            // 
            LPValue.Location = new Point(65, 47);
            LPValue.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            LPValue.Name = "LPValue";
            LPValue.Size = new Size(69, 23);
            LPValue.TabIndex = 9;
            // 
            // MoneyValue
            // 
            MoneyValue.Location = new Point(65, 16);
            MoneyValue.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            MoneyValue.Name = "MoneyValue";
            MoneyValue.Size = new Size(69, 23);
            MoneyValue.TabIndex = 6;
            // 
            // LPbutton
            // 
            LPbutton.Location = new Point(152, 47);
            LPbutton.Name = "LPbutton";
            LPbutton.Size = new Size(82, 23);
            LPbutton.TabIndex = 7;
            LPbutton.Text = "Set LP";
            LPbutton.UseVisualStyleBackColor = true;
            LPbutton.Click += LPbutton_Click;
            // 
            // Moneybutton
            // 
            Moneybutton.Location = new Point(154, 16);
            Moneybutton.Name = "Moneybutton";
            Moneybutton.Size = new Size(80, 23);
            Moneybutton.TabIndex = 1;
            Moneybutton.Text = "Set Money";
            Moneybutton.UseVisualStyleBackColor = true;
            Moneybutton.Click += Moneybutton_Click;
            // 
            // Readbutton
            // 
            Readbutton.Location = new Point(12, 174);
            Readbutton.Name = "Readbutton";
            Readbutton.Size = new Size(92, 23);
            Readbutton.TabIndex = 3;
            Readbutton.Text = "Read Current";
            Readbutton.UseVisualStyleBackColor = true;
            Readbutton.Click += Readbutton_Click;
            // 
            // BlueberryGroup
            // 
            BlueberryGroup.Controls.Add(BlueberryPointLabel);
            BlueberryGroup.Controls.Add(BPValue);
            BlueberryGroup.Controls.Add(BPbutton);
            BlueberryGroup.Location = new Point(12, 100);
            BlueberryGroup.Margin = new Padding(3, 2, 3, 2);
            BlueberryGroup.Name = "BlueberryGroup";
            BlueberryGroup.Padding = new Padding(3, 2, 3, 2);
            BlueberryGroup.Size = new Size(243, 62);
            BlueberryGroup.TabIndex = 13;
            BlueberryGroup.TabStop = false;
            BlueberryGroup.Text = "BP";
            // 
            // BlueberryPointLabel
            // 
            BlueberryPointLabel.AutoSize = true;
            BlueberryPointLabel.Location = new Point(15, 22);
            BlueberryPointLabel.Name = "BlueberryPointLabel";
            BlueberryPointLabel.Size = new Size(21, 15);
            BlueberryPointLabel.TabIndex = 11;
            BlueberryPointLabel.Text = "BP";
            // 
            // BPValue
            // 
            BPValue.Location = new Point(65, 20);
            BPValue.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            BPValue.Name = "BPValue";
            BPValue.Size = new Size(69, 23);
            BPValue.TabIndex = 6;
            // 
            // BPbutton
            // 
            BPbutton.Location = new Point(152, 20);
            BPbutton.Name = "BPbutton";
            BPbutton.Size = new Size(80, 23);
            BPbutton.TabIndex = 1;
            BPbutton.Text = "Set BP";
            BPbutton.UseVisualStyleBackColor = true;
            BPbutton.Click += BPbutton_Click;
            // 
            // MoneyViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(298, 209);
            Controls.Add(BlueberryGroup);
            Controls.Add(Readbutton);
            Controls.Add(MoneyGroup);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MoneyViewer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TimeViewer";
            MoneyGroup.ResumeLayout(false);
            MoneyGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LPValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)MoneyValue).EndInit();
            BlueberryGroup.ResumeLayout(false);
            BlueberryGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)BPValue).EndInit();
            ResumeLayout(false);
        }
        #endregion

        private Button Moneybutton;
        private Button Readbutton;
        private NumericUpDown MoneyValue;
        private Button LPbutton;
        private NumericUpDown LPValue;
        private GroupBox MoneyGroup;
        private GroupBox BlueberryGroup;
        private NumericUpDown BPValue;
        private Button BPbutton;
        private System.Windows.Forms.Label MoneyLabel;
        private System.Windows.Forms.Label LeaguePayLabel;
        private System.Windows.Forms.Label BlueberryPointLabel;
    }
}