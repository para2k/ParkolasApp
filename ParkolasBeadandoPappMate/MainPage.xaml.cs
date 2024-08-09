using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ParkolasBeadandoPappMate.Pages;

namespace ParkolasBeadandoPappMate
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private async void RendszamBevitelButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new RendszamokPage());
		}

		private async void UjParkolasButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new UjParkolasPage());
        }
    }
}
