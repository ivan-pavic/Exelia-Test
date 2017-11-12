using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace Xamarin_Test
{
    public class BillModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private double billAmount = 0.0;

        private int selectedNumberOfPeople = 1;

        private double tip = 0;

        private int totalPerPerson = 0;

        public IList<int> NumberOfPeopleList
        {
            get
            {
                return Enumerable.Range(1, 10).ToList();
            }
        }

        public int SelectedNumberOfPeople
        {
            get
            {
                return this.selectedNumberOfPeople;
            }
            set
            {
                this.selectedNumberOfPeople = value;
                OnPropertyChanged("TotalPerPerson");
            }
        }

        public double BillAmount
        {
            get
            {
                return this.billAmount;
            }
            set
            {
                this.billAmount = value;
            }
        }

        public double TipAmmount
        {
            get
            {
                return this.tip;
            }
            set
            {
                double newTip = Math.Round(value, 2);

                if (this.tip != newTip)
                {
                    this.tip = newTip;
                    OnPropertyChanged("TipPercent");
                    OnPropertyChanged("TotalPerPerson");
                }

            }
        }

        public double TipPercent
        {
            get
            {
                if (this.tip != 0)
                    return this.tip * 100 / this.billAmount;
                return
                    0;
            }
            set
            {
                double newTip = Math.Round(value * this.billAmount / 100, 2);
                if (this.tip != newTip)
                {
                    this.tip = newTip;
                    OnPropertyChanged("TipAmmount");
                    OnPropertyChanged("TotalPerPerson");
                }

            }
        }

        public double TotalPerPerson
        {
            get
            {
                return Math.Round((this.billAmount + this.tip) / this.selectedNumberOfPeople, 2);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
