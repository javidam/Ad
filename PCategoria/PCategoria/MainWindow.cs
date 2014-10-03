using System;
using System.Data;
using Gtk;
using MySql.Data;
using MySql.Data.MySqlClient;
public partial class MainWindow: Gtk.Window
{	

	private MySqlConnection mySqlConnection;
	private ListStore listStore;
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		treeView.AppendColumn ("id", new CellRendererText (), "text", 0);
		treeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);
		listStore = new ListStore (typeof(int), typeof(string));
		treeView.Model = listStore;
		var mySqlDataReader = fillListStore ();
		while (mySqlDataReader.Read()) {
			object id = mySqlDataReader["id"].ToString();
			object nombre = mySqlDataReader["nombre"];
			listStore.AppendValues (id, nombre);
		}
		mySqlDataReader.Close ();
		mySqlConnection.Close ();
	}
	MySqlDataReader fillListStore ()
	{
		mySqlConnection = new MySqlConnection ("Server=localhost; Database=dbprueba; User ID=root; Password=sistemas");
		mySqlConnection.Open ();
		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
		mySqlCommand.CommandText = "SELECT * FROM categoria";
		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();
		return mySqlDataReader;
	}
	protected void OnAddActionActivated (object sender, EventArgs e)
	{
		//listStore.AppendValues ("1", DateTime.Now.ToString());
		mySqlConnection = new MySqlConnection (
			"Server=localhost; Database=dbprueba; User ID=root; Password=sistemas");
		mySqlConnection.Open ();
		MySqlCommand mySqlCommandInsert = mySqlConnection.CreateCommand ();
		mySqlCommandInsert.CommandText = String.Format("INSERT INTO categoria (nombre) VALUES ('{0}')", DateTime.Now);
		mySqlCommandInsert.ExecuteReader();
		mySqlConnection.Close ();
	}
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		TreeIter treeIter;
		treeView.Selection.GetSelected (out treeIter);
		object id = listStore.GetValue (treeIter, 0);
		object nombre = listStore.GetValue (treeIter, 1);

		Console.WriteLine("onDeleteActionActivated id={0} nombre= {1}", id , nombre);
		Application.Quit ();
		a.RetVal = true;
	}
	protected void OnRefreshActionActivated (object sender, EventArgs e)
	{
		listStore.Clear();
		listStore = new ListStore (typeof(string), typeof(string));
		treeView.Model = listStore;

		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
		mySqlCommand.CommandText = "SELECT * FROM categoria";
		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();
		while (mySqlDataReader.Read()) {
			object id = mySqlDataReader["id"].ToString();
			object nombre = mySqlDataReader["nombre"];
			listStore.AppendValues (id, nombre);
		}
	}
}