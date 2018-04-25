# LyonVRTourism

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

- A devlopper preconfigured android mobile phone. More information (TODO)
- A computer as a server which the following tools:
    - NodeJS >= v9.10.0
    - MongoDB >= v3.6.4
    - Yarn >= v1.5.1

**The mobile phone and the computer (server) have to be on the same local network**

### BackEnd

First, install dependies by running the following command: `cd server/ && yarn install`

You have to run:
1. MongoDB database by typing `mongod --dbpath <path-to-your-database>`
2. NodeJS server by typing this command `cd server/ && yarn run prod`

### Android application

You can download the android application [here](#). Transfert it to your phone and launch it.
