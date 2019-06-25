# LuccaTest

Test technique C# et Angular 7 pour l'entreprise Lucca

# LuccaDevises

## Lancement de l'application

Dirigez-vous dans le dossier /LuccaDevises.

Soit par l'exe :

* Ouvrez un invite de commandes dans ce répertoire
* Effectuer l'une des commandes suivantes : `LuccaDevises %file%` (Windows) ou `./LuccaDevises.exe %file%` sous Linux en remplaçant %file% par le chemin vers le fichier (le répertoire par défaut est LuccaTest/LuccaDevises/)
* Exemple (le fichier data.txt se trouve dans le dossier LuccaTest/LuccaDevises)

Windows
```
LuccaDevises data.txt
```
Linux
```
./LuccaDevises data.txt
```

Soit par les scripts :

* Executez start.bat ou start.sh en fonction de votre environnement
* Renseignez le chemin du fichier (le répertoire par défaut est LuccaTest/LuccaDevises/)
* Exemple (le fichier data.txt se trouve dans le dossier LuccaTest/LuccaDevises)

```
File name ? data.txt
```

## Tests unitaires

Ouvrez le fichier `LuccaDevises.sln` dans un IDE C# (ex : Visual Studio).

Effectuez via l'IDE l'ensemble des tests (sur Visual Studio : Test -> Éxecuter -> Tous les tests).

Le code des tests se trouve dans le dossier LuccaDevisesTest.

```
Seuls les tests du FileReaderService ont été effectués en guise d'exemple
```

