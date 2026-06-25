code
Markdown
📋 Main Database Tables:
Users: Stores core account info (Auth).
Roles: Admin, Teacher, Student roles.
Courses: Metadata for courses (Title, Price, Image, TeacherId).
Sections / Units / Lessons: The educational hierarchy.
StudentSubscriptions: Handles payment proof and course access approval.
Notifications: System logs and real-time alerts.
RefreshTokens: Manages long-lived sessions.
✨ Key Features
Authentication System: Secure login/register, JWT management, and Password Reset flow via Email.
Content Management: Hierarchical structure for educational content (Course -> Section -> Unit -> Lesson).
Subscription Lifecycle: Students can request access by uploading receipts; Admins can Accept/Reject requests.
Real-time Alerts: Instant SignalR notifications when a course is added, updated, or an account is modified.
File Management: Specialized service for handling image and video uploads to protected directories.
Streaming: Protected video streaming to prevent unauthorized content downloading.
⚙️ How to Run Locally
Clone the repository:
code
Bash
git clone https://github.com/ahmadyoussefabuhassan/E-Learning.git
Configure settings:
Update appsettings.json with your SQL Server Connection String.
Add your JWT Key and Email Settings.
Apply Migrations:
code
Powershell
Update-Database
Run the project:
code
Bash
dotnet run --project E-Learning.Api
📧 Contact & Support
Developed by Ahmad Youssef Abu Hassan.
Feel free to reach out if you have any questions!
