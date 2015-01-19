package serpis.ad;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;


public class HibernateCategoria {

public static void main(String[] args) {
EntityManagerFactory entityManagerFactory =
Persistence.createEntityManagerFactory("serpis.ad.mysql");
entityManagerFactory.close();
}
}