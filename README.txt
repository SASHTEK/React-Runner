# React Runner 🚀

React Runner is a lightweight Windows Desktop utility designed to streamline the development workflow for React and Node.js developers. Instead of manually opening terminals and typing commands, just drag, drop, and run!

<img src="screenshots/main_ui.png"/>

## ✨ Key Features

- **Drag & Drop Simplicity:** Drop any React/Vite/Node project folder directly into the app to get started.
- **Smart Validation:** Automatically checks for a `package.json` file and provides instant visual feedback (Green for valid, Red for invalid).
- **Automated Browser Launch:** Detects when your local server is ready and automatically opens it in **Edge InPrivate mode** (or your default browser).
- **Zero-Zombie Process Management:** Unlike standard `Process.Kill()`, React Runner forcefully cleans up nested `node.exe` and `esbuild.exe` processes to ensure your ports are always freed when you click Stop.
- **Clean UI Feedback:** Strips terminal ANSI color codes and handles UTF-8 character encoding to show you a clean "Live Status" directly on the UI.

## 🛠 How to Use

1. **Launch** the React Runner executable.
2. **Drag and Drop** your React project folder (the one containing `package.json`) into the center panel.
3. **Click Run.** The app will execute `npm run dev` in the background.
4. **Wait a second.** Your browser will automatically pop open once the server is live.
5. **Click Stop** to completely shut down the server and free up the port for your next session.

## 📋 Requirements

- **Windows OS**
- **Node.js & NPM** installed and added to your System PATH.
- **.NET Framework 4.7.2** or higher.

## 🚀 Installation & Build

If you want to build the project yourself:
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/ReactRunner.git
2. Open the `.sln` file in Visual Studio 2022.
3. Ensure you have the **.NET Desktop Development** workload installed.

## 📄 License
MIT License - Free for everyone!
