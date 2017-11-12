using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin_Test
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            InitializeComponent();
            BindingContext = BillModelHandler.BillModel;
        }

        public void OnNextClick(object sender, EventArgs e)
        {
            App.Current.MainPage = new CalculatorPage();
        }
    }
}
