using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace DesktopClock;

public static class DateTimeUtil
{
    /// <summary>
    /// A collection of DateTime formatting strings.
    /// </summary>
    public static IReadOnlyList<string> DateTimeFormats { get; } = new[]
    {
        "M",
        "dddd, MMMM dd",
        "dddd, MMMM dd, HH:mm",
        "dddd, MMM dd, HH:mm",
        "dddd, MMM dd, HH:mm:ss",
        "ddd, MMMM dd, HH:mm",
        "ddd, MMMM dd, HH:mm:ss",
        "ddd, MMM dd, HH:mm",
        "ddd, MMM dd, HH:mm:ss",
        "ddd, MMM dd, HH:mm K",
        "d",
        "g",
        "G",
        "t",
        "T",
        "O",
    };

    /// <summary>
    /// Common date time formatting strings and an example string for each.
    /// </summary>
    public static IReadOnlyDictionary<string, string> DateTimeFormatsAndExamples { get; } =
        DateTimeFormats.ToDictionary(f => f, DateTimeOffset.Now.ToString);

    public static ObservableCollection<KeyValuePair<string, string>> Links { get; private set; } = new ObservableCollection<KeyValuePair<string, string>>();

    public static void LoadLinks()
    {
        Links.Clear();
        Links.Add(new KeyValuePair<string, string>(Properties.Settings.Default.LinkDirectory, Properties.Settings.Default.LinkDirectory));
        if (Directory.Exists(Properties.Settings.Default.LinkDirectory))
        {
            Directory.EnumerateFiles(Properties.Settings.Default.LinkDirectory).ToList().ForEach(x =>
            {
                Links.Add(new KeyValuePair<string, string>(x, x));
            });
        }
    }

    public static IReadOnlyCollection<TimeZoneInfo> TimeZones { get; } = TimeZoneInfo.GetSystemTimeZones();

    public static bool TryGetTimeZoneById(string timeZoneId, out TimeZoneInfo timeZoneInfo)
    {
        try
        {
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return true;
        }
        catch (TimeZoneNotFoundException)
        {
            timeZoneInfo = null;
            return false;
        }
    }

}