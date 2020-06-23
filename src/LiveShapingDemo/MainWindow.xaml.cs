#define CSORT
// comment out the above line to use ListCollectionView.SortDescriptions instead of ListCollectionView.CustomSort
using System;
using System.Collections;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
#if !CSORT
using System.ComponentModel;
#endif

namespace LiveShapingDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private
        private readonly CustomerCollection customers;
        private readonly Random rand;
        private readonly DispatcherTimer timer;
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            rand = new Random();
            customers = CustomerCollection.Create();

            ListView1 = new ListCollectionView(customers)
            {
                IsLiveFiltering = true,
                IsLiveSorting = true,
                Filter = (item) => item is Customer customer && customer.Balance < 250000,
                CustomSort = new CustomerComparer()
            };

            ListView2 = new ListCollectionView(customers)
            {
                IsLiveFiltering = true,
                IsLiveSorting = true,
                Filter = (item) => item is Customer customer && customer.Balance >= 250000,
                CustomSort = new CustomerComparer()
            };

            /*
             * If you use live sorting with SortDescriptions, you don't need to put the property names in LiveSortingProperties
             * If you use live sorting with CustomSort, you must put the property names in LiveSortingProperties
             * 
             * Either way you go, the underlying data object (in this case, Customer) must implement INotifyPropertyChanged.
             */

            ListView1.LiveFilteringProperties.Add(nameof(Customer.Balance));
            ListView2.LiveFilteringProperties.Add(nameof(Customer.Balance));

#if CSORT
            ListView1.LiveSortingProperties.Add(nameof(Customer.Balance));
            ListView2.LiveSortingProperties.Add(nameof(Customer.Balance));
#else
            // CustomSort was set when creating ListView (above), but gets set to null internally when adding SortDescriptions
            ListView1.SortDescriptions.Add(new SortDescription(nameof(Customer.Balance), ListSortDirection.Ascending));
            ListView2.SortDescriptions.Add(new SortDescription(nameof(Customer.Balance), ListSortDirection.Ascending));
#endif
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(250),
            };
            timer.Tick += TimerTick;

        }
        #endregion

        #region Properties
        public ListCollectionView ListView1
        {
            get;
        }

        public ListCollectionView ListView2
        {
            get;
        }
        #endregion

        #region Private methods
        private void ButtonStartClick(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                ButtonStart.Content = "Start";
                timer.Stop();
            }
            else
            {
                ButtonStart.Content = "Stop";
                timer.Start();
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            int idx = rand.Next(0, customers.Count);
            customers[idx].Balance = rand.Next(Customer.MinBalance, Customer.MaxBalance + 1);
        }
        #endregion

        #region Private helper class
        private class CustomerComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x is Customer s1 && y is Customer s2)
                {
                    return s1.Balance.CompareTo(s2.Balance);
                }
                return 0;
            }
        }
        #endregion
    }
}