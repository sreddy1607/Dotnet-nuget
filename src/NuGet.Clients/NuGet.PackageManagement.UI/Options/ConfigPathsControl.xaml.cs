// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using System.Threading;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.VisualStudio.PlatformUI;
using NuGet.VisualStudio;

namespace NuGet.PackageManagement.UI.Options
{
    /// <summary>
    /// Interaction logic for ConfigPathsControl.xaml
    /// </summary>
    public partial class ConfigPathsControl : UserControl
    {
        public ConfigPathsWindowViewModel ConfigPathsWindow { get; set; }
        public ConfigPathsViewModel SelectedPath { get; set; }
        public ICommand OpenConfigurationFile { get; set; }

        public ConfigPathsControl()
        {
            ConfigPathsWindow = new ConfigPathsWindowViewModel();
            OpenConfigurationFile = new DelegateCommand(ExecuteOpenConfigurationFile, IsSelectedPath, NuGetUIThreadHelper.JoinableTaskFactory);
            DataContext = this;
            InitializeComponent();
        }

        private bool IsSelectedPath()
        {
            return _configurationPaths.SelectedItem != null;
        }

        private void ExecuteOpenConfigurationFile()
        {
            SelectedPath = (ConfigPathsViewModel)_configurationPaths.SelectedItem;
            ConfigPathsWindow.OpenConfigFile(SelectedPath);
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectedPath = (ConfigPathsViewModel)_configurationPaths.SelectedItem;
            ConfigPathsWindow.OpenConfigFile(SelectedPath);
        }

        internal void InitializeOnActivated(CancellationToken cancellationToken)
        {
            ConfigPathsWindow.ConfigPaths.Clear();
            ConfigPathsWindow.SetConfigPaths();
        }

        private void ExecuteOpenExternalLink(object sender, ExecutedRoutedEventArgs e)
        {
            var hyperlink = e.OriginalSource as Hyperlink;
            if (hyperlink != null && hyperlink.NavigateUri != null)
            {
                UIUtility.LaunchExternalLink(hyperlink.NavigateUri);

                e.Handled = true;
            }
        }
    }
}
