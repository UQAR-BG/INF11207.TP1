using INF11207.TP1.Core;
using INF11207.TP1.C45;

Console.WriteLine("This is a simple app to demonstrate the ID3 decision tree algorithm.");

string chemin = "C:\\Users\\goulba01\\source\\repos\\train_reduced.csv";
//string chemin = "/home/bastiengoulet/dev/INF11207.TP1/Data/train_reduced.csv";

string cheminPrevisions = "C:\\Users\\goulba01\\source\\repos\\test_reduced.csv";

Arbre arbre = new ArbreC45(chemin, cheminPrevisions);
arbre.Construire();

arbre.Afficher();

arbre.Elaguer();

//Console.WriteLine("Étiquettage de la prévision suivante:");
//Console.WriteLine("Pluvieux, Moyen, Élevée, Faible");

//Exemple exemple = new Exemple(
//    new string[] { "Previsions", "Temperature", "Humidite", "Vent" },
//    new string[] { "Pluvieux", "Moyen", "Elevee", "Faible" });

//Console.WriteLine($"\nClasse: {arbre.Etiqueter(exemple)}");