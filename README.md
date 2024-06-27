# HiTech Social Network

## Overview

This project is a social networking. The frontend is built using ViteJS with ReactJS, TypeScript, and styled using TailwindCSS, while the backend is powered by ASP.NET Core.

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

**Backend:**
- [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet)

## Getting Started

### Prerequisites

Ensure you have the following installed:
- Node.js (>=14.x)
- npm (>=6.x) or yarn (>=1.x)
- Java (>=11)
- Maven (>=3.6.3)

### Installation

1. **Clone the repository:**
    ```sh
    git clone https://github.com/thanhntse/HiTech-social-network.git
    cd HiTech-social-network
    ```

2. **Backend Setup:**
    ```sh
    cd hitech-backend
    dotnet run
    ```

3. **Frontend Setup:**
    ```sh
    cd platform-frontend
    npm install
    npm run dev
    ```

The frontend application will be available at `http://localhost:3000` and the backend at `http://localhost:5000`.

## Project Structure

```plaintext
HiTech-social-network/
├── hitech-backend/                # ASP.NET Core backend code
│   ├── Controllers/
│   ├── Models/
│   ├── Program.cs
│   └── ...
└── platform-frontend/             # React frontend code
    ├── src/
    ├── public/
    ├── package.json
    └── ...
