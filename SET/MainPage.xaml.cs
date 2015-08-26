using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SET
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();
            g.Children.Add(mainScene.HostGrid);
            mainScene.AddTimer();
            karakib.d = new dealer();
            Button x = new Button();
            x.Content = "Play";
            x.Tapped += x_Tapped;
        }

        void x_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
        }
        

    }

   
 }
    
