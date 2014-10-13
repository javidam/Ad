using MySql.Data.MySqlClient;
using System;
using System.Data;
using Gtk;
using PCategoria;
public partial class MainWindow: Gtk.Window
{	
	private MySqlConnection mySqlConnection;
	private MySqlCommand mySqlCommand;
	private ListStore listStore;
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		deleteAction.Sensitive = false;
		editAction.Sensitive = false;
		treeView.AppendColumn ("id", new CellRendererText (), "text", 0);
		treeView.AppendColumn ("Nombre", new CellRendererText (), "text", 1);
		listStore = new ListStore (typeof(ulong), typeof(string));
		treeView.Model = listStore;//es un set
		leerDatosCategoria ();
		//treeView.Selection.Mode = SelectionMode.Multiple;
		treeView.Selection.Changed += delegate {
			bool hasSelected=treeView.Selection.CountSelectedRows()>0;
			deleteAction.Sensitive=hasSelected;
			editAction.Sensitive=hasSelected;
		};
	}
	protected void leerDatosCategoria ()
	{
		mySqlConnection = new MySqlConnection (
			"Server=localhost;"+
			"Database=dbprueba;"+
			"User ID=root;"+
			"Password=sistemas");
		mySqlConnection.Open ();
		mySqlCommand = mySqlConnection.CreateCommand ();
		mySqlCommand.CommandText = "select * from categoria";
		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();
		while (mySqlDataReader.Read()) {
			object id=mySqlDataReader["id"];
			Console.WriteLine ("id.GetType()={0}", id.GetType ());
			object nombre=mySqlDataReader["nombre"];
			listStore.AppendValues (id,nombre);
		}
		mySqlDataReader.Close ();
	}
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		mySqlConnection.Close ();
		Application.Quit ();
		a.RetVal = true;
	}
	protected void OnAddActionActivated (object sender, EventArgs e)
	{
		//mySqlCommand.CommandText = string.Format ("insert into categoria (nombre) values ('{0}')", DateTime.Now);
		//mySqlCommand.ExecuteNonQuery ();
		//otra forma
		string insertSql = string.Format ("insert into categoria (nombre) values ('{0}')", "Nuevo" + DateTime.Now);
		Console.WriteLine ("InsertSql={0}", insertSql);
		mySqlCommand.CommandText = insertSql;
		mySqlCommand.ExecuteNonQuery ();
	}
	protected void OnRefreshActionActivated (object sender, EventArgs e)
	{
		listStore.Clear ();
		leerDatosCategoria ();
	}
	protected void OnRemoveActionActivated (object sender, EventArgs e)
	{
		if (!ConfirmDelete ())
			return;
		TreeIter treeIter;
		treeView.Selection.GetSelected (out treeIter);
		object id=listStore.GetValue (treeIter, 0);
		object nombre=listStore.GetValue (treeIter, 1);
		Console.WriteLine (id);
		Console.WriteLine (nombre);
		string deleteSql = string.Format ("delete from categoria where id={0}",id);
		mySqlCommand.CommandText = deleteSql;
		mySqlCommand.ExecuteNonQuery ();
		listStore.Clear ();
		leerDatosCategoria ();
	}
	public bool ConfirmDelete(){
		return Confirm ("Eliminar");
	}
	public bool Confirm(string text){
		MessageDialog messageDialog = new MessageDialog (
			this,
			DialogFlags.Modal,
			MessageType.Question,
			ButtonsType.YesNo,
			text
			);
		messageDialog.Title = "Eliminar";
		ResponseType response = (ResponseType)messageDialog.Run ();
		messageDialog.Destroy ();
		return response == ResponseType.Yes;
	}
	protected void OnEditActionActivated (object sender, EventArgs e)
	{
		TreeIter treeIter;
		treeView.Selection.GetSelected (out treeIter);
		object nombre = listStore.GetValue (treeIter, 0);
		CategoriaView categoriaView = new CategoriaView (nombre);
	}
}