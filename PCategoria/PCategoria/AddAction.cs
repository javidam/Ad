using System;
using Gtk;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using PCategoria;

namespace PCategoria
{
	public partial class AddAction : Gtk.Window
	{
		
		private MySqlConnection mySqlConnection;
		
		public AddAction () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		
			aceptarButton.Activated += delegate {
						
			String nombre = entry.Text;		
			
			mySqlConnection = new MySqlConnection("Server=localhost;Database=dboctubre;User Id=root;Password=sistemas");
			mySqlConnection.Open ();
			MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
			mySqlCommand.CommandText = 	"insert into categoria (nombre) values ('" + nombre + "')" ;
			MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
		
			mySqlDataReader.Close ();
		};
		
			cancelarButton.Activated += delegate {
				Destroy();
				
			};
			
		
		
		}
	}
}

