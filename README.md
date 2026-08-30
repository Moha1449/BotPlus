# 🤖 Telegram Bot Management System

A C# application for managing and controlling Telegram bots through a structured and layered architecture.

The project focuses on separating bot management, request handling, command processing, and application storage into independent components.

---

# ✨ Features

## 🤖 Bot Management

* Connect and validate a Telegram bot.
* Start and stop the bot.
* Check the current bot state.
* Retrieve bot information.
* Retrieve commands registered on Telegram.
* Manage the bot connection key.

## 💬 Chat Handler Engine

The application includes a chat-handling engine that can:

* Receive Telegram updates.
* Maintain a queue of incoming chats.
* Configure the maximum queue capacity.
* Process incoming messages.
* Match messages against configured chat templates.
* Send automatic responses.
* Start and stop the chat handler independently from the bot.

## 📝 Chat Templates

The application provides storage operations for chat templates:

* Add a message/response template.
* Retrieve stored templates.
* Delete templates.
* Use templates to generate automatic responses.

---

# 🖥️ Commands

The application can be controlled through the following commands:

| Command                       | Description                                                   |
| ----------------------------- | ------------------------------------------------------------- |
| `RunBot`                      | Turns the bot on.                                             |
| `CloseConnection`             | Turns the bot off.                                            |
| `Commands -g -b`              | Gets the bot commands stored on Telegram servers.             |
| `Commands -g -a`              | Gets the commands used by the application to control the bot. |
| `Bot -g`                      | Gets the bot information.                                     |
| `Bot -s`                      | Gets the current bot state.                                   |
| `Chat -s`                     | Starts the chat handler engine.                               |
| `Chat -c`                     | Closes the chat handler engine.                               |
| `Chat -q,Amount`              | Sets the maximum number of chats handled per queue.           |
| `Chat -q`                     | Gets the maximum number of chats handled per queue.           |
| `Message -a,Message,Response` | Adds a new chat template.                                     |
| `Message -g`                  | Gets the stored chat templates.                               |
| `Message -d,TemplateID`       | Deletes a chat template by its ID.                            |
| `Connection -a,Key`           | Updates or renews the bot connection key.                     |
| `Connection -g`               | Gets the current bot connection key.                          |

---

# 🛠️ Technology Stack

### Programming Language

* **C#**

### Framework

* **.NET Framework**

### Telegram Integration

* **Telegram.Bot**
* Telegram Bot API

---

# 🏗️ Architecture

The project is organized around separated responsibilities rather than putting all functionality into a single class.

```text
                    Application
                         │
                         ▼
                Command Translator
                         │
              ┌──────────┴──────────┐
              ▼                     ▼
        App Storage             Bot Engine
              │                     │
              │              ┌──────┴──────┐
              │              ▼             ▼
              │        Bot Client    Chat Handler
              │                            │
              │                            ▼
              │                       Telegram API
              │
              ▼
        Application Data
```

---

# 🤖 Bot Engine

The `BotEngine` provides an abstraction layer between the application and the underlying bot implementation.

The application can create and control bots without needing to directly manage the implementation details of the Telegram bot.

The Bot Engine is responsible for:

```text
BotEngine
│
├── Bot lifecycle
├── Bot state
├── Bot information
├── Bot commands
├── Chat handler lifecycle
└── Bot configuration
```

The application can request operations such as:

```text
Run the bot
Close the bot
Get bot information
Get bot commands
Start the chat handler
Stop the chat handler
Configure the chat queue
```

without directly interacting with the Telegram API implementation.

---

# 💬 Chat Handler Engine

The `clsChatsHandlerEngine` is responsible for processing incoming Telegram chat updates.

The processing flow is:

```text
Telegram Updates
       │
       ▼
   Update Queue
       │
       ▼
 Message Processing
       │
       ▼
 Template Matching
       │
       ▼
 Generate Response
       │
       ▼
 Send Telegram Message
```

The engine uses a `CancellationToken` to control its lifecycle.

The maximum number of chats that can be held by the queue can also be configured.

---

# 💾 App Storage

`AppStorage` provides an abstraction for application data operations.

The rest of the application does not need to directly manage the implementation details of storing and retrieving application data.

```text
Application
     │
     ▼
 App Storage
     │
     ▼
Data Storage
```

Current storage responsibilities include:

* Connection information.
* Chat templates.
* Adding templates.
* Retrieving templates.
* Deleting templates.

---

# 🔀 Command Translator

`clsCommandTranslator` acts as the command routing layer.

It receives a command and determines which part of the application should handle it.

```text
                    Command
                       │
                       ▼
              Command Translator
                       │
             ┌─────────┴─────────┐
             ▼                   ▼
        App Storage          Bot Engine
        Commands             Commands
```

This keeps command-processing logic separated from the Bot Engine and App Storage implementations.

---

# 🔄 Bot Lifecycle

The bot follows a controlled lifecycle:

```text
Create Bot
    │
    ▼
Connect
    │
    ▼
Running
    │
    ├───────────────┐
    │               │
    ▼               ▼
Start Handler    Bot Operations
    │
    ▼
Process Requests
    │
    ▼
Stop Handler
    │
    ▼
Close Bot
```

Cancellation tokens are used to stop asynchronous operations when the bot or its handler is closed.

---

# 🚀 Version 1

Version 1 provides the core foundation for managing Telegram bots and processing chat requests.

### V1 includes:

* Bot creation and connection.
* Bot lifecycle management.
* Bot information retrieval.
* Telegram command retrieval.
* Chat request handling.
* Queue-based chat processing.
* Configurable chat queue capacity.
* Chat templates.
* Command-based application control.
* Connection key management.
* Cancellation-based handler shutdown.
* Separation between Bot Engine, Chat Handler, Command Translator, and App Storage.

---

# 📌 Project Status

| Version | Status    |
| ------- | --------- |
| V1      | ✅ Current |
