french available after the break

# Technical test for Junior developpers

## Mission statement

This SPA provides 2 main views. the first one allows you to search for a destination and when you click on one, you'll go to the second one that display some information about said destination.

I want you to enrich the page displaying a destination'd details with a list of activities available at said destination. Each destination have `n` activities, and an activity is linked to `1` destination.

You have a list of activities available in firebase under the path `/activities`

A moqup of what is expected is available [here](https://github.com/LuccaSA/test.front.junior/blob/master/moqup-large.png) or [here](https://github.com/LuccaSA/test.front.junior/blob/master/moqup-small.png)

## Getting started

Fork this repo

run in a shell

- `npm i` - it will install all necessary dependencies
- `npm start` - will launch the application on localhost:4200

Submit your work via a PR to this repo

## Technical remarks

This exercise uses a standard angular 7 stack, which is the stack used at Lucca today. If you're not familiar with the framework there are a lot of tutorials available on the web. Here is a [link](https://angular.io/tutorial) to the tutorail provided by the angular team. For this exercise you dont need to master aspects about _routing_ or _http_.

This exercise also use _lucca-front_ as a css framework. lucca-front is our in-house open source framework for building angular applications. You can find documentation of the features at this [url](https://luccasa.github.io/design-system#/) and the source code is available [here](https://github.com/LuccaSA/lucca-front).

For our backend we use a light nodejs RESTful api server. It's full documentation and source code is available [here](https://github.com/LuccaSA/tourism.server). A live version is available under http://tourism.lucienbertin.com. You can clone the repo and work with a local version of the server or choose to use the one available online. All instructions on how to build/deploy the server locally is available on its repository, to route api calls to your local server, see the file [environment.ts](https://github.com/LuccaSA/test.front.junior/blob/master/src/environments/environment.ts#L8).

Some quick rules on how to query it
- there are 2 main apis: [/api/destinations](http://tourism.lucienbertin.com/api/destinations) and [/api/activities](http://tourism.lucienbertin.com/api/activities)
- [/api/destinations](http://tourism.lucienbertin.com/api/destinations) will give you a collection of destinations, [/api/destination/:id](http://tourism.lucienbertin.com/api/destination/1) will give you the destination with id = `:id`. idem for /api/activities and /api/activity/:id
- you can filter the results you want by adding parameters to the url, the synthax is
	- propertyName=value1,value2,value3... - will give you only the items where propertyName equals one of the values
	- ex: [/api/destinations?rating=3,4](http://tourism.lucienbertin.com/api/destinations?rating=3,4) gives you the destinations rated 3 or 4 stars
	- propertyName$like=value1,value2,value3... - will give you the items where propertyName contains one of the values
	- ex: [/api/destinations?name$like=b,c,d](http://tourism.lucienbertin.com/api/destinations?name$like=b,c,d) gives you the destinations with the letter b, c or d in it
- you can add a sort order or paging, the synthax is
	- paging=offset,count
	- orderBy=propertyName,sortOrder - sortOrder is optional and degfault to ascending, use `desc` for a descending order
	- ex: [/api/destinations?orderBy=rating&paging=5,5](http://tourism.lucienbertin.com/api/destinations?orderBy=ratings&paging=5,5) gives you the destinations 6 to 10, ordered by descending rating

## Q&A

> how much time am I suppose to work on this exercise

This exercise is supposed take between 1 to 4 hours. The amount of time you spend on this isn't a criteria, and more isn't necessarily better. I'd rather see someone work 3 hours on this and return something thats no perfect, than someone spending a whole weekend to return something super polished. Keep in mind you're not getting paid for the time you work on this. If you want to spend more time, you can, but do it because it brings you something (you want to hone your skills), not because you expect something from Lucca.

-------------

# Test technique pour dev junior

## Enoncé

L'application met à disposition 2 écrans, le premier permet de chercher une destination et quand vous en sélectionnez une, vous êtes dirigé vers le 2e écran qui affiche le détail de la destination en question.

Ce qui est attendu est que vous enrichissiez cet écran avec une liste d'activités touristiques disponibles à la destination en question. Le lien entre destination et activite est `1-N`, (`1` destination a `N` activites, `1` activite est liee a `1` destination)

Une liste d'activités est diponible dans la base firebase sous le chemin `/activities`

Une maquette du rendu est disponible [ici](https://github.com/LuccaSA/test.front.junior/blob/master/moqup-large.png) ou [là](https://github.com/LuccaSA/test.front.junior/blob/master/moqup-small.png)

## Marche à suivre

Faites un fork ce repo dans votre espace github

lancez ces lignes de commande

- `npm i` - installera toutes les dépendances nécessaires
- `npm start` - lancera l'application à l'adresse localhost:4200

Soumettez votre travail via une PR vers ce repo

## Remarques techniques

Cet exercice utilise une stack angular 7 classique, c'est la stack utilisée chez Lucca aujourd'hui. Si vous n'avez jamais utilisé angular ou êtes un peu rouillé, moult tutoriels sont disponibles sur le web. [Celui-ci](https://angular.io/tutorial) est le tuto fourni par la team angular. Pour cet exercice, il n'est pas nécessaire de maitriser les aspects liés au _routeur_ ou aux _appels http_.

Cet exercice utilise aussi _lucca-front_ comme framework css. Lucca-front est notre librairie open source interne de framework UI/UX pour application angular. Une documentation des composants css est disponible [ici](https://luccasa.github.io/design-system#/) et le code source est [là](https://github.com/LuccaSA/lucca-front)

Pour la partie serveur, cet exercice utilise un serveur nodejs light d'api REST. Sa documentation complète est disponible [ici](https://github.com/LuccaSA/tourism.server). Une version online est disponible sous http://tourism.lucienbertin.com. Vous pouvez choisir d'utiliser la version online, ou monter un serveur localement, toutes les instructions pour builder/deployer le serveur en local sont disponibles dans son repository. Pour rerouter les appels apis vers votre version locale, allez voir le fichier [environment.ts](https://github.com/LuccaSA/test.front.junior/blob/master/src/environments/environment.ts#L8).

Quelques règles rapides pour interroger l'api
- il y a 2 apis principales : [/api/destinations](http://tourism.lucienbertin.com/api/destinations) et [/api/activities](http://tourism.lucienbertin.com/api/activities)
- [/api/destinations](http://tourism.lucienbertin.com/api/destinations) retournera une collection de destinations, [/api/destination/:id](http://tourism.lucienbertin.com/api/destination/1) retournera la destination d'id `:id`. idem pour /api/activities et /api/activity/:id
- il est possible d'appliquer un ou plusieur filtres a la requete, la synthaxe est alors
	- propertyName=value1,value2,value3... - retournera les items pour lesquels `propertyName` vaut l'une des valeurs
	- ex: [/api/destinations?rating=3,4](http://tourism.lucienbertin.com/api/destinations?rating=3,4) retourne les destinations notées 3 ou 4
	- propertyName$like=value1,value2,value3... - retournera les items pour lesquels `propertyName` _contient_ l'une des valeurs
	- ex: [/api/destinations?name$like=b,c,d](http://tourism.lucienbertin.com/api/destinations?name$like=b,c,d) retournera les destinations qui ont la lettre b, c ou d dans leur nom
- vous pouvez ordonner et paginer les resultats
	- paging=offset,count
	- orderBy=propertyName,sortOrder - sortOrder est opttionnel est est ascendant par defaut, utilisez `desc` pour un ordre descendant
	- ex: [/api/destinations?orderBy=rating&paging=5,5](http://tourism.lucienbertin.com/api/destinations?orderBy=ratings&paging=5,5) retourne les destinations 6 a 10, ordonnees par note descendante

## FaQ

> Combien de temps je suis sense bosser dessus

C'est fait pour etre faisable en 1 a 4h. Le temps passé dessus n'est pas un critère de notation, et plus n'est pas forcément mieux. Je prefère relire le travail de quelqu'un qui y passe 3h et rend quelque chose qui marche mais n'est pas parfait, plutot que celui de quelqu'un qui y passe tout un week end et rend quelque chose d'ultra peaufiné. Gardez en memoire que vous ne serez pas payé pour le temps passé à travailler sur ce test, si vous voulez y passer plus de temps, vous pouvez, mais faites le parce que ça vous apporte quelque chose (pour affuter vos compétences angular), plutot que parce que vous attendez quelque chose en retour de la part de Lucca.