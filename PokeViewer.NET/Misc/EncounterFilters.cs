using PKHeX.Core;
using System;
using System.Runtime.CompilerServices;

namespace PokeViewer.NET.Misc
{
    public class EncounterFilter
    {
        public List<int>? ItemList { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Species { get; set; }
        public int? Form { get; set; }
        public int Shiny { get; set; }
        public bool MarkOnly { get; set; }
        public List<Nature>? Nature { get; set; }
        public List<RibbonIndex>? Mark { get; set; }
        public List<RibbonIndex>? UnwantedMark { get; set; }
        public List<Ability>? AbilityList { get; set; }
        public int Gender { get; set; }
        public bool Scale { get; set; }
        public bool ThreeSegment { get; set; }
        public int[]? MaxIVs { get; set; }
        public int[]? MinIVs { get; set; }
        public int? IVMinindex { get; set; }
        public int? IVMaxindex { get; set; }
        public bool ignoreIVs { get; set; }
        public bool Enabled { get; set; }
        public string Rate { get; set; } = string.Empty;

        public bool IsFilterSet()
        {
            if (Species == null && Form == null && Nature == null && ItemList == null && AbilityList == null && MaxIVs == null && MinIVs == null && IVMaxindex == null && IVMinindex == null && Mark == null && UnwantedMark == null && Scale == false && ThreeSegment == false && MarkOnly == false)
                return false;
            return true;
        }
        public (bool, string?, List<RibbonIndex>?) IsTimeMarkTarget()
        {
            if (!Enabled)
                return (false, null, null);
            if (MarkOnly && Mark != null && Mark.Count > 0)
            {
                List<RibbonIndex> TimeMarkList = [];
                foreach (var mark in Mark)
                {
                    if (mark >= RibbonIndex.MarkLunchtime && mark <= RibbonIndex.MarkDawn)
                    {
                        TimeMarkList.Add(mark);
                    }
                }
                if (TimeMarkList.Count > 0)
                    return (true, Name, TimeMarkList);
                return(false, null, null);

            }
            else
                return (false, null, null);
        }
        public (bool, string?, List<RibbonIndex>?) IsWeatherMarkTarget()
        {
            if (!Enabled)
                return (false, null, null);
            if (MarkOnly && Mark != null && Mark.Count > 0)
            {
                List<RibbonIndex> WeatherMarkList = [];
                foreach (var mark in Mark)
                {
                    if (mark >= RibbonIndex.MarkCloudy && mark <= RibbonIndex.MarkMisty)
                    {
                        WeatherMarkList.Add(mark);
                    }
                }
                if (WeatherMarkList.Count > 0)
                    return (true, Name, WeatherMarkList);
                return (false, null, null);

            }
            else
                return (false, null, null);
        }
    }

    public enum FilterMode
    {
        Egg,
        Wide,
        Static,
        MysteryGift_SV,
        MysteryGift_SWSH,
        Other
    }
}

