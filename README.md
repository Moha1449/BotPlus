# 🤖 BotPlus

> A multi-bot Telegram management engine built with C# and .NET.

BotPlus allows a single application to manage multiple independent Telegram bots using reusable connections and chat templates. Instead of building a separate application for every bot, BotPlus provides a shared infrastructure where bots can be created, configured, started, stopped, monitored, and managed through a unified command interface.

## 🚀 The Main Idea

Traditional Telegram bot development often follows this pattern:

> **One bot → One project → Custom logic → Separate lifecycle management**

BotPlus changes that approach to:

> **One application → Multiple bot nodes → Shared infrastructure**

In short:

> **BotPlus turns "one bot = one custom project" into "one bot = one command."**

---

# 📋 Table of Contents

* [The Problem](#-the-problem)
* [Solution](#-solution)
* [Key Features](#-key-features)
* [Architecture](#-architecture)
* [Project Layers](#-project-layers)
* [How a Request Flows](#-how-a-request-flows)
* [Bot Node Lifecycle](#-bot-node-lifecycle)
* [Data Storage vs Runtime State](#-data-storage-vs-runtime-state)
* [Command System](#-command-system)
* [Command Reference](#-command-reference)
* [Technologies](#-technologies)
* [Future Improvements](#-future-improvements)

---

# ❓ The Problem

Managing multiple Telegram FAQ or support bots can become repetitive.

Normally, each bot may require:

* Separate bootstrapping code.
* A separate Telegram connection.
* Hardcoded question-and-answer logic.
* Independent lifecycle management.
* Separate start and stop logic.
* Individual monitoring.

As the number of bots increases, maintaining separate implementations becomes inefficient.

---

# 💡 The Solution

BotPlus acts as a **multi-bot management engine**.

A single running application can host and manage multiple independent Telegram bots.

Each bot node can have its own:

* Telegram connection.
* Bot identity.
* Chat template.
* Message queue configuration.
* Chat handler.
* Runtime lifecycle.

The application provides a single command interface for managing the entire system.

For example:

```text
Connection-a-<BotToken>

Chats-a-Question-Answer

Bot-c-MyBot-<ConnectionID>-<ChatTemplateID>

Bot-h-r-<NodeID>
```

The user does not need to create new application code for every bot.

---

# ✨ Key Features

## 🤖 Multi-Bot Management

Create and manage multiple independent Telegram bot nodes within a single application.

Each bot node maintains its own:

* Telegram bot client.
* Chat handler engine.
* Chat template.
* Runtime state.
* Message queue configuration.

---

## 🔐 Reusable Connections

Telegram bot tokens are stored independently as reusable **Connections**.

A connection can be retrieved and used when creating a bot node.

```text
Connection
├── ID
└── Key
```

---

## 💬 Reusable Chat Templates

Chat behavior is defined using reusable **Chat Templates**.

A chat template contains multiple question-and-answer pairs.

```text
ChatTemplate
├── ID
└── QuestionsAndAnswers
    ├── Question
    └── Answer
```

The same chat template can potentially be assigned to multiple bot nodes.

---

## 🧠 Bot Node Architecture

Every running bot is represented by a `clsBotNode`.

A bot node contains the infrastructure required to manage one Telegram bot.

```text
clsBotNode
│
├── Telegram Bot Client
│
├── Bot Information
│
├── Chat Template
│
├── clsChatsHandlerEngine
│
├── Cancellation Infrastructure
│
└── Runtime State
```

Each node operates independently from other bot nodes.

---

## 🧵 Background Chat Handling

Each bot node has its own `clsChatsHandlerEngine`.

The handler engine:

1. Polls Telegram for updates.
2. Receives incoming messages.
3. Queues updates.
4. Processes messages.
5. Searches for matching responses.
6. Sends answers to users.
7. Stops safely when cancellation is requested.

The chat handler runs in the background so the application remains responsive.

---

## 📦 Message Queue Management

Each bot handler manages a queue for incoming Telegram updates.

The queue capacity can be configured per bot.

This allows each bot node to have its own message-handling configuration.

Example:

```text
Bot A
Queue Capacity: 30

Bot B
Queue Capacity: 100
```

Queue capacity can also be changed at runtime.

---

## 📊 Handler Monitoring

BotPlus includes a stopped-handler monitoring mechanism.

When a handler engine stops:

1. The handler finishes its execution.
2. A stopped event is raised.
3. The event is recorded.
4. `clsStoppedHandlersLogger` stores the information.
5. The UI checks the log periodically.
6. The stop information is displayed to the user.

This allows the application to monitor handler lifecycle events without crashing the application.

---

## 📝 Unified Command Interface

All major application operations are performed through a single command system.

Examples:

```text
Connection-g

Chats-g

Bot-c-MyBot-ConnectionID-TemplateID

Bot-h-r-NodeID
```

The command system provides one consistent interface for:

* Storage operations.
* Bot operations.
* Handler operations.
* Monitoring.
* Configuration.

---

# 🏗 Architecture

BotPlus follows a layered architecture.

The system is divided into:

```text
PresentationLayer
        │
        ▼
BusinessLayer
        │
        ▼
DataLayer
```

However, the project also contains a shared layer:

```text
DataModelLayer
```

`DataModelLayer` is referenced directly by the other layers.

```text
                 PresentationLayer
                        │
                        │
                        ▼
                 BusinessLayer
                    │       │
                    │       │
                    ▼       ▼
               DataLayer   DataModelLayer
                    │            ▲
                    └────────────┘
```

A simplified dependency model:

```text
PresentationLayer ───────► DataModelLayer
        │
        ▼
BusinessLayer ───────────► DataModelLayer
        │
        ▼
DataLayer ───────────────► DataModelLayer
```

The `DataModelLayer` does not depend on:

* PresentationLayer
* BusinessLayer
* DataLayer

This makes it safe to use as a shared model layer.

---

# 📚 Project Layers

## 🧩 DataModelLayer

The `DataModelLayer` contains shared models and infrastructure used throughout the application.

It contains:

### `clsConnection`

Represents a stored Telegram bot connection.

```text
Connection
├── ID
└── Key
```

The `Key` contains the Telegram bot token.

---

### `clsChatTemplate`

Represents a reusable chat template.

```text
ChatTemplate
├── ID
└── QuestionsAndAnswers
```

---

### `clsQuestionAndAnswer`

Represents a single question-and-answer pair.

```text
Question
   ↓
Answer
```

---

### `clsReturnResult`

A shared result object used throughout the application.

Instead of using exceptions for expected outcomes, operations return a `clsReturnResult`.

Possible results include:

```text
Success

Error

NotFound

InvalidInputs

EmptyResult
```

Each result also contains a human-readable detail message.

Conceptually:

```text
clsReturnResult
├── Result
└── Detail
```

This creates a consistent communication mechanism between layers.

---

### `clsErrorLogger`

A shared thread-safe error logger.

Unexpected exceptions can be recorded without interrupting the application's normal flow.

Errors are written with timestamps to:

```text
Logger.txt
```

---

# 💾 DataLayer

The `DataLayer` is responsible only for data access.

Its responsibility is to communicate with MongoDB.

It does not contain:

* Telegram logic.
* UI logic.
* Bot lifecycle logic.
* Command parsing.

Main components include:

### `clsConnectionDataProvider`

Responsible for connection storage operations.

Examples:

* Get connections.
* Get connection by ID.
* Add connection.
* Delete connection.

---

### `clsChatsTemplatesDataProvider`

Responsible for chat template storage operations.

Examples:

* Get templates.
* Get template by ID.
* Add templates.
* Delete templates.

---

### `clsCollectionsConfig`

Responsible for configuring MongoDB collection references and mappings.

---

### `clsDatabaseConnectionConfig`

Stores the MongoDB connection configuration and database reference.

---

# 🧠 BusinessLayer

The `BusinessLayer` contains the application's main logic.

It manages:

* Command processing.
* Bot creation.
* Bot lifecycle.
* Bot nodes.
* Chat handlers.
* Validation.
* Monitoring.

---

## `clsCommandTranslator`

The central entry point for commands.

Example:

```text
Bot-c-MyBot-ConnectionID-TemplateID
```

The translator:

1. Receives the command.
2. Parses the command.
3. Identifies the command resource.
4. Identifies the requested operation.
5. Validates the command.
6. Routes the request.
7. Returns a `clsReturnResult`.

---

## `clsBotEngine`

The main in-memory bot management engine.

It maintains the bot node registry.

Conceptually:

```text
clsBotEngine
│
└── _BotsNodesList
        │
        ├── BotNode 1
        │
        ├── BotNode 2
        │
        └── BotNode N
```

Responsibilities include:

* Creating bot nodes.
* Deleting bot nodes.
* Running handlers.
* Stopping handlers.
* Retrieving node information.
* Retrieving Telegram bot information.
* Managing handler logs.

---

## `clsBotNode`

Represents one independent bot instance.

A node contains:

```text
clsBotNode
│
├── Node ID
│
├── Bot Metadata
│
├── TelegramBotClient
│
├── Chat Template
│
├── clsChatsHandlerEngine
│
└── Cancellation Infrastructure
```

A bot node is responsible for managing the infrastructure associated with one bot.

---

## `clsChatsHandlerEngine`

The chat handler is responsible for processing Telegram updates.

Its workflow can be represented as:

```text
Telegram
   │
   ▼
Receive Updates
   │
   ▼
Update Queue
   │
   ▼
Process Message
   │
   ▼
Search Chat Template
   │
   ▼
Send Response
```

The handler runs independently in the background.

---

## `clsStoppedHandlersLogger`

A thread-safe in-memory monitoring mechanism.

When a chat handler stops, the stop information can be recorded and retrieved.

This allows the UI to monitor background handler activity.

---

# 🖥 PresentationLayer

The application currently uses WinForms.

The main command interface is:

```text
usCommandsScreen
```

The UI is responsible for:

* Receiving commands from the user.
* Sending commands to the BusinessLayer.
* Displaying results.
* Monitoring stopped handlers.

The UI sends commands to:

```text
clsCommandTranslator.Execute()
```

The UI does not communicate directly with:

* MongoDB.
* Data providers.
* Bot infrastructure.

All operations go through the BusinessLayer.

---

# 🔄 How a Request Flows

Every command follows a consistent execution path.

```text
User
 │
 │ Types Command
 ▼

clsCommandTranslator.Execute()
 │
 │ Parses Command
 ▼

Identifies Resource
 │
 ├── Connection
 │
 ├── Chats
 │
 └── Bot
 │
 ▼

Routes Request
 │
 ├── Connection Commands
 │
 ├── Chat Template Commands
 │
 └── Bot Engine Commands
 │
 ▼

Business Logic
 │
 ├── Validation
 │
 ├── Data Access
 │
 └── Bot Management
 │
 ▼

clsReturnResult
 │
 ▼

PresentationLayer
 │
 ▼

Display Result
```

---

# 🔀 Command Routing

The command translator routes commands according to their resource.

```text
Connection-*
        │
        ▼
_ConnectionStorageCommands
        │
        ▼
clsConnectionDataProvider
```

```text
Chats-*
        │
        ▼
_ChatsTemplateStorageCommands
        │
        ▼
clsChatsTemplatesDataProvider
```

```text
Bot-*
        │
        ▼
_BotEngineCommands
        │
        ▼
clsBotEngine
```

This keeps the command infrastructure centralized while allowing different parts of the application to handle their own responsibilities.

---

# 🔁 Bot Node Lifecycle

A bot node goes through several stages.

## 1️⃣ Store Prerequisites

Before creating a bot node, the application requires:

* A `Connection`.
* A `ChatTemplate`.

Example:

```text
Connection-a-<BotToken>
```

```text
Chats-a-Question-Answer
```

These records are stored in MongoDB.

---

## 2️⃣ Create the Bot Node

Command:

```text
Bot-c-BotName-ConnectionID-ChatTemplateID
```

The engine:

1. Retrieves the connection.
2. Retrieves the chat template.
3. Validates the data.
4. Connects to Telegram.
5. Creates a `clsBotNode`.
6. Adds the node to `_BotsNodesList`.

At this stage:

> The bot node exists, but its chat handler is not running.

---

## 3️⃣ Start the Chat Handler

Command:

```text
Bot-h-r-NodeID
```

The node starts its:

```text
clsChatsHandlerEngine
```

The handler begins:

```text
Polling
   ↓
Receiving Updates
   ↓
Queueing Messages
   ↓
Processing Messages
   ↓
Sending Responses
```

---

## 4️⃣ Monitor the Bot

While the bot is running, the application can retrieve information about:

* The bot node.
* The connected Telegram bot.
* The handler queue.
* The handler status.

---

## 5️⃣ Stop the Chat Handler

Command:

```text
Bot-h-c-NodeID
```

The application requests cancellation.

The handler then exits its polling loop safely.

When the handler stops:

```text
Handler Stops
      │
      ▼
Stopped Event
      │
      ▼
clsStoppedHandlersLogger
      │
      ▼
UI Monitoring
      │
      ▼
Display Result
```

---

## 6️⃣ Delete the Bot Node

Command:

```text
Bot-d-NodeID
```

The node is disposed and removed from:

```text
_BotsNodesList
```

Deleting a bot node does **not** delete:

* The Connection.
* The ChatTemplate.

Those records remain stored in MongoDB.

---

# 🧠 Data Storage vs Runtime State

One important part of BotPlus is the separation between persistent data and runtime infrastructure.

## Persistent Data

Stored in MongoDB:

```text
MongoDB
│
├── Connections
│
└── ChatTemplates
```

These records remain available after the application stops.

---

## Runtime State

Stored in memory:

```text
clsBotEngine
│
└── _BotsNodesList
```

Bot nodes exist only while the application is running.

If the application restarts:

```text
Bot Nodes      ❌ Removed
Connections    ✅ Remain
Chat Templates ✅ Remain
```

Therefore, after restarting the application, bot nodes must be:

```text
Created Again
     ↓
Started Again
```

---

# 🧾 Command System

BotPlus uses a text-based command interface.

The command format generally follows:

```text
<Resource>-<Operation>-<Arguments>
```

Examples:

```text
Connection-g
```

Get all connections.

```text
Chats-g
```

Get all chat templates.

```text
Bot-h-r-1
```

Run the handler for bot node `1`.

---

# 📖 Command Reference

## Application Commands

| Command      | Description                                         |
| ------------ | --------------------------------------------------- |
| `Commands-g` | Get the list of all available application commands. |

---

## Connection Commands

| Command            | Description                        |
| ------------------ | ---------------------------------- |
| `Connection-g`     | Get all stored connections.        |
| `Connection-g-ID`  | Get a connection by ID.            |
| `Connection-a-Key` | Add a new Telegram bot connection. |
| `Connection-d-ID`  | Delete a connection by ID.         |

---

## Chat Template Commands

| Command                                        | Description                                                           |
| ---------------------------------------------- | --------------------------------------------------------------------- |
| `Chats-g`                                      | Get all chat templates.                                               |
| `Chats-a-Question-Answer-[Question-Answer]...` | Add a chat template containing one or more question-and-answer pairs. |
| `Chats-g-ID`                                   | Get a chat template by ID.                                            |
| `Chats-d-ID`                                   | Delete a chat template by ID.                                         |

---

## Bot Commands

| Command                                     | Description                                                       |
| ------------------------------------------- | ----------------------------------------------------------------- |
| `Bot-c-BotName-ConnectionID-ChatTemplateID` | Create a bot node using an existing connection and chat template. |
| `Bot-d-NodeID`                              | Delete a bot node and remove it from the runtime registry.        |
| `Bot-g-n-NodeID`                            | Get information about a bot node.                                 |
| `Bot-g-b-NodeID`                            | Get information about the connected Telegram bot.                 |

---

## Handler Commands

| Command                   | Description                                     |
| ------------------------- | ----------------------------------------------- |
| `Bot-h-r-NodeID`          | Run the bot node's chat handler.                |
| `Bot-h-c-NodeID`          | Stop the bot node's chat handler.               |
| `Bot-h-l`                 | Get the stopped-handlers log.                   |
| `Bot-h-q-NodeID-Capacity` | Update the maximum message queue capacity.      |
| `Bot-h-m-NodeID`          | Get the current maximum message queue capacity. |

---

# 🛠 Technologies

BotPlus is built using:

* **C#**
* **.NET Framework 4.8**
* **WinForms**
* **MongoDB**

---

# 🧩 Important Design Concepts Used

This project includes several software engineering concepts:

### Layered Architecture

Responsibilities are separated between:

* Presentation.
* Business logic.
* Data access.
* Shared models.

---

### Separation of Concerns

Each component has a specific responsibility.

For example:

```text
UI
↓
User Interaction

CommandTranslator
↓
Command Parsing

BotEngine
↓
Bot Management

DataProviders
↓
Database Access

ChatsHandlerEngine
↓
Telegram Message Processing
```

---

### Shared Model Layer

`DataModelLayer` provides shared models used across multiple layers.

---

### Result Object Pattern

Operations return:

```text
clsReturnResult
```

instead of relying on exceptions for expected outcomes.

---

### Event-Based Monitoring

Handler engines can report lifecycle events when they stop.

---

### Background Processing

Chat handlers operate independently from the UI.

---

### Cancellation

Handlers can be stopped through cancellation infrastructure rather than forcibly terminating execution.

---

### In-Memory Runtime Registry

Running bot nodes are managed through:

```text
_BotsNodesList
```

while persistent configuration remains in MongoDB.

---

# 🚧 Future Improvements

Possible future improvements include:

* Persistent bot node configuration.
* Automatic bot restoration after application restart.
* Improved bot status monitoring.
* More advanced message processing.
* Inline keyboard support.
* Menu-based Telegram interactions.
* Better queue strategies.
* Additional command features.
* Improved logging.
* Web-based management interface.
* Authentication and authorization.
* REST API integration.

---

# 🎯 Project Purpose

BotPlus is not intended to be only a Telegram bot.

The project was designed as a practical backend and software engineering project for exploring concepts such as:

* System architecture.
* Layer separation.
* Data persistence.
* Multi-instance management.
* Command parsing.
* Background processing.
* Threading.
* Cancellation.
* Event-driven programming.
* MongoDB integration.
* Telegram API integration.

The goal is to build a reusable infrastructure where multiple Telegram bots can be managed through one application.

---

# 👨‍💻 Author

**Mohammad**

Software Engineering Student
Backend Developer | .NET Developer

---

> **BotPlus — One Application. Multiple Bots. Shared Infrastructure.**
