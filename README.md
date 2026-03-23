# NolMed 🏥
Hospital network app with patient assignment, ER vital screening, and patient records all in one place. This app is scalable and highly responsive, suited for use by a large hospital. The patient check in center allows for regular and urgent care visits. The emergency center allows for emergency visits, with vitals tracking and a visual display of all rooms and their status.  

## Features
* <b>HIPAA compliant:</b> Every user action is logged and safely stored
* <b>Encryption:</b> Passwords are encrypted and database access is secure
* <b>Real-time analytics:</b> Live updates for hospital administration  
* <b>Patient record management:</b> Secure CRUD operations on patient data  
* <b>Role-based access:</b> Access to necessary tools by job role

## Stack
* `C#`
* `WPF`
* `Redis`
* `MS SQL`
* `MVVM`

## Setup
### Prerequisites
* Visual Studio 2022 or newer
* .NET 9.0 SDK
* MS SQL Server
* Redis

### Installation
1. Clone the repo: `git clone https://github.com/happySquidd/NolMed`
2. Open the solution `.sln` file in Visual Studio
3. Update the `appsettings.json` with your local MS SQL connection string
4. In the `database` folder open `DatabaseContext.cs` and migrate all tables into your local schema
5. Build and run the project (F5)

### Use Tips
To get access to all tabs, register a new user, and give it "Admin" priviledge in your database. This will give you access to all the tabs.  

## Project Roadmap 
#### ER UI polishing  
#### ER alerts  
#### Redis testing/stress testing  
#### Run and connect app to a live raspy server  

