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
Install node dependencies by running the following command: `yarn install`

#### Create mongoDB database
- Launch your mongo server by typing: `mongod --dbpath <path-to-your-databases-storage-directory>`.
- With a new console, connect to your mongo server: `mongo` (the command use the defaut mongo server configuration: `HOST: localhost` & `PORT: 27017`).
- Create a database by typing: `use UrbanQuestDev` _Note that database name is important because it is hardcoded in `config/db.js`._

![Mongo session](http://storage4.static.itmages.com/i/18/0427/h_1524820475_4222983_08fd426f1b.png)

#### Populate DB

- Run the following script through: `node server/populate/populate.js`

#### Launch the NodeJS server
**Mongo server must run before. See "Create mongoDB database".**

Launch the NodeJS server by typing this command `yarn run dev`

### Android application

You can download the android application [here](#). Transfert it to your phone and launch it.
