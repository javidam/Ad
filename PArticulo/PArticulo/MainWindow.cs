using System;
using Gtk;
using System.Data;

using SerpisAd;
using PArticulo;

public partial class MainWindow: Gtk.Window
{	
	private IDbConnection dbConnection;
	private ListStore listStore, listStore2;
	

	
		public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		deleteAction.Sensitive = false;
		editAction.Sensitive = false;

		Categoria ();
		Articulo ();


	}

		void Categoria(){


			dbConnection = App.Instance.DbConnection;
			treeView2.AppendColumn ("id", new CellRendererText (), "text", 0);
			treeView2.AppendColumn ("nombre", new CellRendererText (), "text", 1);
			listStore2 = new ListStore (typeof(ulong), typeof(string));
			treeView2.Model = listStore2;
			fillListStore2 ();
			treeView2.Selection.Changed += selectionChanged;

		}

		void Articulo(){

			dbConnection = App.Instance.DbConnection;
			treeView.AppendColumn ("id", new CellRendererText (), "text", 0);
			treeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);
			treeView.AppendColumn ("categoria", new CellRendererText (), "text", 0);
			treeView.AppendColumn ("precio", new CellRendererText (), "text", 0);
			listStore = new ListStore (typeof(ulong), typeof(string), typeof(ulong), typeof(string));
			treeView.Model = listStore;
			fillListStore ();
			treeView.Selection.Changed += selectionChanged;
		
		
		}




		private void selectionChanged (object sender, EventArgs e) {
		Console.WriteLine ("selectionChanged");
		bool hasSelected = treeView.Selection.CountSelectedRows () > 0;
		deleteAction.Sensitive = hasSelected;
		editAction.Sensitive = hasSelected;
	}

		private void fillListStore2() {
			IDbCommand dbCommand = dbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from categoria";
			IDataReader dataReader = dbCommand.ExecuteReader ();
			while (dataReader.Read()) {
				object id = dataReader ["id"];
				object nombre = dataReader ["nombre"];
				listStore2.AppendValues (id, nombre);
			}
			dataReader.Close ();
		}

	private void fillListStore(){
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = "select *  from Articulo";
		IDataReader dataReader = dbCommand.ExecuteReader ();
		while(dataReader.Read()){
			object id = dataReader ["id"];
			object nombre = dataReader ["nombre"];
			object categoria = dataReader ["categoria"];
			object precio = dataReader ["precio"].ToString();
			listStore.AppendValues (id, nombre, categoria, precio);
		}
		dataReader.Close ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		dbConnection.Close ();
		Application.Quit ();
		a.RetVal = true;
	}
	protected void OnDeleteActionActivated (object sender, EventArgs e)
	{
		MessageDialog messageDialog = new MessageDialog (
			this,
			DialogFlags.Modal,
			MessageType.Question,
			ButtonsType.YesNo,
			"¿Quieres eliminar el registro?"
			);
		messageDialog.Title = Title;
		ResponseType response = (ResponseType) messageDialog.Run ();
		messageDialog.Destroy ();
		TreeIter treeIter;
		if (response != ResponseType.Yes)
			return;

		if (treeView.Selection.GetSelected (out treeIter)) {
			object id = listStore.GetValue (treeIter, 0);
			string deleteSql = string.Format ("delete from articulo where id={0}", id);
			IDbCommand dbCommand = dbConnection.CreateCommand ();
			dbCommand.CommandText = deleteSql;
			dbCommand.ExecuteNonQuery ();

		} else {treeView2.Selection.GetSelected (out treeIter);
				object id = listStore2.GetValue (treeIter, 0);
				string deleteSql = string.Format ("delete from categoria where id={0}", id);
				IDbCommand dbCommand = dbConnection.CreateCommand ();
				dbCommand.CommandText = deleteSql;


		}


	}
	

	
	protected void OnEditActionActivated (object sender, EventArgs e)
		{
			TreeIter treeIter;
			treeView.Selection.GetSelected (out treeIter);
			object id = listStore.GetValue (treeIter, 0);
			ArticuloView articuloView = new ArticuloView (id);

		}
	


	protected void OnNewActionActivated (object sender, EventArgs e)
	{
		string insertSql = string.Format(
			"insert into articulo (nombre) values ('{0}')",
			"Nuevo " + DateTime.Now
			);
		Console.WriteLine ("insertSql={0}", insertSql);
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = insertSql;

		dbCommand.ExecuteNonQuery ();
	}

	
	protected void OnRefreshActionActivated (object sender, EventArgs e)
	{
		listStore.Clear ();
		fillListStore ();
		fillListStore2 ();
	}
	
}