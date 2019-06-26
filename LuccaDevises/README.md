# LuccaDevises

L'objectif était de créer une application console en C# permettant à partir d'un fichier texte contenant les taux de change de convertir un montant d'une devise donnée à une autre (informations également présentes dans le fichier texte).

Format du fichier :
```
XXX;M;YYY
N
AAA;BBB;T.TTTT
CCC:DDD;U.UUUU
EEE;FFF;V.VVVV
... N fois
```

## Lancement de l'application

Allez dans le dossier /LuccaDevises.

### Par les scripts :

* Executez `start.bat` ou `start.sh` en fonction de votre environnement
* Renseignez le chemin du fichier (le répertoire par défaut est LuccaTest/LuccaDevises/)

Exemple : (le fichier data.txt se trouve dans le dossier LuccaTest/LuccaDevises)
```
File name ? data.txt
```

### Par l'exécutable :

* Ouvrez un invite de commandes dans ce répertoire
* Effectuez l'une des commandes suivantes : 
* `LuccaDevises %file%` sous Windows
* `./LuccaDevises.exe %file%` sous Linux
* Remplacez %file% par le chemin vers le fichier (le répertoire par défaut est LuccaTest/LuccaDevises/)

Exemple : (le fichier data.txt se trouve dans le dossier LuccaTest/LuccaDevises)

Windows
```
LuccaDevises data.txt
```

Linux
```
./LuccaDevises data.txt
```

## Tests unitaires

Ouvrez le fichier `LuccaDevises.sln` dans un IDE C# (ex : Visual Studio).

Effectuez via l'IDE l'ensemble des tests (sur Visual Studio : Test -> Éxecuter -> Tous les tests).

Si il ne trouve pas de test, faites un clean du projet de test (LuccaDevisesTest), regénérez le puis réessayez de lancer les tests cela devrait résoudre le problème.

Le code des tests se trouve dans le dossier LuccaDevisesTest. (Seuls les tests du FileReaderService ont été effectués en guise d'exemple)