import static org.junit.Assert.*;

import org.junit.Test;


public class VectorTest {

	@Test
	public void testMin() {
		assertEquals(12, Vector.min(new int[] {33, 16, 5 , 19, 23}));
		assertEquals(12, Vector.min(new int[] {7 , 8, 9 , 11, 22}));
		assertEquals(12, Vector.min(new int[] {30, 6, 25 , 19, 23}));
	}
	
	@Test(expected= ArrayIndexOutOfBoundsException.class)
	public void testMinEmpty(){
		Vector.min(new int[]{});
	}

}
