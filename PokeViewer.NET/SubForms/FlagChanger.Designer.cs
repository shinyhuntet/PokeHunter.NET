using Newtonsoft.Json.Linq;
using Octokit;

namespace PokeViewer.NET.SubForms
{
    partial class FlagChanger
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
            BoolCombo = new ComboBox();
            Readbutton = new Button();
            ByteBlockGroup = new GroupBox();
            ByteValue = new NumericUpDown();
            ByteValueLabel = new System.Windows.Forms.Label();
            ByteBlockLabel = new System.Windows.Forms.Label();
            ByteCombo = new ComboBox();
            SetByteButton = new Button();
            BoolBlockGroup = new GroupBox();
            BoolValueLabel = new System.Windows.Forms.Label();
            BoolValue = new ComboBox();
            BoolBlockLabel = new System.Windows.Forms.Label();
            SetBoolButton = new Button();
            IntBlockGroup = new GroupBox();
            IntValue = new NumericUpDown();
            IntValueLabel = new System.Windows.Forms.Label();
            IntBlockLabel = new System.Windows.Forms.Label();
            IntCombo = new ComboBox();
            SetIntButton = new Button();
            UIntBlockGroup = new GroupBox();
            UIntValue = new NumericUpDown();
            UIntValueLabel = new System.Windows.Forms.Label();
            UIntBlockLabel = new System.Windows.Forms.Label();
            UIntCombo = new ComboBox();
            SetUIntButton = new Button();
            ByteBlockGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ByteValue).BeginInit();
            BoolBlockGroup.SuspendLayout();
            IntBlockGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)IntValue).BeginInit();
            UIntBlockGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)UIntValue).BeginInit();
            SuspendLayout();
            // 
            // BoolCombo
            // 
            BoolCombo.Location = new Point(82, 25);
            BoolCombo.Name = "BoolCombo";
            BoolCombo.Size = new Size(121, 23);
            BoolCombo.TabIndex = 16;
            // 
            // Readbutton
            // 
            Readbutton.Location = new Point(12, 384);
            Readbutton.Name = "Readbutton";
            Readbutton.Size = new Size(92, 23);
            Readbutton.TabIndex = 3;
            Readbutton.Text = "Read Current";
            Readbutton.UseVisualStyleBackColor = true;
            Readbutton.Click += Readbutton_Click;
            // 
            // ByteBlockGroup
            // 
            ByteBlockGroup.Controls.Add(ByteValue);
            ByteBlockGroup.Controls.Add(ByteValueLabel);
            ByteBlockGroup.Controls.Add(ByteBlockLabel);
            ByteBlockGroup.Controls.Add(ByteCombo);
            ByteBlockGroup.Controls.Add(SetByteButton);
            ByteBlockGroup.Location = new Point(12, 197);
            ByteBlockGroup.Margin = new Padding(3, 2, 3, 2);
            ByteBlockGroup.Name = "ByteBlockGroup";
            ByteBlockGroup.Padding = new Padding(3, 2, 3, 2);
            ByteBlockGroup.Size = new Size(300, 89);
            ByteBlockGroup.TabIndex = 14;
            ByteBlockGroup.TabStop = false;
            ByteBlockGroup.Text = "ByteBlock";
            // 
            // ByteValue
            // 
            ByteValue.Location = new Point(82, 52);
            ByteValue.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            ByteValue.Name = "ByteValue";
            ByteValue.Size = new Size(69, 23);
            ByteValue.TabIndex = 12;
            // 
            // ByteValueLabel
            // 
            ByteValueLabel.AutoSize = true;
            ByteValueLabel.Location = new Point(6, 54);
            ByteValueLabel.Name = "ByteValueLabel";
            ByteValueLabel.Size = new Size(35, 15);
            ByteValueLabel.TabIndex = 19;
            ByteValueLabel.Text = "Value";
            // 
            // ByteBlockLabel
            // 
            ByteBlockLabel.AutoSize = true;
            ByteBlockLabel.Location = new Point(6, 26);
            ByteBlockLabel.Name = "ByteBlockLabel";
            ByteBlockLabel.Size = new Size(70, 15);
            ByteBlockLabel.TabIndex = 19;
            ByteBlockLabel.Text = "Block Name";
            // 
            // ByteCombo
            // 
            ByteCombo.Location = new Point(82, 23);
            ByteCombo.Name = "ByteCombo";
            ByteCombo.Size = new Size(121, 23);
            ByteCombo.TabIndex = 19;
            // 
            // SetByteButton
            // 
            SetByteButton.Location = new Point(209, 52);
            SetByteButton.Name = "SetByteButton";
            SetByteButton.Size = new Size(80, 23);
            SetByteButton.TabIndex = 1;
            SetByteButton.Text = "Set Block";
            SetByteButton.UseVisualStyleBackColor = true;
            SetByteButton.Click += SetByteButton_Click;
            // 
            // BoolBlockGroup
            // 
            BoolBlockGroup.Controls.Add(BoolValueLabel);
            BoolBlockGroup.Controls.Add(BoolValue);
            BoolBlockGroup.Controls.Add(BoolBlockLabel);
            BoolBlockGroup.Controls.Add(SetBoolButton);
            BoolBlockGroup.Controls.Add(BoolCombo);
            BoolBlockGroup.Location = new Point(12, 290);
            BoolBlockGroup.Margin = new Padding(3, 2, 3, 2);
            BoolBlockGroup.Name = "BoolBlockGroup";
            BoolBlockGroup.Padding = new Padding(3, 2, 3, 2);
            BoolBlockGroup.Size = new Size(300, 89);
            BoolBlockGroup.TabIndex = 15;
            BoolBlockGroup.TabStop = false;
            BoolBlockGroup.Text = "BoolBlock";
            // 
            // BoolValueLabel
            // 
            BoolValueLabel.AutoSize = true;
            BoolValueLabel.Location = new Point(6, 57);
            BoolValueLabel.Name = "BoolValueLabel";
            BoolValueLabel.Size = new Size(35, 15);
            BoolValueLabel.TabIndex = 18;
            BoolValueLabel.Text = "Value";
            // 
            // BoolValue
            // 
            BoolValue.Location = new Point(82, 54);
            BoolValue.Name = "BoolValue";
            BoolValue.DataSource = new bool[] { false, true };
            BoolValue.Size = new Size(121, 23);
            BoolValue.TabIndex = 17;
            // 
            // BoolBlockLabel
            // 
            BoolBlockLabel.AutoSize = true;
            BoolBlockLabel.Location = new Point(6, 28);
            BoolBlockLabel.Name = "BoolBlockLabel";
            BoolBlockLabel.Size = new Size(70, 15);
            BoolBlockLabel.TabIndex = 11;
            BoolBlockLabel.Text = "Block Name";
            // 
            // SetBoolButton
            // 
            SetBoolButton.Location = new Point(209, 54);
            SetBoolButton.Name = "SetBoolButton";
            SetBoolButton.Size = new Size(80, 23);
            SetBoolButton.TabIndex = 1;
            SetBoolButton.Text = "Set Block";
            SetBoolButton.UseVisualStyleBackColor = true;
            SetBoolButton.Click += SetBoolButton_Click;
            // 
            // IntBlockGroup
            // 
            IntBlockGroup.Controls.Add(IntValue);
            IntBlockGroup.Controls.Add(IntValueLabel);
            IntBlockGroup.Controls.Add(IntBlockLabel);
            IntBlockGroup.Controls.Add(IntCombo);
            IntBlockGroup.Controls.Add(SetIntButton);
            IntBlockGroup.Location = new Point(12, 104);
            IntBlockGroup.Margin = new Padding(3, 2, 3, 2);
            IntBlockGroup.Name = "IntBlockGroup";
            IntBlockGroup.Padding = new Padding(3, 2, 3, 2);
            IntBlockGroup.Size = new Size(300, 89);
            IntBlockGroup.TabIndex = 20;
            IntBlockGroup.TabStop = false;
            IntBlockGroup.Text = "IntBlock";
            // 
            // IntValue
            // 
            IntValue.Location = new Point(82, 52);
            IntValue.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            IntValue.Name = "IntValue";
            IntValue.Size = new Size(69, 23);
            IntValue.TabIndex = 12;
            // 
            // IntValueLabel
            // 
            IntValueLabel.AutoSize = true;
            IntValueLabel.Location = new Point(6, 53);
            IntValueLabel.Name = "IntValueLabel";
            IntValueLabel.Size = new Size(35, 15);
            IntValueLabel.TabIndex = 19;
            IntValueLabel.Text = "Value";
            // 
            // IntBlockLabel
            // 
            IntBlockLabel.AutoSize = true;
            IntBlockLabel.Location = new Point(6, 25);
            IntBlockLabel.Name = "IntBlockLabel";
            IntBlockLabel.Size = new Size(70, 15);
            IntBlockLabel.TabIndex = 19;
            IntBlockLabel.Text = "Block Name";
            // 
            // IntCombo
            // 
            IntCombo.Location = new Point(82, 23);
            IntCombo.Name = "IntCombo";
            IntCombo.Size = new Size(121, 23);
            IntCombo.TabIndex = 19;
            // 
            // SetIntButton
            // 
            SetIntButton.Location = new Point(209, 52);
            SetIntButton.Name = "SetIntButton";
            SetIntButton.Size = new Size(80, 23);
            SetIntButton.TabIndex = 1;
            SetIntButton.Text = "Set Block";
            SetIntButton.UseVisualStyleBackColor = true;
            SetIntButton.Click += SetIntButton_Click;
            // 
            // UIntBlockGroup
            // 
            UIntBlockGroup.Controls.Add(UIntValue);
            UIntBlockGroup.Controls.Add(UIntValueLabel);
            UIntBlockGroup.Controls.Add(UIntBlockLabel);
            UIntBlockGroup.Controls.Add(UIntCombo);
            UIntBlockGroup.Controls.Add(SetUIntButton);
            UIntBlockGroup.Location = new Point(12, 11);
            UIntBlockGroup.Margin = new Padding(3, 2, 3, 2);
            UIntBlockGroup.Name = "UIntBlockGroup";
            UIntBlockGroup.Padding = new Padding(3, 2, 3, 2);
            UIntBlockGroup.Size = new Size(300, 89);
            UIntBlockGroup.TabIndex = 20;
            UIntBlockGroup.TabStop = false;
            UIntBlockGroup.Text = "UIntBlock";
            // 
            // UIntValue
            // 
            UIntValue.Location = new Point(82, 52);
            UIntValue.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            UIntValue.Name = "UIntValue";
            UIntValue.Size = new Size(69, 23);
            UIntValue.TabIndex = 12;
            // 
            // UIntValueLabel
            // 
            UIntValueLabel.AutoSize = true;
            UIntValueLabel.Location = new Point(6, 53);
            UIntValueLabel.Name = "UIntValueLabel";
            UIntValueLabel.Size = new Size(35, 15);
            UIntValueLabel.TabIndex = 19;
            UIntValueLabel.Text = "Value";
            // 
            // UIntBlockLabel
            // 
            UIntBlockLabel.AutoSize = true;
            UIntBlockLabel.Location = new Point(6, 25);
            UIntBlockLabel.Name = "UIntBlockLabel";
            UIntBlockLabel.Size = new Size(70, 15);
            UIntBlockLabel.TabIndex = 19;
            UIntBlockLabel.Text = "Block Name";
            // 
            // UIntCombo
            // 
            UIntCombo.Location = new Point(82, 23);
            UIntCombo.Name = "UIntCombo";
            UIntCombo.Size = new Size(121, 23);
            UIntCombo.TabIndex = 19;
            // 
            // SetUIntButton
            // 
            SetUIntButton.Location = new Point(209, 52);
            SetUIntButton.Name = "SetUIntButton";
            SetUIntButton.Size = new Size(80, 23);
            SetUIntButton.TabIndex = 1;
            SetUIntButton.Text = "Set Block";
            SetUIntButton.UseVisualStyleBackColor = true;
            SetUIntButton.Click += SetUIntButton_Click;
            // 
            // FlagChanger
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(329, 419);
            Controls.Add(UIntBlockGroup);
            Controls.Add(IntBlockGroup);
            Controls.Add(BoolBlockGroup);
            Controls.Add(ByteBlockGroup);
            Controls.Add(Readbutton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FlagChanger";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TimeViewer";
            ByteBlockGroup.ResumeLayout(false);
            ByteBlockGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ByteValue).EndInit();
            BoolBlockGroup.ResumeLayout(false);
            BoolBlockGroup.PerformLayout();
            IntBlockGroup.ResumeLayout(false);
            IntBlockGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)IntValue).EndInit();
            UIntBlockGroup.ResumeLayout(false);
            UIntBlockGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)UIntValue).EndInit();
            ResumeLayout(false);
        }
        #endregion
        private Button Readbutton;
        private ComboBox BoolCombo;
        private GroupBox ByteBlockGroup;
        private NumericUpDown ByteValue;
        private Button SetByteButton;
        private GroupBox BoolBlockGroup;
        private System.Windows.Forms.Label BoolBlockLabel;
        private Button SetBoolButton;
        private ComboBox BoolValue;
        private ComboBox ByteCombo;
        private System.Windows.Forms.Label BoolValueLabel;
        private System.Windows.Forms.Label ByteValueLabel;
        private System.Windows.Forms.Label ByteBlockLabel;
        private GroupBox IntBlockGroup;
        private NumericUpDown IntValue;
        private System.Windows.Forms.Label IntValueLabel;
        private System.Windows.Forms.Label IntBlockLabel;
        private ComboBox IntCombo;
        private Button SetIntButton;
        private GroupBox UIntBlockGroup;
        private NumericUpDown UIntValue;
        private System.Windows.Forms.Label UIntValueLabel;
        private System.Windows.Forms.Label UIntBlockLabel;
        private ComboBox UIntCombo;
        private Button SetUIntButton;
    }
}