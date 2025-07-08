﻿using PKHeX.Core;
using PKHeX.Drawing;
using PKHeX.Drawing.Misc;
using PKHeX.Drawing.PokeSprite;
using PokeViewer.NET.Properties;
using PokeViewer.NET.SubForms;
using System.Net.WebSockets;
using static PokeViewer.NET.RoutineExecutor;
using static PokeViewer.NET.ViewerUtil;
using ToolTip = System.Windows.Forms.ToolTip;

namespace PokeViewer.NET
{
    public partial class BoxViewerMode : Form
    {
        private readonly int GameType;
        private ToolTip tt = new();
        private bool ReadInProgress;
        private List<string> CurrentSlotStats = [];
        private List<string> CurrentSlotSpecies = [];
        private List<string> CurrentSlotNature = [];
        private List<string> CurrentSlotAbility = [];
        private List<string> CurrentSlotIVs = [];
        private List<string> CurrentSlotScale = [];
        private List<string> CurrentSlotMark = [];
        private List<string> CurrentSlotBall = [];
        private List<long> CurrentSlotPtr = [];
        private List<uint> CurrentSlotOfs = [];
        private List<PKM> PKMs = [];
        private readonly ViewerState Executor;
        private ulong AbsoluteBoxOffset;
        private ulong MainOffset = 0;
        private uint BoxOffset = 0;
        private int BoxSlotSize = 0;
        private int TotalBoxes = 0;
        private Image? EggDefault = null!;
        private Image? ShinySquare = null!;
        private Image? ShinyStar = null!;
        private readonly string Trainer_OT;
        private readonly string Trainer_TID16;
        private readonly string Trainer_SID16;

        private string DumpFolder { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        public BoxViewerMode(int gametype, ref ViewerState executor, (Color, Color) color, string[] trainer)
        {
            InitializeComponent();
            GameType = gametype;
            Executor = executor;
            ViewButton.Text = "View";
            Trainer_OT = trainer[0];
            Trainer_TID16 = trainer[1];
            Trainer_SID16 = trainer[2];
            PrepareWindow(color);
            LoadComboBox();
            LoadResponses();
        }

        public class TrainerInfo
        {
            public string? OT { get; set; }
            public ushort? TID16 { get; set; }
            public ushort? SID16 { get; set; }
        }

        private void PrepareWindow((Color, Color) color)
        {
            BackColor = color.Item1;
            ForeColor = color.Item2;
            ViewButton.BackColor = color.Item1;
            ViewButton.ForeColor = color.Item2;
            FlexButton.BackColor = color.Item1;
            FlexButton.ForeColor = color.Item2;
            button3.BackColor = color.Item1;
            button3.ForeColor = color.Item2;
            button2.BackColor = color.Item1;
            button2.ForeColor = color.Item2;
            comboBox1.BackColor = color.Item1;
            comboBox1.ForeColor = color.Item2;
            HidePIDECCheck.BackColor = color.Item1;
            HidePIDECCheck.ForeColor = color.Item2;
            CSVCheck.BackColor = color.Item1;
            CSVCheck.ForeColor = color.Item2;

            PictureBox[] boxes = [.. Enumerable.Range(1, 30)
                    .Select(i => Controls.Find($"pictureBox{i}", true).FirstOrDefault() as PictureBox)
                    .Where(pb => pb != null)!];

            foreach (var pb in boxes)
            {
                pb.BackColor = color.Item1;
                if (GameType != (int)GameSelected.HOME)
                {
                    pb.ContextMenuStrip = CreateContextMenu(pb);
                    pb.MouseDown += (sender, e) =>
                    {
                        if (e.Button == MouseButtons.Right)
                            contextMenuStrip1.Show(pb, e.Location);
                    };
                }
                pb.MouseEnter += (s, e) => pb.Cursor = Cursors.Hand;
                pb.MouseLeave += (s, e) => pb.Cursor = Cursors.Default;
            }
        }

        private ContextMenuStrip CreateContextMenu(PictureBox pb)
        {
            var menu = new ContextMenuStrip();

            menu.Items.Add("Dump", null, (s, e) => DumpPb(pb));
            menu.Items.Add("Clear", null, (s, e) => ClearPb(pb));
            menu.Items.Add("Inject", null, (s, e) => InjectPb(pb));
            menu.Items.Add("Clear Box", null, (s, e) => ClearBoxPb());
            menu.Items.Add("Dump Box", null, (s, e) => DumpBoxPb());

            //menu.Opening += (s, e) => e.Cancel = pb.Image == null;
            return menu;
        }

        private async void LoadResponses()
        {
            await AnticipateResponse(CancellationToken.None).ConfigureAwait(false);
        }

        private async Task AnticipateResponse(CancellationToken token)
        {
            using HttpClient client = new();
            var eggresponse = await client.GetStreamAsync("https://raw.githubusercontent.com/zyro670/HomeImages/master/128x128/poke_capture_0000_000_uk_n_00000000_f_n.png", token).ConfigureAwait(false);
            EggDefault = Image.FromStream(eggresponse);

            string shinyicon = "https://raw.githubusercontent.com/zyro670/PokeTextures/2137b7024c161aad7ba832da481cff83792f5e67/icon_version/icon_";
            var square = await client.GetStreamAsync(shinyicon + "square.png", token).ConfigureAwait(false);
            ShinySquare = Image.FromStream(square);

            var star = await client.GetStreamAsync(shinyicon + "star.png", token).ConfigureAwait(false);
            ShinyStar = Image.FromStream(star);
        }

        private void LoadComboBox()
        {
            switch (GameType)
            {
                case (int)GameSelected.Sword or (int)GameSelected.Shield: TotalBoxes = 32; break;
                case (int)GameSelected.BrilliantDiamond or (int)GameSelected.ShiningPearl: TotalBoxes = 40; break;
                case (int)GameSelected.LegendsArceus: TotalBoxes = 32; break;
                case (int)GameSelected.LetsGoPikachu or (int)GameSelected.LetsGoEevee: TotalBoxes = 40; break;
                case (int)GameSelected.Scarlet or (int)GameSelected.Violet: TotalBoxes = 32; break;
                case (int)GameSelected.HOME: TotalBoxes = 200; break;
            }
            comboBox1.Items.Clear();
            for (var i = 0; i < TotalBoxes; i++)
                comboBox1.Items.Add($"Box {i + 1}");
        }

        private void DisableAssets()
        {
            ViewButton.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            comboBox1.Enabled = false;
            FlexButton.Enabled = false;
        }

        private void EnableAssets()
        {
            ViewButton.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            comboBox1.Enabled = true;
            FlexButton.Enabled = true;
        }

        public async Task ReadBoxes(int boxnumber, CancellationToken token)
        {
            PictureBox[] boxes =
            [
                pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10,
                pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15, pictureBox16, pictureBox17, pictureBox18, pictureBox19, pictureBox20,
                pictureBox21, pictureBox22, pictureBox23, pictureBox24, pictureBox25, pictureBox26, pictureBox27, pictureBox28, pictureBox29, pictureBox30
            ];

            var box = boxnumber;
            ViewButton.Text = "Reading...";
            DisableAssets();
            List<Image> images = [];
            List<Color> colors = [];
            PKM pk = new PK9();
            var folder = $"BoxViewer";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            var subfolder = $"BoxViewer\\{(GameSelected)GameType}";
            if (!Directory.Exists(subfolder))
                Directory.CreateDirectory(subfolder);
            var trainersubfolder = $"BoxViewer\\{(GameSelected)GameType}\\{Trainer_OT}-{Trainer_TID16}";
            if (!Directory.Exists(trainersubfolder))
                Directory.CreateDirectory(trainersubfolder);

            try
            {
                await BoxRoutine(box, boxes, images, colors, pk, false, token).ConfigureAwait(false);
            }
            catch (Exception ex) { MessageBox.Show($"{ex}"); }
            PKMs = [];
            ViewButton.Text = "View";
            EnableAssets();
        }

        public async Task BoxRoutine(int box, PictureBox[] boxes, List<Image> images, List<Color> colors, PKM pk, bool dumpall, CancellationToken token)
        {
            CurrentSlotStats = [];
            CurrentSlotSpecies = [];
            CurrentSlotNature = [];
            CurrentSlotAbility = [];
            CurrentSlotIVs = [];
            CurrentSlotScale = [];
            CurrentSlotMark = [];
            CurrentSlotBall = [];
            CurrentSlotPtr = [];
            CurrentSlotOfs = [];

            var TempMain = await Executor.SwitchConnection.GetMainNsoBaseAsync(token).ConfigureAwait(false);
            if(TempMain != MainOffset)
                AbsoluteBoxOffset = 0;
            if (GameType is (int)GameSelected.Scarlet && AbsoluteBoxOffset == 0 || GameType is (int)GameSelected.Violet && AbsoluteBoxOffset == 0)
            {
                IReadOnlyList<long> SVptr = [0x47350D8, 0xD8, 0x8, 0xB8, 0x30, 0x9D0, 0x0];
                AbsoluteBoxOffset = await Executor.SwitchConnection.PointerAll(SVptr, CancellationToken.None).ConfigureAwait(false);
                MainOffset = TempMain;
            }
            if (GameType is (int)GameSelected.LegendsArceus && AbsoluteBoxOffset == 0)
            {
                IReadOnlyList<long> LAptr = [0x42BA6B0, 0x1F0, 0x68];
                AbsoluteBoxOffset = await Executor.SwitchConnection.PointerAll(LAptr, CancellationToken.None).ConfigureAwait(false);
                MainOffset = TempMain;
            }

            for (int i = 0; i < 30; i++)
            {
                UpdateProgress(100 / 30 * i, 100);
                switch (GameType)
                {
                    case (int)GameSelected.HOME:
                        {
                            var ofs = (uint)(BoxOffset + (BoxSlotSize * i + (BoxSlotSize * 30 * box)));
                            pk = await ReadPKH(ofs, BoxSlotSize, token).ConfigureAwait(false);
                            PKMs.Add(pk);
                            CurrentSlotOfs.Add(ofs);
                            break;
                        }
                    case (int)GameSelected.Scarlet or (int)GameSelected.Violet:
                        {
                            var boxsize = 30 * BoxSlotSize;
                            var boxStart = AbsoluteBoxOffset + (ulong)(box * boxsize);
                            var slotstart = boxStart + (ulong)(i * BoxSlotSize);
                            pk = await ReadBoxPokemon(slotstart, BoxOffset, BoxSlotSize, token).ConfigureAwait(false);
                            PKMs.Add((PK9)pk);
                            CurrentSlotPtr.Add((long)slotstart);
                            break;
                        }
                    case (int)GameSelected.LegendsArceus:
                        {
                            var boxsize = 30 * BoxSlotSize;
                            var boxStart = AbsoluteBoxOffset + (ulong)(box * boxsize);
                            var slotstart = boxStart + (ulong)(i * BoxSlotSize);
                            pk = await ReadBoxPokemon(slotstart, BoxOffset, BoxSlotSize, token).ConfigureAwait(false);
                            PKMs.Add((PA8)pk);
                            CurrentSlotPtr.Add((long)slotstart);
                            break;
                        }
                    case (int)GameSelected.BrilliantDiamond:
                        {
                            var sizeup = GetBDSPSlotValue(i);
                            var boxvalue = GetBDSPBoxValue(box);
                            var b1s1b = new long[] { 0x4C64DC0, 0xB8, 0x10, 0xA0, boxvalue, sizeup, 0x20 };
                            var boxStart = await Executor.SwitchConnection.PointerAll(b1s1b, token).ConfigureAwait(false);
                            pk = await ReadBoxPokemon(boxStart, BoxOffset, BoxSlotSize, token).ConfigureAwait(false);
                            PKMs.Add((PB8)pk);
                            CurrentSlotPtr.Add((long)boxStart);
                            break;
                        }
                    case (int)GameSelected.ShiningPearl:
                        {
                            var sizeup = GetBDSPSlotValue(i);
                            var boxvalue = GetBDSPBoxValue(box);
                            var b1s1b = new long[] { 0x4E7BE98, 0xB8, 0x10, 0xA0, boxvalue, sizeup, 0x20 };
                            var boxStart = await Executor.SwitchConnection.PointerAll(b1s1b, token).ConfigureAwait(false);
                            pk = await ReadBoxPokemon(boxStart, BoxOffset, BoxSlotSize, token).ConfigureAwait(false);
                            PKMs.Add((PB8)pk);
                            CurrentSlotPtr.Add((long)boxStart);
                            break;
                        }
                    case (int)GameSelected.Sword or (int)GameSelected.Shield:
                        {
                            pk = await ReadBoxPokemon(AbsoluteBoxOffset, (uint)(BoxOffset + (BoxSlotSize * i + (BoxSlotSize * 30 * box))), BoxSlotSize, token).ConfigureAwait(false);
                            PKMs.Add((PK8)pk);
                            CurrentSlotOfs.Add((uint)(BoxOffset + (BoxSlotSize * i + (BoxSlotSize * 30 * box))));
                            break;
                        }
                    case (int)GameSelected.LetsGoPikachu or (int)GameSelected.LetsGoEevee:
                        {
                            pk = await ReadBoxPokemon(AbsoluteBoxOffset, (uint)GetSlotOffset(box, i), LGPESlotSize + LGPEGapSize, token).ConfigureAwait(false);
                            PKMs.Add((PB7)pk);
                            CurrentSlotOfs.Add((uint)(BoxOffset + (BoxSlotSize * i + (BoxSlotSize * 30 * box))));
                            break;
                        }
                }

                if (PKMs[i].Species is 0 or > (int)Species.MAX_COUNT)
                {
                    Image blank = null!;
                    Color ic = Color.WhiteSmoke;
                    if (!dumpall)
                    {
                        images.Add(blank);
                        colors.Add(ic);
                    }
                    CurrentSlotStats.Add($"Box {box + 1} Slot {i + 1} is empty.");
                    continue;
                }
                var img = await Sanitize(pk, token).ConfigureAwait(false);
                Color c = PKMs[i].Gender == 0 ? Color.LightBlue : PKMs[i].Gender == 1 ? Color.LightPink : Color.LightGray;
                if (!dumpall)
                {
                    images.Add(img);
                    colors.Add(c);
                }
            }
            UpdateProgress(100, 100);

            if (!dumpall)
            {
                for (int p = 0; p < images.Count; p++)
                {
                    boxes[p].Image = images[p];
                    boxes[p].BackColor = colors[p];
                }
            }

            if (CSVCheck.Checked)
            {
                var filePath = $"BoxViewer\\{(GameSelected)GameType}\\{Trainer_OT}-{Trainer_TID16}\\Box{box + 1}.csv";
                string res = string.Empty;
                res += "Species" + ",";
                res += "Nature" + ",";
                res += "Ability" + ",";
                res += "IVs" + ",";
                res += "Scale" + ",";
                res += "Mark" + ",";
                res += "Ball" + ",";
                res += Environment.NewLine;
                for (int s = 0; s < CurrentSlotSpecies.Count; s++)
                {
                    res += CurrentSlotSpecies[s].ToString() + ",";
                    res += CurrentSlotNature[s].ToString().Replace("Nature: ", "") + ",";
                    res += CurrentSlotAbility[s].ToString().Replace("Ability: ", "") + ",";
                    res += CurrentSlotIVs[s].ToString().Replace("IVs: ", "") + ",";
                    res += CurrentSlotScale[s].ToString().Replace("Scale: ", "") + ",";
                    res += CurrentSlotMark[s].ToString().Replace("Mark: ", "") + ",";
                    res += CurrentSlotBall[s].ToString().Replace("Ball: ", "") + ",";
                    res += Environment.NewLine;
                }
                using StreamWriter writer = new(new FileStream(filePath, FileMode.Create, FileAccess.Write));
                writer.WriteLine(res);
            }
        }

        private async Task<Image> Sanitize(PKM pk, CancellationToken token)
        {
            using HttpClient client = new();
            string pid = string.Empty;
            if (!HidePIDECCheck.Checked)
                pid = $"PID: {pk.PID:X8}";
            string ec = string.Empty;
            if (!HidePIDECCheck.Checked)
                ec = $"{Environment.NewLine}EC: {pk.EncryptionConstant:X8}";
            string form = FormOutput(pk.Species, pk.Form, out _);
            string gender = string.Empty;
            switch (pk.Gender)
            {
                case 0: gender = " (M)"; break;
                case 1: gender = " (F)"; break;
                case 2: break;
            }

            string msg = string.Empty;
            Image? m = null;
            Image? o = null;
            if (pk is PK8 or PK9)
            {
                var info = RibbonInfo.GetRibbonInfo(pk);
                foreach (var rib in info)
                {
                    if (!rib.HasRibbon)
                        continue;

                    var mimg = RibbonSpriteUtil.GetRibbonSprite(rib.Name);
                    if (mimg is not null)
                        m = new Bitmap(mimg, new Size(mimg.Width + 100, mimg.Height + 100));

                    if (pk is PK9)
                    {
                        bool hasMark = HasMark((PK9)pk, out RibbonIndex mark);
                        msg = hasMark ? $"Mark: {mark.ToString().Replace("Mark", "")}" : "";
                    }
                    else
                    {
                        bool hasMark = HasMark((PK8)pk, out RibbonIndex mark);
                        msg = hasMark ? $"Mark: {mark.ToString().Replace("Mark", "")}" : "";
                    }
                }
            }

            if (pk is PKH)
            {
                bool hasMark = HasAffixedRibbon((IRibbonSetAffixed)pk, out RibbonIndex mark);
                msg = hasMark ? $"Mark: {mark.ToString().Replace("Mark", "")}" : "";
                string markon = "https://raw.githubusercontent.com/zyro670/PokeTextures/5141086ee706c09d6c9aca1a773a3d08143e6460/Ribbons/icon_ribbon_";
                if (hasMark)
                {
                    string val = (int)mark >= 100 ? $"{(int)mark + 1}" : (int)mark < 100 && (int)mark > 10 ? $"0{(int)mark + 1}" : $"00{(int)mark + 1}";
                    var markresponse8 = await client.GetStreamAsync(hasMark ? markon + val + ".png" : markon, token).ConfigureAwait(false);
                    m = Image.FromStream(markresponse8);
                    m = new Bitmap(m, new Size(m.Width, m.Height));
                }
            }

            o = SpriteUtil.GetBallSprite(pk.Ball);

            string alpha = string.Empty;
            if (pk is PA8)
            {
                bool isAlpha = pk is PA8 pa8 && pa8.IsAlpha;
                if (isAlpha)
                    alpha = $"αlpha - ";
            }
            bool isGmax = pk is PK8 pk8 && pk8.CanGigantamax;
            string gMax = isGmax ? "Gigantamax - " : "";

            string scale = string.Empty;
            if (pk is PK9 pk9)
                scale = $"Scale: {PokeSizeDetailedUtil.GetSizeRating(pk9.Scale)} ({pk9.Scale})";
            if (pk is PK8 pk82)
                scale = $"Scale: {PokeSizeDetailedUtil.GetSizeRating(pk82.HeightScalar)} ({pk82.HeightScalar})";
            if (pk is PB8 pb8)
                scale = $"Scale: {PokeSizeDetailedUtil.GetSizeRating(pb8.HeightScalar)} ({pb8.HeightScalar})";
            if (pk is PA8 pa82)
                scale = $"Scale: {PokeSizeDetailedUtil.GetSizeRating(pa82.HeightScalar)} ({pa82.HeightScalar})";
            if (pk is PKH pkh2)
                scale = $"Scale: {PokeSizeDetailedUtil.GetSizeRating(pkh2.HeightScalar)} ({pkh2.HeightScalar})";
            string ballstring = string.Empty;
            ballstring = $"Ball: {(Ball)pk.Ball}";

            string sens = string.Empty;
            if (!string.IsNullOrEmpty(pid) && !string.IsNullOrEmpty(ec))
                sens = pid + ec;
            CurrentSlotSpecies.Add($"{(pk.ShinyXor == 0 ? "■ - " : pk.ShinyXor <= 16 ? "★ - " : "")}{gMax}{alpha}{(Species)pk.Species}{form}{gender}{Environment.NewLine}{sens}");
            CurrentSlotNature.Add($"Nature: {pk.Nature}");
            CurrentSlotAbility.Add($"Ability: {(Ability)pk.Ability}");
            CurrentSlotIVs.Add($"IVs: {pk.IV_HP}/{pk.IV_ATK}/{pk.IV_DEF}/{pk.IV_SPA}/{pk.IV_SPD}/{pk.IV_SPE}");
            CurrentSlotScale.Add(scale);
            CurrentSlotMark.Add(msg);
            CurrentSlotBall.Add(ballstring);
            CurrentSlotStats.Add($"{CurrentSlotSpecies.Last()}{Environment.NewLine}{CurrentSlotNature.Last()}{Environment.NewLine}{CurrentSlotAbility.Last()}{Environment.NewLine}" +
                $"{CurrentSlotIVs.Last()}{Environment.NewLine}{CurrentSlotScale.Last()}{Environment.NewLine}{CurrentSlotBall.Last()}{Environment.NewLine}{CurrentSlotMark.Last()}");
            if (pk is PK8 && isGmax)
            {
                if (pk.Species == (int)Species.Charmander || pk.Species == (int)Species.Charmeleon || pk.Species == (int)Species.Hattrem)
                    isGmax = false;
            }
            if (pk is PB7)
            {
                if (pk.Species == (int)Species.Eevee || pk.Species == (int)Species.Pikachu)
                    pk.Form = 0;
            }
            string? sprite;
            try
            {
                if ((Species)pk.Species is Species.Charmander or Species.Squirtle or Species.Bulbasaur or Species.Charmeleon or Species.Wartortle or Species.Ivysaur or Species.Milcery or Species.Hattrem)
                    isGmax = false;
                sprite = PokeImg(pk, isGmax);
            }
            catch
            {
                sprite = "https://raw.githubusercontent.com/kwsch/PKHeX/master/PKHeX.Drawing.PokeSprite/Resources/img/Pokemon%20Sprite%20Overlays/starter.png";
            }
            var response = await client.GetStreamAsync(sprite, token).ConfigureAwait(false);
            Image img = Image.FromStream(response);
            var img2 = (Image)new Bitmap(img, new Size(img.Width, img.Height));

            if (pk.IsEgg)
            {
                var egg = new Bitmap(EggDefault!, new Size(EggDefault!.Width / 3, EggDefault!.Height / 3));
                img2 = ImageUtil.LayerImage(img2, egg, -5, 85);
            }

            if (pk.IsShiny)
            {
                Image? shiny = pk.ShinyXor == 0 ? ShinySquare : ShinyStar;
                shiny = new Bitmap(shiny!, new Size(shiny!.Width / 2, shiny!.Height / 2));
                img2 = ImageUtil.LayerImage(img2, shiny, 105, 5);
            }

            if (m != null)
            {
                m = new Bitmap(m, new Size(m.Width / 4 + 10, m.Height / 4 + 10));
                img2 = ImageUtil.LayerImage(img2, m, 0, 0);
            }

            if (o != null)
            {
                img2 = ImageUtil.LayerImage(img2, o, 100, 100);
            }
            return img2;
        }

        private readonly uint LGPEStart = 0x533675B0;
        private readonly int LGPESlotSize = 260;
        private readonly int LGPESlotCount = 25;
        private readonly int LGPEGapSize = 380;
        private ulong GetBoxOffset(int box) => (ulong)LGPEStart + (ulong)((LGPESlotSize + LGPEGapSize) * LGPESlotCount * box);
        private ulong GetSlotOffset(int box, int slot) => GetBoxOffset(box) + (ulong)((LGPESlotSize + LGPEGapSize) * slot);

        public async Task<PKM> ReadPKH(uint offset, int size, CancellationToken token)
        {
            var data = await Executor.SwitchConnection.ReadBytesAsync(offset, size, token).ConfigureAwait(false);
            PKM ph = new PKH(data);
            return ph;
        }

        public async Task<PKM> ReadBoxPokemon(ulong absoluteoffset, uint offset, int size, CancellationToken token)
        {
            PKM pk = new PK9();
            byte[]? data;
            switch (GameType)
            {
                case (int)GameSelected.Scarlet or (int)GameSelected.Violet or (int)GameSelected.LegendsArceus or (int)GameSelected.BrilliantDiamond or (int)GameSelected.ShiningPearl:
                    {
                        data = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(absoluteoffset, size, token).ConfigureAwait(false);
                        switch (GameType)
                        {
                            case (int)GameSelected.Scarlet or (int)GameSelected.Violet: pk = new PK9(data); break;
                            case (int)GameSelected.LegendsArceus: pk = new PA8(data); break;
                            case (int)GameSelected.BrilliantDiamond or (int)GameSelected.ShiningPearl: pk = new PB8(data); break;
                        }
                        break;
                    }
                case (int)GameSelected.Sword or (int)GameSelected.Shield or (int)GameSelected.LetsGoPikachu or (int)GameSelected.LetsGoEevee:
                    {
                        data = await Executor.SwitchConnection.ReadBytesAsync(offset, size, token).ConfigureAwait(false);
                        switch (GameType)
                        {
                            case (int)GameSelected.Sword or (int)GameSelected.Shield: pk = new PK8(data); break;
                            case (int)GameSelected.LetsGoPikachu or (int)GameSelected.LetsGoEevee: pk = new PB7(data); break;
                        }
                        break;
                    }
            }
            await Task.Delay(0_500, token).ConfigureAwait(false);
            return pk;
        }

        private void pictureBox_MouseHover(object sender, EventArgs e)
        {
            if (!ReadInProgress)
            {
                PictureBox? pbox = sender as PictureBox;
                tt = new();
                if (pbox is not null)
                {
                    if (pbox.Image == null)
                    {
                        tt.SetToolTip(pbox, null);
                        return;
                    }
                    var currentslot = int.Parse(pbox.Name.Replace("pictureBox", "")) - 1;
                    tt.SetToolTip(pbox, CurrentSlotStats[currentslot]);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ForwardClick(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BackwardClick(sender, e);
        }

        private void PrepareSlots()
        {
            switch (GameType)
            {
                case (int)GameSelected.HOME:
                    {
                        BoxSlotSize = 0x2D0; BoxOffset = 0x12B90; break;
                    }
                case (int)GameSelected.Scarlet or (int)GameSelected.Violet:
                    {
                        BoxSlotSize = 0x158; break;
                    }
                case (int)GameSelected.LegendsArceus:
                    {
                        BoxSlotSize = 0x168; break;
                    }
                case (int)GameSelected.BrilliantDiamond or (int)GameSelected.ShiningPearl:
                    {
                        BoxSlotSize = 0x168;
                        break;
                    }
                case (int)GameSelected.Sword or (int)GameSelected.Shield:
                    {
                        BoxSlotSize = 0x158; BoxOffset = 0x45075880; break;
                    }
                case (int)GameSelected.LetsGoPikachu or (int)GameSelected.LetsGoEevee:
                    {
                        BoxSlotSize = 0x158; BoxOffset = 0x533675B0; break;
                    }
            }
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            var token = CancellationToken.None;
            ReadInProgress = true;
            tt.RemoveAll();
            tt.Dispose();
            tt = new();
            CurrentSlotStats = [];
            if (BoxSlotSize == 0 && BoxOffset == 0)
                PrepareSlots();
            var currentbox = comboBox1.SelectedIndex;
            if (currentbox == -1)
                currentbox = 0;
            await ReadBoxes(currentbox, token).ConfigureAwait(false);
            ReadInProgress = false;
        }

        private async void ForwardClick(object sender, EventArgs e)
        {
            var token = CancellationToken.None;
            ReadInProgress = true;
            CurrentSlotStats = [];
            if (BoxSlotSize == 0 && BoxOffset == 0)
                PrepareSlots();
            var currentbox = comboBox1.SelectedIndex;
            if (currentbox >= TotalBoxes - 1)
                currentbox = 0;
            else
                currentbox++;
            await ReadBoxes(currentbox, token).ConfigureAwait(false);
            comboBox1.SelectedIndex = currentbox;
            ReadInProgress = false;
        }

        private async void BackwardClick(object sender, EventArgs e)
        {
            var token = CancellationToken.None;
            ReadInProgress = true;
            CurrentSlotStats = [];
            if (BoxSlotSize == 0 && BoxOffset == 0)
                PrepareSlots();
            var currentbox = comboBox1.SelectedIndex;
            if (currentbox <= 0)
                currentbox = TotalBoxes - 1;
            else
                currentbox--;
            await ReadBoxes(currentbox, token).ConfigureAwait(false);
            comboBox1.SelectedIndex = currentbox;
            ReadInProgress = false;
        }

        private static uint GetBDSPSlotValue(int slot)
        {
            switch (slot)
            {
                case 0: slot = 0x20; break;
                case 1: slot = 0x28; break;
                case 2: slot = 0x30; break;
                case 3: slot = 0x38; break;
                case 4: slot = 0x40; break;
                case 5: slot = 0x48; break;
                case 6: slot = 0x50; break;
                case 7: slot = 0x58; break;
                case 8: slot = 0x60; break;
                case 9: slot = 0x68; break;
                case 10: slot = 0x70; break;
                case 11: slot = 0x78; break;
                case 12: slot = 0x80; break;
                case 13: slot = 0x88; break;
                case 14: slot = 0x90; break;
                case 15: slot = 0x98; break;
                case 16: slot = 0xA0; break;
                case 17: slot = 0xA8; break;
                case 18: slot = 0xB0; break;
                case 19: slot = 0xB8; break;
                case 20: slot = 0xC0; break;
                case 21: slot = 0xC8; break;
                case 22: slot = 0xD0; break;
                case 23: slot = 0xD8; break;
                case 24: slot = 0xE0; break;
                case 25: slot = 0xE8; break;
                case 26: slot = 0xF0; break;
                case 27: slot = 0xF8; break;
                case 28: slot = 0x100; break;
                case 29: slot = 0x108; break;
                case 30: slot = 0x110; break;
            }
            return (uint)slot;
        }

        private static uint GetBDSPBoxValue(int slot)
        {
            switch (slot)
            {
                case 0: slot = 0x20; break;
                case 1: slot = 0x28; break;
                case 2: slot = 0x30; break;
                case 3: slot = 0x38; break;
                case 4: slot = 0x40; break;
                case 5: slot = 0x48; break;
                case 6: slot = 0x50; break;
                case 7: slot = 0x58; break;
                case 8: slot = 0x60; break;
                case 9: slot = 0x68; break;
                case 10: slot = 0x70; break;
                case 11: slot = 0x78; break;
                case 12: slot = 0x80; break;
                case 13: slot = 0x88; break;
                case 14: slot = 0x90; break;
                case 15: slot = 0x98; break;
                case 16: slot = 0xA0; break;
                case 17: slot = 0xA8; break;
                case 18: slot = 0xB0; break;
                case 19: slot = 0xB8; break;
                case 20: slot = 0xC0; break;
                case 21: slot = 0xC8; break;
                case 22: slot = 0xD0; break;
                case 23: slot = 0xD8; break;
                case 24: slot = 0xE0; break;
                case 25: slot = 0xE8; break;
                case 26: slot = 0xF0; break;
                case 27: slot = 0xF8; break;
                case 28: slot = 0x100; break;
                case 29: slot = 0x108; break;
                case 30: slot = 0x110; break;
                case 31: slot = 0x118; break;
                case 32: slot = 0x120; break;
                case 33: slot = 0x128; break;
                case 34: slot = 0x130; break;
                case 35: slot = 0x138; break;
                case 36: slot = 0x140; break;
                case 37: slot = 0x148; break;
                case 38: slot = 0x150; break;
                case 39: slot = 0x158; break;
                case 40: slot = 0x160; break;
            }
            return (uint)slot;
        }

        private void PictureBox_DoubleClick(object sender, EventArgs e)
        {
            PictureBox? pbox = sender as PictureBox;
            if (pbox is not null)
            {
                if (pbox.Image is null)
                {
                    MessageBox.Show("No data present, click view and try again.");
                    return;
                }
                var currentslot = int.Parse(pbox.Name.Replace("pictureBox", "")) - 1;
                if (pbox.Image is not null)
                {
                    using BoxViewerMini form = new(pbox, CurrentSlotStats[currentslot].ToString(), this.BackColor, this.ForeColor);
                    form.ShowDialog();
                }
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox? pbox = sender as PictureBox;
            if (e.Button == MouseButtons.Right)
            {
                if (pbox is not null)
                {
                    if (pbox.Image is null)
                    {
                        MessageBox.Show("No data present, click view and try again.");
                        return;
                    }
                    var currentslot = int.Parse(pbox.Name.Replace("pictureBox", "")) - 1;
                    if (pbox.Image is not null)
                    {
                        using BoxViewerMini form = new(pbox, CurrentSlotStats[currentslot].ToString(), this.BackColor, this.ForeColor);
                        form.ShowDialog();
                    }
                }
            }
        }

        private static int GetSlotNumber(string pictureBoxName)
        {
            return int.Parse(pictureBoxName.Replace("pictureBox", "")) - 1;
        }

        private static bool UsesOfs()
        {
            return Settings.Default.GameConnected is
                (int)GameSelected.Sword or
                (int)GameSelected.Shield or
                (int)GameSelected.LetsGoPikachu or
                (int)GameSelected.LetsGoEevee;
        }

        private async Task<PKM> ReadPokemon(int slot, CancellationToken token)
        {
            return UsesOfs()
                ? await ReadBoxPokemon(0, CurrentSlotOfs[slot], BoxSlotSize, token)
                : await ReadBoxPokemon((ulong)CurrentSlotPtr[slot], 0, BoxSlotSize, token);
        }

        private async Task WritePokemonData(int slot, PKM pk, CancellationToken token)
        {
            if (UsesOfs())
                await Executor.SwitchConnection.WriteBytesAsync(pk.EncryptedPartyData, CurrentSlotOfs[slot], token);
            else
            {
                var TempMain = await Executor.SwitchConnection.GetMainNsoBaseAsync(token).ConfigureAwait(false);
                if(TempMain != MainOffset)
                {
                    AbsoluteBoxOffset = 0;
                    throw new Exception("BoxOffset is Changed, due to Reset. ReRead Boxes!");
                }
                await Executor.SwitchConnection.WriteBytesAbsoluteAsync(pk.EncryptedBoxData, (ulong)CurrentSlotPtr[slot], token);
            }
        }

        private static PKM CreateBlank(byte[] data = null!)
        {
            return Settings.Default.GameConnected switch
            {
                (int)GameSelected.Scarlet or (int)GameSelected.Violet => data != null ? new PK9(data) : new PK9(),
                (int)GameSelected.LegendsArceus => data != null ? new PA8(data) : new PA8(),
                (int)GameSelected.BrilliantDiamond or (int)GameSelected.ShiningPearl => data != null ? new PB8(data) : new PB8(),
                (int)GameSelected.Sword or (int)GameSelected.Shield => data != null ? new PK8(data) : new PK8(),
                (int)GameSelected.LetsGoPikachu or (int)GameSelected.LetsGoEevee => data != null ? new PB7(data) : new PB7(),
                _ => throw new ArgumentOutOfRangeException(nameof(Settings.Default.GameConnected))
            };
        }

        private async void DumpPb(PictureBox pb)
        {
            if (pb!.Image == null)
            {
                MessageBox.Show("No data present, unable to dump a blank slot. Please click a slot with a Pokémon and try again.");
                return;
            }
            try
            {
                var token = CancellationToken.None;
                var currentslot = GetSlotNumber(pb.Name);
                var pk = await ReadPokemon(currentslot, token);
                DumpPokemon(DumpFolder, $"BoxViewer\\{(GameSelected)GameType}\\{Trainer_OT}-{Trainer_TID16}\\Box{comboBox1.SelectedIndex + 1}\\SlotDump", pk);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error dumping Pokémon: {ex.Message}");
            }
        }

        private async void DumpBoxPb()
        {
            try
            {
                var token = CancellationToken.None;
                for (int i = 0; i < 30; i++)
                {
                    var pk = await ReadPokemon(i, token);
                    if (pk != null)
                        DumpPokemon(DumpFolder, $"BoxViewer\\{(GameSelected)GameType}\\{Trainer_OT}-{Trainer_TID16}\\Box{comboBox1.SelectedIndex + 1}", pk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error dumping Pokémon: {ex.Message}");
            }
        }

        private async void ClearPb(PictureBox pb)
        {
            DialogResult dialogResult = MessageBox.Show("This will clear your box slot of the current Pokémon. Continue?", "Alert!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
                return;

            if (pb!.Image == null)
            {
                MessageBox.Show("No data present, nothing to clear.");
                return;
            }
            try
            {
                var token = CancellationToken.None;
                var currentslot = GetSlotNumber(pb.Name);
                var pk = CreateBlank();
                await WritePokemonData(currentslot, pk, token);
                MessageBox.Show("Slot cleared successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing slot: {ex.Message}");
            }
        }

        private async void ClearBoxPb()
        {
            DialogResult dialogResult = MessageBox.Show("This will clear your boxes of all Pokémon. Continue?", "Alert!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
                return;

            try
            {
                var token = CancellationToken.None;
                var pk = CreateBlank();
                for (int i = 0; i < 30; i++)
                    await WritePokemonData(i, pk, token);
                MessageBox.Show("Box cleared successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing slot: {ex.Message}");
            }
        }

        public async Task<ulong> ReturnBoxSlot(int box, int slot)
        {
            PrepareSlots();
            var TempMain = await Executor.SwitchConnection.GetMainNsoBaseAsync(CancellationToken.None).ConfigureAwait(false);
            if(TempMain != MainOffset)            
                AbsoluteBoxOffset = 0;            
            if (GameType is (int)GameSelected.Scarlet or (int)GameSelected.Violet && AbsoluteBoxOffset == 0)
            {
                var SVptr = new long[] { 0x47350D8, 0xD8, 0x8, 0xB8, 0x30, 0x9D0, 0x0 };
                AbsoluteBoxOffset = await Executor.SwitchConnection.PointerAll(SVptr, CancellationToken.None).ConfigureAwait(false);
                MainOffset = TempMain;
            }
            if (GameType is (int)GameSelected.LegendsArceus && AbsoluteBoxOffset == 0)
            {
                var LAptr = new long[] { 0x42BA6B0, 0x1F0, 0x68 };
                AbsoluteBoxOffset = await Executor.SwitchConnection.PointerAll(LAptr, CancellationToken.None).ConfigureAwait(false);
                MainOffset = TempMain;
            }
            var boxsize = 30 * BoxSlotSize;
            var boxStart = AbsoluteBoxOffset + (ulong)(box * boxsize);
            var slotstart = boxStart + (ulong)(slot * BoxSlotSize);
            return slotstart;
        }
        private static bool IsValidFileExtension(string fileExt, int gameType)
        {
            return gameType switch
            {
                (int)GameSelected.Scarlet or (int)GameSelected.Violet => fileExt == "pk9",
                (int)GameSelected.LegendsArceus => fileExt == "pa8",
                (int)GameSelected.BrilliantDiamond or (int)GameSelected.ShiningPearl => fileExt == "pb8",
                (int)GameSelected.Sword or (int)GameSelected.Shield => fileExt == "pk8",
                (int)GameSelected.LetsGoPikachu or (int)GameSelected.LetsGoEevee => fileExt == "pb7",
                _ => false,
            };
        }

        private async void InjectPb(PictureBox pb)
        {
            if (pb!.Image != null)
            {
                DialogResult dialogResult = MessageBox.Show("Slot is not empty, inject over current Pokémon?", "Alert!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                    return;
            }

            var gameTypeExtensions = new Dictionary<int, string>
            {
                {(int)GameSelected.Scarlet, "pk9"},
                {(int)GameSelected.Violet, "pk9"},
                {(int)GameSelected.LegendsArceus, "pa8"},
                {(int)GameSelected.BrilliantDiamond, "pb8"},
                {(int)GameSelected.ShiningPearl, "pb8"},
                {(int)GameSelected.Sword, "pk8"},
                {(int)GameSelected.Shield, "pk8"},
                {(int)GameSelected.LetsGoPikachu, "pb7"},
                {(int)GameSelected.LetsGoEevee, "pb7"}
             };

            using var openFileDialog = new OpenFileDialog
            {
                Title = "Select PK file to Inject",
                Filter = "Supported Pokémon Files|*.pk9;*.pk8;*.pb8;*.pb7;*.pa8",
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                var filePath = openFileDialog.FileName;
                var fileExt = Path.GetExtension(filePath).TrimStart('.').ToLower();
                var expectedExt = gameTypeExtensions[Settings.Default.GameConnected];

                if (!IsValidFileExtension(fileExt, Settings.Default.GameConnected))
                {
                    MessageBox.Show($"Invalid file type for this game.\nExpected: .{expectedExt}\nFound: .{fileExt}");
                    return;
                }

                var fileData = await File.ReadAllBytesAsync(filePath);
                var token = CancellationToken.None;
                var currentslot = GetSlotNumber(pb.Name);
                var pk = CreateBlank(fileData);

                await WritePokemonData(currentslot, pk, token);
                MessageBox.Show("Pokémon injected successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error injecting Pokémon: {ex.Message}");
            }
        }

        private void UpdateProgress(int currProgress, int maxProgress)
        {
            var value = (100 * currProgress) / maxProgress;
            if (progressBar1.InvokeRequired)
                progressBar1.Invoke(() => progressBar1.Value = value);
            else
                progressBar1.Value = value;
        }

        private void FlexButton_Click(object sender, EventArgs e)
        {
            FlexButton.Visible = false;
            Rectangle bounds = Bounds;
            Bitmap bmp = new(Width, Height - 60);
            DrawToBitmap(bmp, bounds);
            Clipboard.SetImage(bmp);
            MessageBox.Show("Copied to clipboard!");
            FlexButton.Visible = true;
        }
    }
}
