using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace ParkolasBeadandoPappMate
{
    public static class IO_Class
    {
		static string fajlHelye = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Rendszamok.txt");
		public static void RendszamokMentese()
		{
			File.WriteAllLines(fajlHelye, Globals.rendszamok);
		}

		public static void RendszamokBetoltese()
		{
			if (File.Exists(fajlHelye))
			{
				Globals.rendszamok.Clear();
				Globals.rendszamok = new ObservableCollection<string>(File.ReadAllLines(fajlHelye));
			}
		}
	}
}
