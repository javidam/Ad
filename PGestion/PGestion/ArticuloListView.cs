using Gtk;
using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Serpis.Ad
{
	public class ArticuloListView : EntityListView
	{
		public ArticuloListView ()
		{
			
			TreeViewHelper treeViewHelper = new TreeViewHelper(treeView, App.Instance.DbConnection, 
				"select * from articulo"
			);
			

			Gtk.Action refreshAction = new Gtk.Action("refreshAction", null, null, Stock.Refresh);
			
			refreshAction.Activated += delegate {
				treeViewHelper.Refresh();
			};
			actionGroup.Add(refreshAction);
			
		}
	}
}

