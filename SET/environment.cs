using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using NextAgeClock;
using BuddySDK;

namespace SET
{
   
    static class mainScene
    {
        //themes will be here
        //theme chaniging functions will be here
        static public Grid HostGrid;
       static public Grid PlayArea;
       static public Timer GameTime;
       static  mainScene()
       {
           hostTableDef();
           PlayAreaTableDef();
           
       }
        static public void hostTableDef()
        {
            HostGrid = new Grid();
            ColumnDefinition c0 = new ColumnDefinition();
            c0.Width = new GridLength(1.25, GridUnitType.Star);
            HostGrid.ColumnDefinitions.Add(c0);
            ColumnDefinition c1 = new ColumnDefinition();
            c1.Width = new GridLength(3.5, GridUnitType.Star);
            ColumnDefinition c2 = new ColumnDefinition();
            c2.Width = new GridLength(1.25, GridUnitType.Star);
            HostGrid.ColumnDefinitions.Add(c1);
            HostGrid.ColumnDefinitions.Add(c2);
           
            RowDefinition r0 = new RowDefinition();
            r0.Height = new GridLength(0.25,GridUnitType.Star);
            HostGrid.RowDefinitions.Add(r0);
            RowDefinition r1 = new RowDefinition();
            r1.Height = new GridLength(5,GridUnitType.Star);
            HostGrid.RowDefinitions.Add(r1);
            RowDefinition r2 = new RowDefinition();
            r2.Height = new GridLength(0.5,GridUnitType.Star);
            HostGrid.RowDefinitions.Add(r2);
            ImageBrush x = new ImageBrush();
            x.ImageSource = (new BitmapImage(new Uri(@"ms-appx:///Assets/iron_texture47.jpg")));
            HostGrid.Background = x;
        }
     static   public void PlayAreaTableDef()
        {
            PlayArea = new Grid();
            for (Byte i = 0; i < 3; i++)
            {
                PlayArea.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (Byte i = 0; i < 5; i++)
            {
                PlayArea.RowDefinitions.Add(new RowDefinition());
            }
            Grid.SetRow(PlayArea, 1);
         Grid.SetColumn(PlayArea,1);
            HostGrid.Children.Add(PlayArea);
        }
       static public void AddTimer()
       {
            GameTime = new Timer(75);
           GameTime.Width = 150;
           GameTime.Height = 150;
           GameTime.HorizontalAlignment = HorizontalAlignment.Center;
           GameTime.VerticalAlignment = VerticalAlignment.Top;
           GameTime.Margin = new Thickness(0, 50, 0, 0);
           Grid.SetColumn(GameTime, 0);
           Grid.SetRow(GameTime,1);
           GameTime.Start();
           HostGrid.Children.Add(GameTime);
       }
        
    }
    public
 class CARD : Grid
    {
        private  Boolean isHit { get; set; }
        public Ellipse CardEllipse { get; set; }
        public CARD(card c) 
            : base()
        {
            CardEllipse = new Ellipse();
            gridDef();
            EllipseDef(this.CardEllipse);
            base.Children.Add(this.CardEllipse);
            addImage(c.number, c);
            base.Tag = c;
            this.isHit = false;
        }
        void gridDef()
        {
            for (Byte i = 0; i < 3; i++)
            {
                base.ColumnDefinitions.Add(new ColumnDefinition());
            }
            RowDefinition r0 = new RowDefinition();
            r0.Height = new GridLength(1, GridUnitType.Star);
            RowDefinition r1 = new RowDefinition();
            r1.Height = new GridLength(2, GridUnitType.Star);
            RowDefinition r2 = new RowDefinition();
            r2.Height = new GridLength(1, GridUnitType.Star);
            base.RowDefinitions.Add(r0);
            base.RowDefinitions.Add(r1);
            base.RowDefinitions.Add(r2);
        }
         void EllipseDef (Ellipse e)
        {
                // ;
            ImageBrush x = new ImageBrush();
            x.ImageSource = (new BitmapImage(new Uri(@"ms-appx:///Assets/cpper.jpg")));
            e.Fill = x;
            e.HorizontalAlignment = HorizontalAlignment.Stretch;
            e.VerticalAlignment = VerticalAlignment.Stretch;
            Grid.SetColumn(e, 0);
            Grid.SetColumnSpan(e, 3);
            Grid.SetRow(e, 0);
            Grid.SetRowSpan(e, 3);
            e.Tapped += card_Tapped;
            
        }
         Image i0_def(BitmapImage source)
         {
             Image i0 = new Image();
             Grid.SetColumn(i0, 1);
             Grid.SetRow(i0, 0);
             Grid.SetRowSpan(i0, 3);
             i0.HorizontalAlignment = HorizontalAlignment.Stretch;
             i0.VerticalAlignment = VerticalAlignment.Stretch;
             i0.Margin = new Thickness(8, 10, 8, 10);
             i0.Source = source;
             return i0;
         }
         Image i1_def(BitmapImage source)
         {
             Image i0 = new Image();
             Grid.SetColumn(i0, 0);
             Grid.SetRow(i0, 1);
             i0.HorizontalAlignment = HorizontalAlignment.Right;
             i0.VerticalAlignment = VerticalAlignment.Stretch;
             i0.Source = source;
             return i0;
         }
         Image i2_def(BitmapImage source)
         {
             Image i0 = new Image();
             Grid.SetColumn(i0, 3);
             Grid.SetRow(i0, 1);
             i0.HorizontalAlignment = HorizontalAlignment.Left;
             i0.VerticalAlignment = VerticalAlignment.Stretch;
             i0.Source = source;
             
             return i0;
         }
         void addImage(byte i,card c)
         {
             List<Image> images = new List<Image>();
             BitmapImage source = defineSource(c);
             switch (i)
             {
                 case 1:
                     images.Add(i0_def(source));
                     break;
                 case 2:
                     images.Add(i1_def(source));
                     images.Add(i2_def(source));
                     break;
                 case 3:
                     images.Add(i0_def(source));
                     images.Add(i1_def(source));
                     images.Add(i2_def(source));
                     break;
             }
             foreach (Image item in images)
             {
                 base.Children.Add(item);
                 item.Tapped += card_Tapped;
             }
         }
         BitmapImage defineSource(card c)
         {
             string s = c.color.ToString() + c.texture + c.shape + ".png";
             BitmapImage source = new BitmapImage(new Uri(@"ms-appx:///Assets/" + s));

             return source;
         }


         void card_Tapped(object sender, TappedRoutedEventArgs e)
         {
             if (!this.isHit)
             {
                 this.isHit = true;
                 karakib.PlayTimeSet.Add(this);
                 base.Opacity = 0.5;
                 if (karakib.PlayTimeSet.Count == 3)
                 {
                     if (SETfinder.isSet(karakib.ArrayToSet(karakib.PlayTimeSet)))
                     {
                         foreach (CARD item in karakib.PlayTimeSet)
                         {
                             Grid G = (Grid)item.Parent;
                             G.Children.Remove(item);
                             Point x = new Point(Grid.GetRow(item), Grid.GetColumn(item));
                             karakib.d.places.Enqueue(x);
                             karakib.d.table.Remove(item);
                         }
                         karakib.d.turn();
                     }
                     else
                     {
                         foreach (var item in karakib.PlayTimeSet)
                         {
                             item.Opacity = 1;
                             item.isHit = false;
                         }
                         
                     }
                     karakib.EmptyList();
                 }

             }
             else
             {
                 this.Opacity = 1;
                 this.isHit = false;
                 karakib.PlayTimeSet.Remove(this);
             }
         }
    }
   
}
