using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Test
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalculatorPage : ContentPage
	{
        public CalculatorPage()
        {
            InitializeComponent();
            BindingContext = BillModelHandler.BillModel;
        }

        public void OnBackClick(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }
    }
}