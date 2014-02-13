using System;
using Gtk;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using PCategoria; 

public partial class MainWindow: Gtk.Window
{	
	
	private MySqlConnection mySqlConnection;
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		mySqlConnection = new MySqlConnection("Server=localhost;Database=dboctubre;User Id=root;Password=sistemas");
		mySqlConnection.Open ();
		
		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
		mySqlCommand.CommandText = 	"select * from categoria";
		
		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
		
		string[] columnNames = getColumnNames(mySqlDataReader);
		
		appendColumns(columnNames);
		
		ListStore listStore = createListStore(mySqlDataReader.FieldCount);

		while (mySqlDataReader.Read ()) {
			List<string> values = new List<string>();
			for (int index = 0; index < mySqlDataReader.FieldCount; index++)
				values.Add ( mySqlDataReader.GetValue (index).ToString() );
			listStore.AppendValues(values.ToArray());
		}
		mySqlDataReader.Close ();
		
		treeView.Model = listStore;
		
		
		deleteAction.Sensitive = false;
		refreshAction.Sensitive = true;
		addAction.Sensitive = true;
		
		
		addAction.Activated += delegate {
			
			AddAction nueva = new AddAction ();
			nueva.Show();
					
		};
		
		
		deleteAction.Activated += delegate {
			if (treeView.Selection.CountSelectedRows() == 0)
				return;
			
			TreeIter treeIter; //Para mostrar la informacion de la linea seleccionada para borrar
			treeView.Selection.GetSelected(out treeIter);
			object id = listStore.GetValue (treeIter, 0);
			
			MessageDialog md = new MessageDialog (this, 
                                    DialogFlags.DestroyWithParent,
	                              	MessageType.Question, 
                                    ButtonsType.YesNo, 
			                        "¿Quieres eliminar el elemento seleccionado?");
			md.Title = "Eliminar elemento";
			ResponseType response = (ResponseType) md.Run ();
			if (response == ResponseType.Yes) {
				MySqlCommand deleteMySqlCommand = mySqlConnection.CreateCommand();
				deleteMySqlCommand.CommandText = "delete from categoria where id=" + id;
				deleteMySqlCommand.ExecuteNonQuery();
			}
			md.Destroy();		
		};
		
		
		treeView.Selection.Changed += delegate { //Activa el boton cuando alguna fila esta seleccionada
			bool hasSelectedRows = treeView.Selection.CountSelectedRows() >0;
			deleteAction.Sensitive = hasSelectedRows;
		};
		
		
		refreshAction.Activated += delegate {// Actualiza la pantalla
			MySqlDataReader mySqlDataReader1 = mySqlCommand.ExecuteReader();
		
		listStore.Clear();
		
		while (mySqlDataReader1.Read ()) {
			List<string> values = new List<string>();
			for (int index = 0; index < mySqlDataReader1.FieldCount; index++)
				values.Add ( mySqlDataReader1.GetValue (index).ToString() );
			listStore.AppendValues(values.ToArray());
		}
		mySqlDataReader1.Close ();
		
		treeView.Model = listStore;
					
		};
		
		
	}
	
	private string[] getColumnNames(MySqlDataReader mySqlDataReader) {
		List<string> columnNames = new List<string>();
		for (int index = 0; index < mySqlDataReader.FieldCount; index++)
			columnNames.Add (mySqlDataReader.GetName (index));
		return columnNames.ToArray ();
	}
	
	private void appendColumns(string[] columnNames) {
		int index = 0;
		foreach (string columnName in columnNames) 
			treeView.AppendColumn (columnName, new CellRendererText(), "text", index++);
	}
	
	private ListStore createListStore(int fieldCount) {
		Type[] types = new Type[fieldCount];
		for (int index = 0; index < fieldCount; index++)
			types[index] = typeof(string);
		return new ListStore(types);
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
