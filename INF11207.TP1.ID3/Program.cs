// See https://aka.ms/new-console-template for more information

using INF11207.TP1.Core;
using INF11207.TP1.ID3;

Console.WriteLine("This is a simple app to demonstrate the ID3 decision tree algorithm.");

//string chemin = "C:\\Users\\goulba01\\source\\repos\\id3_train_reduced.csv";
string chemin = "/home/bastiengoulet/dev/INF11207.TP1/Data/id3_train_reduced.csv";

Arbre arbre = new ArbreID3(chemin);
arbre.Construire();

arbre.Afficher();

Console.WriteLine("Étiquettage de la prévision suivante:");
Console.WriteLine("Pluvieux, Moyen, Élevée, Faible");

Exemple exemple = new Exemple(
    new string[] { "Previsions", "Temperature", "Humidite", "Vent" },
    new string[] { "Pluvieux", "Moyen", "Elevee", "Faible" });

Console.WriteLine($"\nClasse: {arbre.Etiqueter(exemple)}");