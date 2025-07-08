using NLog;
using PKHeX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeViewer.NET.Misc;

public class Pokemon_Enums
{
    public enum Times
    {
        All_Times, 
        Morning,
        Day,
        Evening,
        Night,
        Morning_to_Evening,
    }

    public struct TimeBase
    {
        public TimeBase()
        {

        }
        public static long GetTimeBase(Times times)
        {
            long BaseTime = 0;
            switch (times)
            {
                case Times.Morning: BaseTime = BaseMorning; break;
                case Times.Day: BaseTime = BaseDay; break;
                case Times.Evening: BaseTime = BaseEvening; break;
                case Times.Night: BaseTime = BaseNight; break;
                case Times.Morning_to_Evening: BaseTime = BaseMorning + BaseDay + BaseEvening; break;
                default: BaseTime = -1; break;
            }
            return BaseTime;
        }
        public static long GetNextTimeBase(Times times)
        {
            long BaseTime = 0;
            switch (times)
            {
                case Times.Morning: BaseTime = BaseMorning + BaseDay; break;
                case Times.Day: BaseTime = BaseDay + BaseEvening; break;
                case Times.Evening: BaseTime = BaseEvening + BaseNight; break;
                case Times.Night: BaseTime = BaseNight + BaseMorning; break;
                default: BaseTime = -1; break;
            }
            return BaseTime;
        }
        public static long GetSecondNextTimeBase(Times times)
        {
            long BaseTime = 0;
            switch (times)
            {
                case Times.Morning: BaseTime = BaseMorning + BaseDay + BaseEvening; break;
                case Times.Day: BaseTime = BaseDay + BaseEvening + BaseNight; break;
                case Times.Evening: BaseTime = BaseEvening + BaseNight + BaseMorning; break;
                case Times.Night: BaseTime = BaseNight + BaseMorning + BaseDay; break;
                default: BaseTime = -1; break;
            }
            return BaseTime;
        }
        public static Times GetNextTimes(Times times)
        {
            if(times == Times.Night)
                return Times.Morning;
            return times + 1;
        }
        public static long GetNextBaseTime(Times CurTimes) =>
            CurTimes switch
            {
                Times.Morning => BaseDay,
                Times.Day => BaseEvening,
                Times.Evening => BaseNight,
                Times.Night => BaseMorning,
                _ => -1,
            };
        
        public static long BaseMorning { get; } = (long)TimeSpan.FromMinutes(18).TotalSeconds;
        public static long BaseDay { get; } = (long)TimeSpan.FromMinutes(18).TotalSeconds;
        public static long BaseEvening { get; } = (long)TimeSpan.FromMinutes(3).TotalSeconds;
        public static long BaseNight { get; } = (long)TimeSpan.FromMinutes(33).TotalSeconds;
    }
    public struct TimesReroll
    {
        public TimesReroll()
        {

        }
        private DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly TimeSpan Diff_From_Kitakami = TimeSpan.FromMinutes(17);
        private static readonly TimeSpan Diff_To_Blueberry = TimeSpan.FromMinutes(18);
        private static List<TimeSpan> PaldeaMorinig = new List<TimeSpan> 
        { 
            new TimeSpan(0, 18, 0), new TimeSpan(1, 30, 0), new TimeSpan(2, 42, 0),
            new TimeSpan(3, 54, 0), new TimeSpan(5, 6, 0), new TimeSpan(6, 18, 0),
            new TimeSpan(7, 30, 0), new TimeSpan(8, 42, 0), new TimeSpan(9, 54, 0),
            new TimeSpan(11, 6, 0)
        };
        private static List<TimeSpan> PaldeaDay = PaldeaMorinig.Select(x => x + TimeSpan.FromSeconds(TimeBase.BaseMorning)).ToList();
        private static List<TimeSpan> PaldeaEvening = PaldeaDay.Select(x => x + TimeSpan.FromSeconds(TimeBase.BaseDay)).ToList();
        private static List<TimeSpan> PaldeaNight = PaldeaEvening.Select(x => x + TimeSpan.FromSeconds(TimeBase.BaseEvening)).ToList();
        private static Dictionary<Times, List<TimeSpan>> PaldeaDict = new Dictionary<Times, List<TimeSpan>>()
        {
            { Times.Morning, PaldeaMorinig },
            { Times.Day, PaldeaDay },
            { Times.Evening, PaldeaEvening },
            { Times.Night, PaldeaNight },
        };
        private static Dictionary<Times, List<TimeSpan>> KitakamiDict = new Dictionary<Times, List<TimeSpan>>()
        {
            { Times.Morning, PaldeaMorinig.Select(x => x - Diff_From_Kitakami).ToList() },
            { Times.Day, PaldeaDay.Select(x => x - Diff_From_Kitakami).ToList() },
            { Times.Evening, PaldeaEvening.Select(x => x - Diff_From_Kitakami).ToList() },
            { Times.Night, PaldeaNight.Select(x => x - Diff_From_Kitakami).ToList() },
        };
        private static Dictionary<Times, List<TimeSpan>> BlueberryDict = new Dictionary<Times, List<TimeSpan>>()
        {
            { Times.Morning, PaldeaMorinig.Select(x => x + Diff_To_Blueberry).ToList() },
            { Times.Day, PaldeaDay.Select(x => x + Diff_To_Blueberry).ToList() },
            { Times.Evening, PaldeaEvening.Select(x => x + Diff_To_Blueberry).ToList() },
            { Times.Night, PaldeaNight.Select(x => x + Diff_To_Blueberry).ToList() },
        };
        private static long ChangeLastDateToPaldea(TeraRaidMapParent map, long Date) => map
        switch
        {
            TeraRaidMapParent.Paldea => Date,
            TeraRaidMapParent.Kitakami => Date + (long)Diff_From_Kitakami.TotalSeconds,
            TeraRaidMapParent.Blueberry => Date - (long)Diff_To_Blueberry.TotalSeconds,
            _ => -1,
        };
        private static long ChangeLastDateFromPaldea(TeraRaidMapParent map, long Date) => map
        switch
        {
            TeraRaidMapParent.Paldea => Date,
            TeraRaidMapParent.Kitakami => Date - (long)Diff_From_Kitakami.TotalSeconds,
            TeraRaidMapParent.Blueberry => Date + (long)Diff_To_Blueberry.TotalSeconds,
            _ => -1,
        };
        public static long ChangeLastDateToTarget(TeraRaidMapParent Original, TeraRaidMapParent Target, long Date)
        {
            long DatePaldea = ChangeLastDateToPaldea(Original, Date);
            return ChangeLastDateFromPaldea(Target, DatePaldea);
        }
        public static (Times, TeraRaidMapParent) GetLastDateDic(DateTime LastDate)
        {
            TimeSpan Timecheck = new(12, 0, 0);
            TimeSpan time = LastDate.TimeOfDay;
            TimeSpan timebata = time >= Timecheck ? time - Timecheck : time + Timecheck;
            foreach(var item in PaldeaDict)
            {
                if (item.Value.Contains(time) || item.Value.Contains(timebata))
                    return (item.Key, TeraRaidMapParent.Paldea);
            }

            foreach(var item in KitakamiDict)
            {
                if (item.Value.Contains(time) || item.Value.Contains(timebata))
                    return (item.Key, TeraRaidMapParent.Kitakami);
            }

            foreach(var item in BlueberryDict)
            {
                if (item.Value.Contains(time) || item.Value.Contains(timebata))
                    return (item.Key, TeraRaidMapParent.Blueberry);
            }

            return (Times.All_Times, TeraRaidMapParent.Paldea);
        }
    }
    public static Times ConvertMarkToTimes(RibbonIndex marks)
    {
        if(marks == RibbonIndex.MarkDawn)
            return Times.Morning;
        if (marks == RibbonIndex.MarkLunchtime)
            return Times.Day;
        if (marks == RibbonIndex.MarkDusk)
            return Times.Evening;
        if(marks == RibbonIndex.MarkSleepyTime)
            return Times.Night;

        return Times.All_Times;
    }
}
