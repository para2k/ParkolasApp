using System;
using System.Collections.Generic;
using System.Text;

namespace ParkolasBeadandoPappMate.Models
{
    public class Parkolas
    {
		public int Id { get; set; }
		public DateTime ParkolasKezdete { get; set; }
		public DateTime ParkolasVege { get; set; }
		public string Rendszam { get; set; }
		public float Ar { get; set; }

		public Parkolas(int id, DateTime parkolasKezdete, DateTime parkolasVege, string rendszam, float ar)
		{
			Id = id;
			ParkolasKezdete = parkolasKezdete;
			ParkolasVege = parkolasVege;
			Rendszam = rendszam;
			Ar = ar;
		}

		public Parkolas(int id, DateTime parkolasKezdete, string rendszam)
		{
			Id = id;
			ParkolasKezdete = parkolasKezdete;
			Rendszam = rendszam;
		}

		public Parkolas(string rendszam)
		{
			Id = 0;
			ParkolasKezdete = DateTime.Now;
			Rendszam = rendszam;
		}

		public override string ToString()
		{
			return $"{Rendszam} - {ParkolasKezdete} - {ParkolasVege}";
		}

		public void KoltsegSzamitas()
		{
			if(ParkolasVege != null)
			{
				Ar = (float)(ParkolasVege - ParkolasKezdete).TotalMinutes * Globals.percDij;
			}
		}
	}
}
