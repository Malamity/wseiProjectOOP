using ConnectDataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace wpf_semestralny
{
    /// <summary>
    /// Logika interakcji dla klasy Performances.xaml
    /// </summary>
    public partial class Performances : Window
    {
        public Performances()
        {
            InitializeComponent();

            ViewData();
        }

        private List<PerformanceInfo> Employer_Info = new List<PerformanceInfo>();
        internal static int ID { get; private set; }

        private void ViewData()
        {
            using var db = new UsersDB();
            Employer_Info = db.Performance.Select(a => new PerformanceInfo(a)).ToList();
            var pracownicy = db.Employers.Select(a => a.Username).ToList();
            var wyposazenie = db.Items.Select(a => a.Item_name).ToList();

            ListaPracownikow.ItemsSource = pracownicy;
            Wystepy.ItemsSource = Employer_Info;
        }

        private void DeleteUser(object o, EventArgs e)
        {
            var id = (int)((Button)o).CommandParameter;

            var performance = Employer_Info.FirstOrDefault(a => a.ID == id);
            Employer_Info.Remove(performance);

            using var db = new UsersDB();
            db.Performance.Remove(performance.Performance);

            db.SaveChanges();

            Wystepy.ItemsSource = null;
            Wystepy.ItemsSource = Employer_Info;
        }

        class PerformanceInfo
        {
            /// <summary>
            /// Pobieranie Performance z bazy danych
            /// </summary>
            public Performance Performance { get; private set; }

            /// <summary>
            /// ID otrzymuje wartość Primarykey Performance
            /// </summary>
            public int ID
            {
                get => Performance.Performance_id;
            }
            /// <summary>
            /// Przypisanie pracownika do obsługi Koncertu
            /// </summary>
            public string Pracownik
            {
                get
                {
                    var a = Performance.Performance_staff.FirstOrDefault();
                    if (a is null)
                        return "";
                    return a.Employers.Username;
                }
                set
                {
                    using var db = new UsersDB();
                    var prac = db.Employers.Where(a => a.Username == value).First();
                    var a = Performance.Performance_staff.FirstOrDefault(a => a.Performance_id == Performance.Performance_id);

                    if (a is null)
                    {
                        var b = new Performance_staff()
                        {
                            Employer_id = prac.Employer_id,
                            Performance_id = Performance.Performance_id
                        };
                        db.Performance_staff.Add(b);
                    }
                    else
                    {
                        db.Performance_staff.Attach(a);
                        a.Employer_id = prac.Employer_id;
                    }
                    db.SaveChanges();
                    refresh();
                }
            }
            /// <summary>
            /// Dodanie nazwy koncertu z wyjątkami w Regex
            /// </summary>
            public string Name
            {
                get => Performance.Performance_name;
                set
                {
                    using var db = new UsersDB();
                    db.Performance.Attach(Performance);
                    if (!Regex.IsMatch(value, "^[A-Z][a-m]*"))
                        return;
                    Performance.Performance_name = value;
                    db.SaveChanges();
                }
            }
            /// <summary>
            /// Dodanie kosztu koncertu
            /// </summary>
            public decimal Cost
            {
                get => Performance.Performace_visit_cost;
                set
                {
                    using var db = new UsersDB();
                    db.Performance.Attach(Performance);

                    if (value < 0)
                        return;
                    Performance.Performace_visit_cost = value;
                    db.SaveChanges();
                }
            }
            /// <summary>
            /// Dodanie daty koncertu
            /// </summary>
            public System.DateTime Date
            {
                get => Performance.Performance_Date;
                set
                {
                    using var db = new UsersDB();
                    db.Performance.Attach(Performance);

                    Performance.Performance_Date = value;
                    db.SaveChanges();
                }
            }

            private void refresh()
            {
                using var db = new UsersDB();
                Performance = db.Performance.Include(a => a.Performance_staff).ThenInclude(a => a.Employers)
                     .First(a => a.Performance_id == Performance.Performance_id);

                Console.WriteLine(123);
            }
            /// <summary>
            /// Przykładowy koncert(PerformanceInfo)
            /// </summary>
            public PerformanceInfo()
            {
                using var db = new UsersDB();

                Performance = new Performance()
                {
                    Performance_name = "Koncert",
                    Performace_visit_cost = 10,
                    Performance_Date = DateTime.Now + new TimeSpan(24, 0, 0)
                };

                db.Performance.Add(Performance);
                db.SaveChanges();

                refresh();
            }
            /// <summary>
            /// Odświeżenie danych w Performance
            /// </summary>
            public PerformanceInfo(Performance performance)
            {
                Performance = performance;
                refresh()
                ;
            }
          
        }
        private void btnBack1_Click(object sender, RoutedEventArgs e)
        {
            new Dashboard().Show();
            this.Close();

        }
    }
}
