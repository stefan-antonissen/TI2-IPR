using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TI2_IPR.Screens;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TI2_IPR
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LevelScreen : Page
    {
        public LevelScreen()
        {
            this.InitializeComponent();
        }

        public void Load()
        {

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void oneButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GameScreen));
        }

        private void twoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void threeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void fourButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void fiveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void sixButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
