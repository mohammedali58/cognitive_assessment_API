
# Cognitive Assessment API

## Overview

This Cognitive Assessment API provides endpoints to  perform cognitive score calculations. Built using .NET 8, PostgreSQL, and Entity Framework Core, it follows clean architecture principles to ensure maintainability and scalability.

## Technologies

- .NET 8
- Entity Framework Core
- PostgreSQL
- Docker
- xUnit (for Unit Testing)

## Features

- RESTful endpoints for managing cognitive journals
- Cognitive assessment scoring based on customizable scoring logic
- Global exception handling and robust error responses
- Dockerized application and testing environment

## Prerequisites

- Docker & Docker Compose

## Setup Instructions

### Clone the Repository

```bash
git clone https://github.com/mohammedali58/Cognitive_assesment_API.git
cd Cognitive_Assessment
```

### Running the Application (Docker)

Ensure Docker is running.

```bash
docker compose up --build
```

Application runs at:

```
http://localhost:5212
```

### Running Unit Tests (Docker)

Run unit tests using Docker Compose:

```bash
docker compose -f docker-compose.test.yml up --build
```

Test results will be displayed in the console.

## API Endpoints

Example endpoints:

- Get Journals:
  ```
  GET /journals/{id}/score
  ```

- Create a new Journal:
  ```
  POST /journals
  ```


## Architecture

Follows clean architecture principles:
- **Core:** Domain models and logic
- **Application:** Use-cases, services, and interfaces
- **Infrastructure:** Data access (EF Core) and external integrations
- **API:** Presentation layer (controllers, middlewares)

## Exception Handling

The application includes a global exception handling middleware providing clear JSON-formatted error responses:

```json
{
  "error": "Error message here",
  "statusCode": 400
}
```

# 🧠 Journal Entry Scoring Logic
Each journal entry submitted by a user is automatically analyzed using a simplified version of the LIWC (Linguistic Inquiry and Word Count) model. The purpose of this analysis is to assign a cognitive/emotional score based on the presence of predefined psychologically meaningful words.

## 🔍 How the Scoring Works
Text Tokenization
The submitted journal text is first converted to lowercase and split into individual words.
Example:
"I feel great today" → ["i", "feel", "great", "today"]

Word Matching
The words are matched against a dictionary stored in the database. This dictionary contains several psychologically relevant categories such as:

Positive Emotions

Negative Emotions

Social

Cognitive

Score Calculation
The scoring logic is handled by the JournalScoringService, which delegates the actual score computation to the IWordRepository. The repository method (GetWordScoresAsync) receives the list of tokenized words and returns a single numeric score representing the number of matched words across all categories.

Response
This score is saved with the journal entry and returned in the API response. It can later be used to visualize or analyze emotional trends over time.

## 🧪 Example
Submitted Journal Text:

```text

I feel happy and grateful but also a bit anxious.
Tokenized Words:
```

```css

["i", "feel", "happy", "and", "grateful", "but", "also", "a", "bit", "anxious"]
```
Matched Words:

"happy" → Positive Emotion

"grateful" → Positive Emotion

"anxious" → Negative Emotion

#Final Score:

```json
{
  "journal_id": 1,
  "score": 3
}
```


# 🗄️ what happens behind the scenes in the db level by the ORM function 

## Word Matching Query & Performance Optimization
To efficiently score a journal entry, the system needs to quickly determine how many words from a user's input match entries in the dictionary table (Words).

## 🔍 Word Matching Query 
The system uses the following SQL query to count matched words:

```sql
SELECT COUNT(*)
FROM Words
WHERE Text IN (LOWER('word1'), LOWER('word2'), ...);
```

Explanation:

The LOWER() function ensures case-insensitive comparison.

The IN clause checks if each lowercase input word exists in the Words table.

The database evaluates LOWER(Text) for each row and compares it to the input values.

A simple COUNT(*) returns the number of matched rows.


-----------------------------------------------------------------------------------------------

## 🚀 On large scale their is "Performance Enhancement Techniques"
To improve the efficiency of this lookup operation, especially on large datasets, we follow the following techniques:

## 1- ✅ Functional Index
Create an index on LOWER(Text) so the database can use the index instead of scanning all rows:

```sql
CREATE INDEX idx_lower_text ON Words (LOWER(Text));
```
This allows faster lookups when using expressions like LOWER(Text) in the WHERE clause.

## 2- ✅ Case-Insensitive collation (PostgreSQL)
  Use case-insensitive data types or collations (e.g., citext in PostgreSQL) to avoid manual case handling.
  


Benefits:

No need for manual LOWER() handling.

Comparisons are case-insensitive by default.

Queries become simpler and potentially faster.


##  3- ✅ In-Memory Database Option (for High Performance)
For even faster lookups during journal scoring, especially in read-heavy environments, consider storing the word dictionary in memory.
Load the dictionary table into RAM using UNLOGGED tables or temp tables (PostgreSQL).


 Benefits:
Near-instant word lookups

Eliminates disk I/O during scoring

Reduces database load for repeated queries

Works well for relatively static data like predefined word lists





