using NLog.Filters;
using PKHeX.Core;
using PokeViewer.NET.Misc;
using SysBot.Base;
using System.ComponentModel;
using System.Text.Json;

namespace PokeViewer.NET.SubForms
{
    public partial class StopConditions : Form
    {
        private readonly List<EncounterFilter> filter;
        private GameStrings Strings;
        private List<string?> FilterNames = new();
        private List<Nature> NatureList = new();
        private List<int> ItemList = new();
        private List<RibbonIndex> MarkList = new();
        private List<RibbonIndex> UnwantedMarkList = new();
        private string[] FormList = [];
        private readonly string[] GenderList = [];
        private string[] TypesList = [];
        private string[] AbilityList = [];
        private FilterMode filtermode;
        private bool FirstFlag = false;
        private bool ResetFalsg = false;
        public StopConditions((Color, Color) color, ref List<EncounterFilter> filters, FilterMode filterMode)
        {
            InitializeComponent();
            UnwantedMarkBox.Enabled = MarkCheck.Checked;
            AddUnwantedMarkButton.Enabled = MarkCheck.Checked;
            RemoveUnwantedMarkButton.Enabled = MarkCheck.Checked;
            filter = filters;
            filtermode = filterMode;
            Language.DataSource = Enum.GetValues(typeof(LanguageID)).Cast<LanguageID>().Where(z => z != LanguageID.None && z != LanguageID.UNUSED_6).ToArray();
            Language.SelectedIndex = 1;
            Strings = GameInfo.GetStrings(Language.SelectedIndex);
            NatureBox.DataSource = Strings.natures;
            SpeciesBox.DataSource = Strings.specieslist;
            MarkBox.DataSource = Strings.ribbons.AsSpan(106, Strings.ribbons.Length - 106).ToArray().Select(z => z = z.Split("\t")[1]).ToArray();
            UnwantedMarkBox.DataSource = Strings.ribbons.AsSpan(106, Strings.ribbons.Length - 106).ToArray().Select(z => z = z.Split("\t")[1]).ToArray();
            Item.DataSource = Strings.itemlist;
            AbilityList = Strings.abilitylist;
            FormList = Strings.forms;
            TypesList = Strings.types;
            GenderList = [.. GameInfo.GenderSymbolUnicode];
            if(filterMode == FilterMode.Wide)
            {
                RateBox.Enabled = false;
            }
            if (filtermode == FilterMode.Egg || filtermode == FilterMode.MysteryGift_SV || filtermode == FilterMode.MysteryGift_SWSH)
            {
                AddItem.Enabled = false;
                RemoveItem.Enabled = false;
                Item.Enabled = false;
                AddMarkButton.Enabled = false;
                RemoveMarkButton.Enabled = false;
                MarkBox.Enabled = false;
                MarkCheck.Enabled = false;
                UnwantedMarkBox.Enabled = false;
                AddUnwantedMarkButton.Enabled = false;
                RemoveUnwantedMarkButton.Enabled = false;
            }
            FirstFlag = true;
            ResetActiveFilters();
            SetColors(color);
        }
        private void ResetActiveFilters()
        {
            FilterNames = new();
            for (int i = 0; i < filter.Count; i++)
                FilterNames.Add(filter[i].Name);
            FilterName.DataSource = FilterNames;
            if (FilterName.Items.Count == 0)
                RemoveFilter.Enabled = false;
            else
                FilterName.SelectedIndex = 0;

        }

        private void SetColors((Color, Color) color)
        {
            BackColor = color.Item1;
            ForeColor = color.Item2;
            SaveButton.BackColor = color.Item1;
            SaveButton.ForeColor = color.Item2;
            ResetButton.BackColor = color.Item1;
            ResetButton.ForeColor = color.Item2;
            AddFilter.BackColor = color.Item1;
            AddFilter.ForeColor = color.Item2;
            RemoveFilter.BackColor = color.Item1;
            RemoveFilter.ForeColor = color.Item2;
            AddMarkButton.BackColor = color.Item1;
            AddMarkButton.ForeColor = color.Item2;
            RemoveMarkButton.BackColor = color.Item1;
            RemoveMarkButton.ForeColor= color.Item2;
            AddNatureButton.BackColor = color.Item1;
            AddNatureButton.ForeColor = color.Item2;
            RemoveNature.BackColor = color.Item1;
            RemoveNature.ForeColor = color.Item2;
            AddItem.BackColor = color.Item1;
            AddItem.ForeColor = color.Item2;
            RemoveItem.BackColor = color.Item1;
            RemoveItem.ForeColor = color.Item2;
            NatureBox.BackColor = color.Item1;
            NatureBox.ForeColor = color.Item2;
            FilterName.BackColor = color.Item1;
            FilterName.ForeColor = color.Item2;
            ShinyBox.BackColor = color.Item1;
            ShinyBox.ForeColor = color.Item2;
            GenderBox.BackColor = color.Item1;
            GenderBox.ForeColor = color.Item2;
            Item.BackColor = color.Item1;
            Item.ForeColor = color.Item2;
            MarkBox.BackColor = color.Item1;
            MarkBox.ForeColor = color.Item2;
            UnwantedMarkBox.BackColor = color.Item1;
            UnwantedMarkBox.ForeColor = color.Item2;
            SpeciesBox.BackColor = color.Item1;
            SpeciesBox.ForeColor = color.Item2;
            FormBox.BackColor = color.Item1;
            FormBox.ForeColor = color.Item2;
            CheckBoxOf3.BackColor = color.Item1;
            CheckBoxOf3.ForeColor = color.Item2;
            FilterEnabled.BackColor = color.Item1;
            FilterEnabled.ForeColor = color.Item2;
            ScaleBox.BackColor = color.Item1;
            ScaleBox.ForeColor = color.Item2;
            GenderFilter.BackColor = color.Item1;
            GenderFilter.ForeColor = color.Item2;
            IgnoreIVFilter.BackColor = color.Item1;
            IgnoreIVFilter.ForeColor = color.Item2;
            PresetIVMaxBox.BackColor = color.Item1;
            PresetIVMaxBox.ForeColor = color.Item2;
            PresetIVs.BackColor = color.Item1;
            PresetIVs.ForeColor = color.Item2;
            HPFilterMax.BackColor = color.Item1;
            HPFilterMax.ForeColor = color.Item2;
            AtkFilterMax.BackColor = color.Item1;
            AtkFilterMax.ForeColor = color.Item2;
            DefFilterMax.BackColor = color.Item1;
            DefFilterMax.ForeColor = color.Item2;
            SpaFilterMax.BackColor = color.Item1;
            SpaFilterMax.ForeColor = color.Item2;
            SpdFilterMax.BackColor = color.Item1;
            SpdFilterMax.ForeColor = color.Item2;
            SpeFilterMax.BackColor = color.Item1;
            SpeFilterMax.ForeColor = color.Item2;
            HPFilterMin.BackColor = color.Item1;
            HPFilterMin.ForeColor = color.Item2;
            AtkFilterMin.BackColor = color.Item1;
            AtkFilterMin.ForeColor = color.Item2;
            DefFilterMin.BackColor = color.Item1;
            DefFilterMin.ForeColor = color.Item2;
            SpaFilterMin.BackColor = color.Item1;
            SpaFilterMin.ForeColor = color.Item2;
            SpdFilterMin.BackColor = color.Item1;
            SpdFilterMin.ForeColor = color.Item2;
            SpeFilterMin.BackColor = color.Item1;
            SpeFilterMin.ForeColor = color.Item2;
            TargetATK.BackColor = color.Item1;
            TargetATK.ForeColor = color.Item2;
            TargetHP.BackColor = color.Item1;
            TargetHP.ForeColor = color.Item2;
            TargetDEF.BackColor = color.Item1;
            TargetDEF.ForeColor = color.Item2;
            TargetSPA.BackColor = color.Item1;
            TargetSPA.ForeColor = color.Item2;
            TargetSPD.BackColor = color.Item1;
            TargetSPD.ForeColor = color.Item2;
            TargetSPE.BackColor = color.Item1;
            TargetSPE.ForeColor = color.Item2;
            StopConditionsGroup.BackColor = color.Item1;
            StopConditionsGroup.ForeColor = color.Item2;
            Abilitys.BackColor = color.Item1;
            Abilitys.ForeColor = color.Item2;
        }
        private void Language_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Language.SelectedIndex < 0)
                return;
            Strings = GameInfo.GetStrings(Language.SelectedIndex);
            var speciesindex = SpeciesBox.SelectedIndex;
            var markindex = MarkBox.SelectedIndex;
            var unwantedmarkindex = UnwantedMarkBox.SelectedIndex;
            var natuteindex = NatureBox.SelectedIndex;
            var itemindex = Item.SelectedIndex;
            var formindex = FormBox.Visible ? FormBox.SelectedIndex : 0;
            List<Ability> abilities = [];
            List<int> abilitylindex = [];
            for (int i = 0; i < Abilitys.Items.Count; i++)
            {
                if (!Abilitys.GetItemChecked(i))
                    continue;
                var item = Abilitys.Items[i].ToString();
                var ability = (Ability)AbilityList.ToList().IndexOf(item == null ? "" : item);
                abilitylindex.Add(i);
                abilities.Add(ability);
            }            
            SpeciesBox.DataSource = Strings.specieslist;
            MarkBox.DataSource = Strings.ribbons.AsSpan(106, Strings.ribbons.Length - 106).ToArray().Select(z => z = z.Split("\t")[1]).ToArray();
            UnwantedMarkBox.DataSource = Strings.ribbons.AsSpan(106, Strings.ribbons.Length - 106).ToArray().Select(z => z = z.Split("\t")[1]).ToArray();
            NatureBox.DataSource = Strings.natures;
            Item.DataSource = Strings.itemlist;
            FormList = Strings.forms;
            AbilityList = Strings.abilitylist;
            TypesList = Strings.types;
            SpeciesBox.SelectedIndex = speciesindex < 0 ? 0 : speciesindex;
            if(FormBox.Visible)
                FormBox.SelectedIndex = formindex < 0 ? 0 : formindex;
            SetAbility(FormBox.Visible ? (byte)FormBox.SelectedIndex : (byte)0);
            if(abilities.Count > 0 && abilitylindex.Count > 0)
            {
                for(int i = 0; i < Abilitys.Items.Count; i++)
                {
                    var item = Abilitys.Items[i].ToString();
                    var ability = (Ability)AbilityList.ToList().IndexOf(item == null ? "" : item);
                    if (abilities.Contains(ability) && abilitylindex.Contains(i))
                    {
                        Abilitys.SetItemChecked(i, true);
                    }
                }
            }
            MarkBox.SelectedIndex = markindex < 0 ? 0 : markindex;
            UnwantedMarkBox.SelectedIndex = unwantedmarkindex < 0 ? 0 : unwantedmarkindex;
            NatureBox.SelectedIndex = natuteindex < 0 ? 0 : natuteindex;
            Item.SelectedIndex = itemindex < 0 ? 0 : itemindex;
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            string output = JsonSerializer.Serialize(filter);
            using StreamWriter sw = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{filtermode}filters.json"));
            sw.Write(output);
            Close();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetFilters();
            MessageBox.Show("Stop Conditions have been reset to default values.");
        }
        private void ResetFilters()
        {
            RateBox.Text = string.Empty;
            ShinyBox.SelectedIndex = 2;
            CheckBoxOf3.Checked = false;
            GenderBox.SelectedIndex = 3;
            NatureBox.SelectedIndex = 0;
            Item.SelectedIndex = 0;
            SpeciesBox.SelectedIndex = 0;
            Abilitys.Items.Clear();
            HPFilterMax.Value = 31;
            AtkFilterMax.Value = 31;
            DefFilterMax.Value = 31;
            SpaFilterMax.Value = 31;
            SpdFilterMax.Value = 31;
            SpeFilterMax.Value = 31;
            HPFilterMin.Value = 0;
            AtkFilterMin.Value = 0;
            DefFilterMin.Value = 0;
            SpaFilterMin.Value = 0;
            SpdFilterMin.Value = 0;
            SpeFilterMin.Value = 0;
            IgnoreIVFilter.Checked = true;
            PresetIVMaxBox.SelectedIndex = 0;
            PresetIVMinBox.SelectedIndex = 0;
            ScaleBox.Checked = false;
            NatureList = new();
            ItemList = new();
            MarkList = new();
            UnwantedMarkList = new();
            MarkCheck.Checked = false;
        }
        private void SetFilters(EncounterFilter filter)
        {
            NumericUpDown[] IVMax = { HPFilterMax, AtkFilterMax, DefFilterMax, SpaFilterMax, SpdFilterMax, SpeFilterMax };
            NumericUpDown[] IVMin = { HPFilterMin, AtkFilterMin, DefFilterMin, SpaFilterMin, SpdFilterMin, SpeFilterMin };
            CurrentFilterName.Text = filter.Name;
            RateBox.Text = RateBox.Enabled ? filter.Rate : string.Empty;
            SpeciesBox.SelectedIndex = filter.Species == null ? 0 : (int)filter.Species;
            SetForm();
            if(FormBox.Visible)
                FormBox.SelectedIndex = filter.Form == null ? 0 : (int)filter.Form;
            Abilitys.Items.Clear();
            if(filter.Species != null)
                SetAbility((byte)FormBox.SelectedIndex);    
            if(filter.AbilityList != null)
            {
                for(int i = 0; i < Abilitys.Items.Count; i++)
                {
                    var item = Abilitys.Items[i].ToString();
                    if (filter.AbilityList.Contains((Ability)(AbilityList.ToList().IndexOf(item == null ? "": item))))
                    {
                        Abilitys.SetItemChecked(i, true);
                    }
                }
            }
            GenderBox.SelectedIndex = filter.Gender;
            ShinyBox.SelectedIndex = filter.Shiny;
            IgnoreIVFilter.Checked = filter.ignoreIVs;
            PresetIVMaxBox.SelectedIndex = filter.IVMaxindex == null ? 0 : (int)filter.IVMaxindex;
            PresetIVMinBox.SelectedIndex = filter.IVMinindex == null ? 0 : (int)filter.IVMinindex;
            if (!filter.ignoreIVs)
            {
                for (int i = 0; i < filter.MaxIVs!.Length; i++)
                    IVMax[i].Value = filter.MaxIVs[i];
                for (int i = 0; i < filter.MinIVs!.Length; i++)
                    IVMin[i].Value = filter.MinIVs[i];
            }
            else
            {
                for (int i = 0; i < IVMax.Length; i++)
                    IVMax[i].Value = 31;
                for (int i = 0; i < IVMin.Length; i++)
                    IVMin[i].Value = 0;
            }
            ScaleBox.Checked = filter.Scale;
            CheckBoxOf3.Checked = filter.ThreeSegment;
            ItemList = filter.ItemList == null ? new() : filter.ItemList;
            NatureList = filter.Nature == null ? new() : filter.Nature;
            MarkList = filter.Mark == null || filter.Mark.Count == 0 ? new() : filter.Mark;
            UnwantedMarkList = !filter.MarkOnly || filter.UnwantedMark == null ? new() : filter.UnwantedMark; 
            MarkCheck.Checked = (filter.Mark != null && filter.Mark.Count > 0) || filter.MarkOnly;
            FilterEnabled.Checked = filter.Enabled;
            ShowFilterInfo();
        }
        private void AddFilter_Click(object sender, EventArgs e)
        {
            FilterName.SelectedIndex = -1;
            if (CurrentFilterName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Name is a required field!");
                return;
            }
            EncounterFilter encfilter = new();
            NumericUpDown[] IVMax = { HPFilterMax, AtkFilterMax, DefFilterMax, SpaFilterMax, SpdFilterMax, SpeFilterMax };
            NumericUpDown[] IVMin = { HPFilterMin, AtkFilterMin, DefFilterMin, SpaFilterMin, SpdFilterMin, SpeFilterMin };

            encfilter.Name = CurrentFilterName.Text.Trim();
            encfilter.Rate = RateBox.Enabled ? RateBox.Text.Replace(" ", "") : string.Empty;
            encfilter.ignoreIVs = IgnoreIVFilter.Checked;
            if (!IgnoreIVFilter.Checked)
            {
                encfilter.MaxIVs = new int[6];
                encfilter.MinIVs = new int[6];
                for (int i = 0; i < IVMax.Length; i++)
                    encfilter.MaxIVs[i] = (int)IVMax[i].Value;
                for (int i = 0; i  < IVMin.Length; i++)
                    encfilter.MinIVs[i] =(int)IVMin[i].Value;
            }
            else
            {
                encfilter.MaxIVs = null;
                encfilter.MinIVs = null;
            }
            encfilter.IVMinindex = IgnoreIVFilter.Checked ? null : PresetIVMinBox.SelectedIndex;
            encfilter.IVMaxindex = IgnoreIVFilter.Checked ? null : PresetIVMaxBox.SelectedIndex;
            encfilter.Species = SpeciesBox.SelectedIndex == -1 ? null : SpeciesBox.SelectedIndex;
            encfilter.Form = FormBox.Visible ? FormBox.SelectedIndex >= 0 ? FormBox.SelectedIndex : null : null;
            encfilter.Gender = GenderBox.SelectedIndex;
            encfilter.ItemList = ItemList;
            encfilter.Nature = NatureList;
            encfilter.Mark = MarkList;
            encfilter.MarkOnly = MarkCheck.Checked || MarkList.Count > 0 || UnwantedMarkList.Count > 0;
            encfilter.UnwantedMark = MarkCheck.Checked ? UnwantedMarkList : null;
            if (Abilitys.CheckedItems.Count > 0)
            {
                encfilter.AbilityList = new();
                foreach (string ab in Abilitys.CheckedItems)
                {
                    var index = AbilityList.ToList().IndexOf(ab);
                    encfilter.AbilityList.Add((Ability)index);
                }
            }
            else
                encfilter.AbilityList = null;
            encfilter.Shiny = ShinyBox.SelectedIndex;
            encfilter.Scale = ScaleBox.Checked;
            encfilter.ThreeSegment = CheckBoxOf3.Checked;
            encfilter.Enabled = FilterEnabled.Checked;

            if (encfilter.IsFilterSet())
            {
                for (int i = 0; i < FilterName.Items.Count; i++)
                {
                    var f = filter.ElementAt(i);
                    if (f.Name == encfilter.Name)
                    {
                        filter.RemoveAt(i);
                        break;
                    }
                }
                filter.Add(encfilter);

                if (FilterName.Items.Contains(encfilter.Name))
                    MessageBox.Show($"Filter: {encfilter.Name} is updated!");
                else
                    MessageBox.Show($"Filter: {encfilter.Name} is added!");
                FirstFlag = true;
                ResetActiveFilters();
                ResetFalsg = false;
                FilterName.SelectedIndex = FilterName.Items.Count - 1;
            }
            else
            {
                MessageBox.Show("You have not set any stop conditions. No filter will be added.");
            }
        }
        private void SetForm()
        {
            FormBox.Items.Clear();
            FormBox.Text = string.Empty;
            if (SpeciesBox.SelectedIndex <= 0)
            {
                FormBox.Visible = false;
                Abilitys.Enabled = false;
                return;
            }
            else
            {
                FormBox.Visible = true;
                Abilitys.Enabled = true;
            }
            var formlist = FormConverter.GetFormList((ushort)(Species)SpeciesBox.SelectedIndex, TypesList, FormList, GenderList, EntityContext.Gen9);
            if ((Species)SpeciesBox.SelectedIndex == Species.Minior)
                formlist = formlist.Take((formlist.Length + 1) / 2).ToArray();

            if (formlist.Length == 0 || (formlist.Length == 1 && formlist[0].Equals("")))
                FormBox.Visible = false;
            else
            {
                FormBox.Items.AddRange(formlist);
                FormBox.Visible = true;
            }
        }
        public static double CalcRate(List<EncounterFilter> encounterFilters)
        {
            double Rate = 0.00;
            foreach (var filter in encounterFilters)
            {
                if (!filter.Enabled)
                    continue;
                string rate = filter.Rate.Replace(" ", "");
                string[] ratearray = rate.Split('/', StringSplitOptions.RemoveEmptyEntries);
                if (ratearray.Length == 2)
                {
                    if (int.TryParse(ratearray[0], out var Rate1) && int.TryParse(ratearray[1], out var Rate2))                    
                        Rate += (Rate1 * 1.00) / Rate2;                    
                }
            }
            return Rate;
        }
        private void SetAbility(byte form = 0)
        {
            Abilitys.Items.Clear();
            Abilitys.Items.Add(AbilityList[PersonalTable.SV.GetFormEntry((ushort)(Species)SpeciesBox.SelectedIndex, form).Ability1], false);
            Abilitys.Items.Add(AbilityList[PersonalTable.SV.GetFormEntry((ushort)(Species)SpeciesBox.SelectedIndex, form).Ability2], false);
            Abilitys.Items.Add(AbilityList[PersonalTable.SV.GetFormEntry((ushort)(Species)SpeciesBox.SelectedIndex, form).AbilityH], false);
        }
        private void SetForm(object sender, EventArgs e)
        {
            SetForm();
            SetAbility();
        }
        private void FormBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetAbility(FormBox.SelectedIndex < 0 ? (byte)0 :(byte)FormBox.SelectedIndex);
        }

        private void AddItem_Click(object sender, EventArgs e)
        {
            if (Item.SelectedIndex == -1)
                return;

            if (!ItemList.Contains(Item.SelectedIndex))
            {
                ItemList.Add(Item.SelectedIndex);
                MessageBox.Show($"{Strings.itemlist[Item.SelectedIndex]} is added!");
            }
        }
        private void RemoveItem_Click(object sender, EventArgs e)
        {
            if (Item.SelectedIndex == -1)
                return;

            if (ItemList.Contains(Item.SelectedIndex))
            {
                ItemList.Remove(Item.SelectedIndex);
                MessageBox.Show($"{Strings.itemlist[Item.SelectedIndex]} is removed!");
            }
        }
        private void AddNatureButton_Click(object sender, EventArgs e)
        {
            if (NatureBox.SelectedIndex == -1)
                return;
            var nature = Enum.Parse<Nature>(GameInfo.GetStrings(1).Natures[NatureBox.SelectedIndex]);
            if (!NatureList.Contains(nature))
            {
                NatureList.Add(nature);
                MessageBox.Show($"{Strings.natures[(int)nature]} is added!");
            }
        }
        private void RemoveNature_Click(object sender, EventArgs e)
        {
            if (NatureBox.SelectedIndex == -1)
                return;
            var nature = Enum.Parse<Nature>(GameInfo.GetStrings(1).Natures[NatureBox.SelectedIndex]);
            if (NatureList.Contains(nature))
            {
                NatureList.Remove(nature);
                MessageBox.Show($"{Strings.natures[(int)nature]} is removed!");
            }
        }
        private void MarkCheck_CheckedChanged(object sender, EventArgs e)
        {
            if(MarkCheck.Checked)
            {
                UnwantedMarkBox.Enabled = true;
                AddUnwantedMarkButton.Enabled = true;
                RemoveUnwantedMarkButton.Enabled = true;
            }
            else
            {
                UnwantedMarkBox.Enabled = false;
                AddUnwantedMarkButton.Enabled = false;
                RemoveUnwantedMarkButton.Enabled = false;
            }
        }

        private void AddUnwantedMarkButton_Click(object sender, EventArgs e)
        {
            if (UnwantedMarkBox.SelectedIndex < 0)
                return;
            var unwantedmark = (RibbonIndex)(UnwantedMarkBox.SelectedIndex + 53);
            if(!UnwantedMarkList.Contains(unwantedmark))
            {
                UnwantedMarkList.Add(unwantedmark);
                MessageBox.Show($"Unwanted Mark: {unwantedmark}{Environment.NewLine}{Strings.ribbons[(int)unwantedmark + 53].Split("\t")[1]} is added!");
            }
        }

        private void RemoveUnwantedMarkButton_Click(object sender, EventArgs e)
        {
            if(UnwantedMarkBox.SelectedIndex < 0)
                return;
            var unwantedmark = (RibbonIndex)(UnwantedMarkBox.SelectedIndex + 53);
            if(UnwantedMarkList.Contains(unwantedmark))
            {
                UnwantedMarkList.Remove(unwantedmark);
                MessageBox.Show($"Unwanted Mark: {unwantedmark}{Environment.NewLine}{Strings.ribbons[(int)unwantedmark + 53].Split("\t")[1]} is removed!");
            }
        }
        private void RemoveMarkButton_Click(object sender, EventArgs e)
        {
            if (MarkBox.SelectedIndex == -1)
                return;

            var mark = (RibbonIndex)(MarkBox.SelectedIndex + 53);
            if (MarkList.Contains(mark))
            {
                MarkList.Remove(mark);
                MessageBox.Show($"Target Mark: {mark}{Environment.NewLine}{Strings.ribbons[(int)mark + 53].Split("\t")[1]} is removed!");
            }
        }

        private void AddMarkButton_Click(object sender, EventArgs e)
        {
            if (MarkBox.SelectedIndex == -1)
                return;

            var mark = (RibbonIndex)(MarkBox.SelectedIndex + 53);
            if (!MarkList.Contains(mark))
            {
                MarkList.Add(mark);
                MessageBox.Show($"Target Mark: {mark}{Environment.NewLine}{Strings.ribbons[(int)mark + 53].Split("\t")[1]} is added!");
            }
        }
        private void FilterName_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemoveFilter.Enabled = FilterName.SelectedIndex >= 0;
            if (FilterName.SelectedIndex < 0)
                return;

            if (FilterName.Items.Count == 1)
                FirstFlag = false;
            if (FirstFlag)
            {
                FirstFlag = false;
                return;
            }

            SetFilters(filter[FilterName.SelectedIndex]);

        }

        private void RemoveFilter_Click(object sender, EventArgs e)
        {
            if (FilterName.Items.Count == 0 || FilterName.SelectedIndex == -1)
                return;

            var index = FilterName.SelectedIndex;
            var msg = $"Filter: {filter[index].Name} is removed!";
            filter.RemoveAt(index);
            FirstFlag = true;
            ResetActiveFilters();
            MessageBox.Show(msg);
        }
        private void CurrentFilterName_TextChanged(object sender, EventArgs e)
        {
            if(!FilterName.Items.Contains(CurrentFilterName.Text))
                FilterName.SelectedIndex = -1;
            else
                FilterName.SelectedIndex = FilterName.Items.IndexOf(CurrentFilterName.Text);
            ResetFilters();
            ResetFalsg = true;
            if (FilterName.SelectedIndex > -1 && CurrentFilterName.Text == filter[FilterName.SelectedIndex].Name)
            {
                ResetFalsg = false;
                AddFilter.Text = "Update Filter";
            }
            else
                AddFilter.Text = "Add Filter";
        }
        private void PresetIVMaxBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selection = PresetIVMaxBox.SelectedIndex;
            switch (selection)
            {
                case 0: break; // Disablecheck
                case 1:
                    {
                        HPFilterMax.Value = 31; AtkFilterMax.Value = 31; DefFilterMax.Value = 31; SpaFilterMax.Value = 31; SpdFilterMax.Value = 31; SpeFilterMax.Value = 31; IgnoreIVFilter.Checked = false; break;
                    }
                case 2:
                    {
                        HPFilterMax.Value = 31; AtkFilterMax.Value = 0; DefFilterMax.Value = 31; SpaFilterMax.Value = 31; SpdFilterMax.Value = 31; SpeFilterMax.Value = 0; IgnoreIVFilter.Checked = false; break;
                    }
                case 3:
                    {
                        HPFilterMax.Value = 31; AtkFilterMax.Value = 0; DefFilterMax.Value = 31; SpaFilterMax.Value = 31; SpdFilterMax.Value = 31; SpeFilterMax.Value = 31; IgnoreIVFilter.Checked = false; break;
                    }
                case 4:
                    {
                        HPFilterMax.Value = 31; AtkFilterMax.Value = 31; DefFilterMax.Value = 31; SpaFilterMax.Value = 31; SpdFilterMax.Value = 31; SpeFilterMax.Value = 0; IgnoreIVFilter.Checked = false; break;
                    }
            }
        }

        private void PresetIVMinBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selection = PresetIVMinBox.SelectedIndex;
            switch (selection)
            {
                case 0: break; // Disablecheck
                case 1:
                    {
                        HPFilterMin.Value = 31; AtkFilterMin.Value = 31; DefFilterMin.Value = 31; SpaFilterMin.Value = 31; SpdFilterMin.Value = 31; SpeFilterMin.Value = 31; IgnoreIVFilter.Checked = false; break;
                    }
                case 2:
                    {
                        HPFilterMin.Value = 31; AtkFilterMin.Value = 0; DefFilterMin.Value = 31; SpaFilterMin.Value = 31; SpdFilterMin.Value = 31; SpeFilterMin.Value = 0; IgnoreIVFilter.Checked = false; break;
                    }
                case 3:
                    {
                        HPFilterMin.Value = 31; AtkFilterMin.Value = 0; DefFilterMin.Value = 31; SpaFilterMin.Value = 31; SpdFilterMin.Value = 31; SpeFilterMin.Value = 31; IgnoreIVFilter.Checked = false; break;
                    }
                case 4:
                    {
                        HPFilterMin.Value = 31; AtkFilterMin.Value = 31; DefFilterMin.Value = 31; SpaFilterMin.Value = 31; SpdFilterMin.Value = 31; SpeFilterMin.Value = 0; IgnoreIVFilter.Checked = false; break;
                    }
            }
        }
        private void ShowFilterInfo()
        {
            if(ResetFalsg)
            {
                ResetFalsg = false;
                return;
            }
            var msgnature = "Nature List";
            foreach (var nature in NatureList)
                msgnature += $"{Environment.NewLine}{Strings.natures[(int)nature]}";
            var msgability = "Ability List";
            foreach (string ab in Abilitys.CheckedItems)
                msgability += $"{Environment.NewLine}{ab}";
            MessageBox.Show(msgnature);
            MessageBox.Show(msgability);
            if (filtermode == FilterMode.Egg || filtermode == FilterMode.MysteryGift_SV || filtermode == FilterMode.MysteryGift_SWSH)
                return;
            var msgmark = "Mark List";
            var msgunwantedmark = "Unwanted Mark List";
            foreach (var mark in MarkList)
                msgmark += $"{Environment.NewLine}{Strings.ribbons[(int)mark + 53].Split("\t")[1]}";
            foreach (var unwantedmark in UnwantedMarkList)
                msgunwantedmark += $"{Environment.NewLine}{Strings.ribbons[(int)unwantedmark + 53].Split("\t")[1]}";
            var msgItem = "Item List";
            foreach (var item in ItemList)
                msgItem += $"{Environment.NewLine}{Strings.itemlist[item]}";
            MessageBox.Show(msgItem);
            MessageBox.Show(msgmark);
            MessageBox.Show(msgunwantedmark);
        }
    }
}
