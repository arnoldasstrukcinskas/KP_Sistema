# KP_Sistema
## Table of Contents
- [About](#about)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Installation](#installation)
- [Downloading project](#downloading-project)
- [Launching](#launching)
- [Database Testing](#database-testing)
- [Overall Testing](#overall-testing)
## About
  The "Community Management System" is developed as a practice project. The system is designed as a dashboard (front-end + back-end) with basic functionality for managing users, communities, and utility services.  
  It provides different roles: regular users, managers, and administrators, each with specific permissions. Users can view their communities and pay for services, managers can create, edit, delete, and assign utility tasks, while administrators can also manage communities, user roles, and overall system data.  
## Features
- Register/Login/Logout
- Roles system
- Communities management
- Utility Services management
- Users management
## Tech Stack
| Technology | Purpose |
|------------|---------|
| ASP.NET Core 8.0 | Web API Framework |
| Blazor 8.0.21 | Front-End Framework |
| C# 12 | Programming language |
| Swagger | API documentation |
| MySql | Database |
| Docker | Components containerization |

## Installation
1. Whole project is containerized and created using containers so firstly you need to install docker engine:
Link: https://docs.docker.com/engine/install/

## Downloading project
For project to launch firstly need to download project or clone it from repository.
#### 1. Download
- Go to this repository: https://github.com/arnoldasstrukcinskas/KP_Sistema
- Then press green button Code and and Download Zip.
- Unpack zip file
- Move to [Launching](#launching)
#### 2. Clone(If you have git)
- Open Terminal and go to directory you want to clone project(add your own directory)
  ```bash
  cd D:\example
  ```
- In terminal use this command:
  ```bash
  git clone https://github.com/arnoldasstrukcinskas/KP_Sistema.git
  ```
- Move to [Launching](#launching)
  
## Launching
#### 1. If you do not have .NET tools pacakge you have to install or update it(for migrations):
##### Install (Use in any directory):
```bash
dotnet tool install --global dotnet-ef
```
##### Update: (Use in any directory)
```bash
dotnet tool update --global dotnet-ef
```
#### 2. Go to project directory(this is example directory)
```bash
cd "D:\Downloads\KP_Sistema"
```
#### 3. Build project(with logs)
```bash
docker-compose build
```
#### 4. Launch project(with logs)
```bash
docker-compose up
```
#### 5. Launch project(without logs) -> OPTIONAL
```bash
docker-compose up -d
```
#### 6. Stop project(without logs)
```bash
ctrl + c
```

#### EXTRA! Migrations are created. (In case u will need to create migrations of database)
##### In main directory of project(go via CMD):
```bash
dotnet ef migrations add InitialCreate --project KP_Sistema.DATA --startup-project KP_Sistema.API
```
##### Auto-migrations are used but if needed to update manually:
```bash
dotnet ef database update --project KP_Sistema.DATA --startup-project KP_Sistema.API
```

## Database Testing
(How to connect for testing via cmd)
#### 1. Open terminal and check container ID
```bash
docker ps
```
#### 2. Connect to database(Enter container ID or name)
```bash
docker exec -it kp_sistemaDB mysql -u root -p
(passwort: root)
```
#### 3. Chech if database exists
```bash
SHOW DATABASES;
```
#### 4. Enter Database
```bash
USE kpsistema;
```

## Overall Testing
(How to connect for testing via front-end)
### For testing - some demo data is created
#### 1. Open terminal and check container ID
```bash
Go to: http://localhost:8081/login or http://localhost:8080/swagger/index.html
(But there is craeted accounts for testing)
```
#### 2. Connect to database(Enter conainer ID)
```bash
Enter:
  username: admin
  password: admin
  username: manager
  password: manager
```
#### 3. Last bot not least
```bash
HAVE FUN!
```
