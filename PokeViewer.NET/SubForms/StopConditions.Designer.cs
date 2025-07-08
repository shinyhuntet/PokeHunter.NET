using PKHeX.Core;

namespace PokeViewer.NET.SubForms
{
    partial class StopConditions
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
            Abilitys = new CheckedListBox();
            RemoveNature = new Button();
            RemoveItem = new Button();
            AddItem = new Button();
            Item = new ComboBox();
            SpeciesBox = new ComboBox();
            AddFilter = new Button();
            RemoveFilter = new Button();
            AddNatureButton = new Button();
            FilterEnabled = new CheckBox();
            FilterName = new ComboBox();
            CurrentFilterName = new TextBox();
            NatureBox = new ComboBox();
            StopConditionsGroup = new GroupBox();
            RemoveUnwantedMarkButton = new Button();
            AddUnwantedMarkButton = new Button();
            UnwantedMarkBox = new ComboBox();
            label9 = new Label();
            MarkCheck = new CheckBox();
            LanguageLabel = new Label();
            Language = new ComboBox();
            FormBox = new ComboBox();
            AbilityLabel = new Label();
            RemoveMarkButton = new Button();
            AddMarkButton = new Button();
            MarkLabel = new Label();
            MarkBox = new ComboBox();
            FormLabel = new Label();
            SpeciesLabel = new Label();
            CurrentFilterNameLabel = new Label();
            ItemLable = new Label();
            NatureLabel = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            ScaleBox = new CheckBox();
            ShinyBox = new ComboBox();
            label1 = new Label();
            PresetIVs = new Label();
            PresetIVMaxBox = new ComboBox();
            PresetIVMinBox = new ComboBox();
            GenderBox = new ComboBox();
            IgnoreIVFilter = new CheckBox();
            CheckBoxOf3 = new CheckBox();
            TargetSPE = new Label();
            TargetSPD = new Label();
            GenderFilter = new Label();
            TargetSPA = new Label();
            TargetIVs = new Label();
            TargetDEF = new Label();
            TargetHP = new Label();
            TargetATK = new Label();
            HPFilterMax = new NumericUpDown();
            AtkFilterMax = new NumericUpDown();
            SpeFilterMax = new NumericUpDown();
            DefFilterMax = new NumericUpDown();
            SpdFilterMax = new NumericUpDown();
            SpaFilterMax = new NumericUpDown();
            HPFilterMin = new NumericUpDown();
            AtkFilterMin = new NumericUpDown();
            SpeFilterMin = new NumericUpDown();
            DefFilterMin = new NumericUpDown();
            SpdFilterMin = new NumericUpDown();
            SpaFilterMin = new NumericUpDown();
            FilterNameList = new Label();
            SaveButton = new Button();
            ResetButton = new Button();
            RateLabel = new Label();
            RateBox = new TextBox();
            StopConditionsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)HPFilterMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AtkFilterMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpeFilterMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DefFilterMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpdFilterMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpaFilterMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HPFilterMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AtkFilterMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpeFilterMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DefFilterMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpdFilterMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpaFilterMin).BeginInit();
            SuspendLayout();
            // 
            // Abilitys
            // 
            Abilitys.Location = new Point(548, 65);
            Abilitys.Name = "Abilitys";
            Abilitys.Size = new Size(120, 76);
            Abilitys.TabIndex = 0;
            // 
            // RemoveNature
            // 
            RemoveNature.BackColor = Color.Transparent;
            RemoveNature.Location = new Point(595, 325);
            RemoveNature.Name = "RemoveNature";
            RemoveNature.Size = new Size(97, 24);
            RemoveNature.TabIndex = 64;
            RemoveNature.Text = "Remove Nature";
            RemoveNature.UseVisualStyleBackColor = false;
            RemoveNature.Click += RemoveNature_Click;
            // 
            // RemoveItem
            // 
            RemoveItem.BackColor = Color.Transparent;
            RemoveItem.Location = new Point(595, 368);
            RemoveItem.Name = "RemoveItem";
            RemoveItem.Size = new Size(92, 23);
            RemoveItem.TabIndex = 64;
            RemoveItem.Text = "Remove Item";
            RemoveItem.UseVisualStyleBackColor = false;
            RemoveItem.Click += RemoveItem_Click;
            // 
            // AddItem
            // 
            AddItem.BackColor = Color.Transparent;
            AddItem.Location = new Point(501, 368);
            AddItem.Name = "AddItem";
            AddItem.Size = new Size(75, 23);
            AddItem.TabIndex = 64;
            AddItem.Text = "Add Item";
            AddItem.UseVisualStyleBackColor = false;
            AddItem.Click += AddItem_Click;
            // 
            // Item
            // 
            Item.FormattingEnabled = true;
            Item.Location = new Point(374, 368);
            Item.Name = "Item";
            Item.Size = new Size(121, 23);
            Item.TabIndex = 65;
            // 
            // SpeciesBox
            // 
            SpeciesBox.FormattingEnabled = true;
            SpeciesBox.Location = new Point(374, 65);
            SpeciesBox.Name = "SpeciesBox";
            SpeciesBox.Size = new Size(121, 23);
            SpeciesBox.TabIndex = 65;
            SpeciesBox.SelectedIndexChanged += SetForm;
            // 
            // AddFilter
            // 
            AddFilter.BackColor = Color.Transparent;
            AddFilter.Location = new Point(45, 481);
            AddFilter.Name = "AddFilter";
            AddFilter.Size = new Size(101, 23);
            AddFilter.TabIndex = 64;
            AddFilter.Text = "Add Filter";
            AddFilter.UseVisualStyleBackColor = false;
            AddFilter.Click += AddFilter_Click;
            // 
            // RemoveFilter
            // 
            RemoveFilter.BackColor = Color.Transparent;
            RemoveFilter.Location = new Point(152, 481);
            RemoveFilter.Name = "RemoveFilter";
            RemoveFilter.Size = new Size(98, 23);
            RemoveFilter.TabIndex = 64;
            RemoveFilter.Text = "Remove Filter";
            RemoveFilter.UseVisualStyleBackColor = false;
            RemoveFilter.Click += RemoveFilter_Click;
            // 
            // AddNatureButton
            // 
            AddNatureButton.BackColor = Color.Transparent;
            AddNatureButton.Location = new Point(501, 326);
            AddNatureButton.Name = "AddNatureButton";
            AddNatureButton.Size = new Size(88, 23);
            AddNatureButton.TabIndex = 64;
            AddNatureButton.Text = "Add Nature";
            AddNatureButton.UseVisualStyleBackColor = false;
            AddNatureButton.Click += AddNatureButton_Click;
            // 
            // FilterEnabled
            // 
            FilterEnabled.AutoSize = true;
            FilterEnabled.Location = new Point(425, 485);
            FilterEnabled.Name = "FilterEnabled";
            FilterEnabled.Size = new Size(61, 19);
            FilterEnabled.TabIndex = 74;
            FilterEnabled.Text = "Enable";
            FilterEnabled.UseVisualStyleBackColor = true;
            // 
            // FilterName
            // 
            FilterName.FormattingEnabled = true;
            FilterName.Location = new Point(344, 514);
            FilterName.Margin = new Padding(3, 4, 3, 4);
            FilterName.Name = "FilterName";
            FilterName.Size = new Size(138, 23);
            FilterName.TabIndex = 65;
            FilterName.SelectedIndexChanged += FilterName_SelectedIndexChanged;
            // 
            // CurrentFilterName
            // 
            CurrentFilterName.BorderStyle = BorderStyle.FixedSingle;
            CurrentFilterName.Location = new Point(84, 26);
            CurrentFilterName.Name = "CurrentFilterName";
            CurrentFilterName.Size = new Size(136, 23);
            CurrentFilterName.TabIndex = 21;
            CurrentFilterName.TextChanged += CurrentFilterName_TextChanged;
            // 
            // NatureBox
            // 
            NatureBox.FormattingEnabled = true;
            NatureBox.Location = new Point(374, 326);
            NatureBox.Name = "NatureBox";
            NatureBox.Size = new Size(121, 23);
            NatureBox.TabIndex = 65;
            // 
            // StopConditionsGroup
            // 
            StopConditionsGroup.Controls.Add(RateBox);
            StopConditionsGroup.Controls.Add(RateLabel);
            StopConditionsGroup.Controls.Add(RemoveUnwantedMarkButton);
            StopConditionsGroup.Controls.Add(AddUnwantedMarkButton);
            StopConditionsGroup.Controls.Add(UnwantedMarkBox);
            StopConditionsGroup.Controls.Add(label9);
            StopConditionsGroup.Controls.Add(MarkCheck);
            StopConditionsGroup.Controls.Add(LanguageLabel);
            StopConditionsGroup.Controls.Add(Language);
            StopConditionsGroup.Controls.Add(FormBox);
            StopConditionsGroup.Controls.Add(Abilitys);
            StopConditionsGroup.Controls.Add(AbilityLabel);
            StopConditionsGroup.Controls.Add(RemoveMarkButton);
            StopConditionsGroup.Controls.Add(AddMarkButton);
            StopConditionsGroup.Controls.Add(MarkLabel);
            StopConditionsGroup.Controls.Add(MarkBox);
            StopConditionsGroup.Controls.Add(FormLabel);
            StopConditionsGroup.Controls.Add(SpeciesLabel);
            StopConditionsGroup.Controls.Add(CurrentFilterNameLabel);
            StopConditionsGroup.Controls.Add(ItemLable);
            StopConditionsGroup.Controls.Add(NatureLabel);
            StopConditionsGroup.Controls.Add(NatureBox);
            StopConditionsGroup.Controls.Add(RemoveItem);
            StopConditionsGroup.Controls.Add(RemoveNature);
            StopConditionsGroup.Controls.Add(AddItem);
            StopConditionsGroup.Controls.Add(Item);
            StopConditionsGroup.Controls.Add(CurrentFilterName);
            StopConditionsGroup.Controls.Add(SpeciesBox);
            StopConditionsGroup.Controls.Add(AddNatureButton);
            StopConditionsGroup.Controls.Add(label8);
            StopConditionsGroup.Controls.Add(label7);
            StopConditionsGroup.Controls.Add(label6);
            StopConditionsGroup.Controls.Add(label5);
            StopConditionsGroup.Controls.Add(label4);
            StopConditionsGroup.Controls.Add(label3);
            StopConditionsGroup.Controls.Add(label2);
            StopConditionsGroup.Controls.Add(ScaleBox);
            StopConditionsGroup.Controls.Add(ShinyBox);
            StopConditionsGroup.Controls.Add(label1);
            StopConditionsGroup.Controls.Add(PresetIVs);
            StopConditionsGroup.Controls.Add(PresetIVMaxBox);
            StopConditionsGroup.Controls.Add(PresetIVMinBox);
            StopConditionsGroup.Controls.Add(GenderBox);
            StopConditionsGroup.Controls.Add(IgnoreIVFilter);
            StopConditionsGroup.Controls.Add(CheckBoxOf3);
            StopConditionsGroup.Controls.Add(TargetSPE);
            StopConditionsGroup.Controls.Add(TargetSPD);
            StopConditionsGroup.Controls.Add(GenderFilter);
            StopConditionsGroup.Controls.Add(TargetSPA);
            StopConditionsGroup.Controls.Add(TargetIVs);
            StopConditionsGroup.Controls.Add(TargetDEF);
            StopConditionsGroup.Controls.Add(TargetHP);
            StopConditionsGroup.Controls.Add(TargetATK);
            StopConditionsGroup.Controls.Add(HPFilterMax);
            StopConditionsGroup.Controls.Add(AtkFilterMax);
            StopConditionsGroup.Controls.Add(SpeFilterMax);
            StopConditionsGroup.Controls.Add(DefFilterMax);
            StopConditionsGroup.Controls.Add(SpdFilterMax);
            StopConditionsGroup.Controls.Add(SpaFilterMax);
            StopConditionsGroup.Controls.Add(HPFilterMin);
            StopConditionsGroup.Controls.Add(AtkFilterMin);
            StopConditionsGroup.Controls.Add(SpeFilterMin);
            StopConditionsGroup.Controls.Add(DefFilterMin);
            StopConditionsGroup.Controls.Add(SpdFilterMin);
            StopConditionsGroup.Controls.Add(SpaFilterMin);
            StopConditionsGroup.Location = new Point(35, 12);
            StopConditionsGroup.Name = "StopConditionsGroup";
            StopConditionsGroup.Size = new Size(698, 463);
            StopConditionsGroup.TabIndex = 63;
            StopConditionsGroup.TabStop = false;
            StopConditionsGroup.Text = "Stop Conditions";
            // 
            // RemoveUnwantedMarkButton
            // 
            RemoveUnwantedMarkButton.BackColor = Color.Transparent;
            RemoveUnwantedMarkButton.Location = new Point(595, 406);
            RemoveUnwantedMarkButton.Name = "RemoveUnwantedMarkButton";
            RemoveUnwantedMarkButton.Size = new Size(97, 24);
            RemoveUnwantedMarkButton.TabIndex = 92;
            RemoveUnwantedMarkButton.Text = "Remove Mark";
            RemoveUnwantedMarkButton.UseVisualStyleBackColor = false;
            RemoveUnwantedMarkButton.Click += RemoveUnwantedMarkButton_Click;
            // 
            // AddUnwantedMarkButton
            // 
            AddUnwantedMarkButton.BackColor = Color.Transparent;
            AddUnwantedMarkButton.Location = new Point(501, 407);
            AddUnwantedMarkButton.Name = "AddUnwantedMarkButton";
            AddUnwantedMarkButton.Size = new Size(88, 23);
            AddUnwantedMarkButton.TabIndex = 91;
            AddUnwantedMarkButton.Text = "Add Mark";
            AddUnwantedMarkButton.UseVisualStyleBackColor = false;
            AddUnwantedMarkButton.Click += AddUnwantedMarkButton_Click;
            // 
            // UnwantedMarkBox
            // 
            UnwantedMarkBox.FormattingEnabled = true;
            UnwantedMarkBox.Location = new Point(374, 412);
            UnwantedMarkBox.Name = "UnwantedMarkBox";
            UnwantedMarkBox.Size = new Size(121, 23);
            UnwantedMarkBox.TabIndex = 90;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(280, 415);
            label9.Name = "label9";
            label9.Size = new Size(88, 15);
            label9.TabIndex = 89;
            label9.Text = "UnwantedMark";
            // 
            // MarkCheck
            // 
            MarkCheck.AutoSize = true;
            MarkCheck.Location = new Point(501, 164);
            MarkCheck.Name = "MarkCheck";
            MarkCheck.Size = new Size(86, 19);
            MarkCheck.TabIndex = 88;
            MarkCheck.Text = "Mark Only?";
            MarkCheck.UseVisualStyleBackColor = true;
            MarkCheck.CheckedChanged += MarkCheck_CheckedChanged;
            // 
            // LanguageLabel
            // 
            LanguageLabel.AutoSize = true;
            LanguageLabel.Location = new Point(302, 28);
            LanguageLabel.Name = "LanguageLabel";
            LanguageLabel.Size = new Size(59, 15);
            LanguageLabel.TabIndex = 87;
            LanguageLabel.Text = "Language";
            // 
            // Language
            // 
            Language.FormattingEnabled = true;
            Language.Location = new Point(372, 26);
            Language.Name = "Language";
            Language.Size = new Size(121, 23);
            Language.TabIndex = 86;
            Language.SelectedIndexChanged += Language_SelectedIndexChanged;
            // 
            // FormBox
            // 
            FormBox.FormattingEnabled = true;
            FormBox.Location = new Point(374, 94);
            FormBox.Name = "FormBox";
            FormBox.Size = new Size(121, 23);
            FormBox.TabIndex = 85;
            FormBox.SelectedIndexChanged += FormBox_SelectedIndexChanged;
            // 
            // AbilityLabel
            // 
            AbilityLabel.AutoSize = true;
            AbilityLabel.Location = new Point(501, 68);
            AbilityLabel.Name = "AbilityLabel";
            AbilityLabel.Size = new Size(41, 15);
            AbilityLabel.TabIndex = 84;
            AbilityLabel.Text = "Ability";
            // 
            // RemoveMarkButton
            // 
            RemoveMarkButton.BackColor = Color.Transparent;
            RemoveMarkButton.Location = new Point(595, 282);
            RemoveMarkButton.Name = "RemoveMarkButton";
            RemoveMarkButton.Size = new Size(97, 24);
            RemoveMarkButton.TabIndex = 83;
            RemoveMarkButton.Text = "Remove Mark";
            RemoveMarkButton.UseVisualStyleBackColor = false;
            RemoveMarkButton.Click += RemoveMarkButton_Click;
            // 
            // AddMarkButton
            // 
            AddMarkButton.BackColor = Color.Transparent;
            AddMarkButton.Location = new Point(501, 282);
            AddMarkButton.Name = "AddMarkButton";
            AddMarkButton.Size = new Size(88, 23);
            AddMarkButton.TabIndex = 82;
            AddMarkButton.Text = "Add Mark";
            AddMarkButton.UseVisualStyleBackColor = false;
            AddMarkButton.Click += AddMarkButton_Click;
            // 
            // MarkLabel
            // 
            MarkLabel.AutoSize = true;
            MarkLabel.Location = new Point(325, 284);
            MarkLabel.Name = "MarkLabel";
            MarkLabel.Size = new Size(34, 15);
            MarkLabel.TabIndex = 81;
            MarkLabel.Text = "Mark";
            // 
            // MarkBox
            // 
            MarkBox.FormattingEnabled = true;
            MarkBox.Location = new Point(374, 281);
            MarkBox.Name = "MarkBox";
            MarkBox.Size = new Size(121, 23);
            MarkBox.TabIndex = 80;
            // 
            // FormLabel
            // 
            FormLabel.AutoSize = true;
            FormLabel.Location = new Point(325, 96);
            FormLabel.Name = "FormLabel";
            FormLabel.Size = new Size(34, 15);
            FormLabel.TabIndex = 79;
            FormLabel.Text = "Form";
            // 
            // SpeciesLabel
            // 
            SpeciesLabel.AutoSize = true;
            SpeciesLabel.Location = new Point(322, 68);
            SpeciesLabel.Name = "SpeciesLabel";
            SpeciesLabel.Size = new Size(46, 15);
            SpeciesLabel.TabIndex = 78;
            SpeciesLabel.Text = "Species";
            // 
            // CurrentFilterNameLabel
            // 
            CurrentFilterNameLabel.AutoSize = true;
            CurrentFilterNameLabel.Location = new Point(6, 28);
            CurrentFilterNameLabel.Name = "CurrentFilterNameLabel";
            CurrentFilterNameLabel.Size = new Size(72, 15);
            CurrentFilterNameLabel.TabIndex = 76;
            CurrentFilterNameLabel.Text = "CurrentFilter";
            // 
            // ItemLable
            // 
            ItemLable.AutoSize = true;
            ItemLable.Location = new Point(325, 371);
            ItemLable.Name = "ItemLable";
            ItemLable.Size = new Size(30, 15);
            ItemLable.TabIndex = 75;
            ItemLable.Text = "Item";
            // 
            // NatureLabel
            // 
            NatureLabel.AutoSize = true;
            NatureLabel.Location = new Point(325, 332);
            NatureLabel.Name = "NatureLabel";
            NatureLabel.Size = new Size(43, 15);
            NatureLabel.TabIndex = 74;
            NatureLabel.Text = "Nature";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(59, 284);
            label8.Name = "label8";
            label8.Size = new Size(23, 15);
            label8.TabIndex = 73;
            label8.Text = "HP";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(130, 284);
            label7.Name = "label7";
            label7.Size = new Size(28, 15);
            label7.TabIndex = 72;
            label7.Text = "ATK";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(202, 284);
            label6.Name = "label6";
            label6.Size = new Size(27, 15);
            label6.TabIndex = 71;
            label6.Text = "DEF";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(57, 326);
            label5.Name = "label5";
            label5.Size = new Size(28, 15);
            label5.TabIndex = 70;
            label5.Text = "SPA";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(130, 326);
            label4.Name = "label4";
            label4.Size = new Size(28, 15);
            label4.TabIndex = 69;
            label4.Text = "SPD";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(203, 326);
            label3.Name = "label3";
            label3.Size = new Size(26, 15);
            label3.TabIndex = 68;
            label3.Text = "SPE";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(34, 386);
            label2.Name = "label2";
            label2.Size = new Size(84, 15);
            label2.TabIndex = 67;
            label2.Text = "Preset IVs Min:";
            // 
            // ScaleBox
            // 
            ScaleBox.AutoSize = true;
            ScaleBox.Location = new Point(372, 164);
            ScaleBox.Name = "ScaleBox";
            ScaleBox.Size = new Size(123, 19);
            ScaleBox.TabIndex = 66;
            ScaleBox.Text = "Mini/Jumbo Only?";
            ScaleBox.UseVisualStyleBackColor = true;
            // 
            // ShinyBox
            // 
            ShinyBox.FormattingEnabled = true;
            ShinyBox.Items.AddRange(new object[] { "DisableOption", "NonShiny", "AnyShiny", "StarOnly", "SquareOnly" });
            ShinyBox.Location = new Point(374, 239);
            ShinyBox.MaxDropDownItems = 5;
            ShinyBox.Name = "ShinyBox";
            ShinyBox.Size = new Size(121, 23);
            ShinyBox.TabIndex = 65;
            ShinyBox.Text = "DisableOption";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(288, 242);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 64;
            label1.Text = "Shiny Target";
            // 
            // PresetIVs
            // 
            PresetIVs.AutoSize = true;
            PresetIVs.Location = new Point(34, 216);
            PresetIVs.Name = "PresetIVs";
            PresetIVs.Size = new Size(86, 15);
            PresetIVs.TabIndex = 63;
            PresetIVs.Text = "Preset IVs Max:";
            // 
            // PresetIVMaxBox
            // 
            PresetIVMaxBox.FormattingEnabled = true;
            PresetIVMaxBox.Items.AddRange(new object[] { "None Selected", "6 IV", "0 ATK 0 SPE", "0 ATK 5 IV", "0 SPE 5 IV" });
            PresetIVMaxBox.Location = new Point(130, 214);
            PresetIVMaxBox.Name = "PresetIVMaxBox";
            PresetIVMaxBox.Size = new Size(141, 23);
            PresetIVMaxBox.TabIndex = 62;
            PresetIVMaxBox.Text = "None Selected";
            PresetIVMaxBox.SelectedIndexChanged += PresetIVMaxBox_SelectedIndexChanged;
            // 
            // PresetIVMinBox
            // 
            PresetIVMinBox.FormattingEnabled = true;
            PresetIVMinBox.Items.AddRange(new object[] { "None Selected", "6 IV", "0 ATK 0 SPE", "0 ATK 5 IV", "0 SPE 5 IV" });
            PresetIVMinBox.Location = new Point(130, 383);
            PresetIVMinBox.Name = "PresetIVMinBox";
            PresetIVMinBox.Size = new Size(141, 23);
            PresetIVMinBox.TabIndex = 62;
            PresetIVMinBox.Text = "None Selected";
            PresetIVMinBox.SelectedIndexChanged += PresetIVMinBox_SelectedIndexChanged;
            // 
            // GenderBox
            // 
            GenderBox.FormattingEnabled = true;
            GenderBox.Items.AddRange(new object[] { "Male", "Female", "Genderless", "Any" });
            GenderBox.Location = new Point(374, 201);
            GenderBox.MaxDropDownItems = 4;
            GenderBox.Name = "GenderBox";
            GenderBox.Size = new Size(106, 23);
            GenderBox.TabIndex = 61;
            GenderBox.Text = "Male";
            // 
            // IgnoreIVFilter
            // 
            IgnoreIVFilter.AutoSize = true;
            IgnoreIVFilter.Location = new Point(113, 425);
            IgnoreIVFilter.Name = "IgnoreIVFilter";
            IgnoreIVFilter.Size = new Size(107, 19);
            IgnoreIVFilter.TabIndex = 60;
            IgnoreIVFilter.Text = "Ignore IV Filter?";
            IgnoreIVFilter.UseVisualStyleBackColor = true;
            // 
            // CheckBoxOf3
            // 
            CheckBoxOf3.AutoSize = true;
            CheckBoxOf3.Location = new Point(372, 127);
            CheckBoxOf3.Name = "CheckBoxOf3";
            CheckBoxOf3.Size = new Size(147, 19);
            CheckBoxOf3.TabIndex = 12;
            CheckBoxOf3.Text = "3 Segment/Family of 3?";
            CheckBoxOf3.UseVisualStyleBackColor = true;
            // 
            // TargetSPE
            // 
            TargetSPE.AutoSize = true;
            TargetSPE.Location = new Point(203, 154);
            TargetSPE.Name = "TargetSPE";
            TargetSPE.Size = new Size(26, 15);
            TargetSPE.TabIndex = 59;
            TargetSPE.Text = "SPE";
            // 
            // TargetSPD
            // 
            TargetSPD.AutoSize = true;
            TargetSPD.Location = new Point(130, 154);
            TargetSPD.Name = "TargetSPD";
            TargetSPD.Size = new Size(28, 15);
            TargetSPD.TabIndex = 58;
            TargetSPD.Text = "SPD";
            // 
            // GenderFilter
            // 
            GenderFilter.AutoSize = true;
            GenderFilter.Location = new Point(314, 204);
            GenderFilter.Name = "GenderFilter";
            GenderFilter.Size = new Size(45, 15);
            GenderFilter.TabIndex = 45;
            GenderFilter.Text = "Gender";
            // 
            // TargetSPA
            // 
            TargetSPA.AutoSize = true;
            TargetSPA.Location = new Point(57, 154);
            TargetSPA.Name = "TargetSPA";
            TargetSPA.Size = new Size(28, 15);
            TargetSPA.TabIndex = 57;
            TargetSPA.Text = "SPA";
            // 
            // TargetIVs
            // 
            TargetIVs.AutoSize = true;
            TargetIVs.Location = new Point(140, 102);
            TargetIVs.Name = "TargetIVs";
            TargetIVs.Size = new Size(46, 15);
            TargetIVs.TabIndex = 53;
            TargetIVs.Text = "IV Filter";
            // 
            // TargetDEF
            // 
            TargetDEF.AutoSize = true;
            TargetDEF.Location = new Point(202, 125);
            TargetDEF.Name = "TargetDEF";
            TargetDEF.Size = new Size(27, 15);
            TargetDEF.TabIndex = 56;
            TargetDEF.Text = "DEF";
            // 
            // TargetHP
            // 
            TargetHP.AutoSize = true;
            TargetHP.Location = new Point(59, 125);
            TargetHP.Name = "TargetHP";
            TargetHP.Size = new Size(23, 15);
            TargetHP.TabIndex = 54;
            TargetHP.Text = "HP";
            // 
            // TargetATK
            // 
            TargetATK.AutoSize = true;
            TargetATK.Location = new Point(130, 125);
            TargetATK.Name = "TargetATK";
            TargetATK.Size = new Size(28, 15);
            TargetATK.TabIndex = 55;
            TargetATK.Text = "ATK";
            // 
            // HPFilterMax
            // 
            HPFilterMax.Location = new Point(85, 123);
            HPFilterMax.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            HPFilterMax.Name = "HPFilterMax";
            HPFilterMax.Size = new Size(42, 23);
            HPFilterMax.TabIndex = 47;
            HPFilterMax.Value = new decimal(new int[] { 31, 0, 0, 0 });
            // 
            // AtkFilterMax
            // 
            AtkFilterMax.Location = new Point(158, 123);
            AtkFilterMax.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            AtkFilterMax.Name = "AtkFilterMax";
            AtkFilterMax.Size = new Size(41, 23);
            AtkFilterMax.TabIndex = 48;
            AtkFilterMax.Value = new decimal(new int[] { 31, 0, 0, 0 });
            // 
            // SpeFilterMax
            // 
            SpeFilterMax.Location = new Point(230, 151);
            SpeFilterMax.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            SpeFilterMax.Name = "SpeFilterMax";
            SpeFilterMax.Size = new Size(40, 23);
            SpeFilterMax.TabIndex = 52;
            SpeFilterMax.Value = new decimal(new int[] { 31, 0, 0, 0 });
            // 
            // DefFilterMax
            // 
            DefFilterMax.Location = new Point(230, 123);
            DefFilterMax.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            DefFilterMax.Name = "DefFilterMax";
            DefFilterMax.Size = new Size(40, 23);
            DefFilterMax.TabIndex = 49;
            DefFilterMax.Value = new decimal(new int[] { 31, 0, 0, 0 });
            // 
            // SpdFilterMax
            // 
            SpdFilterMax.Location = new Point(158, 151);
            SpdFilterMax.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            SpdFilterMax.Name = "SpdFilterMax";
            SpdFilterMax.Size = new Size(41, 23);
            SpdFilterMax.TabIndex = 51;
            SpdFilterMax.Value = new decimal(new int[] { 31, 0, 0, 0 });
            // 
            // SpaFilterMax
            // 
            SpaFilterMax.Location = new Point(85, 151);
            SpaFilterMax.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            SpaFilterMax.Name = "SpaFilterMax";
            SpaFilterMax.Size = new Size(42, 23);
            SpaFilterMax.TabIndex = 50;
            SpaFilterMax.Value = new decimal(new int[] { 31, 0, 0, 0 });
            // 
            // HPFilterMin
            // 
            HPFilterMin.Location = new Point(85, 282);
            HPFilterMin.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            HPFilterMin.Name = "HPFilterMin";
            HPFilterMin.Size = new Size(42, 23);
            HPFilterMin.TabIndex = 47;
            HPFilterMin.Value = new decimal(new int[] { 31, 0, 0, 0 });
            // 
            // AtkFilterMin
            // 
            AtkFilterMin.Location = new Point(158, 282);
            AtkFilterMin.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            AtkFilterMin.Name = "AtkFilterMin";
            AtkFilterMin.Size = new Size(41, 23);
            AtkFilterMin.TabIndex = 48;
            AtkFilterMin.Value = new decimal(new int[] { 31, 0, 0, 0 });
            // 
            // SpeFilterMin
            // 
            SpeFilterMin.Location = new Point(230, 324);
            SpeFilterMin.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            SpeFilterMin.Name = "SpeFilterMin";
            SpeFilterMin.Size = new Size(40, 23);
            SpeFilterMin.TabIndex = 52;
            SpeFilterMin.Value = new decimal(new int[] { 31, 0, 0, 0 });
            // 
            // DefFilterMin
            // 
            DefFilterMin.Location = new Point(230, 282);
            DefFilterMin.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            DefFilterMin.Name = "DefFilterMin";
            DefFilterMin.Size = new Size(40, 23);
            DefFilterMin.TabIndex = 49;
            DefFilterMin.Value = new decimal(new int[] { 31, 0, 0, 0 });
            // 
            // SpdFilterMin
            // 
            SpdFilterMin.Location = new Point(158, 324);
            SpdFilterMin.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            SpdFilterMin.Name = "SpdFilterMin";
            SpdFilterMin.Size = new Size(41, 23);
            SpdFilterMin.TabIndex = 51;
            SpdFilterMin.Value = new decimal(new int[] { 31, 0, 0, 0 });
            // 
            // SpaFilterMin
            // 
            SpaFilterMin.Location = new Point(85, 324);
            SpaFilterMin.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            SpaFilterMin.Name = "SpaFilterMin";
            SpaFilterMin.Size = new Size(42, 23);
            SpaFilterMin.TabIndex = 50;
            SpaFilterMin.Value = new decimal(new int[] { 31, 0, 0, 0 });
            // 
            // FilterNameList
            // 
            FilterNameList.AutoSize = true;
            FilterNameList.Location = new Point(256, 517);
            FilterNameList.Name = "FilterNameList";
            FilterNameList.Size = new Size(82, 15);
            FilterNameList.TabIndex = 77;
            FilterNameList.Text = "FilterNameList";
            // 
            // SaveButton
            // 
            SaveButton.BackColor = Color.Transparent;
            SaveButton.Location = new Point(256, 482);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 64;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = false;
            SaveButton.Click += SaveButton_Click;
            // 
            // ResetButton
            // 
            ResetButton.BackColor = Color.Transparent;
            ResetButton.Location = new Point(344, 482);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(75, 23);
            ResetButton.TabIndex = 65;
            ResetButton.Text = "Reset";
            ResetButton.UseVisualStyleBackColor = false;
            ResetButton.Click += ResetButton_Click;
            // 
            // RateLabel
            // 
            RateLabel.AutoSize = true;
            RateLabel.Location = new Point(48, 68);
            RateLabel.Name = "RateLabel";
            RateLabel.Size = new Size(30, 15);
            RateLabel.TabIndex = 93;
            RateLabel.Text = "Rate";
            // 
            // RateBox
            // 
            RateBox.BorderStyle = BorderStyle.FixedSingle;
            RateBox.Location = new Point(84, 63);
            RateBox.Name = "RateBox";
            RateBox.Size = new Size(136, 23);
            RateBox.TabIndex = 94;
            // 
            // StopConditions
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(745, 550);
            Controls.Add(ResetButton);
            Controls.Add(SaveButton);
            Controls.Add(FilterNameList);
            Controls.Add(AddFilter);
            Controls.Add(RemoveFilter);
            Controls.Add(StopConditionsGroup);
            Controls.Add(FilterEnabled);
            Controls.Add(FilterName);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "StopConditions";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Stop Conditions";
            StopConditionsGroup.ResumeLayout(false);
            StopConditionsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)HPFilterMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)AtkFilterMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpeFilterMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)DefFilterMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpdFilterMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpaFilterMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)HPFilterMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)AtkFilterMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpeFilterMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)DefFilterMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpdFilterMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpaFilterMin).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private GroupBox StopConditionsGroup;
        private CheckBox IgnoreIVFilter;
        private CheckBox CheckBoxOf3;
        private Label TargetSPE;
        private Label TargetSPD;
        private Label GenderFilter;
        private Label TargetSPA;
        private Label TargetIVs;
        private Label TargetDEF;
        private Label TargetHP;
        private Label TargetATK;
        private NumericUpDown HPFilterMax;
        private NumericUpDown AtkFilterMax;
        private NumericUpDown SpeFilterMax;
        private NumericUpDown DefFilterMax;
        private NumericUpDown SpdFilterMax;
        private NumericUpDown SpaFilterMax;
        private NumericUpDown HPFilterMin;
        private NumericUpDown AtkFilterMin;
        private NumericUpDown SpeFilterMin;
        private NumericUpDown DefFilterMin;
        private NumericUpDown SpdFilterMin;
        private NumericUpDown SpaFilterMin;
        private Button SaveButton;
        private Button ResetButton;
        private ComboBox GenderBox;
        private Label PresetIVs;
        private ComboBox PresetIVMaxBox;
        private ComboBox PresetIVMinBox;
        private ComboBox ShinyBox;
        private Label label1;
        private CheckBox ScaleBox;
        private Label label2;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Button AddNatureButton;
        private Button AddFilter;
        private Button RemoveFilter;
        private ComboBox FilterName;
        private ComboBox NatureBox;
        private CheckBox FilterEnabled;
        private ComboBox SpeciesBox;
        private TextBox CurrentFilterName;
        private ComboBox Item;
        private Button AddItem;
        private Button RemoveNature;
        private Button RemoveItem;
        private Label NatureLabel;
        private Label ItemLable;
        private Label CurrentFilterNameLabel;
        private Label FilterNameList;
        private Label SpeciesLabel;
        private Label FormLabel;
        private ComboBox MarkBox;
        private Button RemoveMarkButton;
        private Button AddMarkButton;
        private Label MarkLabel;
        private CheckedListBox Abilitys;
        private Label AbilityLabel;
        private ComboBox FormBox;
        private Label LanguageLabel;
        private ComboBox Language;
        private CheckBox MarkCheck;
        private Label label9;
        private Button RemoveUnwantedMarkButton;
        private Button AddUnwantedMarkButton;
        private ComboBox UnwantedMarkBox;
        private TextBox RateBox;
        private Label RateLabel;
    }
}