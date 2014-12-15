
public class Vector {

	
	
	public static void main(String[] args) {
		int [] vector = new int [] {7,9,5,10};
		
		

	}
	public static int min(int[] vector){
		int numeros= vector.length;
		int min=0;
		int i=0;
		for (i=0; i<numeros; i++){
			if(vector[i]<min){
				min= vector[i];
			}
			
		}
		return min;
	}
	

}
