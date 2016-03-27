using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641
using NetduinoControl.Phone.Api;
using NetduinoControl.Phone.Models;

namespace NetduinoControl.Phone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Settings _settings;
        private NetduinoApi _api;

        private readonly OutletCollection _outlets;
        
        public MainPage()
        {
            this.InitializeComponent();
            _outlets = new OutletCollection();
            OutletsListBox.DataContext = _outlets;
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            _settings = await Settings.LoadAsync();
            this.SettingsPanel.DataContext = _settings;
            _api = new NetduinoApi(_settings);

            await Task.Delay(TimeSpan.FromSeconds(20));

            var result = await _api.GetOutletStates();
            for (int i = 0; i < result.Length; i++)
            {
                _outlets.Add(new Outlet { Index = i, Name = "Outlet " + i, State = result[i] });
            }
        }

        private async void ToggleSwitch_OnToggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggle = ((ToggleSwitch) sender);
            Outlet outlet = ((Outlet) toggle.Tag);

            if (outlet.State == toggle.IsOn)
                return;

            var result = await _api.SetOutletState(outlet.Index, !outlet.State);
            outlet.State = result;
        }
    }
}
