using System;
using Gtk;
using System.Data;

using SerpisAd;
using PArticulo;

public partial class MainWindow: Gtk.Window
{	
	private IDbConnection dbConnection;
	private ListStore listStore;
	

	
		public MainWindow (): base (Gtk.WindowType.Toplevel)
		{
			Build ();

			deleteAction.Sensitive = false;
			editAction.Sensitive = false;
			dbConnection = App.Instance.DbConnection;
			treeView2.AppendColumn ("id", new CellRendererText (), "text", 0);
			treeView2.AppendColumn ("nombre", new CellRendererText (), "text", 1);
			listStore = new ListStore (typeof(ulong), typeof(string));
			treeView2.Model = listStore;
			fillListStore ();
			treeView.Selection.Changed += selectionChanged;


		}
		private void selectionChanged (object sender, EventArgs e) {
			Console.WriteLine ("selectionChanged");
			bool hasSelected = treeView.Selection.CountSelectedRows () > 0;
			deleteAction.Sensitive = hasSelected;
			editAction.Sensitive = hasSelected;
		}
		private void fillListStore() {
			IDbCommand dbCommand = dbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from categoria";
			IDataReader dataReader = dbCommand.ExecuteReader ();
			while (dataReader.Read()) {
				object id = dataReader ["id"];
				object nombre = dataReader ["nombre"];
				listStore.AppendValues (id, nombre);
			}
			dataReader.Close ();
		}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	protected void OnDeleteActionActivated (object sender, EventArgs e)
	{
		throw new NotImplementedException ();
	}
	

	
	protected void OnEditActionActivated (object sender, EventArgs e)
	{
		throw new NotImplementedException ();
	}


	protected void OnNewActionActivated (object sender, EventArgs e)
	{
		throw new NotImplementedException ();
	}

	
	protected void OnRefreshActionActivated (object sender, EventArgs e)
	{
		throw new NotImplementedException ();
	}
	
}