using System;

namespace PHolaMySql
{
	class MainClass
	{
		public static void Main (string[] args)
		{


			MySqlConnection mySqlConnection= new MySqlConnection("" +
			                                                     "Server= localhost;Database=dbprueba;User ID = root; Password= sistemas"");

mySqlConnection.Open();	

Console.WriteLine ("Hello World!");

MySqlCommand mySqlCommand= mySqlConnection.CreateCommand();
mySqlCommand.CommandText= string.Format("insert into categoria(nombre)values ('{0}')", DateTime.Now);
mySqlCommand.ExecuteNonQuery;

mySqlCommand.CommandText= "select * from categoria";

MySqlDataReader mySqlDataReader= mySqlCommand.ExecuteReader();

Console.WriteLine("FieldCount={0}" = mySqlDataReader.FieldCount);
for(int index=0; index<mySqlDataReader.FieldCount; index++)

mySqlConnection.Close();
}
}
