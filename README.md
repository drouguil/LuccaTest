# LuccaTest

Test technique C# et Angular 7 pour l'entreprise Lucca

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

Le code des tests se trouve dans le dossier LuccaDevisesTest. (Seuls les tests du FileReaderService ont été effectués en guise d'exemple)

# LuccaVoyages

L'objectif était de travailler sur une application Web de voyages sous Angular 7 afin de compléter la page de détails des destinations pour y afficher les différentes activités.

Les destinations et activités sont récupérées depuis un serveur Express que l'on peut installer en local ou en ligne.

Malheureusement j'ai lu le sujet un peu vite et je pensais qu'il fallait faire le site de zéro à partir des screens, je ne m'en suis rendu compte qu'il y a peu...

Je partage donc le travail demandé ainsi que le travail supplémentaire (qui n'était pas demandé) que j'ai effectué.

## Lancement des applications

Allez dans le dossier /AngularLucca

### Par les scripts :

* Exécutez `start.bat` ou `start.sh` en fonction de votre environnement
* Les deux applications Angular se lancent automatiquement dans le navigateur par défaut

Test angular
```
localhost:4200
```

Travail supplémentaire
```
localhost:4300
```

### Par les commandes :

#### Test angular

* Allez dans le dossier /test.front.junior
* Exécutez la commande `npm i`
* Exécutez la commande `npm start`
```
localhost:4200
```

#### Travail supplémentaire

* Allez dans le dossier /LuccaWebsite
* Exécutez la commande `npm i`
* Exécutez la commande `npm start`
```
localhost:4300
```

## Tests unitaires

### Par les scripts :

* Exécutez `test.bat` ou `test.sh` en fonction de votre environnement

### Par les commandes :

* Allez dans le dossier /test.front.junior
* Exécutez la commande `npm test`