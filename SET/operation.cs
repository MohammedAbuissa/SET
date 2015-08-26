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

namespace SET
{
    static class karakib
    {
        public static dealer d;
        public static Byte count;
        public static List<CARD> PlayTimeSet;
        public static set ArrayToSet(List<CARD> c)
        {
            set s = new set();
            s.a =(card) c[0].Tag;
            s.b = (card)c[1].Tag;
            s.c = (card)c[2].Tag;
            return s;
        }
        public static void EmptyList()
        {
            while (karakib.PlayTimeSet.Count > 0)
            {
                CARD c = karakib.PlayTimeSet[0];
                karakib.PlayTimeSet.Remove(c);
            }
        }
        static karakib()
        {
            count = 0;
            PlayTimeSet = new  List<CARD>(3);
        }
    }
    class dealer
    {
        public Boolean IsFirstTurn { get; set; }
        public List<CARD> table = new List<CARD>(15);
        public Queue<Point> places { get; set; }
        public dealer()
        {
            places = new Queue<Point>();
            placesDEf();
            DeckCreation.Create();
            FirstTurn();
            turn();
        }
      public  void turn()
        {
            while (!SETfinder.set_finder(table))
            {
                addCARD();
            }
        }
        void placesDEf()
        {
            byte k = 0;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 3; j++, k++)
                {
                    places.Enqueue(new Point(i, j));
                }
        }
        void FirstTurn()
        {
            for (Byte i = 0; i < 12; i++)
            {
                addCARD();
            }
        }
        void addCARD()
        {
            CARD x ;
            if (DeckCreation.GameCards.Count > 0)
            {
                Point y;
                x = DeckCreation.GameCards.Dequeue();
                if (places.Count > 0)
                    y = places.Dequeue();
                else
                {
                    MessageDialog m = new MessageDialog("Too many cards", "stop");
                    m.ShowAsync();
                }
                Grid.SetColumn(x, (int)y.Y);
                Grid.SetRow(x, (int)y.X);
                mainScene.PlayArea.Children.Add(x);
                table.Add(x);
            }
            else
            {
                mainScene.GameTime.Stop();
            }
             
        }
        
    }
   static class DeckCreation
    {
       static public Queue<CARD> GameCards = new Queue<CARD>(81);
     static   public card[] deck = new card[81];
     static private char[] shapes = new char[] { 'D', 'O', 'C' };
     static private char[] colors = new char[] { 'P', 'G', 'R' };
     static private char[] textures = new char[] { 'L', 'S', 'H' };
     static private byte[] numbers = new byte[] { 1, 2, 3 };
     static private List<int> box = new List<int>();
     static public void Create()
        {
            for (byte i = 0; i < 3; i++)
                for (byte j = 0; j < 3; j++)
                    for (byte k = 0; k < 3; k++)
                        for (byte l = 0; l < 3; l++)
                        {
                        k: Random x = new Random();
                            int y = x.Next(81);
                            if (box.Contains(y)) goto k;
                            else 
                                box.Add(y);
                            deck[y] = new card { color = colors[i], number = numbers[j], shape = shapes[k], texture = textures[l] };
                        }
            ConvertToCARD();
        }
       static private void ConvertToCARD ()
       {
           foreach (var item in deck)
           {
               GameCards.Enqueue(new CARD(item));
           }
       }
    }
   public struct card
    {
        public char shape;
        public char color;
        public byte number;
        public char texture;
    }

    struct set
    {
        public card a, b, c;
    }
 static   class SETfinder
    {
  
    static public Boolean set_finder(List<CARD> table)
        {
            Boolean solution = false;
            for (int i = 0; i < table.Count - 2; i++)
                for (int j = i + 1; j < table.Count - 1; j++)
                    for (int k = j + 1; k < table.Count; k++)
                    {
                        set s = new set() { a = (card)table[i].Tag, b = (card)table[j].Tag, c = (card)table[k].Tag };

                        if (isSet(s))
                        {
                            solution = true;
                            goto k;
                        }
                           
                    }
        k:  return solution;
        }

    static    public bool isSet(set s)
        {
            bool q2 = s.a.number != s.b.number && s.b.number != s.c.number && s.a.number != s.c.number;
            bool q1 = s.a.number == s.b.number && s.b.number == s.c.number && s.a.number == s.c.number;
            bool q = q1 || q2;
            bool w2 = s.a.color != s.b.color && s.b.color != s.c.color && s.a.color != s.c.color;
            bool w1 = s.a.color == s.b.color && s.b.color == s.c.color && s.a.color == s.c.color;
            bool w = w1 || w2;
            bool e2 = s.a.shape != s.b.shape && s.b.shape != s.c.shape && s.a.shape != s.c.shape;
            bool e1 = s.a.shape == s.b.shape && s.b.shape == s.c.shape && s.a.shape == s.c.shape;
            bool e = e1 || e2;
            bool r2 = s.a.texture != s.b.texture && s.b.texture != s.c.texture && s.a.texture != s.c.texture;
            bool r1 = s.a.texture == s.b.texture && s.b.texture == s.c.texture && s.a.texture == s.c.texture;
            bool r = r1 || r2;
            return q && w && e && r;
        }
    }
}
