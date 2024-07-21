# AdminUserManagementService

## Description

AdminUserManagementService is a service designed for managing admin users and their roles within an ASP.NET Core application. This service utilizes ASP.NET Core Identity to handle user onboarding, role assignments, and role management. It simplifies the process of creating new admin users, assigning them to roles, and ensuring that roles are properly created and assigned.

## Features

- Onboard new admin users with email, password, and role information.
- Create roles if they do not exist.
- Assign roles to users.
- Integrated with ASP.NET Core Identity for user and role management.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or higher)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or any compatible database

### Installation

1. **Clone the repository:**

    ```bash
    git clone https://github.com/yourusername/AdminUserManagementService.git
    ```

2. **Navigate to the project directory:**

    ```bash
    cd AdminUserManagementService
    ```

3. **Restore the dependencies:**

    ```bash
    dotnet restore
    ```

4. **Build the project:**

    ```bash
    dotnet build
    ```

5. **Run the application:**

    ```bash
    dotnet run
    ```

### Running in Visual Studio

1. **Open Visual Studio** and select **Open a project or solution** from the start window.

2. **Navigate to the project directory** and select the `.csproj` file for the project.

3. **Open the project** in Visual Studio.

4. **Restore the NuGet packages** if they haven't been restored automatically:
    - Right-click on the solution in the Solution Explorer.
    - Select **Restore NuGet Packages**.

5. **Build the project**:
    - Go to **Build** in the menu bar.
    - Select **Build Solution** (or press `Ctrl+Shift+B`).

6. **Run the application**:
    - Set the project as the startup project if it isn’t already.
    - Click the **Start** button in the toolbar or press `F5` to start debugging.

7. **Configure the project**:
    - Update the `appsettings.json` file with your database connection string and other configuration settings.
    - Apply migrations to set up your database schema using the Package Manager Console:
        - Go to **Tools** > **NuGet Package Manager** > **Package Manager Console**.
        - Run the following commands:
        
        ```powershell
        Add-Migration InitialCreate
        Update-Database
        ```

### Usage

- **Onboard Admin User:** POST to `/api/adminuser/onboard` with the following JSON payload:

    ```json
    {
        "fullName": "John Doe",
        "email": "john.doe@example.com",
        "password": "YourSecurePassword",
        "role": "Admin"
    }
    ```

    This will create a new admin user, ensure the role exists, and assign the role to the user.

### API Endpoints

- `POST /api/adminuser/onboard` - Onboards a new admin user and assigns a role.
- `POST /api/adminuser/create-role` - Creates a new role (if needed).

### Contributing

Contributions are welcome! Please open an issue or submit a pull request to contribute to this project.

### License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

Feel free to modify this template to better suit your project’s specifics and any additional details you want to include.
