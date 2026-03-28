# QuizGameApp
Quiz Game Application built with C#, Windows Forms, SQL Server, and ADO.NET featuring login, category and difficulty selection, randomized questions, shuffled answers, score tracking, timer, and leaderboard.

# Quiz Game Application

A sample desktop quiz game project built with **C# Windows Forms**, **SQL Server**, and **ADO.NET**.

This application allows users to log in, choose a quiz category and difficulty level, answer randomized questions, track scores, and view a leaderboard.

## Features

- User login
- Category selection
- Difficulty levels: Easy, Moderate, Hard
- Random questions from SQL Server
- Shuffled answer options
- Timer for each question
- Score calculation
- Result screen
- Leaderboard
- Improved UI design

## Tech Stack

- C#
- .NET Windows Forms
- SQL Server
- ADO.NET

## Database Tables

### Users
- UserId
- Name

### Questions
- QuestionId
- QuestionText
- OptionA
- OptionB
- OptionC
- OptionD
- CorrectAnswer
- Category
- Difficulty

### Scores
- ScoreId
- UserId
- Score

## Project Structure

- `Program.cs`
- `DbConfig.cs`
- `LoginForm.cs`
- `CategoryForm.cs`
- `QuizForm.cs`
- `ResultForm.cs`
- `LeaderboardForm.cs`
- `RecentQuestionTracker.cs`
- `Data/DatabaseHelper.cs`
- `Database/QuizGameDb.sql`

## How to Run

1. Open the project in Visual Studio or use the .NET CLI
2. Make sure SQL Server is installed and running
3. Update the connection string in `DbConfig.cs` if needed
4. Run the application

## Database Setup

Run the SQL script:

- `Database/QuizGameDb.sql`

This creates:
- `Users`
- `Questions`
- `Scores`

## Future Enhancements

- Admin panel for question management
- More categories
- Score history
- Sound effects
- Better analytics

