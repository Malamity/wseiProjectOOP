using ConnectDataBase;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace wpf_semestralny
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            ViewData();
        }

        private List<WyposazenieInfo> wyposazenie = new List<WyposazenieInfo>();


        private void ViewData()
        {
            using var db = new UsersDB();
            wyposazenie = db.Items.Select(a => new WyposazenieInfo(a)).ToList();
            if (wyposazenie.Count == 0)
                wyposazenie.Add(new WyposazenieInfo());

            Wyposazenie.ItemsSource = wyposazenie;
        }

        private void DeleteUser(object o, EventArgs e)
        {
            var id = (int)((Button)o).CommandParameter;

            var item = wyposazenie.FirstOrDefault(a => a.ID == id);
            wyposazenie.Remove(item);

            using var db = new UsersDB();
            db.Items.Remove(item.Item);

            db.SaveChanges();

            Wyposazenie.ItemsSource = null;
            Wyposazenie.ItemsSource = wyposazenie;
        }

        class WyposazenieInfo
        {
            /// <summary>
            /// Pobranie listy Wyposażenia
            /// </summary>
            public Items Item { get; }

            public int ID
            {
                get => Item.Item_ID;
            }
            /// <summary>
            /// Pobranie/Dodanie Nazwy Wyposażenia
            /// </summary>
            public string ItemName
            {
                get => Item.Item_name;
                set
                {
                    using var db = new UsersDB();
                    db.Items.Attach(Item);
                    if (!Regex.IsMatch(value, "^[A-Z][a-m]*"))
                        return;
                    Item.Item_name = value;
                    db.SaveChanges();
                }
            }
            /// <summary>
            /// Pobranie/Dodanie Ilości Wyposażenia
            /// </summary>
            public int Count
            {
                get => Item.Item_Count;
                set
                {
                    using var db = new UsersDB();
                    db.Items.Attach(Item);

                    if (value < 0)
                        return;
                    Item.Item_Count = value;
                    db.SaveChanges();
                }
            }

            /// <summary>
            /// Dodanie przykładowego przedmiotu do bazy danych
            /// </summary>
            public WyposazenieInfo()
            {
                using var db = new UsersDB();

                Item = new Items()
                {
                    Item_name="Instrument",
                    Item_Count=0
                };

                db.Items.Add(Item);
                db.SaveChanges();
            }
            /// <summary>
            /// Odświeżenie danych w Wyposażeniu
            /// </summary>
            public WyposazenieInfo(Items item)
            {
                Item = item;
            }

        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            new Dashboard().Show();
            this.Close();

        }
    }
}
