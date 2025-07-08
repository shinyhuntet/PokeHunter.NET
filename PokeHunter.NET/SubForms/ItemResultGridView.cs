using Newtonsoft.Json.Linq;
using PKHeX.Core;
using PokeViewer.NET.Misc;
using RaidCrawler.Core.Structures;
using SysBot.Base;

namespace PokeViewer.NET.SubForms;

public partial class ItemResultGridView : UserControl
{
    public ItemResultGridView() => InitializeComponent();
    
    public void Populate(List<InventoryItem> itemSpan, int language)
    {
        var rows = DGV_View.Rows;
        Image img = null!;
        string url = string.Empty;
        rows.Clear();
        rows = rows == null ? DGV_View.Rows : rows;
        foreach (var item in itemSpan)
        {
            if (Rewards.IsTM(item.Index))
            {
                img = Properties.Resources.tm;
            }
            else if (ItemStructure.IsMaterial(item))
            {
                img = Properties.Resources.material;
            }
            else
            {
                url = $"https://raw.githubusercontent.com/kwsch/PKHeX/master/PKHeX.Drawing.PokeSprite/Resources/img/Artwork Items/aitem_{item.Index}.png";
                img = GetItemImage(url);
            }
            rows.Add(item.Count, img, GameInfo.GetStrings(language).itemlist[item.Index]);
        }
    }
    public Image GetItemImage(string url)
    {
        PictureBox pictureBox = new();
        pictureBox.Load(url);
        return pictureBox.Image;
    }
    public void Clear() => DGV_View.Rows.Clear();
}
