using NUnit.Framework;

public class TestEditor
{
    [Test]
    public void TestBombHandlerPickUpFunction() //Test qui cherche à savoir si la méthode PickUp du script BombHandler permet bien d'ajuster le nombre de bombe
    {
        //--Arrange
        BombHandler handler = new BombHandler();
        int bombe = handler.BombsPossessedCount;

        //--Act
        handler.Pickup();
        handler.Pickup();
        int nouveauNombreBombe = handler.BombsPossessedCount;

        //--Assert
        Assert.That(nouveauNombreBombe - bombe, Is.EqualTo(2)); //On vérifie bien une différence de 2 entre les 2 scores
        Assert.That(bombe, Is.Not.EqualTo(nouveauNombreBombe)); //En cas d'erreur à voir si le Not marche bien mais normalement les 2 nombres sont différents
    }


    [Test]
    public void TestBombHandlerEquipFunction() //Test qui cherche à savoir si la méthode Equip du script BombHandler permet bien d'ajuster le nombre de bombe
    {
        //--Arrange
        BombHandler handler = new BombHandler();
        int bombe = handler.BombsPossessedCount;
        if (bombe == 0) { bombe++; } //Si la valeur est à 0 dans le script j'ajoute manuellement 1 bombe pour pouvoir tester le faites qu'elle soit supprimer

        //--Act
        handler.Equip();
        int nouveauNombreBombe = handler.BombsPossessedCount;

        //--Assert
        Assert.That(bombe - nouveauNombreBombe, Is.EqualTo(1)); //On vérifie bien une différence de 1 entre les 2 scores
        Assert.That(bombe, Is.Not.EqualTo(nouveauNombreBombe)); //En cas d'erreur à voir si le Not marche bien mais normalement les 2 nombres sont différents
    }
}
