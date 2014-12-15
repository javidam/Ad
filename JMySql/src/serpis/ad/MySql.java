package serpis.ad;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.*;

public class MySql {
	
	private static Scanner teclado = new Scanner(System.in);
	private static int opcion=0;

	public static void main(String[] args) throws ClassNotFoundException, SQLException {
		//Class.forName("com.mysql.jdbc.Driver"); necesario en tipo 3 o anterior
		System.out.println("Hola MySql desde eclipse");
		Connection connection = DriverManager.getConnection(
			"jdbc:mysql://localhost/dbprueba?user=root&password=sistemas");
		
		System.out.println("Menu: 0|| 1-Salir|| 2-Nuevo || 3-eliminar || 4-ver");
		opcion= teclado.nextInt();
		
		switch(opcion){
		case 1: System.out.println("Ingrese el nombre");
				String nombre= teclado.nextLine();
				System.out.println("ingrese la categoria");
				int categoria= teclado.nextInt();
				System.out.println("ingrese el precio");
				int precio= teclado.nextInt();
				
				break;
		
		
		case 4: PreparedStatement preparedStatement= connection.prepareStatement("select * from categoria where id>=?");
				preparedStatement.setLong(1,1);
				ResultSet resultSet= preparedStatement.executeQuery();
				
				
		

		while (resultSet.next()) 
			System.out.print("id=%4s nombre=%s\n"); 
				resultSet.getObject("id");
				resultSet.getObject("nombre"); 
		
		
		resultSet.close();
		preparedStatement.close();
		connection.close();
	}
	}
}
