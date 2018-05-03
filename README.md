# UrbanQuest Project

This project is a school project for [GrandLyon](https://www.grandlyon.com/) account.

## Contributors

- Corentin Giraud
- Hubert Hamelin
- Thibault Douzon
- Thomas Sassano
- Lucas Ouaniche
- Clara Pourcel
- Victor Morel

## Purpose

The main goal is to provide a mobile video game to promote tourism and culture on the Lyon city. This application use the GrandLyon open database.

## Usage

### Pre-Requisites

- A developper preconfigured android mobile phone. More information _(TODO)_
- A computer as a server which the fosllowing tools installed:
    - NodeJS >= v9.10.0
    - MongoDB >= v3.6.4
    - Yarn >= v1.5.0

**The mobile phone and the computer (server) have to be on the same local network**

### BackEnd

**Go to the server directory: `cd server/`**

#### Install node project dependencies
Install node dependencies by running the following command: `cd server/ && yarn install`

#### Create mongoDB database
- Launch your mongo server by typing: `mongod --dbpath <path-to-your-databases-storage-directory>`.
- With a new console, connect to your mongo server: `mongo` (the command use the defaut mongo server configuration: `HOST: localhost` & `PORT: 27017`).
- Create a database by typing: `use UrbanQuestDev` _Note that database name is important because it is hardcoded in `config/db.js`._

#### Launch the NodeJS server
**Mongo server must run before. See "Create mongoDB database".**

Launch the NodeJS server by typing this command `yarn run dev`

### Android application

You can download the android application [here](#) (Not available yet). Transfert it to your phone and launch it.
You can build the .apk from Unity.To run the application a server must be availabled (and its URL specified in [this file](https://github.com/TeamBurtonPain/UrbanQuest/blob/master/CityQuest/Assets/Scripts/Service/HTTPHelper.cs)).


## Possible improvements
- Add a personnal feedback on a quest and mark it (/5).
- Add statistical Badges (the only badges we can have for the moment are the one we get on a specific checkpoint.) as "play 200 quest in Lyon"...
- Editor : quest created in the editor and parsed, ready to be sent to the server, but it's not effective yet.
- Editor : add an interface to allow free-text answer (not a multiple choices answer)
- Add other minigames than the "question" one.
- Add a Quest Manager for Editors.
- Find an automatical solution or alternative to manually convert Images (editor photographs) to markers db (use for image recognition).
- Have a better managment for each checkpoint difficulty and add quest difficulty.
- 
