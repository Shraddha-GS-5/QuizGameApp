# QuizGameApp
Quiz Game Application built with C#, Windows Forms, SQL Server, and ADO.NET featuring login, category and difficulty selection, randomized questions, shuffled answers, score tracking, timer, and leaderboard.

# Quiz Game Application

A desktop quiz game built with **C# Windows Forms**, **SQL Server**, and **ADO.NET**.  
This project is designed as a beginner-friendly but presentation-ready college project with a polished UI, difficulty levels, timer-based gameplay, randomized questions, shuffled answers, score saving, and leaderboard support.

## Features

- Login screen with user name saved to database
- Category selection:
  - Programming
  - GK
  - Aptitude
- Difficulty levels:
  - Easy
  - Moderate
  - Hard
- 5 random questions loaded from SQL Server
- Answer choices shuffled in C#
- Timer with progress bar
- Score tracking and result screen
- Score saved in database
- Leaderboard for top scores
- Improved UI for college/project presentation
- Expanded question bank to reduce repeated questions
- Recent-question tracking to avoid showing the same questions too often on replay

## Tech Stack

- C#
- .NET Windows Forms
- SQL Server
- ADO.NET

## Database Tables

### Users
- `UserId`
- `Name`

### Questions
- `QuestionId`
- `QuestionText`
- `OptionA`
- `OptionB`
- `OptionC`
- `OptionD`
- `CorrectAnswer`
- `Category`
- `Difficulty`

### Scores
- `ScoreId`
- `UserId`
- `Score`

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

## How It Works

1. User enters name on the login screen
2. User selects category and difficulty
3. App loads 5 random questions from SQL Server
4. Answer options are shuffled before display
5. Timer runs for each question
6. Final score is shown and saved
7. Leaderboard displays top scores

## Connection String

The project uses the connection string from `DbConfig.cs`:

```csharp
Server=localhost;Database=QuizGameDb;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;
```

Update this if your SQL Server instance name is different.

## Database Setup

You can set up the database in either of these ways:

### Option 1: Automatic setup
The application creates the database structure and seeds questions automatically on startup if needed.

### Option 2: Manual setup in SSMS
Run:

- `Database/QuizGameDb.sql`

This script creates:
- `QuizGameDb`
- `Users`
- `Questions`
- `Scores`

## How to Run the Project

### Using PowerShell

```powershell
cd "C:\Users\shrad\Documents\New project\QuizGameApp"
dotnet run
```

### Using Visual Studio

1. Open Visual Studio
2. Open `QuizGameApp.csproj`
3. Build and run the project

## Screenshots to Add on GitHub

You can improve this README further by adding screenshots of:

- Login screen
- Category and difficulty screen
- Quiz screen
- Result screen
- SSMS database tables

Example:

```md
![Login Screen](screenshots/login.png)
![Quiz Screen](screenshots/quiz.png)
```

## Future Improvements

- Admin panel to add or manage questions
- Sound effects and animations
- User-wise score history
- More categories
- Export results
- Dark mode / theme switcher

## Author

Developed as a sample project using Windows Forms, SQL Server, and ADO.NET.

