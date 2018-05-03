# UrbanQuest

_Jouer. Découvrir. Apprendre. S’amuser._

## Introduction

UrbanQuest est un jeu développé pour la plateforme mobile Android. Ce jeu vous enmène au pein coeur de la ville de Lyon afin de découvrir différents lieux emblématiques de la cité romaine au travers de quêtes. Les nombreuses fonctionnalités comme la reconnaissance d'image rendent le jeu très ludique et adictif.

Alors n'attendez plus, armez vous de votre smartphone, téléchargez l'application et partez à la découverte de la magnifique ville de Lyon.

## Le concept

Le but de l'application est permettre aux joueurs d'apprendre de manière ludique les différentes facettes de la ville de Lyon. Le joueur pourra se promener et commencer des quêtes s'il se trouve dans un rayon de moins d'un kilomètre autour du point de départ de la quête.

Une quête se compose d'un ensemble de checkpoints (étapes) que le joueur devra valider à l'aide de la reconnaissance d'image. En effet, une fois la quête démarée, le joueur observera une image floue. Il devra alors retrouver l'endroit où l'image a été prise, puis valider le checkpoint en dirrigeant l'appareil photo de son téléphone dans la même dirrection que l'image floue. 

L'application validera le checkpoint si l'image est reconnue (reconnaissance d'image). En cas de trop grande difficulté, le joueur a la possibilité de dé-flouté progressivement la photo jusqu'à la rendre nette.

Lorsque que le joueur a validé la photo du checkpoint, il obtiendra une description à visée culturelle de l'endroit photographié ainsi que la possibilité de:

- Soit répondre à un quizz
- Soit répondre à une enigme

La combinaison de la réponse pour un checkpoint et de la reconnaissance d'image rapporte un certain nombre d'expérience à un joueur et fait avancé la progression de la quête. Attention, utiliser le défloutage diminue l'expérience rapportée par le checkpoint. Certains checkpoints peuvent rapporter un badge au joueur et ainsi augmenter sa notoriété.

## Utilisation de l'application

Une fois l'application lancée, elle demande votre autorisation pour utiliser la géolocalisation de votre téléphone. Une carte s'affiche alors avec la possibilité

// TODO: à compléter avec des screenshot une fois la charte graphique implementée 

Vous appercevez plusieurs marqueurs:
- Un unique marqueur symbolisant votre position
- Plusieurs marqueur représentant les quêtes disponibles.

## Choix technique

### L'architecture client / serveur

![architecture 1](https://user-images.githubusercontent.com/29222996/39568086-5e11f596-4ec1-11e8-9333-b4e06ba96728.png)

Nous avons défini une architecture type client / serveur dialoguant à travers une API RESTful en utilisant des documents JSON. Nous avons spécifié l'API dès le début du projet ce qui a permi de bien paralléliser le travail. Vous pouvez regarder les spécification de l'API dans le fichier API-specification.md de ce dossier.

### Le serveur

Du côté BACKEND, nous avons choisi des technologies récentes pour construire rapidement et simplement un API fonctionnelle et sécurisée. Nous avons utilisé le language NodeJS permettant l'excecution de code javascript côté serveur pour créer un serveur web avec le module Express. Nous avons lié le serveur à une base de données NoSQL MongoDB.

Nous avons aussi mis en place un système d'authentification par identifiant / mot de passe et nous avons ainsi protéger certains  de nos endpoints. Par exemple, seul un administrateur authentifié pourra mettre à jour un compte utilisateur. Nous utilisons des JWT (JSON Web Token), générés par le serveur lors d'une authentification réussie. Le clietn devra passer en paramètre ce token à chaque fois qu'il souhaite faire une requête authentifiée. De même, toutes les informations sur la protection des endpoints peuvent se retrouver dans les spécifications de l'API.
Enfin, nous avons pu créer un script pour peupler la base de données, ce qui permet d'avoir un compte administrateur connu au premier lancement du serveur.

Les compétences initial du groupe dans ce technologie nous a permi d'arriver rapidement à une implémentation fonctionnelle de l'API.

La manipulation d'outils / frameworks / technologies récents a été une belle opportunité pour l'ensemble des développeur car ces derniers sont en train de devenir les nouveaux standards du développement web. 

### Le client

// TODO

## L'organisation de l'équipe de développement

// Scrum master
