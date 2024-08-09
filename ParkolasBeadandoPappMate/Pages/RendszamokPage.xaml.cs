using System;
using System.Text.RegularExpressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParkolasBeadandoPappMate.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RendszamokPage : ContentPage
	{
		public RendszamokPage()
		{
			InitializeComponent();
			IO_Class.RendszamokBetoltese();
			KomponensFrissites();
		}

		private void RendszamHozzaadasGomb_Clicked(object sender, EventArgs e)
		{
			if (UjRendszamBevitelEntry.Text == string.Empty) return;

			string ujRendszam = UjRendszamBevitelEntry.Text.ToUpper().Trim();
			if (RendszamValidacio(ujRendszam))
			{
				Globals.rendszamok.Add(ujRendszam);
				IO_Class.RendszamokMentese();
				IO_Class.RendszamokBetoltese();
				KomponensFrissites();
			}
			else
			{
				DisplayAlert("Hiba", "Hibás rendszám formátum (4 betű 3 szám, vagy 3 betű 3 szám)", "OK");
			}

			UjRendszamBevitelEntry.Text = string.Empty;
		}

		private void RendszamTorlesGomb_Clicked(object sender, EventArgs e)
		{
			if (sender is Button button && button.CommandParameter is string rendszam)
			{
				Globals.rendszamok.Remove(rendszam);
				IO_Class.RendszamokMentese();
				IO_Class.RendszamokBetoltese();
				KomponensFrissites();
			}
		}

		void RendszamDarabszamLabelFrissites()
		{
			RendszamDarabszamLabel.Text = $"Elmentett rendszámok száma: {Globals.rendszamok.Count} db";
		}

		void KomponensFrissites()
		{
			RendszamokCV.ItemsSource = Globals.rendszamok;
			RendszamDarabszamLabelFrissites();
		}

		bool RendszamValidacio(string vizsgaltRendszam)
		{
			string minta = @"^[A-Z]{4}\d{3}$|^[A-Z]{3}\d{3}$";

			return Regex.IsMatch(vizsgaltRendszam.ToUpper(), minta);
		}

		private void VisszaFooldalraGomb_Clicked(object sender, EventArgs e)
		{
			Navigation.PopModalAsync();
		}
	}
}