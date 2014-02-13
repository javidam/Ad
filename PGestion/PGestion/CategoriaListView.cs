using Gtk;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Serpis.Ad
{
	public class CategoriaListView : EntityListView
	{
		public CategoriaListView ()
		{
			TreeViewHelper treeViewHelper = new TreeViewHelper(treeView, App.Instance.DbConnection, 
				"select id, nombre from categoria"
			);
			

			Gtk.Action refreshAction = new Gtk.Action("refreshAction", null, null, Stock.Refresh);
			refreshAction.Activated += delegate {
				treeViewHelper.Refresh();
			};
			actionGroup.Add(refreshAction);
			
			
			Gtk.Action editAction = new Gtk.Action ("editAction", null, null, Stock.Edit);
			editAction.Activated += delegate {
					
			};
			actionGroup.Add (editAction);

			
			
			
			
			
			treeView.Selection.Changed += delegate {
				Console.WriteLine("treeViewHelper.Id='{0}'", treeViewHelper.Id);
				editAction.Sensitive = treeView.Selection.CountSelectedRows() > 0;
			};
						
			
		}
	}
}

