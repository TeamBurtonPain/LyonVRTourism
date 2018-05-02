# UrbanQuest - API Specification

## Models

### Account

```
{
    connection: {
        email: {
            type: String,
            validate: {
                regEx: /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/
            },
            required: true
        },
        password: {
            type: String,
            required: true,
        },
    },
    userInformation: {
        lastName: String,
        firstName: String,
        dateOfBirth: Date,
        username: {
            type: String,
            required: true
        }, // Pseudo
        accountType: {
            type: String,
            enum: ['ADMIN', 'EDITOR', 'GAMER']
            default: 'GAMER'
        },
    },
    dates: {
        createdAt: Date, // Auto (server side)
        updatedAt: Date // Auto (server side)
    },
    game: {
        badges: [Schema.Types.ObjectId], // Badges ObjectId Array.
        quests: [
            {
                _idQuest: Schema.Types.ObjectId, // Quest ObjectId
                state: {
                    type: String,
                    enum: ['IN_PROGRESS', 'DONE'],
                    default: 'IN_PROGRESS'
                },
                stats: {
                    earnedXp: Number,
                },
                /* feedback: {
                    comment: String,
                    mark: Number
                } NOT IMPLEMENTED */ 
            }
        ],
        xp: Number,
        elapsedTime: Number // Second
    }
}
```

### Quest

```
{
    _idCreator: Schema.Types.ObjectId,
    geo: {
        lat: Number,
        long: Number
    },
    title: {
        type: String,
        required: true
    },
    description: {
        type: String,
        required: true
    },
    picture: {
        type: String,
        required: true
    }, // Base64 encoded. Available only on /api/quests/<id> endpoint.
    checkpoints: [
        {
            picture: String, // Base64 encoded. Available only on /api/quests/<id> endpoint.
            text: String,
            choices: {
                type: [String],
                required: true
            },
            enigmAnswer: String,
            difficulty: {
                type: Number,
                required: true,
                min: 0,
                max: 5
            } // O < difficulty < 5
        }
    ],
    dates: {
        createdAt: Date, // Auto (server side)
        updatedAt: Date // Auto (server side)
    },
    feedbacks: [
        {
            _idAccount: Schema.Types.ObjectId,
            comment: {
                type: String,
                required: true
            },
            mark: {
                type: Number,
                required: true,
                min: 0,
                max: 10
            } // 0 < note < 10
        }
    ],
    disable: {
        type: Boolean,
        required: true
    }
}
```


## EndPoints

Every request must match the following syntax: `http://<server-url>:3000/api/<Request path>`

### Authentification

Request methods | Path | Auth | AccountType | Require body | Response | Description 
:---: | :---: | :---: | :---: | :---: | :---: | :---:
**POST** | `auth/login` | No | * | `{ email: String, password: String }` | `{ jwt: String }` | Log a user and provide a JWT
**GET** | `auth/logout` | Yes | * | _null_ | _null_ | Logout a user by removing his associated JWT

### Accounts

Request methods | Path | Auth | AccountType | Require body | Response | Description 
:---: | :---: | :---: | :---: | :---: | :---: | :---:
**POST** | `accounts` | No | * | `account model` | `account model` | Persist an account
**GET** | `accounts/<id>` | Yes | * | _null_ | `account model` | Get account by id
**PUT** | `accounts/<id>` | Yes | * | `account model` | `account model` | Update an account by id. **See [PUT SPECIFICATION](#PUT\ Specification).**
**DELETE** | `accounts/<id>` | Yes | * | _null_ | _null_ | Delete an account by id.

### Quests

Request methods | Path | Auth | AccountType | Require body | Response | Description 
:---: | :---: | :---: | :---: | :---: | :---: | :---:
**GET** | `quests/<id>` | Yes | * | _null_ | `quest model` | Get all quests
**POST** | `quests` | Yes | * | `quest model` | `quest model` | Persist a quest
**GET** | `quests/<id>` | Yes | * | _null_ | `quest model` | Get quest by id
**PUT** | `quests/<id>` | Yes | * | `quest model` | `quest model` | Update a quest by id. Request body will erase old quest. **See [PUT SPECIFICATION](#PUT\ Specification).**
**DELETE** | `quests/<id>` | Yes | * | _null_ | _null_ | Delete a quest and every associated pictures.

## PUT Specification

If you update an array, the old array and new array won't be compared. It means that new array will erase old array even if new array if empty. Be carefull :)

## Errors

ErrorName | Code | Description
:---: | :---: | :---:
**BadRequest** | 400 | Your request doesn't match the require schema (description will give more information)
**LoginError** | 400 | Wrong email/password combinaison

## Enhancement

- Allow a user to update only his information
- Client application authentification (server will only accept request from the client application and not form anybody)
- Improve update (PUT) protocol for ARRAY UPDATE: for now, if a ADMIN want to update checkpoints of a quest, he have to send the full checkpoint ARRAY (including all pictures). Consequently, bandwidth and memory are affected.
- Add role guard in some endpoints. For example, it could prevent GAMER to add a quest (ADMIN right).
