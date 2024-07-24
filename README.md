# HiTech Social Network

## Overview

HiTech Social Network is a social networking platform. The frontend is built using ViteJS with ReactJS, TypeScript, and styled using TailwindCSS, while the backend is powered by ASP.NET Core with a microservices architecture and Docker for containerization.

## Features

1. **Home Feed**
   - Display posts from friends and users that the user follows.
   - Real-time updates.

2. **User Profile**
   - Customize profiles with profile pictures, bios, and personal information.
   - Display user posts, friends, and photos.

3. **Post Creation**
   - Create text, image, and video posts.
   - Tag friends and locations in posts.

4. **Comments and Likes**
   - Comment on and like posts.
   - Display post likes and comments.

5. **Messaging and Chat**
   - Direct messaging system between users.
   - Group chat functionality.

6. **Notifications**
   - Notifications for likes, comments, and shares.
   - Friend requests and other event notifications.

7. **Friends and Following**
   - Send friend requests and follow other users.
   - Display friends and followers lists.

## Tech Stack

**Frontend:**
- [Vite](https://vitejs.dev/)
- [React](https://reactjs.org/)
- [TypeScript](https://www.typescriptlang.org/)
- [SWC](https://swc.rs/)
- [TailwindCSS](https://tailwindcss.com/)
- [Material UI](https://mui.com/core/)

**Backend:**
- [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet)
- Microservices architecture
- Docker for containerization

## Getting Started

### Prerequisites

Ensure you have the following installed:
- Node.js (>=14.x)
- npm (>=6.x) or yarn (>=1.x)
- .NET SDK (>=5.0)
- Docker

### Installation

1. **Clone the repository:**
    ```sh
    git clone https://github.com/thanhntse/HiTech-social-network.git
    cd HiTech-social-network
    ```

2. **Backend Setup with Docker:**
    ```sh
    cd hitech-backend
    docker-compose up
    ```

3. **Frontend Setup:**
    ```sh
    cd platform-frontend
    npm install
    npm run dev
    ```

## Project Structure

```plaintext
HiTech-social-network/
├── hitech-backend/                # ASP.NET Core backend code
│   ├── Services/                  # Microservices
│   ├── Dockerfile                 # Docker configuration
│   ├── docker-compose.yml         # Docker Compose file
│   └── ...
└── hitech-frontend/               # React frontend code
    ├── src/
    ├── public/
    ├── package.json
    └── ...
