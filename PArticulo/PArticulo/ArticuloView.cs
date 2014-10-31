using System;
using System.Data;

using SerpisAd;


namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		private object id;
		public ArticuloView () : base(Gtk.WindowType.Toplevel) {
			this.Build ();
		}
		public ArticuloView(object id) : this() {
			this.id = id;
			IDbCommand dbCommand =
				App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = String.Format (
				"select * from articulo where id={0}", id
				);
			IDataReader dataReader = dbCommand.ExecuteReader ();
			dataReader.Read ();
			entryId.Text = dataReader ["id"].ToString ();
			entryNombre.Text = dataReader ["nombre"].ToString ();
			entryCategoria.Text = dataReader ["categoria"].ToString ();
			entryPrecio.Text = dataReader ["precio"].ToString ();

			dataReader.Close ();
		}
		protected void OnSaveActionActivated (object sender, EventArgs e)
		{

				IDbCommand dbCommand =
					App.Instance.DbConnection.CreateCommand ();
				dbCommand.CommandText = String.Format (
					"update categoria set nombre=@nombre where id={0}", id
					);

			dbCommand.AddParameter ("nombre", entryNombre.Text);
			dbCommand.AddParameter ("categoria", entryNombre.Text);
			dbCommand.AddParameter ("precio", entryNombre.Text);
				dbCommand.ExecuteNonQuery ();
				Destroy ();

		}


	}
}

