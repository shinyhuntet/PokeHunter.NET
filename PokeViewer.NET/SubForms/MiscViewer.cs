using PKHeX.Core;
using PokeViewer.NET.Properties;
using System.Text;
using Newtonsoft.Json;
using static SysBot.Base.SwitchButton;
using static SysBot.Base.SwitchStick;
using static System.Buffers.Binary.BinaryPrimitives;
using static PokeViewer.NET.RoutineExecutor;
using System.Globalization;
using SysBot.Base;
using System.Net.Sockets;

namespace PokeViewer.NET.SubForms
{
    public partial class MiscViewer : Form
    {
        private readonly ViewerState Executor;
        private static ulong BaseBlockKeyPointer = 0;
        private ulong PlayerOnMountOffset = 0;
        public ulong CountCacheP;
        public ulong CountCacheK;
        public ulong CountCacheB;
        public ulong CountCacheBCP;
        public ulong CountCacheBCK;
        public ulong CountCacheBCB;
        public ulong EventOutbreakActive;
        private DateTime StartTime;
        private System.Timers.Timer timer = new();
        public List<OutbreakStash> OutbreakCache = [];
        public List<OutbreakStash> BCATOutbreakCache = [];
        private List<Image> MapSpritesP = [];
        private List<byte[]?> MapPOSP = [];
        private List<string> MapCountP = [];
        private List<string> MapStringsP = [];
        private List<Image> MapSpritesObP = [];
        private List<byte[]?> MapPOSObP = [];
        private List<string> MapCountObP = [];
        private List<string> MapStringsObP = [];
        private List<Image> MapSpritesK = [];
        private List<byte[]?> MapPOSK = [];
        private List<string> MapCountK = [];
        private List<string> MapStringsK = [];
        private List<Image> MapSpritesObK = [];
        private List<byte[]?> MapPOSObK = [];
        private List<string> MapCountObK = [];
        private List<string> MapStringsObK = [];
        private List<Image> MapSpritesB = [];
        private List<byte[]?> MapPOSB = [];
        private List<string> MapCountB = [];
        private List<string> MapStringsB = [];
        private List<Image> MapSpritesObB = [];
        private List<byte[]?> MapPOSObB = [];
        private List<string> MapCountObB = [];
        private List<string> MapStringsObB = [];
        private List<(PK9, bool, float, float, int)> pkList = [];
        private List<byte[]?> CenterPosP = [];
        private List<byte[]?> CenterPosK = [];
        private List<byte[]?> CenterPosB = [];
        private List<byte[]?> CenterPos0bP = [];
        private List<byte[]?> CenterPos0bK = [];
        private List<byte[]?> CenterPos0bB = [];
        private string[] SpeciesList = null!;
        private string[] FormsList = null!;
        private string[] TypesList = null!;
        private readonly string[] GenderList = null!;
        private GameStrings Strings;
        private GameSelected GameType;
        protected ViewerOffsets Offsets { get; } = new();
        public MiscViewer(ref ViewerState executor, int gameType, (Color, Color) color)
        {
            InitializeComponent();
            Executor = executor;
            SetColors(color);
            MapGroup.SelectedIndex = Settings.Default.MapSetting;
            LanguageBox.DataSource = Enum.GetValues(typeof(LanguageID)).Cast<LanguageID>().Where(z => z != LanguageID.None && z != LanguageID.UNUSED_6).ToArray();
            LanguageBox.SelectedIndex = 1;
            Strings = GameInfo.GetStrings(LanguageBox.SelectedIndex);
            SpeciesList = Strings.specieslist;
            SpeciesBox.DataSource = Strings.specieslist.Where(z => PersonalTable.SV.IsSpeciesInGame((ushort)SpeciesList.ToList().IndexOf(z))).ToArray();
            FormsList = Strings.forms;
            TypesList = Strings.types;
            GenderList = [.. GameInfo.GenderSymbolUnicode];
            var path = "refs\\outbreakfilters.txt";
            if (File.Exists(path))
                LoadFilters(path);
            GameType = (GameSelected)gameType;
            EnableSVAssets(GameType);
            LoadOutbreakCache();
            LoadBCATOutbreakCache();
        }
        private void LoadFilters(string data)
        {
            string contents = File.ReadAllText(data);
            string[] monlist = contents.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string mons in monlist)
            {
                string[] mon = mons.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                PK9 pk = new()
                {
                    Species = Convert.ToUInt16(mon[0]),
                    Form = (byte)Convert.ToUInt16(mon[1])
                };
                var coordcheck = mon[2].ToLower() == "true";
                if (!Single.TryParse(mon[3], out float XCoord))
                    coordcheck = false;
                if (!Single.TryParse(mon[4], out float ZCoord))
                    coordcheck = false;
                if (!int.TryParse(mon[5], out int range))
                    coordcheck = false;
                pkList.Add((pk, coordcheck, XCoord, ZCoord, range));
            }
        }
        private void EnableSVAssets(GameSelected game)
        {
            if (game is GameSelected.Scarlet or GameSelected.Violet)
            {
                WildSpawnGroup.Enabled = true;
                MIscGroup.Enabled = true;
                CoordGroup.Enabled = true;
                SearchGroup.Enabled = true;
                ResetGroup.Enabled = true;
                LanguageGroup.Enabled = true;
                TeleportGroup.Enabled = true;
                AddSpecies.Enabled = true;
                RemoveSpecies.Enabled = true;
                OpenMapPaldea.Enabled = true;
                OpenMapKitakami.Enabled = true;
                OpenMapBlueberry.Enabled = true;
                OutbreakScan.Enabled = true;
            }
        }
private void LanguageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LanguageBox.SelectedIndex < 0)
                return;
            var speciesindex = SpeciesBox.SelectedIndex;
            var formindex = FormCombo.Visible ? FormCombo.SelectedIndex : 0;
            Strings = GameInfo.GetStrings(LanguageBox.SelectedIndex);
            languageindex = LanguageBox.SelectedIndex;
            SpeciesList = Strings.specieslist;
            SpeciesBox.DataSource = Strings.specieslist.Where(z => PersonalTable.SV.IsSpeciesInGame((ushort)SpeciesList.ToList().IndexOf(z))).ToArray();
            FormsList = Strings.forms;
            TypesList = Strings.types;
            SpeciesBox.SelectedIndex = speciesindex;
            if (FormCombo.Visible)
                FormCombo.SelectedIndex = formindex;
        }
        private void SetColors((Color, Color) color)
        {
            BackColor = color.Item1;
            ForeColor = color.Item2;
            OutbreakScan.BackColor = color.Item1;
            OutbreakScan.ForeColor = color.Item2;
            OpenMapPaldea.BackColor = color.Item1;
            OpenMapPaldea.ForeColor = color.Item2;
            OpenMapKitakami.BackColor = color.Item1;
            OpenMapKitakami.ForeColor = color.Item2;
            OpenMapBlueberry.BackColor = color.Item1;
            OpenMapBlueberry.ForeColor = color.Item2;
            ViewList.BackColor = color.Item1;
            ViewList.ForeColor = color.Item2;
            ClearList.BackColor = color.Item1;
            ClearList.ForeColor = color.Item2;
            ChangeFormButton.BackColor = color.Item1;
            ChangeFormButton.ForeColor = color.Item2;
            ReadValues.BackColor = color.Item1;
            ReadValues.ForeColor = color.Item2;
            AddSpecies.BackColor = color.Item1;
            AddSpecies.ForeColor = color.Item2;
            RemoveSpecies.BackColor = color.Item1;
            RemoveSpecies.ForeColor = color.Item2;
            groupBox1.BackColor = color.Item1;
            groupBox1.ForeColor = color.Item2;
            PaldeaGroup.BackColor = color.Item1;
            PaldeaGroup.ForeColor = color.Item2;
            KitakamiGroup.BackColor = color.Item1;
            KitakamiGroup.ForeColor = color.Item2;
            SearchGroup.BackColor = color.Item1;
            SearchGroup.ForeColor = color.Item2;
            BlueberryGroup.BackColor = color.Item1;
            BlueberryGroup.ForeColor = color.Item2;
            SpeciesBox.BackColor = color.Item1;
            SpeciesBox.ForeColor = color.Item2;
            FormCombo.ForeColor = color.Item2;
            FormCombo.BackColor = color.Item1;
            V_ComboBox.BackColor = color.Item1;
            V_ComboBox.ForeColor = color.Item2;
            MapGroup.BackColor = color.Item1;
            MapGroup.ForeColor = color.Item2;
            TimeViewerButton.BackColor = color.Item1;
            TimeViewerButton.ForeColor = color.Item2;
        }

        public void LoadOutbreakCache()
        {
            for (int i = 0; i < 18; i++)
                OutbreakCache.Add(new());
        }

        public void LoadBCATOutbreakCache()
        {
            for (int i = 0; i < 30; i++)
                BCATOutbreakCache.Add(new());
        }
        private void SelectedIndex_IsChanged(object sender, EventArgs e)
        {
            OutbreakCache = [];
            BCATOutbreakCache = [];
            LoadOutbreakCache();
            LoadBCATOutbreakCache();
            CountCacheP = 0;
            CountCacheK = 0;
            CountCacheB = 0;
            CountCacheBCP = 0;
            CountCacheBCK = 0;
            CountCacheBCB = 0;
            Settings.Default.MapSetting = MapGroup.SelectedIndex;
            Settings.Default.Save();
        }

        private async Task SearchForOutbreak(CancellationToken token)
        {
            await ParseBlockKeyPointer(false, token).ConfigureAwait(false);
            Label[] Plist = { Ob1Results, Ob2Results, Ob3Results, Ob4Results, Ob5Results, Ob6Results, Ob7Results, Ob8Results };
            PictureBox[] SprP = { OBSprite1, OBSprite2, OBSprite3, OBSprite4, OBSprite5, OBSprite6, OBSprite7, OBSprite8 };
            Label[] KList = { ObResults9, ObResults10, ObResults11, ObResults12 };
            PictureBox[] SprK = { ObSprite9, ObSprite10, ObSprite11, ObSprite12 };
            Label[] BList = { ObResults13, ObResults14, ObResults15, ObResults16, ObResults17 };
            PictureBox[] SprB = { ObSprite13, ObSprite14, ObSprite15, ObSprite16, ObSprite17 };
            Label[] CountP = { ObCount1, ObCount2, ObCount3, ObCount4, ObCount5, ObCount6, ObCount7, ObCount8 };
            Label[] CountK = { ObCount9, ObCount10, ObCount11, ObCount12 };
            Label[] CountB = { ObCount13, ObCount14, ObCount15, ObCount16, ObCount17 };
            Label[] BCPlist = { ObE1Results, ObE2Results, ObE3Results, ObE4Results, ObE5Results, ObE6Results, ObE7Results, ObE8Results, ObE9Results, ObE10Results };
            PictureBox[] SprBCP = { OBESprite1, OBESprite2, OBESprite3, OBESprite4, OBESprite5, OBESprite6, OBESprite7, OBESprite8, OBESprite9, OBESprite10 };
            Label[] BCKList = { ObE11Results, ObE12Results, ObE13Results, ObE14Results, ObE15Results, ObE16Results, ObE17Results, ObE18Results, ObE19Results, ObE20Results };
            PictureBox[] SprBCK = { ObESprite11, ObESprite12, ObESprite13, ObESprite14, ObESprite15, ObESprite16, ObESprite17, ObESprite18, ObESprite19, ObESprite20 };
            Label[] BCBList = { ObE21Results, ObE22Results, ObE23Results, ObE24Results, ObE25Results, ObE26Results, ObE27Results, ObE28Results, ObE29Results, ObE30Results };
            PictureBox[] SprBCB = { ObESprite21, ObESprite22, ObESprite23, ObESprite24, ObESprite25, ObESprite26, ObESprite27, ObESprite28, ObESprite29, ObESprite30 };
            Label[] CountBCP = { ObECount1, ObECount2, ObECount3, ObECount4, ObECount5, ObECount6, ObECount7, ObECount8, ObECount9, ObECount10 };
            Label[] CountBCK = { ObECount11, ObECount12, ObECount13, ObECount14, ObECount15, ObECount16, ObECount17, ObECount18, ObECount19, ObECount20 };
            Label[] CountBCB = { ObECount21, ObECount22, ObECount23, ObECount24, ObECount25, ObECount26, ObECount27, ObECount28, ObECount29, ObECount30 };

            List<Image> ImgP = new();
            List<Image> ImgK = new();
            List<Image> ImgB = new();
            List<Image> ImgObP = new();
            List<Image> ImgObK = new();
            List<Image> ImgObB = new();
            List<byte[]?> POSlistP = new();
            List<byte[]?> POSlistK = new();
            List<byte[]?> POSlistB = new();
            List<byte[]?> POSlistObP = new();
            List<byte[]?> POSlistObK = new();
            List<byte[]?> POSlistObB = new();
            List<uint> kolistP = new();
            List<uint> kolistK = new();
            List<uint> kolistB = new();
            List<uint> kolistObP = new();
            List<uint> kolistObK = new();
            List<uint> kolistObB = new();
            List<uint> totallistP = new();
            List<uint> totallistK = new();
            List<uint> totallistB = new();
            List<uint> totallistObP = new();
            List<uint> totallistObK = new();
            List<uint> totallistObB = new();
            List<string> stringsP = new();
            List<string> stringsK = new();
            List<string> stringsB = new();
            List<string> stringsObP = new();
            List<string> stringsObK = new();
            List<string> stringsObB = new();
            List<string> CountlistP = new();
            List<string> CountlistK = new();
            List<string> CountlistB = new();
            List<string> CountlistObP = new();
            List<string> CountlistObK = new();
            List<string> CountlistObB = new();
            List<PK9> monsP = new();
            List<PK9> monsK = new();
            List<PK9> monsB = new();
            List<PK9> monsObP = new();
            List<PK9> monsObK = new();
            List<PK9> monsObB = new();
            CenterPosP = new();
            CenterPosK = new();
            CenterPosB = new();
            CenterPos0bP = new();
            CenterPos0bK = new();
            CenterPos0bB = new();
            MapSpritesP = [];
            MapPOSP = [];
            MapCountP = [];
            MapStringsP = [];
            MapSpritesK = [];
            MapPOSK = [];
            MapCountK = [];
            MapStringsK = [];
            MapSpritesB = [];
            MapPOSB = [];
            MapCountB = [];
            MapStringsB = [];
            MapSpritesObP = [];
            MapPOSObP = [];
            MapCountObP = [];
            MapStringsObP = [];
            MapSpritesObK = [];
            MapPOSObK = [];
            MapCountObK = [];
            MapStringsObK = [];
            MapSpritesObB = [];
            MapPOSObB = [];
            MapCountObB = [];
            MapStringsObB = [];
            bool matchfound = false;

            CollideButton.Enabled = false;
            DisableAssets();
            int dayskip = 0;
            int ResetCount = 0;
            ulong dayseed;
            ulong dayseed_old;
            int OutbreaktotalP = 0;
            int OutbreaktotalK = 0;
            int OutbreaktotalB = 0;
            int OutbreaktotalBCP = 0;
            int OutbreaktotalBCK = 0;
            int OutbreaktotalBCB = 0;
            var DayseedOffset = await ParseDayseedPointer(token).ConfigureAwait(false);
            dayseed = BitConverter.ToUInt64(await Executor.SwitchConnection.ReadBytesAbsoluteAsync(DayseedOffset, 8, token).ConfigureAwait(false));
            while (!token.IsCancellationRequested)
            {
                dayskip++;
                ResetCount++;

                if (ResetNum.Value > 0 && ResetCount == ResetNum.Value)
                {
                    await ResetGame(token, false).ConfigureAwait(false);
                    DayseedOffset = await ParseDayseedPointer(token).ConfigureAwait(false);
                    await ParseBlockKeyPointer(false, token).ConfigureAwait(false);
                    ResetCount = 0;
                }

                for (int i = 0; i < Plist.Length; i++)
                {
                    SprP[i].Image = null;
                    Plist[i].Text = string.Empty;
                    CountP[i].Text = string.Empty;
                }

                for (int i = 0; i < KList.Length; i++)
                {
                    SprK[i].Image = null;
                    KList[i].Text = string.Empty;
                    CountK[i].Text = string.Empty;
                }
                for (int i = 0; i < BList.Length; i++)
                {
                    SprB[i].Image = null;
                    BList[i].Text = string.Empty;
                    CountB[i].Text = string.Empty;
                }
                for (int i = 0; i < BCPlist.Length; i++)
                {
                    SprBCP[i].Image = null;
                    BCPlist[i].Text = string.Empty;
                    CountBCP[i].Text = string.Empty;
                }
                for (int i = 0; i < BCKList.Length; i++)
                {
                    SprBCK[i].Image = null;
                    BCKList[i].Text = string.Empty;
                    CountBCK[i].Text = string.Empty;
                }
                for (int i = 0; i < BCBList.Length; i++)
                {
                    SprBCB[i].Image = null;
                    BCBList[i].Text = string.Empty;
                    CountBCB[i].Text = string.Empty;
                }
                OpenMapPaldea.Text = "Paldea";
                OpenMapKitakami.Text = "Kitakami";
                OpenMapBlueberry.Text = "Blueberry";
                StatusLabel.Text = "Status: Saving...";
                UpdateProgress(10, 100);
                if(WildCheck.Checked)
                {
                    bool wait = false;
                    if (await IsInBattle(token).ConfigureAwait(false))
                        wait = true;
                    await DefeatPokemon(token).ConfigureAwait(false);
                    if(wait)
                        await Task.Delay(2_000, token).ConfigureAwait(false);
                }
                await SVSaveGameOverworld(token).ConfigureAwait(false);
                StatusLabel.Text = "Status: Scanning...";

                var block = Blocks.KOutbreakSpecies1;
                var koblock = Blocks.KMassOutbreakKO1;
                var totalblock = Blocks.KMassOutbreak01TotalSpawns;
                var formblock = Blocks.KMassOutbreak01Form;
                var pos = Blocks.KMassOutbreak01CenterPos;
                if (MapGroup.SelectedIndex is 0 or 1)
                {
                    var dataP = await ReadEncryptedBlockByte(Blocks.KMassOutbreakTotalPaldea, CountCacheP, token).ConfigureAwait(false);
                    if (CountCacheP == 0)
                        CountCacheP = dataP.Item2;

                    OutbreaktotalP = Convert.ToInt32(dataP.Item1);
                    if (OutbreaktotalP > 8)
                    {
                        BaseBlockKeyPointer = await Executor.SwitchConnection.PointerAll(Offsets.BlockKeyPointer, token).ConfigureAwait(false);
                        // Rerun in case of bad pointer
                        OutbreakCache = [];
                        LoadOutbreakCache();
                        CountCacheP = 0;
                        continue;
                    }
                    UpdateProgress(20, 100);
                    for (int i = 0; i < Blocks.OutbreakMainMaxCount; i++)
                    {
                        switch (i)
                        {
                            case 0: break;
                            case 1: block = Blocks.KOutbreakSpecies2; formblock = Blocks.KMassOutbreak02Form; koblock = Blocks.KMassOutbreakKO2; totalblock = Blocks.KMassOutbreak02TotalSpawns; pos = Blocks.KMassOutbreak02CenterPos; break;
                            case 2: block = Blocks.KOutbreakSpecies3; formblock = Blocks.KMassOutbreak03Form; koblock = Blocks.KMassOutbreakKO3; totalblock = Blocks.KMassOutbreak03TotalSpawns; pos = Blocks.KMassOutbreak03CenterPos; break;
                            case 3: block = Blocks.KOutbreakSpecies4; formblock = Blocks.KMassOutbreak04Form; koblock = Blocks.KMassOutbreakKO4; totalblock = Blocks.KMassOutbreak04TotalSpawns; pos = Blocks.KMassOutbreak04CenterPos; break;
                            case 4: block = Blocks.KOutbreakSpecies5; formblock = Blocks.KMassOutbreak05Form; koblock = Blocks.KMassOutbreakKO5; totalblock = Blocks.KMassOutbreak05TotalSpawns; pos = Blocks.KMassOutbreak05CenterPos; break;
                            case 5: block = Blocks.KOutbreakSpecies6; formblock = Blocks.KMassOutbreak06Form; koblock = Blocks.KMassOutbreakKO6; totalblock = Blocks.KMassOutbreak06TotalSpawns; pos = Blocks.KMassOutbreak06CenterPos; break;
                            case 6: block = Blocks.KOutbreakSpecies7; formblock = Blocks.KMassOutbreak07Form; koblock = Blocks.KMassOutbreakKO7; totalblock = Blocks.KMassOutbreak07TotalSpawns; pos = Blocks.KMassOutbreak07CenterPos; break;
                            case 7: block = Blocks.KOutbreakSpecies8; formblock = Blocks.KMassOutbreak08Form; koblock = Blocks.KMassOutbreakKO8; totalblock = Blocks.KMassOutbreak08TotalSpawns; pos = Blocks.KMassOutbreak08CenterPos; break;
                        }
                        if (i > OutbreaktotalP - 1)
                            break;

                        var (kocount, lofs) = await ReadEncryptedBlockUint(koblock, OutbreakCache[i].SpeciesKOCountLoaded, token).ConfigureAwait(false);
                        OutbreakCache[i].SpeciesKOCountLoaded = lofs;
                        var (totalcount, tofs) = await ReadEncryptedBlockUint(totalblock, OutbreakCache[i].SpeciesTotalCountLoaded, token).ConfigureAwait(false);
                        OutbreakCache[i].SpeciesTotalCountLoaded = tofs;
                        var (species, sofs) = await ReadEncryptedBlockUint(block, OutbreakCache[i].SpeciesLoaded, token).ConfigureAwait(false);
                        OutbreakCache[i].SpeciesLoaded = sofs;
                        var (form, fofs) = await ReadEncryptedBlockByte(formblock, OutbreakCache[i].SpeciesFormLoaded, token).ConfigureAwait(false);
                        OutbreakCache[i].SpeciesFormLoaded = fofs;
                        var (obpos, bofs) = await ReadEncryptedBlockArray(pos, OutbreakCache[i].SpeciesCenterPOSLoaded, token).ConfigureAwait(false);
                        OutbreakCache[i].SpeciesCenterPOSLoaded = bofs;

                        PK9 pk = new()
                        {
                            Species = SpeciesConverter.GetNational9((ushort)species),
                            Form = form,
                        };
                        CommonEdits.SetIsShiny(pk, false);
                        string pkform = FormOutput(pk.Species, pk.Form, out _);
                        stringsP.Add($"{Strings.specieslist[pk.Species]}{pkform}");
                        CountlistP.Add($"{kocount}/{totalcount}");
                        var imgurl = PokeImg(pk, false);
                        PictureBox picture = new();
                        picture.Load(imgurl);
                        ImgP.Add(picture.Image);
                        monsP.Add(pk);
                        POSlistP.Add(obpos);
                        kolistP.Add(kocount);
                        totallistP.Add(totalcount);
                    }
                    UpdateProgress(25, 100);
                }
                if (MapGroup.SelectedIndex is 0 or 2)
                {
                    var dataK = await ReadEncryptedBlockByte(Blocks.KMassOutbreakTotalKitakami, CountCacheK, token).ConfigureAwait(false);
                    if (CountCacheK == 0)
                        CountCacheK = dataK.Item2;

                    OutbreaktotalK = Convert.ToInt32(dataK.Item1);
                    if (OutbreaktotalK > 4)
                    {
                        BaseBlockKeyPointer = await Executor.SwitchConnection.PointerAll(Offsets.BlockKeyPointer, token).ConfigureAwait(false);
                        // Rerun in case of bad pointer
                        OutbreakCache = [];
                        LoadOutbreakCache();
                        CountCacheK = 0;
                        continue;
                    }
                    UpdateProgress(30, 100);
                    for (int i = 8; i < Blocks.OutbreakMainMaxCount + Blocks.OutbreakDLC1MaxCount; i++)
                    {
                        switch (i)
                        {
                            case 8: block = Blocks.KOutbreakSpecies9; formblock = Blocks.KMassOutbreak09Form; koblock = Blocks.KMassOutbreakKO9; totalblock = Blocks.KMassOutbreak09TotalSpawns; pos = Blocks.KMassOutbreak09CenterPos; break;
                            case 9: block = Blocks.KOutbreakSpecies10; formblock = Blocks.KMassOutbreak10Form; koblock = Blocks.KMassOutbreakKO10; totalblock = Blocks.KMassOutbreak10TotalSpawns; pos = Blocks.KMassOutbreak10CenterPos; break;
                            case 10: block = Blocks.KOutbreakSpecies11; formblock = Blocks.KMassOutbreak11Form; koblock = Blocks.KMassOutbreakKO11; totalblock = Blocks.KMassOutbreak11TotalSpawns; pos = Blocks.KMassOutbreak11CenterPos; break;
                            case 11: block = Blocks.KOutbreakSpecies12; formblock = Blocks.KMassOutbreak12Form; koblock = Blocks.KMassOutbreakKO12; totalblock = Blocks.KMassOutbreak12TotalSpawns; pos = Blocks.KMassOutbreak12CenterPos; break;
                        }
                        if (i > OutbreaktotalK + 8 - 1)
                            break;

                        var (kocount, lofs) = await ReadEncryptedBlockUint(koblock, OutbreakCache[i].SpeciesKOCountLoaded, token).ConfigureAwait(false);
                        OutbreakCache[i].SpeciesKOCountLoaded = lofs;
                        var (totalcount, tofs) = await ReadEncryptedBlockUint(totalblock, OutbreakCache[i].SpeciesTotalCountLoaded, token).ConfigureAwait(false);
                        OutbreakCache[i].SpeciesTotalCountLoaded = tofs;
                        var (species, sofs) = await ReadEncryptedBlockUint(block, OutbreakCache[i].SpeciesLoaded, token).ConfigureAwait(false);
                        OutbreakCache[i].SpeciesLoaded = sofs;
                        var (form, fofs) = await ReadEncryptedBlockByte(formblock, OutbreakCache[i].SpeciesFormLoaded, token).ConfigureAwait(false);
                        OutbreakCache[i].SpeciesFormLoaded = fofs;
                        var (obpos, bofs) = await ReadEncryptedBlockArray(pos, OutbreakCache[i].SpeciesCenterPOSLoaded, token).ConfigureAwait(false);
                        OutbreakCache[i].SpeciesCenterPOSLoaded = bofs;

                        PK9 pk = new()
                        {
                            Species = SpeciesConverter.GetNational9((ushort)species),
                            Form = form,
                        };
                        CommonEdits.SetIsShiny(pk, false);
                        string pkform = FormOutput(pk.Species, pk.Form, out _);
                        stringsK.Add($"{Strings.specieslist[pk.Species]}{pkform}");
                        CountlistK.Add($"{kocount}/{totalcount}");
                        var imgurl = PokeImg(pk, false);
                        PictureBox picture = new();
                        picture.Load(imgurl);
                        ImgK.Add(picture.Image);
                        monsK.Add(pk);
                        POSlistK.Add(obpos);
                        kolistK.Add(kocount);
                        totallistK.Add(totalcount);
                    }
                    UpdateProgress(35, 100);
                }

                if (MapGroup.SelectedIndex is 0 or 3)
                {
                    var dataB = await ReadEncryptedBlockByte(Blocks.KMassOutbreakTotalBlueberry, CountCacheB, token).ConfigureAwait(false);
                    if (CountCacheB == 0)
                        CountCacheB = dataB.Item2;

                    OutbreaktotalB = Convert.ToInt32(dataB.Item1);
                    if (OutbreaktotalB > 5)
                    {
                        BaseBlockKeyPointer = await Executor.SwitchConnection.PointerAll(Offsets.BlockKeyPointer, token).ConfigureAwait(false);
                        // Rerun in case of bad pointer
                        OutbreakCache = [];
                        LoadOutbreakCache();
                        CountCacheB = 0;
                        continue;
                    }
                    UpdateProgress(40, 100);
                    for (int i = 12; i < Blocks.OutbreakMainMaxCount + Blocks.OutbreakDLC1MaxCount + Blocks.OutbreakDLC2MaxCount; i++)
                    {
                        switch (i)
                        {
                            case 12: block = Blocks.KOutbreakSpecies13; formblock = Blocks.KMassOutbreak13Form; koblock = Blocks.KMassOutbreakKO13; totalblock = Blocks.KMassOutbreak13TotalSpawns; pos = Blocks.KMassOutbreak13CenterPos; break;
                            case 13: block = Blocks.KOutbreakSpecies14; formblock = Blocks.KMassOutbreak14Form; koblock = Blocks.KMassOutbreakKO14; totalblock = Blocks.KMassOutbreak14TotalSpawns; pos = Blocks.KMassOutbreak14CenterPos; break;
                            case 14: block = Blocks.KOutbreakSpecies15; formblock = Blocks.KMassOutbreak15Form; koblock = Blocks.KMassOutbreakKO15; totalblock = Blocks.KMassOutbreak15TotalSpawns; pos = Blocks.KMassOutbreak15CenterPos; break;
                            case 15: block = Blocks.KOutbreakSpecies16; formblock = Blocks.KMassOutbreak16Form; koblock = Blocks.KMassOutbreakKO16; totalblock = Blocks.KMassOutbreak16TotalSpawns; pos = Blocks.KMassOutbreak16CenterPos; break;
                            case 16: block = Blocks.KOutbreakSpecies17; formblock = Blocks.KMassOutbreak17Form; koblock = Blocks.KMassOutbreakKO17; totalblock = Blocks.KMassOutbreak17TotalSpawns; pos = Blocks.KMassOutbreak17CenterPos; break;
                        }
                        if (i > OutbreaktotalB + 12 - 1)
                            break;

                        var (kocount, lofs) = await ReadEncryptedBlockUint(koblock, OutbreakCache[i].SpeciesKOCountLoaded, token).ConfigureAwait(false);
                        OutbreakCache[i].SpeciesKOCountLoaded = lofs;
                        var (totalcount, tofs) = await ReadEncryptedBlockUint(totalblock, OutbreakCache[i].SpeciesTotalCountLoaded, token).ConfigureAwait(false);
                        OutbreakCache[i].SpeciesTotalCountLoaded = tofs;
                        var (species, sofs) = await ReadEncryptedBlockUint(block, OutbreakCache[i].SpeciesLoaded, token).ConfigureAwait(false);
                        OutbreakCache[i].SpeciesLoaded = sofs;
                        var (form, fofs) = await ReadEncryptedBlockByte(formblock, OutbreakCache[i].SpeciesFormLoaded, token).ConfigureAwait(false);
                        OutbreakCache[i].SpeciesFormLoaded = fofs;
                        var (obpos, bofs) = await ReadEncryptedBlockArray(pos, OutbreakCache[i].SpeciesCenterPOSLoaded, token).ConfigureAwait(false);
                        OutbreakCache[i].SpeciesCenterPOSLoaded = bofs;

                        PK9 pk = new()
                        {
                            Species = SpeciesConverter.GetNational9((ushort)species),
                            Form = form,
                        };
                        CommonEdits.SetIsShiny(pk, false);
                        string pkform = FormOutput(pk.Species, pk.Form, out _);
                        stringsB.Add($"{Strings.specieslist[pk.Species]}{pkform}");
                        CountlistB.Add($"{kocount}/{totalcount}");
                        var imgurl = PokeImg(pk, false);
                        PictureBox picture = new();
                        picture.Load(imgurl);
                        ImgB.Add(picture.Image);
                        monsB.Add(pk);
                        POSlistB.Add(obpos);
                        kolistB.Add(kocount);
                        totallistB.Add(totalcount);
                    }
                    UpdateProgress(45, 100);
                }
                if (ScanForEventOutbreak.Checked)
                {
                    var (BCATObEnabled, eof) = await ReadEncryptedBlockBool(Blocks.KBCATOutbreakEnabled, EventOutbreakActive, token).ConfigureAwait(false);
                    EventOutbreakActive = eof;
                    if (BCATObEnabled)
                    {
                        var BCOspecies = Blocks.KOutbreakBC01MainSpecies;
                        var BCOko = Blocks.KOutbreakBC01MainNumKOed;
                        var BCOtotal = Blocks.KOutbreakBC01MainTotalSpawns;
                        var BCOform = Blocks.KOutbreakBC01MainForm;
                        var BCOcenter = Blocks.KOutbreakBC01MainCenterPos;

                        if (MapGroup.SelectedIndex is 0 or 1)
                        {
                            var dataBCP = await ReadEncryptedBlockByte(Blocks.KOutbreakBCMainNumActive, CountCacheBCP, token).ConfigureAwait(false);
                            if (CountCacheBCP == 0)
                                CountCacheBCP = dataBCP.Item2;

                            OutbreaktotalBCP = Convert.ToInt32(dataBCP.Item1);
                            if (OutbreaktotalBCP > 10)
                            {
                                BaseBlockKeyPointer = await Executor.SwitchConnection.PointerAll(Offsets.BlockKeyPointer, token).ConfigureAwait(false);
                                // Rerun in case of bad pointer
                                BCATOutbreakCache = [];
                                LoadBCATOutbreakCache();
                                CountCacheBCP = 0;
                                continue;
                            }

                            UpdateProgress(50, 100);
                            for (int i = 0; i < Blocks.OutbreakBCMainMaxCount; i++)
                            {
                                switch (i)
                                {
                                    case 1: BCOspecies = Blocks.KOutbreakBC02MainSpecies; BCOform = Blocks.KOutbreakBC02MainForm; BCOko = Blocks.KOutbreakBC02MainNumKOed; BCOtotal = Blocks.KOutbreakBC02MainTotalSpawns; BCOcenter = Blocks.KOutbreakBC02MainCenterPos; break;
                                    case 2: BCOspecies = Blocks.KOutbreakBC03MainSpecies; BCOform = Blocks.KOutbreakBC03MainForm; BCOko = Blocks.KOutbreakBC03MainNumKOed; BCOtotal = Blocks.KOutbreakBC03MainTotalSpawns; BCOcenter = Blocks.KOutbreakBC03MainCenterPos; break;
                                    case 3: BCOspecies = Blocks.KOutbreakBC04MainSpecies; BCOform = Blocks.KOutbreakBC04MainForm; BCOko = Blocks.KOutbreakBC04MainNumKOed; BCOtotal = Blocks.KOutbreakBC04MainTotalSpawns; BCOcenter = Blocks.KOutbreakBC04MainCenterPos; break;
                                    case 4: BCOspecies = Blocks.KOutbreakBC05MainSpecies; BCOform = Blocks.KOutbreakBC05MainForm; BCOko = Blocks.KOutbreakBC05MainNumKOed; BCOtotal = Blocks.KOutbreakBC05MainTotalSpawns; BCOcenter = Blocks.KOutbreakBC05MainCenterPos; break;
                                    case 5: BCOspecies = Blocks.KOutbreakBC06MainSpecies; BCOform = Blocks.KOutbreakBC06MainForm; BCOko = Blocks.KOutbreakBC06MainNumKOed; BCOtotal = Blocks.KOutbreakBC06MainTotalSpawns; BCOcenter = Blocks.KOutbreakBC06MainCenterPos; break;
                                    case 6: BCOspecies = Blocks.KOutbreakBC07MainSpecies; BCOform = Blocks.KOutbreakBC07MainForm; BCOko = Blocks.KOutbreakBC07MainNumKOed; BCOtotal = Blocks.KOutbreakBC07MainTotalSpawns; BCOcenter = Blocks.KOutbreakBC07MainCenterPos; break;
                                    case 7: BCOspecies = Blocks.KOutbreakBC08MainSpecies; BCOform = Blocks.KOutbreakBC08MainForm; BCOko = Blocks.KOutbreakBC08MainNumKOed; BCOtotal = Blocks.KOutbreakBC08MainTotalSpawns; BCOcenter = Blocks.KOutbreakBC08MainCenterPos; break;
                                    case 8: BCOspecies = Blocks.KOutbreakBC09MainSpecies; BCOform = Blocks.KOutbreakBC09MainForm; BCOko = Blocks.KOutbreakBC09MainNumKOed; BCOtotal = Blocks.KOutbreakBC09MainTotalSpawns; BCOcenter = Blocks.KOutbreakBC09MainCenterPos; break;
                                    case 9: BCOspecies = Blocks.KOutbreakBC10MainSpecies; BCOform = Blocks.KOutbreakBC10MainForm; BCOko = Blocks.KOutbreakBC10MainNumKOed; BCOtotal = Blocks.KOutbreakBC10MainTotalSpawns; BCOcenter = Blocks.KOutbreakBC10MainCenterPos; break;
                                }
                                if (i > OutbreaktotalBCP - 1)
                                    break;

                                var (kocount, lofs) = await ReadEncryptedBlockUint(BCOko, BCATOutbreakCache[i].SpeciesKOCountLoaded, token).ConfigureAwait(false);
                                BCATOutbreakCache[i].SpeciesKOCountLoaded = lofs;
                                var (totalcount, tofs) = await ReadEncryptedBlockUint(BCOtotal, BCATOutbreakCache[i].SpeciesTotalCountLoaded, token).ConfigureAwait(false);
                                BCATOutbreakCache[i].SpeciesTotalCountLoaded = tofs;
                                var (species, sofs) = await ReadEncryptedBlockUint(BCOspecies, BCATOutbreakCache[i].SpeciesLoaded, token).ConfigureAwait(false);
                                BCATOutbreakCache[i].SpeciesLoaded = sofs;
                                var (form, fofs) = await ReadEncryptedBlockByte(BCOform, BCATOutbreakCache[i].SpeciesFormLoaded, token).ConfigureAwait(false);
                                BCATOutbreakCache[i].SpeciesFormLoaded = fofs;
                                var (obpos, bofs) = await ReadEncryptedBlockArray(BCOcenter, BCATOutbreakCache[i].SpeciesCenterPOSLoaded, token).ConfigureAwait(false);
                                BCATOutbreakCache[i].SpeciesCenterPOSLoaded = bofs;

                                PK9 pk = new()
                                {
                                    Species = SpeciesConverter.GetNational9((ushort)species),
                                    Form = form,
                                };
                                CommonEdits.SetIsShiny(pk, false);
                                string pkform = FormOutput(pk.Species, pk.Form, out _);
                                stringsObP.Add($"{Strings.specieslist[pk.Species]}{pkform}");
                                CountlistObP.Add($"{kocount}/{totalcount}");
                                var imgurl = PokeImg(pk, false);
                                PictureBox picture = new();
                                picture.Load(imgurl);
                                ImgObP.Add(picture.Image);
                                monsObP.Add(pk);
                                POSlistObP.Add(obpos);
                                kolistObP.Add(kocount);
                                totallistObP.Add(totalcount);
                            }
                            UpdateProgress(55, 100);
                        }

                        if (MapGroup.SelectedIndex is 0 or 2)
                        {
                            var dataBCK = await ReadEncryptedBlockByte(Blocks.KOutbreakBCDLC1NumActive, CountCacheBCK, token).ConfigureAwait(false);
                            if (CountCacheBCK == 0)
                                CountCacheBCK = dataBCK.Item2;

                            OutbreaktotalBCK = Convert.ToInt32(dataBCK.Item1);
                            if (OutbreaktotalBCK > 10)
                            {
                                BaseBlockKeyPointer = await Executor.SwitchConnection.PointerAll(Offsets.BlockKeyPointer, token).ConfigureAwait(false);
                                // Rerun in case of bad pointer
                                BCATOutbreakCache = [];
                                LoadBCATOutbreakCache();
                                CountCacheBCK = 0;
                                continue;
                            }

                            BCOspecies = Blocks.KOutbreakBC01DLC1Species;
                            BCOko = Blocks.KOutbreakBC01DLC1NumKOed;
                            BCOtotal = Blocks.KOutbreakBC01DLC1TotalSpawns;
                            BCOform = Blocks.KOutbreakBC01DLC1Form;
                            BCOcenter = Blocks.KOutbreakBC01DLC1CenterPos;

                            UpdateProgress(60, 100);
                            for (int i = 10; i < Blocks.OutbreakBCMainMaxCount + Blocks.OutbreakBCDLC1MaxCount; i++)
                            {
                                switch (i)
                                {
                                    case 11: BCOspecies = Blocks.KOutbreakBC02DLC1Species; BCOform = Blocks.KOutbreakBC02DLC1Form; BCOko = Blocks.KOutbreakBC02DLC1NumKOed; BCOtotal = Blocks.KOutbreakBC02DLC1TotalSpawns; BCOcenter = Blocks.KOutbreakBC02DLC1CenterPos; break;
                                    case 12: BCOspecies = Blocks.KOutbreakBC03DLC1Species; BCOform = Blocks.KOutbreakBC03DLC1Form; BCOko = Blocks.KOutbreakBC03DLC1NumKOed; BCOtotal = Blocks.KOutbreakBC03DLC1TotalSpawns; BCOcenter = Blocks.KOutbreakBC03DLC1CenterPos; break;
                                    case 13: BCOspecies = Blocks.KOutbreakBC04DLC1Species; BCOform = Blocks.KOutbreakBC04DLC1Form; BCOko = Blocks.KOutbreakBC04DLC1NumKOed; BCOtotal = Blocks.KOutbreakBC04DLC1TotalSpawns; BCOcenter = Blocks.KOutbreakBC04DLC1CenterPos; break;
                                    case 14: BCOspecies = Blocks.KOutbreakBC05DLC1Species; BCOform = Blocks.KOutbreakBC05DLC1Form; BCOko = Blocks.KOutbreakBC05DLC1NumKOed; BCOtotal = Blocks.KOutbreakBC05DLC1TotalSpawns; BCOcenter = Blocks.KOutbreakBC05DLC1CenterPos; break;
                                    case 15: BCOspecies = Blocks.KOutbreakBC06DLC1Species; BCOform = Blocks.KOutbreakBC06DLC1Form; BCOko = Blocks.KOutbreakBC06DLC1NumKOed; BCOtotal = Blocks.KOutbreakBC06DLC1TotalSpawns; BCOcenter = Blocks.KOutbreakBC06DLC1CenterPos; break;
                                    case 16: BCOspecies = Blocks.KOutbreakBC07DLC1Species; BCOform = Blocks.KOutbreakBC07DLC1Form; BCOko = Blocks.KOutbreakBC07DLC1NumKOed; BCOtotal = Blocks.KOutbreakBC07DLC1TotalSpawns; BCOcenter = Blocks.KOutbreakBC07DLC1CenterPos; break;
                                    case 17: BCOspecies = Blocks.KOutbreakBC08DLC1Species; BCOform = Blocks.KOutbreakBC08DLC1Form; BCOko = Blocks.KOutbreakBC08DLC1NumKOed; BCOtotal = Blocks.KOutbreakBC08DLC1TotalSpawns; BCOcenter = Blocks.KOutbreakBC08DLC1CenterPos; break;
                                    case 18: BCOspecies = Blocks.KOutbreakBC09DLC1Species; BCOform = Blocks.KOutbreakBC09DLC1Form; BCOko = Blocks.KOutbreakBC09DLC1NumKOed; BCOtotal = Blocks.KOutbreakBC09DLC1TotalSpawns; BCOcenter = Blocks.KOutbreakBC09DLC1CenterPos; break;
                                    case 19: BCOspecies = Blocks.KOutbreakBC10DLC1Species; BCOform = Blocks.KOutbreakBC10DLC1Form; BCOko = Blocks.KOutbreakBC10DLC1NumKOed; BCOtotal = Blocks.KOutbreakBC10DLC1TotalSpawns; BCOcenter = Blocks.KOutbreakBC10DLC1CenterPos; break;
                                }
                                if (i > OutbreaktotalBCK + 10 - 1)
                                    break;

                                var (kocount, lofs) = await ReadEncryptedBlockUint(BCOko, BCATOutbreakCache[i].SpeciesKOCountLoaded, token).ConfigureAwait(false);
                                BCATOutbreakCache[i].SpeciesKOCountLoaded = lofs;
                                var (totalcount, tofs) = await ReadEncryptedBlockUint(BCOtotal, BCATOutbreakCache[i].SpeciesTotalCountLoaded, token).ConfigureAwait(false);
                                BCATOutbreakCache[i].SpeciesTotalCountLoaded = tofs;
                                var (species, sofs) = await ReadEncryptedBlockUint(BCOspecies, BCATOutbreakCache[i].SpeciesLoaded, token).ConfigureAwait(false);
                                BCATOutbreakCache[i].SpeciesLoaded = sofs;
                                var (form, fofs) = await ReadEncryptedBlockByte(BCOform, BCATOutbreakCache[i].SpeciesFormLoaded, token).ConfigureAwait(false);
                                BCATOutbreakCache[i].SpeciesFormLoaded = fofs;
                                var (obpos, bofs) = await ReadEncryptedBlockArray(BCOcenter, BCATOutbreakCache[i].SpeciesCenterPOSLoaded, token).ConfigureAwait(false);
                                BCATOutbreakCache[i].SpeciesCenterPOSLoaded = bofs;

                                PK9 pk = new()
                                {
                                    Species = SpeciesConverter.GetNational9((ushort)species),
                                    Form = form,
                                };
                                CommonEdits.SetIsShiny(pk, false);
                                string pkform = FormOutput(pk.Species, pk.Form, out _);
                                stringsObK.Add($"{Strings.specieslist[pk.Species]}{pkform}");
                                CountlistObK.Add($"{kocount}/{totalcount}");
                                var imgurl = PokeImg(pk, false);
                                PictureBox picture = new();
                                picture.Load(imgurl);
                                ImgObK.Add(picture.Image);
                                monsObK.Add(pk);
                                POSlistObK.Add(obpos);
                                kolistObK.Add(kocount);
                                totallistObK.Add(totalcount);
                            }
                            UpdateProgress(65, 100);
                        }
                        if (MapGroup.SelectedIndex is 0 or 3)
                        {
                            var dataBCB = await ReadEncryptedBlockByte(Blocks.KOutbreakBCDLC2NumActive, CountCacheBCB, token).ConfigureAwait(false);
                            if (CountCacheBCB == 0)
                                CountCacheBCB = dataBCB.Item2;

                            OutbreaktotalBCB = Convert.ToInt32(dataBCB.Item1);
                            if (OutbreaktotalBCB > 10)
                            {
                                BaseBlockKeyPointer = await Executor.SwitchConnection.PointerAll(Offsets.BlockKeyPointer, token).ConfigureAwait(false);
                                // Rerun in case of bad pointer
                                BCATOutbreakCache = [];
                                LoadBCATOutbreakCache();
                                CountCacheBCB = 0;
                                continue;
                            }

                            BCOspecies = Blocks.KOutbreakBC01DLC2Species;
                            BCOko = Blocks.KOutbreakBC01DLC2NumKOed;
                            BCOtotal = Blocks.KOutbreakBC01DLC2TotalSpawns;
                            BCOform = Blocks.KOutbreakBC01DLC2Form;
                            BCOcenter = Blocks.KOutbreakBC01DLC2CenterPos;

                            UpdateProgress(70, 100);
                            for (int i = 20; i < Blocks.OutbreakBCMainMaxCount + Blocks.OutbreakBCDLC1MaxCount + Blocks.OutbreakBCDLC2MaxCount; i++)
                            {
                                switch (i)
                                {
                                    case 20: break;
                                    case 21: BCOspecies = Blocks.KOutbreakBC02DLC2Species; BCOform = Blocks.KOutbreakBC02DLC2Form; BCOko = Blocks.KOutbreakBC02DLC2NumKOed; BCOtotal = Blocks.KOutbreakBC02DLC2TotalSpawns; BCOcenter = Blocks.KOutbreakBC02DLC2CenterPos; break;
                                    case 22: BCOspecies = Blocks.KOutbreakBC03DLC2Species; BCOform = Blocks.KOutbreakBC03DLC2Form; BCOko = Blocks.KOutbreakBC03DLC2NumKOed; BCOtotal = Blocks.KOutbreakBC03DLC2TotalSpawns; BCOcenter = Blocks.KOutbreakBC03DLC2CenterPos; break;
                                    case 23: BCOspecies = Blocks.KOutbreakBC04DLC2Species; BCOform = Blocks.KOutbreakBC04DLC2Form; BCOko = Blocks.KOutbreakBC04DLC2NumKOed; BCOtotal = Blocks.KOutbreakBC04DLC2TotalSpawns; BCOcenter = Blocks.KOutbreakBC04DLC2CenterPos; break;
                                    case 24: BCOspecies = Blocks.KOutbreakBC05DLC2Species; BCOform = Blocks.KOutbreakBC05DLC2Form; BCOko = Blocks.KOutbreakBC05DLC2NumKOed; BCOtotal = Blocks.KOutbreakBC05DLC2TotalSpawns; BCOcenter = Blocks.KOutbreakBC05DLC2CenterPos; break;
                                    case 25: BCOspecies = Blocks.KOutbreakBC06DLC2Species; BCOform = Blocks.KOutbreakBC06DLC2Form; BCOko = Blocks.KOutbreakBC06DLC2NumKOed; BCOtotal = Blocks.KOutbreakBC06DLC2TotalSpawns; BCOcenter = Blocks.KOutbreakBC06DLC2CenterPos; break;
                                    case 26: BCOspecies = Blocks.KOutbreakBC07DLC2Species; BCOform = Blocks.KOutbreakBC07DLC2Form; BCOko = Blocks.KOutbreakBC07DLC2NumKOed; BCOtotal = Blocks.KOutbreakBC07DLC2TotalSpawns; BCOcenter = Blocks.KOutbreakBC07DLC2CenterPos; break;
                                    case 27: BCOspecies = Blocks.KOutbreakBC08DLC2Species; BCOform = Blocks.KOutbreakBC08DLC2Form; BCOko = Blocks.KOutbreakBC08DLC2NumKOed; BCOtotal = Blocks.KOutbreakBC08DLC2TotalSpawns; BCOcenter = Blocks.KOutbreakBC08DLC2CenterPos; break;
                                    case 28: BCOspecies = Blocks.KOutbreakBC09DLC2Species; BCOform = Blocks.KOutbreakBC09DLC2Form; BCOko = Blocks.KOutbreakBC09DLC2NumKOed; BCOtotal = Blocks.KOutbreakBC09DLC2TotalSpawns; BCOcenter = Blocks.KOutbreakBC09DLC2CenterPos; break;
                                    case 29: BCOspecies = Blocks.KOutbreakBC10DLC2Species; BCOform = Blocks.KOutbreakBC10DLC2Form; BCOko = Blocks.KOutbreakBC10DLC2NumKOed; BCOtotal = Blocks.KOutbreakBC10DLC2TotalSpawns; BCOcenter = Blocks.KOutbreakBC10DLC2CenterPos; break;
                                }
                                if (i > OutbreaktotalBCB + 20 - 1)
                                    break;

                                var (kocount, lofs) = await ReadEncryptedBlockUint(BCOko, BCATOutbreakCache[i].SpeciesKOCountLoaded, token).ConfigureAwait(false);
                                BCATOutbreakCache[i].SpeciesKOCountLoaded = lofs;
                                var (totalcount, tofs) = await ReadEncryptedBlockUint(BCOtotal, BCATOutbreakCache[i].SpeciesTotalCountLoaded, token).ConfigureAwait(false);
                                BCATOutbreakCache[i].SpeciesTotalCountLoaded = tofs;
                                var (species, sofs) = await ReadEncryptedBlockUint(BCOspecies, BCATOutbreakCache[i].SpeciesLoaded, token).ConfigureAwait(false);
                                BCATOutbreakCache[i].SpeciesLoaded = sofs;
                                var (form, fofs) = await ReadEncryptedBlockByte(BCOform, BCATOutbreakCache[i].SpeciesFormLoaded, token).ConfigureAwait(false);
                                BCATOutbreakCache[i].SpeciesFormLoaded = fofs;
                                var (obpos, bofs) = await ReadEncryptedBlockArray(BCOcenter, BCATOutbreakCache[i].SpeciesCenterPOSLoaded, token).ConfigureAwait(false);
                                BCATOutbreakCache[i].SpeciesCenterPOSLoaded = bofs;

                                PK9 pk = new()
                                {
                                    Species = SpeciesConverter.GetNational9((ushort)species),
                                    Form = form,
                                };
                                CommonEdits.SetIsShiny(pk, false);
                                string pkform = FormOutput(pk.Species, pk.Form, out _);
                                stringsObB.Add($"{Strings.specieslist[pk.Species]}{pkform}");
                                CountlistObB.Add($"{kocount}/{totalcount}");
                                var imgurl = PokeImg(pk, false);
                                PictureBox picture = new();
                                picture.Load(imgurl);
                                ImgObB.Add(picture.Image);
                                monsObB.Add(pk);
                                POSlistObB.Add(obpos);
                                kolistObB.Add(kocount);
                                totallistObB.Add(totalcount);
                            }
                            UpdateProgress(75, 100);
                        }
                    }
                }


                UpdateProgress(100, 100);
                var textp = ScanForEventOutbreak.Checked ? OutbreaktotalBCP + OutbreaktotalP : OutbreaktotalP;
                var textk = ScanForEventOutbreak.Checked ? OutbreaktotalBCK + OutbreaktotalK : OutbreaktotalK;
                var textb = ScanForEventOutbreak.Checked ? OutbreaktotalBCB + OutbreaktotalB : OutbreaktotalB;
                OpenMapPaldea.Text = $"Paldea: {textp}";
                OpenMapKitakami.Text = $"Kitakami: {textk}";
                OpenMapBlueberry.Text = $"Blueberry: {textb}";

                for (int i = 0; i < OutbreaktotalP; i++)
                {
                    SprP[i].Image = ImgP[i];
                    Plist[i].Text = stringsP[i];
                    CountP[i].Text = CountlistP[i];
                }

                foreach (var pips in ImgP)
                    MapSpritesP.Add(pips);
                foreach (var pps in POSlistP)
                    MapPOSP.Add(pps);
                foreach (var kps in CountlistP)
                    MapCountP.Add(kps);
                foreach (var kps in stringsP)
                    MapStringsP.Add(kps);

                for (int i = 0; i < OutbreaktotalK; i++)
                {
                    SprK[i].Image = ImgK[i];
                    KList[i].Text = stringsK[i];
                    CountK[i].Text = CountlistK[i];
                }

                foreach (var kips in ImgK)
                    MapSpritesK.Add(kips);
                foreach (var kps in POSlistK)
                    MapPOSK.Add(kps);
                foreach (var kps in CountlistK)
                    MapCountK.Add(kps);
                foreach (var kps in stringsK)
                    MapStringsK.Add(kps);

                for (int i = 0; i < OutbreaktotalB; i++)
                {
                    SprB[i].Image = ImgB[i];
                    BList[i].Text = stringsB[i];
                    CountB[i].Text = CountlistB[i];
                }

                foreach (var bips in ImgB)
                    MapSpritesB.Add(bips);
                foreach (var bps in POSlistB)
                    MapPOSB.Add(bps);
                foreach (var bps in CountlistB)
                    MapCountB.Add(bps);
                foreach (var bps in stringsB)
                    MapStringsB.Add(bps);

                if (ScanForEventOutbreak.Checked)
                {
                    for (int i = 0; i < OutbreaktotalBCP; i++)
                    {
                        SprBCP[i].Image = ImgObP[i];
                        BCPlist[i].Text = stringsObP[i];
                        CountBCP[i].Text = CountlistObP[i];
                    }

                    foreach (var pips in ImgObP)
                        MapSpritesObP.Add(pips);
                    foreach (var pps in POSlistObP)
                        MapPOSObP.Add(pps);
                    foreach (var kps in CountlistObP)
                        MapCountObP.Add(kps);
                    foreach (var kps in stringsObP)
                        MapStringsObP.Add(kps);

                    await Task.Delay(0_500, token).ConfigureAwait(false);

                    for (int i = 0; i < OutbreaktotalBCK; i++)
                    {
                        SprBCK[i].Image = ImgObK[i];
                        BCKList[i].Text = stringsObK[i];
                        CountBCK[i].Text = CountlistObK[i];
                    }

                    foreach (var kips in ImgObK)
                        MapSpritesObK.Add(kips);
                    foreach (var kps in POSlistObK)
                        MapPOSObK.Add(kps);
                    foreach (var kps in CountlistObK)
                        MapCountObK.Add(kps);
                    foreach (var kps in stringsObK)
                        MapStringsObK.Add(kps);

                    await Task.Delay(0_500, token).ConfigureAwait(false);

                    for (int i = 0; i < OutbreaktotalBCB; i++)
                    {
                        SprBCB[i].Image = ImgObB[i];
                        BCBList[i].Text = stringsObB[i];
                        CountBCB[i].Text = CountlistObB[i];
                    }

                    foreach (var kips in ImgObB)
                        MapSpritesObB.Add(kips);
                    foreach (var kps in POSlistObB)
                        MapPOSObB.Add(kps);
                    foreach (var bps in CountlistObB)
                        MapCountObB.Add(bps);
                    foreach (var bps in stringsObB)
                        MapStringsObB.Add(bps);
                }

                string msgp = string.Empty;
                string msgk = string.Empty;
                string msgb = string.Empty;
                string msg0p = string.Empty;
                string msg0k = string.Empty;
                string msg0b = string.Empty;
                for (int i = 0; i < OutbreaktotalP; i++)
                {
                    bool hunted = false;
                    var XPos = BitConverter.ToSingle(POSlistP[i].AsSpan()[..4]);
                    var ZPos = BitConverter.ToSingle(POSlistP[i].AsSpan()[8..12]);
                    foreach (var p in pkList)
                    {
                        var XisinRange = p.Item3 - p.Item5 <= XPos && XPos <= p.Item3 + p.Item5;
                        var ZisinRange = p.Item4 - p.Item5 <= ZPos && ZPos <= p.Item4 + p.Item5;
                        if (p.Item1.Species == monsP[i].Species && p.Item1.Form == monsP[i].Form && (!p.Item2 || XisinRange && ZisinRange))
                            hunted = true;
                    }
                    if (hunted is true && OutbreakSearch.Checked)
                    {
                        matchfound = true;
                        msgp += $"{(Species)monsP[i].Species}{FormOutput(monsP[i].Species, monsP[i].Form, out _)} outbreak found!{Environment.NewLine}";
                        string msg = $"{(Species)monsP[i].Species}{FormOutput(monsP[i].Species, monsP[i].Form, out _)} outbreak found!";
                        CenterPosP.Add(POSlistP[i]);
                        var x = BitConverter.ToSingle(CenterPosP.Last().AsSpan()[..4]);
                        var y = BitConverter.ToSingle(CenterPosP.Last().AsSpan()[4..8]);
                        var z = BitConverter.ToSingle(CenterPosP.Last().AsSpan()[8..12]);
                        if (x == 0 || y == 0 || z == 0)
                        {
                            CenterPosP.Remove(CenterPosP.Last());
                            MessageBox.Show($"Invalid coordnates Pokemon {(Species)monsP[i].Species}{FormOutput(monsP[i].Species, monsP[i].Form, out _)}!{Environment.NewLine}xcoord: {x}, ycoord: {y}, zcoord: {z}");
                        }
                        if (EnableWebhook.Checked)
                        {
                            var sprite = PokeImg(monsP[i], false);
                            await SendNotifications(msg, sprite).ConfigureAwait(false);
                        }
                    }
                }

                for (int i = 0; i < OutbreaktotalK; i++)
                {
                    bool hunted = false;
                    var XPos = BitConverter.ToSingle(POSlistK[i].AsSpan()[..4]);
                    var ZPos = BitConverter.ToSingle(POSlistK[i].AsSpan()[8..12]);
                    foreach (var p in pkList)
                    {
                        var XisinRange = p.Item3 - p.Item5 <= XPos && XPos <= p.Item3 + p.Item5;
                        var ZisinRange = p.Item4 - p.Item5 <= ZPos && ZPos <= p.Item4 + p.Item5;
                        if (p.Item1.Species == monsK[i].Species && p.Item1.Form == monsK[i].Form && (!p.Item2 || XisinRange && ZisinRange))
                            hunted = true;
                    }
                    if (hunted is true && OutbreakSearch.Checked)
                    {
                        matchfound = true;
                        msgk += $"{(Species)monsK[i].Species}{FormOutput(monsK[i].Species, monsK[i].Form, out _)} outbreak found!{Environment.NewLine}";
                        string msg = $"{(Species)monsK[i].Species}{FormOutput(monsK[i].Species, monsK[i].Form, out _)} outbreak found!";
                        CenterPosK.Add(POSlistK[i]);
                        var x = BitConverter.ToSingle(CenterPosK.Last().AsSpan()[..4]);
                        var y = BitConverter.ToSingle(CenterPosK.Last().AsSpan()[4..8]);
                        var z = BitConverter.ToSingle(CenterPosK.Last().AsSpan()[8..12]);
                        if (x == 0 || y == 0 || z == 0)
                        {
                            CenterPosK.Remove(CenterPosK.Last());
                            MessageBox.Show($"Invalid coordnates Pokemon {(Species)monsK[i].Species}{FormOutput(monsK[i].Species, monsK[i].Form, out _)}!{Environment.NewLine}xcoord: {x}, ycoord: {y}, zcoord: {z}");
                        }
                        if (EnableWebhook.Checked)
                        {
                            var sprite = PokeImg(monsK[i], false);
                            await SendNotifications(msg, sprite).ConfigureAwait(false);
                        }

                    }
                }
                for (int i = 0; i < OutbreaktotalB; i++)
                {
                    bool hunted = false;
                    var XPos = BitConverter.ToSingle(POSlistB[i].AsSpan()[..4]);
                    var ZPos = BitConverter.ToSingle(POSlistB[i].AsSpan()[8..12]);
                    foreach (var p in pkList)
                    {
                        var XisinRange = p.Item3 - p.Item5 <= XPos && XPos <= p.Item3 + p.Item5;
                        var ZisinRange = p.Item4 - p.Item5 <= ZPos && ZPos <= p.Item4 + p.Item5;
                        if (p.Item1.Species == monsB[i].Species && p.Item1.Form == monsB[i].Form && (!p.Item2 || XisinRange && ZisinRange))
                            hunted = true;
                    }
                    if (hunted is true && OutbreakSearch.Checked)
                    {
                        matchfound = true;
                        msgb += $"{(Species)monsB[i].Species}{FormOutput(monsB[i].Species, monsB[i].Form, out _)} outbreak found!{Environment.NewLine}";
                        string msg = $"{(Species)monsB[i].Species}{FormOutput(monsB[i].Species, monsB[i].Form, out _)} outbreak found!";
                        CenterPosB.Add(POSlistB[i]);
                        var x = BitConverter.ToSingle(CenterPosB.Last().AsSpan()[..4]);
                        var y = BitConverter.ToSingle(CenterPosB.Last().AsSpan()[4..8]);
                        var z = BitConverter.ToSingle(CenterPosB.Last().AsSpan()[8..12]);
                        if (x == 0 || y == 0 || z == 0)
                        {
                            CenterPosB.Remove(CenterPosB.Last());
                            MessageBox.Show($"Invalid coordnates Pokemon {(Species)monsB[i].Species}{FormOutput(monsB[i].Species, monsB[i].Form, out _)}!{Environment.NewLine}xcoord: {x}, ycoord: {y}, zcoord: {z}");
                        }
                        if (EnableWebhook.Checked)
                        {
                            var sprite = PokeImg(monsB[i], false);
                            await SendNotifications(msg, sprite).ConfigureAwait(false);
                        }
                    }
                }
                for (int i = 0; i < OutbreaktotalBCP; i++)
                {
                    bool hunted = false;
                    var XPos = BitConverter.ToSingle(POSlistObP[i].AsSpan()[..4]);
                    var ZPos = BitConverter.ToSingle(POSlistObP[i].AsSpan()[8..12]);
                    foreach (var p in pkList)
                    {
                        var XisinRange = p.Item3 - p.Item5 <= XPos && XPos <= p.Item3 + p.Item5;
                        var ZisinRange = p.Item4 - p.Item5 <= ZPos && ZPos <= p.Item4 + p.Item5;
                        if (p.Item1.Species == monsObP[i].Species && p.Item1.Form == monsObP[i].Form && (!p.Item2 || XisinRange && ZisinRange))
                            hunted = true;
                    }
                    if (hunted is true && OutbreakSearch.Checked)
                    {
                        matchfound = true;
                        string msg = $"{(Species)monsObP[i].Species}{FormOutput(monsObP[i].Species, monsObP[i].Form, out _)} outbreak found!";
                        msg0b += $"{(Species)monsObP[i].Species}{FormOutput(monsObP[i].Species, monsObP[i].Form, out _)} outbreak found!{Environment.NewLine}";

                        CenterPos0bP.Add(POSlistObP[i]);
                        var x = BitConverter.ToSingle(CenterPos0bP.Last().AsSpan()[..4]);
                        var y = BitConverter.ToSingle(CenterPos0bP.Last().AsSpan()[4..8]);
                        var z = BitConverter.ToSingle(CenterPos0bP.Last().AsSpan()[8..12]);
                        if (x == 0 || y == 0 || z == 0)
                        {
                            CenterPos0bP.Remove(CenterPos0bP.Last());
                            MessageBox.Show($"Invalid coordnates Pokemon {(Species)monsObP[i].Species}{FormOutput(monsObP[i].Species, monsObP[i].Form, out _)}!{Environment.NewLine}xcoord: {x}, ycoord: {y}, zcoord: {z}");
                        }
                        if (EnableWebhook.Checked)
                        {
                            var sprite = PokeImg(monsObP[i], false);
                            await SendNotifications(msg, sprite).ConfigureAwait(false);
                        }
                    }
                }

                for (int i = 0; i < OutbreaktotalBCK; i++)
                {
                    bool hunted = false;
                    var XPos = BitConverter.ToSingle(POSlistObK[i].AsSpan()[..4]);
                    var ZPos = BitConverter.ToSingle(POSlistObK[i].AsSpan()[8..12]);
                    foreach (var p in pkList)
                    {
                        var XisinRange = p.Item3 - p.Item5 <= XPos && XPos <= p.Item3 + p.Item5;
                        var ZisinRange = p.Item4 - p.Item5 <= ZPos && ZPos <= p.Item4 + p.Item5;
                        if (p.Item1.Species == monsObK[i].Species && p.Item1.Form == monsObK[i].Form && (!p.Item2 || XisinRange && ZisinRange))
                            hunted = true;

                    }
                    if (hunted is true && OutbreakSearch.Checked)
                    {
                        matchfound = true;
                        string msg = $"{(Species)monsObK[i].Species}{FormOutput(monsObK[i].Species, monsObK[i].Form, out _)} outbreak found!";
                        msg0k += $"{(Species)monsObK[i].Species}{FormOutput(monsObK[i].Species, monsObK[i].Form, out _)}  outbreak found!{Environment.NewLine}";

                        CenterPos0bK.Add(POSlistObK[i]);
                        var x = BitConverter.ToSingle(CenterPos0bK.Last().AsSpan()[..4]);
                        var y = BitConverter.ToSingle(CenterPos0bK.Last().AsSpan()[4..8]);
                        var z = BitConverter.ToSingle(CenterPos0bK.Last().AsSpan()[8..12]);
                        if (x == 0 || y == 0 || z == 0)
                        {
                            CenterPos0bK.Remove(CenterPos0bK.Last());
                            MessageBox.Show($"Invalid coordnates Pokemon {(Species)monsObK[i].Species}{FormOutput(monsObK[i].Species, monsObK[i].Form, out _)}!{Environment.NewLine}xcoord: {x}, ycoord: {y}, zcoord: {z}");
                        }

                        if (EnableWebhook.Checked)
                        {
                            var sprite = PokeImg(monsObK[i], false);
                            await SendNotifications(msg, sprite).ConfigureAwait(false);
                        }
                    }
                }
                for (int i = 0; i < OutbreaktotalBCB; i++)
                {
                    bool hunted = false;
                    var XPos = BitConverter.ToSingle(POSlistObB[i].AsSpan()[..4]);
                    var ZPos = BitConverter.ToSingle(POSlistObB[i].AsSpan()[8..12]);
                    foreach (var p in pkList)
                    {
                        var XisinRange = p.Item3 - p.Item5 <= XPos && XPos <= p.Item3 + p.Item5;
                        var ZisinRange = p.Item4 - p.Item5 <= ZPos && ZPos <= p.Item4 + p.Item5;
                        if (p.Item1.Species == monsObB[i].Species && p.Item1.Form == monsObB[i].Form && (!p.Item2 || XisinRange && ZisinRange))
                            hunted = true;
                    }
                    if (hunted is true && OutbreakSearch.Checked)
                    {
                        matchfound = true;
                        string msg = $"{(Species)monsObB[i].Species}{FormOutput(monsObB[i].Species, monsObB[i].Form, out _)} outbreak found!";
                        msg0b += $"{(Species)monsObB[i].Species}{FormOutput(monsObB[i].Species, monsObB[i].Form, out _)}  outbreak found!{Environment.NewLine}";

                        CenterPos0bB.Add(POSlistObB[i]);
                        var x = BitConverter.ToSingle(CenterPos0bB.Last().AsSpan()[..4]);
                        var y = BitConverter.ToSingle(CenterPos0bB.Last().AsSpan()[4..8]);
                        var z = BitConverter.ToSingle(CenterPos0bB.Last().AsSpan()[8..12]);
                        if (x == 0 || y == 0 || z == 0)
                        {
                            CenterPos0bB.Remove(CenterPos0bB.Last());
                            MessageBox.Show($"Invalid coordnates Pokemon {(Species)monsObB[i].Species}{FormOutput(monsObB[i].Species, monsObB[i].Form, out _)}!{Environment.NewLine}xcoord: {x}, ycoord: {y}, zcoord: {z}");
                        }

                        if (EnableWebhook.Checked)
                        {
                            var sprite = PokeImg(monsObB[i], false);
                            await SendNotifications(msg, sprite).ConfigureAwait(false);
                        }
                    }
                }

                if (matchfound)
                {
                    timer.Stop();
                    if (Apply0To64.Checked)
                    {
                        StatusLabel.Text = "Status: 0 -> 64...";
                        await ResetGame(token, true).ConfigureAwait(false);
                    }
                    StatusLabel.Text = "Status:";
                    await Click(HOME, 1_000, token).ConfigureAwait(false);
                    CollideButton.Enabled = true;
                    if (ScanForEventOutbreak.Checked)
                        Teleportindex.Maximum = CenterPos0bB.Count + CenterPos0bK.Count + CenterPos0bP.Count - 1 < (int)Teleportindex.Minimum ? (int)Teleportindex.Minimum : CenterPos0bB.Count + CenterPos0bK.Count + CenterPos0bP.Count - 1;
                    else
                        Teleportindex.Maximum = CenterPosB.Count + CenterPosK.Count + CenterPosP.Count - 1 < (int)Teleportindex.Minimum ? (int)Teleportindex.Minimum : CenterPosB.Count + CenterPosK.Count + CenterPosP.Count - 1;
                    EnableAssets();
                    if (!EnableWebhook.Checked)
                        MessageBox.Show(msgp + msgk + msgb + msg0p + msg0k + msg0b);
                    return;
                }

                if (HardStopOutbreak.Checked)
                {
                    MessageBox.Show("HardStop enabled, ending task. Uncheck if you wish to scan until match is found.");
                    {
                        EnableAssets();
                        return;
                    }
                }
                else if (!OutbreakSearch.Checked)
                    break;

                ImgP = [];
                ImgK = [];
                ImgB = [];
                ImgObK = [];
                ImgObP = [];
                ImgObB = [];
                POSlistP = [];
                POSlistK = [];
                POSlistB = [];
                POSlistObK = [];
                POSlistObP = [];
                POSlistObB = [];
                kolistP = [];
                kolistObP = [];
                totallistP = [];
                totallistObP = [];
                stringsP = [];
                stringsObP = [];
                monsP = [];
                monsObP = [];
                monsK = [];
                monsObK = [];
                kolistK = [];
                kolistObK = [];
                totallistK = [];
                totallistObK = [];
                stringsK = [];
                stringsObK = [];
                monsB = [];
                monsObB = [];
                kolistB = [];
                kolistObB = [];
                totallistB = [];
                totallistObB = [];
                stringsB = [];
                stringsObB = [];
                CountlistP = [];
                CountlistK = [];
                CountlistB = [];
                CountlistObP = [];
                CountlistObK = [];
                CountlistObB = [];

                MapSpritesP = [];
                MapPOSP = [];
                MapCountP = [];
                MapStringsP = [];
                MapSpritesK = [];
                MapPOSK = [];
                MapCountK = [];
                MapStringsK = [];
                MapSpritesB = [];
                MapPOSB = [];
                MapCountB = [];
                MapStringsB = [];
                MapSpritesObP = [];
                MapPOSObP = [];
                MapCountObP = [];
                MapStringsObP = [];
                MapSpritesObK = [];
                MapPOSObK = [];
                MapCountObK = [];
                MapStringsObK = [];
                MapSpritesObB = [];
                MapPOSObB = [];
                MapCountObB = [];
                MapStringsObB = [];

                DaySkipTotal.Text = $"Day Skips: {dayskip}";

                if (OutbreakSearch.Checked)
                {
                    StatusLabel.Text = "Status: Skipping...";
skip:
                    if (WildCheck.Checked)
                        await DefeatPokemon(token).ConfigureAwait(false);
                    dayseed_old = BitConverter.ToUInt64(await Executor.SwitchConnection.ReadBytesAbsoluteAsync(DayseedOffset, 8, token).ConfigureAwait(false));
                    await DaySkipFaster(token).ConfigureAwait(false);
                    await Task.Delay(3_000, token).ConfigureAwait(false);
                    await ResetTime(token).ConfigureAwait(false);
                    await Task.Delay(3_000, token).ConfigureAwait(false);
                    dayseed = BitConverter.ToUInt64(await Executor.SwitchConnection.ReadBytesAbsoluteAsync(DayseedOffset, 8, token).ConfigureAwait(false));
                    if (dayseed == dayseed_old)
                    {
                        StatusLabel.Text = "DaySkip Failed. Status: Skipping...";
                        goto skip;
                    }
                }
            }
            EnableAssets();
            timer.Stop();
        }
        private async Task<bool> PlayerNotOnMount(CancellationToken token)
        {
            var Data = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(PlayerOnMountOffset, 1, token).ConfigureAwait(false);
            return Data[0] == 0x00; // 0 nope else yes
        }
        private async Task Collide(CancellationToken token)
        {
            await ParseMountPointer(token).ConfigureAwait(false);
            var checkcount = 0;
            if (await PlayerNotOnMount(token).ConfigureAwait(false))
                await Click(PLUS, 0_800, token).ConfigureAwait(false);
            while (await PlayerNotOnMount(token).ConfigureAwait(false))
            {
                await Click(PLUS, 0_800, token).ConfigureAwait(false);
                checkcount++;
                if (!await PlayerNotOnMount(token).ConfigureAwait(false))
                    break;
                if (checkcount >= 2)
                    await Click(B, 0_500, token).ConfigureAwait(false);
            }

        }
        private async Task DisCollide(CancellationToken token)
        {
            var checkcount = 0;
            if (!await PlayerNotOnMount(token).ConfigureAwait(false))
                await Click(PLUS, 0_800, token).ConfigureAwait(false);
            while (!await PlayerNotOnMount(token).ConfigureAwait(false))
            {
                await Click(PLUS, 0_800, token).ConfigureAwait(false);
                if (await PlayerNotOnMount(token).ConfigureAwait(false))
                    break;
                checkcount++;
                if (checkcount >= 2)
                    break;
            }

        }
        private async Task ParseBlockKeyPointer(bool reset, CancellationToken token)
        {
            BaseBlockKeyPointer = await Executor.SwitchConnection.PointerAll(Offsets.BlockKeyPointer, token).ConfigureAwait(false);
            while (BaseBlockKeyPointer == 0)
            {
                await Task.Delay(reset ? 0_500 : 0_050).ConfigureAwait(false);
                BaseBlockKeyPointer = await Executor.SwitchConnection.PointerAll(Offsets.BlockKeyPointer, token).ConfigureAwait(false);
            }
        }
        private async Task ParseMountPointer(CancellationToken token)
        {
            PlayerOnMountOffset = await Executor.SwitchConnection.PointerAll(Offsets.PlayerOnMountPointer, token).ConfigureAwait(false);
            while (PlayerOnMountOffset == 0)
            {
                await Task.Delay(0_050).ConfigureAwait(false);
                PlayerOnMountOffset = await Executor.SwitchConnection.PointerAll(Offsets.PlayerOnMountPointer, token).ConfigureAwait(false);
            }
        }
        private async Task<ulong> ParseDayseedPointer(CancellationToken token)
        {
            ulong DayseedOfs = await Executor.SwitchConnection.PointerAll(Offsets.RaidBlockPointerP, token).ConfigureAwait(false);
            while (DayseedOfs == 0)
            {
                await Task.Delay(0_050).ConfigureAwait(false);
                DayseedOfs = await Executor.SwitchConnection.PointerAll(Offsets.RaidBlockPointerP, token).ConfigureAwait(false);
            }
            return DayseedOfs;
        }
        private async Task CollideRead(CancellationToken token)
        {
            await Collide(token).ConfigureAwait(false);
            var coords = await Executor.SwitchConnection.PointerPeek(12, Offsets.CollisionPointer, token).ConfigureAwait(false);
            XCoordValue.Text = $"{BitConverter.ToSingle(coords.AsSpan()[..4])}";
            YCoordValue.Text = $"{BitConverter.ToSingle(coords.AsSpan()[4..8])}";
            ZCoordValue.Text = $"{BitConverter.ToSingle(coords.AsSpan()[8..12])}";
        }
        private void ReadCoordButton_Click(object sender, EventArgs e)
        {
            Task.Run(async () => await CollideRead(CancellationToken.None).ConfigureAwait(false));
        }
        private void RaidButton_Click(object sender, EventArgs e)
        {
            var colors = MainViewer.CheckForColors(Settings.Default.DarkMode);
            RaidCodeEntry RaidForm = new(Executor, colors);
            RaidForm.ShowDialog();
        }
        private async void CollideButton_Click(object sender, EventArgs e)
        {
            DisableAssets();
            CollideButton.Enabled = false;
            bool kitakami = false;
            bool blueberry = false;
            int index = 0;
            await Collide(CancellationToken.None).ConfigureAwait(false);
            if (ScanForEventOutbreak.Checked)
            {
                blueberry = (int)Teleportindex.Value >= CenterPos0bP.Count + CenterPos0bK.Count;
                kitakami = (int)Teleportindex.Value >= CenterPos0bP.Count && !blueberry;
                index = blueberry ? (int)Teleportindex.Value - CenterPos0bK.Count - CenterPos0bP.Count : kitakami ? (int)Teleportindex.Value - CenterPos0bP.Count : (int)Teleportindex.Value;
                if (blueberry)
                {
                    if (CenterPos0bB[index] != null)
                    {
                        var (x, y, z) = CoordChanger(CenterPos0bB[index]!);
                        await CollideToCave(x, y, z, PlayerOnMountOffset, CancellationToken.None).ConfigureAwait(false);
                        return;
                    }

                }
                else if (kitakami)
                {
                    if (CenterPos0bK[index] != null)
                    {
                        var (x, y, z) = CoordChanger(CenterPos0bK[index]!);
                        await CollideToCave(x, y, z, PlayerOnMountOffset, CancellationToken.None).ConfigureAwait(false);
                        return;
                    }
                }
                else
                {
                    if (CenterPos0bP[index] != null)
                    {
                        var (x, y, z) = CoordChanger(CenterPos0bP[index]!);
                        await CollideToCave(x, y, z, PlayerOnMountOffset, CancellationToken.None).ConfigureAwait(false);
                        return;
                    }
                }
            }
            else
            {
                blueberry = (int)Teleportindex.Value >= CenterPosP.Count + CenterPosK.Count;
                kitakami = (int)Teleportindex.Value >= CenterPosP.Count && !blueberry;
                index = blueberry ? (int)Teleportindex.Value - CenterPosK.Count - CenterPosP.Count : kitakami ? (int)Teleportindex.Value - CenterPosP.Count : (int)Teleportindex.Value;
                if (blueberry)
                {
                    if (CenterPosB[index] != null)
                    {
                        var (x, y, z) = CoordChanger(CenterPosB[index]!);
                        await CollideToCave(x, y, z, PlayerOnMountOffset, CancellationToken.None).ConfigureAwait(false);
                        return;
                    }

                }
                else if (kitakami)
                {
                    if (CenterPosK[index] != null)
                    {
                        var (x, y, z) = CoordChanger(CenterPosK[index]!);
                        await CollideToCave(x, y, z, PlayerOnMountOffset, CancellationToken.None).ConfigureAwait(false);
                        return;
                    }
                }
                else
                {
                    if (CenterPosP[index] != null)
                    {
                        var (x, y, z) = CoordChanger(CenterPosP[index]!);
                        await CollideToCave(x, y, z, PlayerOnMountOffset, CancellationToken.None).ConfigureAwait(false);
                        return;
                    }
                }
            }
            MessageBox.Show("No valid coordinates present. Try again after finding a desired outbreak.");
            CollideButton.Enabled = false;
            EnableAssets();

        }
        public (string, string, string) CoordChanger(byte[] Coords)
        {
            var x = $"{BitConverter.ToSingle(Coords.AsSpan()[..4])}";
            var y = $"{BitConverter.ToSingle(Coords.AsSpan()[4..8])}";
            var z = $"{BitConverter.ToSingle(Coords.AsSpan()[8..12])}";

            return (x, y, z);
        }
        public async Task CollideToCave(string xcoord, string ycoord, string zcoord, ulong PlayerMountOffset, CancellationToken token)
        {
            float coordx = Single.Parse(xcoord, NumberStyles.Float);
            byte[] X1 = BitConverter.GetBytes(coordx);
            float coordy = Single.Parse(ycoord, NumberStyles.Float);
            byte[] Y1 = BitConverter.GetBytes(coordy);
            float coordz = Single.Parse(zcoord, NumberStyles.Float);
            byte[] Z1 = BitConverter.GetBytes(coordz);

            X1 = X1.Concat(Y1).Concat(Z1).ToArray();
            float y = BitConverter.ToSingle(X1, 4);
            y += 120;
            WriteSingleLittleEndian(X1.AsSpan()[4..], y);

            for (int i = 0; i < 15; i++)
                await Executor.SwitchConnection.PointerPoke(X1, Offsets.CollisionPointer, token).ConfigureAwait(false);
            await Task.Delay(3_000, token).ConfigureAwait(false);
            await Click(B, 19_000, token).ConfigureAwait(false);

            await Task.Delay(5_000, token).ConfigureAwait(false);
            await DisCollide(token).ConfigureAwait(false);
            await Executor.SetStick(LEFT, 0, 10000, 0_050, token).ConfigureAwait(false);
            await Executor.SetStick(LEFT, 0, 0, 0_500, token).ConfigureAwait(false);
            CollideButton.Enabled = true;
            EnableAssets();
            MessageBox.Show("Completed!");

        }

        private void UpdateProgress(int currProgress, int maxProgress)
        {
            var value = (100 * currProgress) / maxProgress;
            if (progressBar1.InvokeRequired)
                progressBar1.Invoke(() => progressBar1.Value = value);
            else
                progressBar1.Value = value;
        }
        private async Task ResetGame(CancellationToken token, bool knockout)
        {
            await CloseGame(token).ConfigureAwait(false);
            await StartGameScreen(token).ConfigureAwait(false);
            CountCacheP = 0;
            CountCacheK = 0;
            CountCacheB = 0;
            CountCacheBCP = 0;
            CountCacheBCK = 0;
            CountCacheBCB = 0;
            OutbreakCache = [];
            BCATOutbreakCache = [];
            EventOutbreakActive = 0;
            LoadOutbreakCache();
            LoadBCATOutbreakCache();
            if (knockout is true)
                await KOToSeventy_Click(token).ConfigureAwait(false);
            while (!await IsOnOverworldTitle(token).ConfigureAwait(false))
                await Click(A, 6_000, token).ConfigureAwait(false);
        }

        public async Task CloseGame(CancellationToken token)
        {
            // Close out of the game
            await Click(B, 0_500, token).ConfigureAwait(false);
            await Click(HOME, 2_000, token).ConfigureAwait(false);
            await Click(X, 1_000, token).ConfigureAwait(false);
            await Click(A, 5_000, token).ConfigureAwait(false);
        }

        public async Task StartGameScreen(CancellationToken token)
        {
            // Open game.
            await Click(A, 1_000, token).ConfigureAwait(false);

            // Menus here can go in the order: Update Prompt -> Profile -> DLC check -> Unable to use DLC.
            //  The user can optionally turn on the setting if they know of a breaking system update incoming.
            await Task.Delay(1_000, token).ConfigureAwait(false);
            await Click(DUP, 0_600, token).ConfigureAwait(false);
            await Click(A, 1_000, token).ConfigureAwait(false);

            await Click(DUP, 0_600, token).ConfigureAwait(false);
            await Click(A, 0_600, token).ConfigureAwait(false);


            // Switch Logo and game load screen
            await Task.Delay(19_000, token).ConfigureAwait(false);
            while (!await IsOnTitleScreen(token).ConfigureAwait(false))
                await Task.Delay(2_000).ConfigureAwait(false);
        }
        private async Task<bool> IsOnTitleScreen(CancellationToken token)
        {
            var offset = await Executor.SwitchConnection.PointerAll(Offsets.BlockKeyPointer, token).ConfigureAwait(false);
            if (offset == 0)
                return false;
            if (!await IsInTitle(token).ConfigureAwait(false))
                return false;
            return true;
        }
        private async Task<bool> IsInTitle(CancellationToken token)
        {
            var data = await Executor.SwitchConnection.ReadBytesMainAsync(Offsets.PicnicMenu, 1, token).ConfigureAwait(false);
            return data[0] == 0x01; // 1 when in picnic, 2 in sandwich menu, 3 when eating, 2 when done eating
        }

        private async Task KOToSeventy_Click(CancellationToken token)
        {
            OutbreakScan.Enabled = false;
            await ParseBlockKeyPointer(true, token).ConfigureAwait(false);

            var (validOutbreaksP, _) = await ReadEncryptedBlockByte(Blocks.KMassOutbreakTotalPaldea, 0, token).ConfigureAwait(false);
            var (validOutbreaksK, _) = await ReadEncryptedBlockByte(Blocks.KMassOutbreakTotalKitakami, 0, token).ConfigureAwait(false);
            var (validOutbreaksB, _) = await ReadEncryptedBlockByte(Blocks.KMassOutbreakTotalBlueberry, 0, token).ConfigureAwait(false);
            var (validOutbreaksBCP, _) = await ReadEncryptedBlockByte(Blocks.KOutbreakBCMainNumActive, 0, token).ConfigureAwait(false);
            var (validOutbreaksBCK, _) = await ReadEncryptedBlockByte(Blocks.KOutbreakBCDLC1NumActive, 0, token).ConfigureAwait(false);
            var (validOutbreaksBCB, _) = await ReadEncryptedBlockByte(Blocks.KOutbreakBCDLC2NumActive, 0, token).ConfigureAwait(false);

            var OutbreaktotalP = Convert.ToInt32(validOutbreaksP);
            var OutbreaktotalK = Convert.ToInt32(validOutbreaksK);
            var OutbreaktotalB = Convert.ToInt32(validOutbreaksB);
            var OutbreaktotalBCP = Convert.ToInt32(validOutbreaksBCP);
            var OutbreaktotalBCK = Convert.ToInt32(validOutbreaksBCK);
            var OutbreaktotalBCB = Convert.ToInt32(validOutbreaksBCB);
            var koblock = Blocks.KMassOutbreakKO1;
            if (MapGroup.SelectedIndex is 0 or 1)
            {
                for (int i = 0; i < Blocks.OutbreakMainMaxCount; i++)
                {
                    StatusLabel.Text = $"KOing Paldea ({i + 1}/{OutbreaktotalP}): {100.00 * (i + 1) / OutbreaktotalP:0.00}%";
                    switch (i)
                    {
                        case 0: break;
                        case 1: koblock = Blocks.KMassOutbreakKO2; break;
                        case 2: koblock = Blocks.KMassOutbreakKO3; break;
                        case 3: koblock = Blocks.KMassOutbreakKO4; break;
                        case 4: koblock = Blocks.KMassOutbreakKO5; break;
                        case 5: koblock = Blocks.KMassOutbreakKO6; break;
                        case 6: koblock = Blocks.KMassOutbreakKO7; break;
                        case 7: koblock = Blocks.KMassOutbreakKO8; break;
                    }
                    if (i > OutbreaktotalP - 1)
                        break;
                    var (currentcount, _) = await ReadEncryptedBlockInt32(koblock, 0, token).ConfigureAwait(false);
                    uint inj = 64;
                    await WriteBlock(inj, koblock, token, (uint)currentcount).ConfigureAwait(false);
                }
            }
            if (MapGroup.SelectedIndex is 0 or 2)
            {
                for (int i = 0; i < Blocks.OutbreakDLC1MaxCount; i++)
                {
                    StatusLabel.Text = $"KOing Kitakami ({i + 1}/{OutbreaktotalK}): {100.00 * (i + 1) / OutbreaktotalK:0.00}%";
                    switch (i)
                    {
                        case 0: koblock = Blocks.KMassOutbreakKO9; break;
                        case 1: koblock = Blocks.KMassOutbreakKO10; break;
                        case 2: koblock = Blocks.KMassOutbreakKO11; break;
                        case 3: koblock = Blocks.KMassOutbreakKO12; break;
                    }
                    if (i > OutbreaktotalK - 1)
                        break;
                    var (currentcount, _) = await ReadEncryptedBlockInt32(koblock, 0, token).ConfigureAwait(false);
                    uint inj = 64;
                    await WriteBlock(inj, koblock, token, (uint)currentcount).ConfigureAwait(false);
                }
            }
            if (MapGroup.SelectedIndex is 0 or 3)
            {
                for (int i = 0; i < Blocks.OutbreakDLC2MaxCount; i++)
                {
                    StatusLabel.Text = $"KOing Blueberry ({i + 1}/{OutbreaktotalB}): {100.00 * (i + 1) / OutbreaktotalB:0.00}%";
                    switch (i)
                    {
                        case 0: koblock = Blocks.KMassOutbreakKO13; break;
                        case 1: koblock = Blocks.KMassOutbreakKO14; break;
                        case 2: koblock = Blocks.KMassOutbreakKO15; break;
                        case 3: koblock = Blocks.KMassOutbreakKO16; break;
                        case 4: koblock = Blocks.KMassOutbreakKO17; break;
                    }
                    if (i > OutbreaktotalB - 1)
                        break;
                    var (currentcount, _) = await ReadEncryptedBlockInt32(koblock, 0, token).ConfigureAwait(false);
                    uint inj = 64;
                    await WriteBlock(inj, koblock, token, (uint)currentcount).ConfigureAwait(false);
                }
            }
            if (MapGroup.SelectedIndex is 0 or 1 && ScanForEventOutbreak.Checked)
            {
                for (int i = 0; i < Blocks.OutbreakBCMainMaxCount; i++)
                {
                    StatusLabel.Text = $"KOing PaldeaEvent ({i + 1}/{OutbreaktotalBCP}): {100.00 * (i + 1) / OutbreaktotalBCP:0.00}%";
                    switch (i)
                    {
                        case 0: koblock = Blocks.KOutbreakBC01MainNumKOed; break;
                        case 1: koblock = Blocks.KOutbreakBC02MainNumKOed; break;
                        case 2: koblock = Blocks.KOutbreakBC03MainNumKOed; break;
                        case 3: koblock = Blocks.KOutbreakBC04MainNumKOed; break;
                        case 4: koblock = Blocks.KOutbreakBC05MainNumKOed; break;
                        case 5: koblock = Blocks.KOutbreakBC06MainNumKOed; break;
                        case 6: koblock = Blocks.KOutbreakBC07MainNumKOed; break;
                        case 7: koblock = Blocks.KOutbreakBC08MainNumKOed; break;
                        case 8: koblock = Blocks.KOutbreakBC09MainNumKOed; break;
                        case 9: koblock = Blocks.KOutbreakBC10MainNumKOed; break;
                    }
                    if (i > OutbreaktotalBCP - 1)
                        break;
                    var (currentcount, _) = await ReadEncryptedBlockInt32(koblock, 0, token).ConfigureAwait(false);
                    uint inj = 64;
                    await WriteBlock(inj, koblock, token, (uint)currentcount).ConfigureAwait(false);
                }
            }
            if (MapGroup.SelectedIndex is 0 or 2 && ScanForEventOutbreak.Checked)
            {
                for (int i = 0; i < Blocks.OutbreakBCDLC1MaxCount; i++)
                {
                    StatusLabel.Text = $"KOing KitakamiEvent ({i + 1}/{OutbreaktotalBCK}): {100.00 * (i + 1) / OutbreaktotalBCK:0.00}%";
                    switch (i)
                    {
                        case 0: koblock = Blocks.KOutbreakBC01DLC1NumKOed; break;
                        case 1: koblock = Blocks.KOutbreakBC02DLC1NumKOed; break;
                        case 2: koblock = Blocks.KOutbreakBC03DLC1NumKOed; break;
                        case 3: koblock = Blocks.KOutbreakBC04DLC1NumKOed; break;
                        case 4: koblock = Blocks.KOutbreakBC05DLC1NumKOed; break;
                        case 5: koblock = Blocks.KOutbreakBC06DLC1NumKOed; break;
                        case 6: koblock = Blocks.KOutbreakBC07DLC1NumKOed; break;
                        case 7: koblock = Blocks.KOutbreakBC08DLC1NumKOed; break;
                        case 8: koblock = Blocks.KOutbreakBC09DLC1NumKOed; break;
                        case 9: koblock = Blocks.KOutbreakBC10DLC1NumKOed; break;
                    }
                    if (i > OutbreaktotalBCK - 1)
                        break;
                    var (currentcount, _) = await ReadEncryptedBlockInt32(koblock, 0, token).ConfigureAwait(false);
                    uint inj = 64;
                    await WriteBlock(inj, koblock, token, (uint)currentcount).ConfigureAwait(false);
                }
            }
            if (MapGroup.SelectedIndex is 0 or 3 && ScanForEventOutbreak.Checked)
            {
                for (int i = 0; i < 10; i++)
                {
                    StatusLabel.Text = $"KOing BlueEvent ({i + 1}/{OutbreaktotalBCB}): {100.00 * (i + 1) / OutbreaktotalBCB:0.00}%";
                    switch (i)
                    {
                        case 0: koblock = Blocks.KOutbreakBC01DLC2NumKOed; break;
                        case 1: koblock = Blocks.KOutbreakBC02DLC2NumKOed; break;
                        case 2: koblock = Blocks.KOutbreakBC03DLC2NumKOed; break;
                        case 3: koblock = Blocks.KOutbreakBC04DLC2NumKOed; break;
                        case 4: koblock = Blocks.KOutbreakBC05DLC2NumKOed; break;
                        case 5: koblock = Blocks.KOutbreakBC06DLC2NumKOed; break;
                        case 6: koblock = Blocks.KOutbreakBC07DLC2NumKOed; break;
                        case 7: koblock = Blocks.KOutbreakBC08DLC2NumKOed; break;
                        case 8: koblock = Blocks.KOutbreakBC09DLC2NumKOed; break;
                        case 9: koblock = Blocks.KOutbreakBC10DLC2NumKOed; break;
                    }
                    if (i > OutbreaktotalBCB - 1)
                        break;
                    var (currentcount, _) = await ReadEncryptedBlockInt32(koblock, 0, token).ConfigureAwait(false);
                    uint inj = 64;
                    await WriteBlock(inj, koblock, token, (uint)currentcount).ConfigureAwait(false);
                }
            }
            StatusLabel.Text = "Status:";
            OutbreakScan.Enabled = true;
        }
        private async Task<bool> IsOnOverworldTitle(CancellationToken token)
        {
            var offset = await Executor.SwitchConnection.PointerAll(Offsets.OverworldPointer, token).ConfigureAwait(false);
            if (offset == 0)
                return false;
            return await IsOnOverworld(offset, token).ConfigureAwait(false);
        }
        private async Task<bool> IsOnOverworld(ulong offset, CancellationToken token)
        {
            var data = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(offset, 1, token).ConfigureAwait(false);
            return data[0] == 0x11;
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Executor.SwitchConnection.Connected ||!Executor.Connection.Connected)
                {
                    Executor.SwitchConnection.Reset();
                    if (!Executor.SwitchConnection.Connected)
                        throw new Exception("SwitchConnection can't reconnect!");
                }
                StartTime = DateTime.Now;
                UptimeOnLoad(sender, e);
                await SearchForOutbreak(CancellationToken.None).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                if(Executor.SwitchConnection.Connected)
                {
                    try
                    {
                        Executor.SwitchConnection.Disconnect();
                    }
                    catch (SocketException soketEx)
                    {
                        MessageBox.Show(this, soketEx.ToString(), "Sokect Connetion Exception!");
                    }
                }
                MessageBox.Show(this, ex.ToString(), "Exception!");
                if (!OutbreakScan.Enabled)
                    EnableAssets();
            }
        }

        private void UptimeOnLoad(object sender, EventArgs e)
        {
            timer = new System.Timers.Timer { Interval = 1000 };
            timer.Elapsed += (o, args) =>
            {
                UptimeLabel.Text = $"Uptime: {StartTime - DateTime.Now:d\\.hh\\:mm\\:ss}";
            };
            timer.Start();
        }

        public class OutbreakStash
        {
            public ulong SpeciesLoaded { get; set; } = 0;
            public ulong SpeciesFormLoaded { get; set; } = 0;
            public ulong SpeciesTotalCountLoaded { get; set; } = 0;
            public ulong SpeciesKOCountLoaded { get; set; } = 0;
            public ulong SpeciesCenterPOSLoaded { get; set; } = 0;
        }



        public new async Task Click(SwitchButton b, int delay, CancellationToken token)
        {
            await Executor.SwitchConnection.SendAsync(SwitchCommand.Click(b, true), token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }

        public async Task PressAndHold(SwitchButton b, int hold, int delay, CancellationToken token)
        {
            await Executor.SwitchConnection.SendAsync(SwitchCommand.Hold(b, true), token).ConfigureAwait(false);
            await Task.Delay(hold, token).ConfigureAwait(false);
            await Executor.SwitchConnection.SendAsync(SwitchCommand.Release(b, true), token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }
        public async Task Touch(int x, int y, int hold, int delay, CancellationToken token, bool crlf = true)
        {
            var command = Encoding.ASCII.GetBytes($"touchHold {x} {y} {hold}{(crlf ? "\r\n" : "")}");
            await Executor.SwitchConnection.SendAsync(command, token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }

        private async Task DaySkip(CancellationToken token)
        {
            await Click(B, 0_150, token).ConfigureAwait(false);
            await Click(HOME, 2_000, token).ConfigureAwait(false); // Back to title screen

            await Touch(845, 545, 0_050, 1_000, token).ConfigureAwait(false);
            await Touch(845, 545, 0_050, 1_000, token).ConfigureAwait(false); // Enter settings

            await PressAndHold(DDOWN, 2_000, 0_250, token).ConfigureAwait(false); // Scroll to system settings
            await Click(A, 1_250, token).ConfigureAwait(false);

            await PressAndHold(DDOWN, 930, 0_100, token).ConfigureAwait(false);
            await Click(DUP, 0_500, token).ConfigureAwait(false);

            await Click(A, 1_250, token).ConfigureAwait(false);
            await Touch(1006, 386, 0_050, 1_000, token).ConfigureAwait(false);
            await Task.Delay(0_150, token).ConfigureAwait(false);
            await Touch(151, 471, 0_050, 0_500, token).ConfigureAwait(false);
            await Task.Delay(0_150, token).ConfigureAwait(false);

            await Touch(1102, 470, 0_050, 1_000, token).ConfigureAwait(false);
            await Task.Delay(0_150, token).ConfigureAwait(false);

            await Click(HOME, 1_000, token).ConfigureAwait(false);
            await Click(A, 4_000, token).ConfigureAwait(false); // Back to title screen
        }
        public async Task DaySkipFaster(CancellationToken token) => await Executor.SwitchConnection.SendAsync(SwitchCommand.DaySkip(Executor.UseCRLF), token).ConfigureAwait(false);
        public async Task TimeSkipFwd(CancellationToken token) => await Executor.SwitchConnection.SendAsync(SwitchCommand.TimeSkipForward(Executor.UseCRLF), token).ConfigureAwait(false);
        public async Task TimeSkipBwd(CancellationToken token) => await Executor.SwitchConnection.SendAsync(SwitchCommand.TimeSkipBack(Executor.UseCRLF), token).ConfigureAwait(false);
        public async Task ResetTime(CancellationToken token) => await Executor.SwitchConnection.SendAsync(SwitchCommand.ResetTime(Executor.UseCRLF), token).ConfigureAwait(false);

        private static HttpClient? _client;
        private static HttpClient Client
        {
            get
            {
                _client ??= new HttpClient();
                return _client;
            }
        }

        private static string[]? DiscordWebhooks;

        private async Task SendNotifications(string results, string thumbnail)
        {
            if (string.IsNullOrEmpty(results) || string.IsNullOrEmpty(Settings.Default.WebHook))
                return;
            DiscordWebhooks = Settings.Default.WebHook.Split(',');
            if (DiscordWebhooks == null)
                return;

            var webhook = GenerateWebhook(results, thumbnail);
            var content = new StringContent(JsonConvert.SerializeObject(webhook), Encoding.UTF8, "application/json");
            foreach (var url in DiscordWebhooks)
                await Client.PostAsync(url, content).ConfigureAwait(false);
        }

        private static object GenerateWebhook(string results, string thumbnail)
        {
            var WebHook = new
            {
                username = $"{(!string.IsNullOrEmpty(Settings.Default.DiscordUserName) ? Settings.Default.DiscordUserName : "PokeViewer.NET")}",
                content = $"<@{Settings.Default.UserDiscordID}>",
                embeds = new List<object>
                {
                    new
                    {
                        title = $"Match Found!",
                        thumbnail = new
                        {
                            url = thumbnail
                        },
                        fields = new List<object>
                        {
                            new { name = "Description               ", value = results, inline = true, },
                        },
                    }
                }
            };
            return WebHook;
        }

        // Read, Decrypt, and Write Block tasks from Tera-Finder/RaidCrawler/sv-livemap.
        #region saveblocktasks
        public static byte[] DecryptBlock(uint key, byte[] block)
        {
            var rng = new SCXorShift32(key);
            for (int i = 0; i < block.Length; i++)
                block[i] = (byte)(block[i] ^ rng.Next());
            return block;
        }

        private async Task<(byte, ulong)> ReadEncryptedBlockByte(DataBlock block, ulong init, CancellationToken token)
        {
            var (header, address) = await ReadEncryptedBlockHeader(block, init, token).ConfigureAwait(false);
            return (header[1], address);
        }

        private async Task<(byte[], ulong)> ReadEncryptedBlockHeader(DataBlock block, ulong init, CancellationToken token)
        {
            if (init == 0)
            {
                var address = await SearchSaveKey(block.Key, token).ConfigureAwait(false);
                address = BitConverter.ToUInt64(await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address + 8, 0x8, token).ConfigureAwait(false), 0);
                init = address;
            }

            var header = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(init, 5, token).ConfigureAwait(false);
            header = DecryptBlock(block.Key, header);

            return (header, init);
        }

        private async Task<(byte[]?, ulong)> ReadEncryptedBlockArray(DataBlock block, ulong init, CancellationToken token)
        {
            if (init == 0)
            {
                var address = await SearchSaveKey(block.Key, token).ConfigureAwait(false);
                address = BitConverter.ToUInt64(await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address + 8, 0x8, token).ConfigureAwait(false), 0);
                init = address;
            }

            var data = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(init, 6 + block.Size, token).ConfigureAwait(false);
            data = DecryptBlock(block.Key, data);

            return (data[6..], init);
        }

        private async Task<(uint, ulong)> ReadEncryptedBlockUint(DataBlock block, ulong init, CancellationToken token)
        {
            var (header, address) = await ReadEncryptedBlockHeader(block, init, token).ConfigureAwait(false);
            return (ReadUInt32LittleEndian(header.AsSpan()[1..]), address);
        }

        private async Task<(int, ulong)> ReadEncryptedBlockInt32(DataBlock block, ulong init, CancellationToken token)
        {
            var (header, address) = await ReadEncryptedBlockHeader(block, init, token).ConfigureAwait(false);
            return (ReadInt32LittleEndian(header.AsSpan()[1..]), address);
        }

        private async Task<(bool, ulong)> ReadEncryptedBlockBool(DataBlock block, ulong init, CancellationToken token)
        {
            if (init == 0)
            {
                var address = await SearchSaveKey(block.Key, token).ConfigureAwait(false);
                address = BitConverter.ToUInt64(await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address + 8, 0x8, token).ConfigureAwait(false), 0);
                init = address;
            }
            var data = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(init, block.Size, token).ConfigureAwait(false);
            var res = DecryptBlock(block.Key, data);
            return (res[0] == 2, init);
        }

        private async Task<byte[]> ReadEncryptedBlock(DataBlock block, CancellationToken token)
        {
            var address = await SearchSaveKey(block.Key, token).ConfigureAwait(false);
            address = BitConverter.ToUInt64(await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address + 8, 0x8, token).ConfigureAwait(false), 0);
            var data = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address, 6 + block.Size, token).ConfigureAwait(false);
            data = DecryptBlock(block.Key, data);

            return data[6..];
        }

        private async Task<byte[]?> ReadEncryptedBlockObject(DataBlock block, CancellationToken token)
        {
            var address = await SearchSaveKey(block.Key, token).ConfigureAwait(false);
            address = BitConverter.ToUInt64(await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address + 8, 0x8, token).ConfigureAwait(false), 0);
            var header = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address, 5, token).ConfigureAwait(false);
            header = DecryptBlock(block.Key, header);
            var size = ReadUInt32LittleEndian(header.AsSpan()[1..]);
            var data = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address, 5 + (int)size, token).ConfigureAwait(false);
            var res = DecryptBlock(block.Key, data)[5..];

            return res;
        }

        public async Task<bool> WriteBlock(object data, DataBlock block, CancellationToken token, object? toExpect = default)
        {
            if (block.IsEncrypted)
                return await WriteEncryptedBlockSafe(block, toExpect, data, token).ConfigureAwait(false);
            else
                return await WriteDecryptedBlock((byte[])data!, block, token).ConfigureAwait(false);
        }

        private async Task<bool> WriteDecryptedBlock(byte[] data, DataBlock block, CancellationToken token)
        {
            await Executor.SwitchConnection.PointerPoke(data, block.Pointer!, token).ConfigureAwait(false);

            return true;
        }

        private async Task<bool> WriteEncryptedBlockSafe(DataBlock block, object? toExpect, object toWrite, CancellationToken token)
        {
            if (toExpect == default || toWrite == default)
                return false;

            return block.Type switch
            {
                SCTypeCode.Array => await WriteEncryptedBlockArray(block, (byte[])toExpect, (byte[])toWrite, token).ConfigureAwait(false),
                SCTypeCode.Bool1 or SCTypeCode.Bool2 or SCTypeCode.Bool3 => await WriteEncryptedBlockBool(block, (bool)toExpect, (bool)toWrite, token).ConfigureAwait(false),
                SCTypeCode.Byte or SCTypeCode.SByte => await WriteEncryptedBlockByte(block, (byte)toExpect, (byte)toWrite, token).ConfigureAwait(false),
                SCTypeCode.UInt32 or SCTypeCode.UInt64 => await WriteEncryptedBlockUint(block, (uint)toExpect, (uint)toWrite, token).ConfigureAwait(false),
                SCTypeCode.Int32 => await WriteEncryptedBlockInt32(block, (int)toExpect, (int)toWrite, token).ConfigureAwait(false),
                _ => throw new NotSupportedException($"Block {block.Name} (Type {block.Type}) is currently not supported.")
            };
        }

        private async Task<bool> WriteEncryptedBlockUint(DataBlock block, uint valueToExpect, uint valueToInject, CancellationToken token)
        {
            ulong address;
            try
            {
                address = await SearchSaveKey(block.Key, token).ConfigureAwait(false);
                address = BitConverter.ToUInt64(await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address + 8, 0x8, token).ConfigureAwait(false), 0);
            }
            catch (Exception) { return false; }
            //If we get there without exceptions, the block address is valid
            var header = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address, 5, token).ConfigureAwait(false);
            header = DecryptBlock(block.Key, header);
            //Validate ram data
            var ram = ReadUInt32LittleEndian(header.AsSpan()[1..]);
            if (ram != valueToExpect) return false;
            //If we get there then both block address and block data are valid, we can safely inject
            WriteUInt32LittleEndian(header.AsSpan()[1..], valueToInject);
            header = EncryptBlock(block.Key, header);
            await Executor.SwitchConnection.WriteBytesAbsoluteAsync(header, address, token).ConfigureAwait(false);

            return true;
        }

        private async Task<bool> WriteEncryptedBlockInt32(DataBlock block, int valueToExpect, int valueToInject, CancellationToken token)
        {
            ulong address;
            try
            {
                address = await SearchSaveKey(block.Key, token).ConfigureAwait(false);
                address = BitConverter.ToUInt64(await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address + 8, 0x8, token).ConfigureAwait(false), 0);
            }
            catch (Exception) { return false; }
            //If we get there without exceptions, the block address is valid
            var header = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address, 5, token).ConfigureAwait(false);
            header = DecryptBlock(block.Key, header);
            //Validate ram data
            var ram = ReadInt32LittleEndian(header.AsSpan()[1..]);
            if (ram != valueToExpect) return false;
            //If we get there then both block address and block data are valid, we can safely inject
            WriteInt32LittleEndian(header.AsSpan()[1..], valueToInject);
            header = EncryptBlock(block.Key, header);
            await Executor.SwitchConnection.WriteBytesAbsoluteAsync(header, address, token).ConfigureAwait(false);

            return true;
        }

        private async Task<bool> WriteEncryptedBlockByte(DataBlock block, byte valueToExpect, byte valueToInject, CancellationToken token)
        {
            ulong address;
            try
            {
                address = await SearchSaveKey(block.Key, token).ConfigureAwait(false);
                address = BitConverter.ToUInt64(await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address + 8, 0x8, token).ConfigureAwait(false), 0);
            }
            catch (Exception) { return false; }
            //If we get there without exceptions, the block address is valid
            var header = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address, 5, token).ConfigureAwait(false);
            header = DecryptBlock(block.Key, header);
            //Validate ram data
            var ram = header[1];
            if (ram != valueToExpect) return false;
            //If we get there then both block address and block data are valid, we can safely inject
            header[1] = valueToInject;
            header = EncryptBlock(block.Key, header);
            await Executor.SwitchConnection.WriteBytesAbsoluteAsync(header, address, token).ConfigureAwait(false);

            return true;
        }

        private async Task<bool> WriteEncryptedBlockArray(DataBlock block, byte[] arrayToExpect, byte[] arrayToInject, CancellationToken token)
        {
            ulong address;
            try
            {
                address = await SearchSaveKey(block.Key, token).ConfigureAwait(false);
                address = BitConverter.ToUInt64(await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address + 8, 0x8, token).ConfigureAwait(false), 0);
            }
            catch (Exception) { return false; }
            //If we get there without exceptions, the block address is valid
            var data = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address, 6 + block.Size, token).ConfigureAwait(false);
            data = DecryptBlock(block.Key, data);
            //Validate ram data
            var ram = data[6..];
            if (!ram.SequenceEqual(arrayToExpect)) return false;
            //If we get there then both block address and block data are valid, we can safely inject
            Array.ConstrainedCopy(arrayToInject, 0, data, 6, block.Size);
            data = EncryptBlock(block.Key, data);
            await Executor.SwitchConnection.WriteBytesAbsoluteAsync(data, address, token).ConfigureAwait(false);

            return true;
        }

        private async Task<bool> WriteEncryptedBlockBool(DataBlock block, bool valueToExpect, bool valueToInject, CancellationToken token)
        {
            ulong address;
            try
            {
                address = await SearchSaveKey(block.Key, token).ConfigureAwait(false);
                address = BitConverter.ToUInt64(await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address + 8, 0x8, token).ConfigureAwait(false), 0);
            }
            catch (Exception) { return false; }
            //If we get there without exceptions, the block address is valid
            var data = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(address, block.Size, token).ConfigureAwait(false);
            data = DecryptBlock(block.Key, data);
            //Validate ram data
            var ram = data[0] == 2;
            if (ram != valueToExpect) return false;
            //If we get there then both block address and block data are valid, we can safely inject
            data[0] = valueToInject ? (byte)2 : (byte)1;
            data = EncryptBlock(block.Key, data);
            await Executor.SwitchConnection.WriteBytesAbsoluteAsync(data, address, token).ConfigureAwait(false);

            return true;
        }

        public async Task<ulong> SearchSaveKey(uint key, CancellationToken token)
        {
            var data = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(BaseBlockKeyPointer + 8, 16, token).ConfigureAwait(false);
            var start = BitConverter.ToUInt64(data.AsSpan()[..8]);
            var end = BitConverter.ToUInt64(data.AsSpan()[8..]);

            while (start < end)
            {
                var block_ct = (end - start) / 48;
                var mid = start + (block_ct >> 1) * 48;

                data = await Executor.SwitchConnection.ReadBytesAbsoluteAsync(mid, 4, token).ConfigureAwait(false);
                var found = BitConverter.ToUInt32(data);
                if (found == key)
                    return mid;

                if (found >= key)
                    end = mid;
                else start = mid + 48;
            }
            return start;
        }
        public static byte[] EncryptBlock(uint key, byte[] block) => DecryptBlock(key, block);
        #endregion

        private async Task SVSaveGameOverworld(CancellationToken token)
        {
            await Click(X, 1_000, token).ConfigureAwait(false);
            await Click(R, 1_000, token).ConfigureAwait(false);
            await Click(A, 3_000, token).ConfigureAwait(false);
            for (int i = 0; i < 3; i++) // Mash B
                await Click(B, 0_500, token).ConfigureAwait(false);
        }
        private async Task DefeatPokemon(CancellationToken token)
        {
            while (await IsInBattle(token).ConfigureAwait(false))
                await Click(A, 0_800, token).ConfigureAwait(false);
        }
        private async Task<bool> IsInBattle(CancellationToken token)
        {
            var data = await Executor.SwitchConnection.ReadBytesMainAsync(Offsets.IsInBattle, 1, token).ConfigureAwait(false);
            return data[0] <= 0x05;
        }

        private void EnableAssets()
        {
            OutbreakScan.Enabled = true;
            OutbreakScan.Text = "Scan";
            OutbreakSearch.Enabled = true;
            Apply0To64.Enabled = true;
            OpenMapPaldea.Enabled = true;
            OpenMapKitakami.Enabled = true;
            OpenMapBlueberry.Enabled = true;
            ResetNum.Enabled = true;
            WildCheck.Enabled = true;
        }

        private void DisableAssets()
        {
            OutbreakScan.Enabled = false;
            OutbreakSearch.Enabled = false;
            Apply0To64.Enabled = false;
            OpenMapPaldea.Enabled = false;
            OpenMapKitakami.Enabled = false;
            OpenMapBlueberry.Enabled = false;
            ResetNum.Enabled = false;
            WildCheck.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string r = string.Empty;
            foreach (var p in pkList.ToList())
            {
                r += $"{SpeciesList[p.Item1.Species]}{FormOutput(p.Item1.Species, p.Item1.Form, out _)}-CheckCoord: {p.Item2}{(p.Item2 ? $"-TargetX: {p.Item3}, TargetZ: {p.Item4}, Range: {p.Item5}" : "")}{Environment.NewLine}";
            }
            MessageBox.Show(r, "Search List");
        }
        private void SetForm(object sender, EventArgs e)
        {
            SetForm();
            CoordCheck.Checked = false;
        }
        private void FormCombo_SelectedIndexChanged(object sender, EventArgs e) => CoordCheck.Checked = false;
        private void SetForm()
        {
            if (SpeciesBox.SelectedIndex < 0)
                return;
            FormCombo.Items.Clear();
            FormCombo.Text = string.Empty;
            ushort SpeciesIndex = (ushort)SpeciesList.ToList().IndexOf((string)SpeciesBox.Items[SpeciesBox.SelectedIndex]!);
            var FormCount = PersonalTable.SV.GetFormEntry(SpeciesIndex, 0).FormCount;
            List<byte> FormList = [];
            for(byte form = 0; form < FormCount; form++)
            {
                if (PersonalTable.SV.IsPresentInGame(SpeciesIndex, form))
                    FormList.Add(form);
            }
            var FormStringList = FormConverter.GetFormList(SpeciesIndex, TypesList, FormsList, GenderList, EntityContext.Gen9);
            var formlist = FormList.Select(x => FormStringList[x]).ToArray();
            if ((Species)SpeciesIndex == Species.Minior)
                formlist = formlist.Skip((formlist.Length + 1) / 2).ToArray();

            if (formlist.Length == 0 || (formlist.Length == 1 && formlist[0].Equals("")))
                FormCombo.Visible = false;
            else
            {
                FormCombo.Items.AddRange(formlist);
                FormCombo.Visible = true;
            }
        }

        private void EventSearchChanged(object sender, EventArgs e)
        {
            if (ScanForEventOutbreak.Checked)
            {
                Teleportindex.Maximum = CenterPos0bP.Count + CenterPos0bK.Count + CenterPos0bB.Count - 1 < (int)Teleportindex.Minimum ? (int)Teleportindex.Minimum : CenterPos0bP.Count + CenterPos0bK.Count + CenterPos0bB.Count - 1;
                if (CenterPos0bP.Count + CenterPos0bK.Count + CenterPos0bB.Count - 1 < 0)
                    CollideButton.Enabled = false;
                else
                    CollideButton.Enabled = true;
            }
            else
            {
                Teleportindex.Maximum = CenterPosP.Count + CenterPosK.Count + CenterPosB.Count - 1 < (int)Teleportindex.Minimum ? (int)Teleportindex.Minimum : CenterPosP.Count + CenterPosK.Count + CenterPosB.Count - 1;
                if (CenterPosP.Count + CenterPosK.Count + CenterPosB.Count - 1 < 0)
                    CollideButton.Enabled = false;
                else
                    CollideButton.Enabled = true;
            }

        }

        private void AddSpecies_Click(object sender, EventArgs e)
        {
            if(SpeciesBox.SelectedIndex < 0)
            {
                MessageBox.Show("You can't add a blank species!", "Search List");
                return;
            }
            var SpeciesIndex = SpeciesList.ToList().IndexOf((string)SpeciesBox.Items[SpeciesBox.SelectedIndex]!);
            var forms = FormConverter.GetFormList((ushort)SpeciesIndex, TypesList, FormsList, GenderList, EntityContext.Gen9);
            PK9 pk = new()
            {
                Species = (ushort)SpeciesIndex,
                Form = FormCombo.SelectedIndex < 0 ? (byte)0 : (byte)forms.ToList().IndexOf((string)FormCombo.Items[FormCombo.SelectedIndex]!),
            };

            if (pk.Species == 0 || pk.Form < 0)
            {
                MessageBox.Show("You can't add a blank species!", "Search List");
                return;
            }
            float X = 0;
            float Z = 0;
            if (CoordCheck.Checked && (!Single.TryParse(XCoordValue.Text, out X) || !Single.TryParse(ZCoordValue.Text, out Z)))
            {
                MessageBox.Show("Coords are Invalid!");
                return;
            }
            if (CoordCheck.Checked && (X == 0 || Z == 0))
            {
                MessageBox.Show("Target Coord is 0!");
                return;
            }
            var XCoord = CoordCheck.Checked ? X : 0;
            var ZCoord = CoordCheck.Checked ? Z : 0;
            var Range = CoordCheck.Checked ? (int)RangeNum.Value : 0;
            if (pkList.Count == 0)
            {
                pkList.Add((pk, CoordCheck.Checked, XCoord, ZCoord, Range));
                SaveFilter();
                MessageBox.Show($"Added {SpeciesList[SpeciesIndex]}{FormOutput(pk.Species, pk.Form, out _)}{(CoordCheck.Checked ? $"- TargetCoordX: {XCoord}, TargetCoordZ: {ZCoord}, Range: {Range}" : "")}", "Search List");
                return;
            }
            List<(ushort, byte, bool, float, float)> DataList = [];
            foreach (var p in pkList.ToList())
                DataList.Add((p.Item1.Species, p.Item1.Form, p.Item2, p.Item3, p.Item4));
            if (DataList.Contains((pk.Species, pk.Form, CoordCheck.Checked, XCoord, ZCoord)))
            {
                MessageBox.Show($"List contains {SpeciesList[SpeciesIndex]}{FormOutput(pk.Species, pk.Form, out _)}{(CoordCheck.Checked ? $"- TargetCoordX: {XCoord}, TargetCoordZ: {ZCoord}, Range: {Range}" : "")}", "Search List");
                return;
            }
            else
            {
                pkList.Add((pk, CoordCheck.Checked, XCoord, ZCoord, Range));
                SaveFilter();
                MessageBox.Show($"New Target Added {SpeciesList[SpeciesIndex]}{FormOutput(pk.Species, pk.Form, out _)}{(CoordCheck.Checked ? $"- TargetCoordX: {XCoord}, TargetCoordZ: {ZCoord}, Range: {Range}" : "")}", "Search List");
            }
        }
        private void SaveFilter()
        {
            string output = string.Empty;
            foreach (var pA in pkList.ToList())
            {
                output += $"{pA.Item1.Species}-{pA.Item1.Form}-{pA.Item2}-{pA.Item3}-{pA.Item4}-{pA.Item5},";
            }
            using StreamWriter sw = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "refs", "outbreakfilters.txt"));
            sw.Write(output);
        }

        private void RemoveSpecies_Click(object sender, EventArgs e)
        {
            if (pkList.Count == 0)
                return;

            if (SpeciesBox.SelectedIndex < 0)
            {
                MessageBox.Show("You can't remove a blank species!", "Search List");
                return;
            }
            var SpeciesIndex = SpeciesList.ToList().IndexOf((string)SpeciesBox.Items[SpeciesBox.SelectedIndex]!);
            var forms = FormConverter.GetFormList((ushort)SpeciesIndex, TypesList, FormsList, GenderList, EntityContext.Gen9);
            PK9 pk = new()
            {
                Species = (ushort)SpeciesIndex,
                Form = FormCombo.SelectedIndex < 0 ? (byte)0 : (byte)forms.ToList().IndexOf((string)FormCombo.Items[FormCombo.SelectedIndex]!),
            };
            var msg = string.Empty;
            foreach (var p in pkList.ToList())
            {
                if (p.Item1.Species != pk.Species || p.Item1.Form != pk.Form)
                    continue;

                pkList.Remove(p);
                msg += $"Removed {SpeciesList[p.Item1.Species]}{FormOutput(p.Item1.Species, p.Item1.Form, out _)}{(p.Item2 ? $"- TargetCoordX: {p.Item3}, TargetCoordZ: {p.Item4}, Range: {p.Item5}" : "")}{Environment.NewLine}";

            }
            SaveFilter();
            MessageBox.Show(msg);
        }
        private void ClearList_Click(object sender, EventArgs e)
        {
            pkList.Clear();
            pkList = new();
            MessageBox.Show("Cleared all filters", "Search List");
        }

        private void OpenMap_Click(object sender, EventArgs e)
        {
            MapViewPaldea form = new(MapSpritesP, MapPOSP, MapStringsP, MapCountP, MapSpritesObP, MapStringsObP, MapCountObP, MapPOSObP);
            form.ShowDialog();
        }

        private void OpenMapKitakami_Click(object sender, EventArgs e)
        {
            MapViewKitakami form = new(MapSpritesK, MapPOSK, MapStringsK, MapCountK, MapSpritesObK, MapStringsObK, MapCountObK, MapPOSObK);
            form.ShowDialog();
        }

        private void OpenMapBlueberry_Click(object sender, EventArgs e)
        {
            MapViewBlueberry form = new(MapSpritesB, MapPOSB, MapStringsB, MapCountB, MapSpritesObB, MapStringsObB, MapCountObB, MapPOSObB);
            form.ShowDialog();
            Show();
        }
        private void TimeViewerButton_Click(object sender, EventArgs e)
        {
            if (!Executor.SwitchConnection.Connected)            
                Executor.SwitchConnection.Reset();
            
            TimeViewer form = new(Executor);
            form.ShowDialog();
        }
        private void MoneyButton_Click(object sender, EventArgs e)
        {
            if (!Executor.SwitchConnection.Connected)            
                Executor.SwitchConnection.Reset();

            MoneyViewer form = new(Executor);
            form.ShowDialog();
        }
        private async void WildSpawnBtn_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                await ParseBlockKeyPointer(false, CancellationToken.None).ConfigureAwait(false);
                var (wildstatus, _) = await ReadEncryptedBlockBool(Blocks.KWildSpawnsEnabled, 0, CancellationToken.None).ConfigureAwait(false);
                await CloseGame(CancellationToken.None).ConfigureAwait(false);
                await StartGameScreen(CancellationToken.None).ConfigureAwait(false);
                await ParseBlockKeyPointer(true, CancellationToken.None).ConfigureAwait(false);
                if (WildEnable.Checked)
                {
                    if (wildstatus)
                    {
                        (wildstatus, _) = await ReadEncryptedBlockBool(Blocks.KWildSpawnsEnabled, 0, CancellationToken.None).ConfigureAwait(false);
                        MessageBox.Show($"Wild Spawns Status: {wildstatus}");
                    }
                    else
                    {
                        await WriteEncryptedBlockBool(Blocks.KWildSpawnsEnabled, false, true, CancellationToken.None).ConfigureAwait(false);
                        (wildstatus, _) = await ReadEncryptedBlockBool(Blocks.KWildSpawnsEnabled, 0, CancellationToken.None).ConfigureAwait(false);
                        MessageBox.Show($"Wild Spawns Status: {wildstatus}");
                    }
                }
                if (WildDisable.Checked)
                {
                    if (wildstatus)
                    {
                        await WriteEncryptedBlockBool(Blocks.KWildSpawnsEnabled, true, false, CancellationToken.None).ConfigureAwait(false);
                        (wildstatus, _) = await ReadEncryptedBlockBool(Blocks.KWildSpawnsEnabled, 0, CancellationToken.None).ConfigureAwait(false);
                        MessageBox.Show($"Wild Spawns Status: {wildstatus}");
                    }
                    else
                    {
                        (wildstatus, _) = await ReadEncryptedBlockBool(Blocks.KWildSpawnsEnabled, 0, CancellationToken.None).ConfigureAwait(false);
                        MessageBox.Show($"Wild Spawns Status: {wildstatus}");
                    }
                }
            }
            catch (Exception ex)
            {
                if (Executor.SwitchConnection.Connected)
                {
                    try
                    {
                        Executor.SwitchConnection.Disconnect();
                    }
                    catch (SocketException soketEx)
                    {
                        MessageBox.Show(this, soketEx.ToString(), "Sokect Connetion Exception!");
                    }
                }
                MessageBox.Show(this, ex.ToString(), "Exception!");
            }
        }

        private void SnackworthBtn_Click(object sender, EventArgs e)
        {
            if (!Executor.SwitchConnection.Connected)            
                Executor.SwitchConnection.Reset();
            
            SnackworthViewer form = new(Executor);
            form.ShowDialog();
        }
    }
}
