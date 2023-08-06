using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace DesktopClock;

public class LinksUtil: INotifyPropertyChanged
{
    public ObservableCollection<(string Key, string Value)> Links { get; private set; } = new ObservableCollection<(string Key, string Value)>();

    public event PropertyChangedEventHandler PropertyChanged;

    private void LoadLinks()
    {
        Links.Clear();
        if (Directory.Exists(Properties.Settings.Default.LinkDirectory))
        {
            Directory.EnumerateFiles(Properties.Settings.Default.LinkDirectory).ToList().ForEach(x =>
            {
                Links.Add((x, x));
            });
        }
    }

}