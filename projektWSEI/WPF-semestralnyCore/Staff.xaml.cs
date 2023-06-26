using ConnectDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace wpf_semestralny
{
    /// <summary>
    /// Logika interakcji dla klasy Staff.xaml
    /// </summary>
    public partial class Staff : Window
    {
        /// <summary>
        /// Okienko z listą pracowników
        /// </summary>
        public Staff()
        {
            InitializeComponent();

            ViewData();
        }

        private List<EmployerInfo> Employer_Info = new List<EmployerInfo>();

        private void ViewData()
        {
            using var db = new UsersDB();
            Employer_Info = db.Employers.Select(a => new EmployerInfo(a)).ToList();

            Pracownicy.ItemsSource = Employer_Info;
        }

        private void DeleteUser(object o, EventArgs e)
        {
            var id = (int)((Button)o).CommandParameter;

            var employer = Employer_Info.FirstOrDefault(a => a.Employer_id == id);
            Employer_Info.Remove(employer);

            using var db = new UsersDB();
            db.Employers.Remove(employer.Employer);

            db.SaveChanges();

            Pracownicy.ItemsSource = null;
            Pracownicy.ItemsSource = Employer_Info;
        }

        class EmployerInfo
        {
            /// <summary>
            /// Pracownicy
            /// </summary>
            public Employers Employer { get; }

            public int Employer_id
            {
                get => Employer.Employer_id;
            }
            /// <summary>
            /// Pobranie/Dodanie Imienia Pracownika
            /// </summary>
            public string Employer_name
            {
                get => Employer.Employer_name;
                set
                {
                    using var db = new UsersDB();
                    db.Employers.Attach(Employer);
                    if (!Regex.IsMatch(value, "^[A-Z][a-m]*"))
                        return;
                    Employer.Employer_name = value;
                    db.SaveChanges();
                }
            }
            /// <summary>
            /// Pobranie/Dodanie Nazwiska Pracownika
            /// </summary>
            public string Employer_last_name
            {
                get => Employer.Employer_last_name;
                set
                {
                    using var db = new UsersDB();
                    db.Employers.Attach(Employer);
                    if (!Regex.IsMatch(value, "^[A-Z][a-m]*"))
                        return;
                    Employer.Employer_last_name = value;
                    db.SaveChanges();
                }

            }
            /// <summary>
            /// Pobranie/Dodanie Daty zatrudnienia Pracownika
            /// </summary>
            public System.DateTime Employment_date
            {
                get => Employer.Employment_date;
                set
                {
                    using var db = new UsersDB();
                    db.Employers.Attach(Employer);

                    Employer.Employment_date = value;
                    db.SaveChanges();
                }
            }
            /// <summary>
            /// Pobranie/Dodanie Hasła Pracownika
            /// </summary>
            public string Password
            {
                get => Employer.Password;
                set
                {
                    using var db = new UsersDB();
                    db.Employers.Attach(Employer);
                    if (value.Length < 3)
                        return;
                    Employer.Password = value;
                    db.SaveChanges();
                }
            }
            /// <summary>
            /// Pobranie/Dodanie Nazwy użytkownika Pracownika
            /// </summary>
            public string Username
            {
                get => Employer.Username;
                set
                {
                    using var db = new UsersDB();
                    db.Employers.Attach(Employer);
                    if (value.Length < 3)
                        return;
                    if (CheckAll(value))
                        return;
                    Employer.Username = value;
                    db.SaveChanges();
                }
            }

            private bool CheckAll(string name)
            {
                using var db = new UsersDB();
                var a = db.Employers.Where(a => a.Username == name);

                return (a.Count() > 0);

            }
            /// <summary>
            /// Dodawanie pracownika do listy wraz z przykładowymi danymi
            /// </summary>
            public EmployerInfo()
            {
                using var db = new UsersDB();

                Employer = new Employers()
                {
                    Employer_name = "Jan",
                    Employer_last_name = "Nowak",
                    Employment_date = DateTime.Now,
                    Username = "User",
                    Password = "Password"
                };

                for(int i = 0; CheckAll(Employer.Username); i++)
                {
                    Employer.Username = "User_" + i.ToString();
                }

                db.Employers.Add(Employer);
                db.SaveChanges();
            }
            /// <summary>
            /// Odświeżenie danych w Employer
            /// </summary>
            public EmployerInfo(Employers employer)
            {
                Employer = employer;
            }
            
        }
        private void btnBack2_Click(object sender, RoutedEventArgs e)
        {
            new Dashboard().Show();
            this.Close();

        }
    }
}
