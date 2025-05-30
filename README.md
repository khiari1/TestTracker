# Tsi-ERP-TestTracker

## Description
Tsi-ERP-TestTracker is a web platform designed for monitoring and visualizing software test campaigns. This project combines a robust backend developed in .NET and a modern frontend built with Angular, providing a comprehensive solution for test tracking and management.

---

## Table of Contents
- [Project Structure](#project-structure)
- [Technologies Used](#technologies-used)
- [CI/CD and Docker](#ci-cd-and-docker)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Running the Application](#running-the-application)
- [Contributing](#contributing)

---

## Project Structure

The project is organized as follows:

```
Tsi-ERP-TestTracker/
├── .github/workflows/          # Contains CI/CD pipeline configurations for GitHub Actions
│   └── deploy.yml              # Workflow for deployment
├── Core/                       # Core business logic and domain entities
├── Identity/                   # User authentication and authorization services
├── Infrastructure/             # Infrastructure-specific implementations (e.g., database, external services)
├── Presentations/              # Contains the frontend and backend presentation layers
│   ├── TestTracker-ClientApp/  # Angular-based frontend application
│   └── Tsi.Erp.TestTracker.Api # .NET-based backend API
├── Tsi.AutomatedTestRunner/    # Automated testing components
├── .editorconfig               # Code style and formatting configuration
├── .gitattributes              # Git attributes configuration
├── .gitignore                  # Files and directories to ignore in Git
├── Dockerfile.dev              # Dockerfile for building the development image
├── README.md                   # Project documentation (this file)
├── Tsi.Erp.TestTracker.sln     # Visual Studio solution file
└── azure-pipelines.yml         # Azure DevOps pipeline configuration for CI/CD
```

### Key Components:
- **`Core/`**: Houses the core business logic and domain models to ensure a clean architecture.
- **`Identity/`**: Manages user authentication and authorization, including token issuance and validation.
- **`Infrastructure/`**: Responsible for database access, external API integrations, and other infrastructure-specific concerns.
- **`Presentations/`**: Contains the two main layers:
  - `TestTracker-ClientApp/`: The Angular frontend application for user interaction.
  - `Tsi.Erp.TestTracker.Api/`: The .NET Web API responsible for serving backend functionalities.
- **`Tsi.AutomatedTestRunner/`**: Contains logic and tools for automated testing, ensuring software reliability.
- **`.github/workflows/`**: Includes GitHub Actions workflows for automating CI/CD processes.
- **`azure-pipelines.yml`**: Configures Azure DevOps pipelines for CI/CD, enabling automated builds, tests, and deployments.
- **`Dockerfile.dev`**: A Docker configuration file for generating a Docker image for development purposes.

---

## Technologies Used

- **Backend**:
  - .NET 6.0: Framework for building the backend API.
  - Entity Framework Core: ORM for database management.
- **Frontend**:
  - Angular: Framework for building the user interface.
  - TypeScript: Language used for Angular development.
- **Others**:
  - SQL Server: Database used for storing test campaign data.
  - Docker: Containerization platform for consistent development and deployment.

---

## CI/CD and Docker

### CI/CD Pipelines
The project employs the following CI/CD practices:
- **GitHub Actions**: Defined in `.github/workflows/deploy.yml` for automating builds, tests, and deployments.
- **Azure DevOps**: Configured in `azure-pipelines.yml` for managing CI/CD workflows, integrating with Azure services.

### Docker
- **`Dockerfile.dev`**: Used to create Docker images for development. It simplifies the setup process by packaging dependencies and the application into a consistent environment.

---

## Getting Started

### Prerequisites
Ensure you have the following installed:
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) and npm (required for Angular)
- [Angular CLI](https://angular.io/cli)
- [Docker](https://www.docker.com/) (optional but recommended for containerized development)
- A compatible browser (e.g., Chrome, Firefox, Edge)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/TSI-Softwares/Tsi-Erp-TestTracker.git
   cd Tsi-ERP-TestTracker
   ```

2. Set up the backend:
   ```bash
   cd Presentations/Tsi.Erp.TestTracker.Api
   dotnet restore
   dotnet build
   ```

3. Set up the frontend:
   ```bash
   cd ../TestTracker-ClientApp
   npm install
   ```

### Running the Application

#### Backend
1. Navigate to the `Tsi.Erp.TestTracker.Api` folder:
   ```bash
   cd Presentations/Tsi.Erp.TestTracker.Api
   ```

2. Start the backend server:
   ```bash
   dotnet run
   ```

3. By default, the API will be available at `http://localhost:5000`.

#### Frontend
1. Navigate to the `TestTracker-ClientApp` folder:
   ```bash
   cd Presentations/TestTracker-ClientApp
   ```

2. Start the Angular application:
   ```bash
   npm start
   ```

3. By default, the application will be accessible at `http://localhost:4200`.

---

## Contributing
Contributions are welcome! To contribute:

1. Fork the repository.
2. Create a new branch for your feature or bug fix:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add a new feature"
   ```
4. Push your changes to your fork:
   ```bash
   git push origin feature/your-feature-name
   ```
5. Submit a Pull Request with a detailed description of your changes.

---

Thank you for contributing to Tsi-ERP-TestTracker!
