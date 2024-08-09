using MySqlConnector;
using ParkolasBeadandoPappMate.Models;
using System;

namespace ParkolasBeadandoPappMate
{
	public static class SQL_Kezelo
	{
		static string kapcsolatString = "Server=mysql.nethely.hu;Database=parkingapp;User Id=parkingapp;Password=prooktatas2024;";
		static MySqlConnection kapcsolat = new MySqlConnection(kapcsolatString);

		public static void ParkolasBevitel(Parkolas ujParkolas)
		{
			string query = "INSERT INTO `parkolasok_pmz` (Rendszam, ParkolasKezdete) VALUES (@rendszam, @parkolaskezdete)";

			try
			{
				kapcsolat.Open();

				using (MySqlCommand cmd = new MySqlCommand(query, kapcsolat))
				{
					cmd.Parameters.AddWithValue("@rendszam", ujParkolas.Rendszam);
					cmd.Parameters.AddWithValue("@parkolaskezdete", DateTime.Now);
					cmd.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Hiba történt a parkolás létrehozása közben!" + ex.Message);
			}
			finally
			{
				kapcsolat.Close();
			}
		}

		public static bool FuteParkolas()
		{
			bool vanFutoParkolas = false;
			string query = "SELECT * FROM `parkolasok_pmz` WHERE ParkolasVege IS NULL";

			try
			{
				kapcsolat.Open();

				using (MySqlCommand cmd = new MySqlCommand(query, kapcsolat))
				{
					using (MySqlDataReader dr = cmd.ExecuteReader())
					{
						if (dr.Read())
						{
							vanFutoParkolas = true;
							dr.Close();
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Hiba történt a futó parkolás vizsgálata közben!" + ex.Message);
			}
			finally
			{
				kapcsolat.Close();
			}

			return vanFutoParkolas;
		}

		public static Parkolas ParkolasLekeres()
		{
			Parkolas futoParkolas = null;

			string query = "SELECT * FROM `parkolasok_pmz` WHERE ParkolasVege IS NULL LIMIT 1";

			try
			{
				kapcsolat.Open();

				using (MySqlCommand cmd = new MySqlCommand(query, kapcsolat))
				{
					using (MySqlDataReader dr = cmd.ExecuteReader())
					{
						if (dr.Read())
						{
							int id = dr.GetInt32(0);
							DateTime kezdete = dr.GetDateTime(1);
							string rendszam = dr.GetString(3);
							futoParkolas = new Parkolas(id, kezdete, rendszam);
						}
					}
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine("Hiba történt a futó parkolás lekérése közben!" + ex.Message);
			}
			finally
			{
				kapcsolat.Close();
			}

			return futoParkolas;
		}

		public static void ParkolasLezaras(Parkolas lezarandoParkolas)
		{
			string query = "UPDATE `parkolasok_pmz` SET ParkolasVege = @parkolasvege, Ar=@ar WHERE Id = @id";

			try
			{
				kapcsolat.Open();

				using (MySqlCommand cmd = new MySqlCommand(query, kapcsolat))
				{
					cmd.Parameters.AddWithValue("@parkolasvege", lezarandoParkolas.ParkolasVege);
					cmd.Parameters.AddWithValue("@ar", lezarandoParkolas.Ar);
					cmd.Parameters.AddWithValue("@id", lezarandoParkolas.Id);
					cmd.ExecuteNonQuery();
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine("Hiba történt a parkolás lezárása során!" + ex.Message);
			}
			finally
			{
				kapcsolat.Close();
			}
		}
	}
}
