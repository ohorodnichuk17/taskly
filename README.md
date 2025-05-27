# 🟣 Taskly — AI-powered Task & Board Manager with Solana Integration

**Taskly** is a full-stack task management platform inspired by Trello, built from scratch using modern technologies.  
Designed for individuals and teams, it features **Solana wallet authentication** and an **AI assistant** that enhances productivity by analyzing and improving tasks in real-time.

---

## 🔗 Live Demo  
🌐 Render: https://taskly-frontend-5bz1.onrender.com

📺 YouTube demo: https://www.youtube.com/watch?v=DW_Gt-20srE

🐦 Follow us on Twitter: [https://x.com/TasklyWeb](https://x.com/TasklyWeb)

---

## 🛠 Tech Stack

### Frontend
- **React** (Vite)
- **TypeScript**
- **Saas**
- **Solana Wallet Adapter**
- **Axios + Zustand**

### Backend
- **ASP.NET Core Web API**
- **CQRS + Clean Architecture**
- **Entity Framework Core + PostgreSQL**
- **Authentication via Solana Wallet Signature**
- **AI module (custom logic)**
- **SignalR for Boards**

### Infrastructure
- **Docker + Docker Compose**
- **PostgreSQL**

---

## ⚙️ Features

- ✅ Solana Wallet authentication
- ✅ Smart AI assistant for improving tasks
- ✅ Kanban board interface
- ✅ Create/update/delete boards, tasks, and columns
- ✅ Completion checkboxes and sorting for done tasks
- ✅ Responsive UI
- ✅ Role-based access (planned)

---

## 🧠 AI Integration

The built-in AI assistant helps users write better task descriptions, detect missing details, and suggest improvements — boosting both clarity and productivity.

---

## 🔐 Solana Wallet Integration

We use Solana Wallet Adapter for secure login without passwords. Users sign a unique message to authenticate and access their boards and tasks.

---

## 📸 Screenshots


<img width="1440" alt="main_page" src="https://github.com/user-attachments/assets/9f6be97b-89d4-47d1-9ecf-6953bf0ba8e3" />

<img width="406" alt="login solana" src="https://github.com/user-attachments/assets/58ea80ab-b015-4c32-a46d-d8cc831f7680" />

<img width="1436" alt="board in use with ai" src="https://github.com/user-attachments/assets/d0963fbf-310c-432c-a186-27c5ec79ae10" />

<img width="1437" alt="feedbacks" src="https://github.com/user-attachments/assets/9fa0138e-a1af-44f7-91d3-457104ddc828" />

<img width="509" alt="board_create" src="https://github.com/user-attachments/assets/f8bec1d2-0257-4f96-bb58-354219d600de" />

<img width="1439" alt="table" src="https://github.com/user-attachments/assets/c827d7ed-8243-4519-a485-eefea02b827d" />


---

## 📁 Repository Structure

This is a mono-repo with multiple parts:

```bash
/taskly-frontend    # React + Vite frontend  
/taskly-backend     # ASP.NET Core backend
/taskly-sender      # ASP.NET Core Microservice for sending emails to the mail using the SMTP protocol
```

## 🚀 Getting Started

### Prerequisites
- Node.js
- .NET SDK 8
- Docker & Docker Compose

## 👥 Authors

- [Yulia]([https://www.linkedin.com/in/...](https://www.linkedin.com/in/julia-ohorodnichuk/)) —  Fullstack Engineer. Expert in frontend and backend technologies driving seamless user experiences and design.
- [Nazar]([https://www.linkedin.com/in/...](https://github.com/zubnaz)) — Fullstack Engineer. Specialized in both frontend and backend, ensuring scalability and robust architecture for Taskly’s platform.

Made with ❤️ during the [Breakout Hackathon](https://www.colosseum.org/breakout)
