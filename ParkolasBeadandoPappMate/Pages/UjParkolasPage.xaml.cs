using ParkolasBeadandoPappMate.Models;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParkolasBeadandoPappMate.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UjParkolasPage : ContentPage
	{
		Parkolas AktualisParkolas { get; set; }
		string KivalasztottRendszam { get; set; }
		public UjParkolasPage()
		{
			InitializeComponent();

			IO_Class.RendszamokBetoltese();
			RendszamokCV.ItemsSource = Globals.rendszamok;
			ParkolasFrissitese();
			Device.StartTimer(TimeSpan.FromSeconds(5), () =>
			{
				ParkolasFrissitese();
				return true;
			});
		}

		private void ParkolasFrissitese()
		{
			AktualisParkolas = SQL_Kezelo.ParkolasLekeres();
			if (AktualisParkolas != null)
			{
				FutoParkolasGrid.IsVisible = true;
				TimeSpan parkolasIdotartama = DateTime.Now - AktualisParkolas.ParkolasKezdete;
				int parkolasAra =(int)Math.Round(parkolasIdotartama.TotalMinutes * Globals.percDij);
				AktualisParkolasAdatokLabel.Text = $"Aktuális parkolás: {AktualisParkolas.Rendszam}\n {parkolasIdotartama.Minutes} perc, " +
					$"{parkolasIdotartama.Seconds} másodperc, \nÁra: {parkolasAra} Ft";
			}
			else
			{
				FutoParkolasGrid.IsVisible = false;
			}
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (Globals.rendszamok.Count != 0)
			{
				UjRendszamStack.IsVisible = false;
			}
		}

		private void RendszamokCV_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.CurrentSelection.FirstOrDefault() != null)
			{
				KivalasztottRendszam = e.CurrentSelection.FirstOrDefault().ToString();
			}
		}

		private void ParkolasGomb_Clicked(object sender, EventArgs e)
		{
			if (KivalasztottRendszam != null && !SQL_Kezelo.FuteParkolas())
			{
				Parkolas ujParkolas = new Parkolas(KivalasztottRendszam);
				SQL_Kezelo.ParkolasBevitel(ujParkolas);
				DisplayAlert("Sikeres", "Parkolás sikeresen megkezdve!", "OK");
			}
			else if (KivalasztottRendszam == null)
			{
				DisplayAlert("Hiba", "Kérem válasszon ki egy rendszámot a parkolás elindításához!", "OK");
			}
			else
			{
				DisplayAlert("Hiba", "Jelenleg már van egy futó parkolása!", "OK");
			}
		}

		private void RendszamHozzaadasGomb_Clicked(object sender, EventArgs e)
		{
			Navigation.PopModalAsync();
			Navigation.PushModalAsync(new RendszamokPage());
		}

		private void ParkolasLeallitasGomb_Clicked(object sender, EventArgs e)
		{
			if (AktualisParkolas != null)
			{
				AktualisParkolas.ParkolasVege = DateTime.Now;
				AktualisParkolas.KoltsegSzamitas();
				int kerekitettAr = (int)Math.Round(AktualisParkolas.Ar);
				SQL_Kezelo.ParkolasLezaras(AktualisParkolas);
				DisplayAlert("Sikeres", $" Parkolás lezárva! \n {AktualisParkolas.Rendszam} \n Kezdete: {AktualisParkolas.ParkolasKezdete} \n " +
					$"Vége: {AktualisParkolas.ParkolasVege} \n Ára: {kerekitettAr} ft", "OK");
				AktualisParkolas = null;
				ParkolasFrissitese();
			}
			else
			{
				DisplayAlert("Hiba", "Nincs aktuális parkolás!", "OK");
			}
		}

		//VISSZA A FŐOLDALRA
		private void Button_Clicked(object sender, EventArgs e)
		{
			Navigation.PopModalAsync();
		}
	}
}