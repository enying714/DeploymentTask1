# SWE40006 Deployment Activity 1 – WiX Desktop Deployment

## Assignment Overview

This repository contains the source code and WiX installer configuration for **Deployment Activity 1** in **SWE40006 Software Deployment and Evolution**.
Student: Cheong En Ying
ID: 105965515

The assignment demonstrates the deployment of a C# Windows desktop application using the **Windows Installer XML (WiX) Toolset**. The application developed for this task is a simple **Study Task Manager** built with Windows Forms.

The assignment is structured to demonstrate the following task levels:

- **Task 1.2 – Credit:** Deploy my own C# desktop application using WiX.
- **Task 1.3 – Distinction:** Deploy the application with multiple DLL dependencies.
- **Task 1.4 – High Distinction:** Explain in detail how the completed MSI-based application could be prepared and deployed through the Microsoft Store.

## Application Features

The Study Task Manager allows a user to add a study task, enter a subject, select a due date, view created tasks, mark a task as completed, delete a task, and view the number of pending tasks.

The application is intentionally small so that the main focus of the activity remains on deployment, dependency management, WiX packaging, installation and troubleshooting.

## Solution Structure

The Visual Studio solution contains four projects:

```text
StudyTaskManager
│
├── StudyTaskManager.App
│   └── Main Windows Forms desktop application
│
├── StudyTaskManager.Core
│   └── Class library compiled as StudyTaskManager.Core.dll
│
├── StudyTaskManager.Validation
│   └── Class library compiled as StudyTaskManager.Validation.dll
│
└── StudyTaskManager.Setup
    └── WiX installer project
```

### StudyTaskManager.App

This assignment contains the Windows Forms user interface and is the main application executed by the user.

### StudyTaskManager.Core

This assignment contains the task model and task-management operations. It is compiled as:

```text
StudyTaskManager.Core.dll
```

The main application depends on this DLL for task-related functionality.

### StudyTaskManager.Validation

This assignment contains validation logic for task title, subject and due date input. It is compiled as:

```text
StudyTaskManager.Validation.dll
```

The main application depends on this DLL for input validation.

### StudyTaskManager.Setup

This assignment contains the WiX configuration used to package the application and its required dependencies into an MSI installer.

The final MSI includes:

```text
StudyTaskManager.App.exe
StudyTaskManager.App.dll
StudyTaskManager.Core.dll
StudyTaskManager.Validation.dll
StudyTaskManager.App.deps.json
StudyTaskManager.App.runtimeconfig.json
```

## Development Environment

The project was developed and tested using:

```text
Visual Studio 2022
C# / Windows Forms
.NET 8 target framework
WiX Toolset v4
WiX CLI 4.0.6
HeatWave Visual Studio extension
Windows 11
```

## Build Instructions

Open PowerShell in the repository root.

To build the C# desktop application:

```powershell
dotnet build StudyTaskManager.App\StudyTaskManager.App.csproj
```

This compiles the main application together with the two referenced class-library projects.

To build the WiX installer:

```powershell
dotnet build StudyTaskManager.Setup\StudyTaskManager.Setup.wixproj
```

If the build completes successfully, the MSI is generated under the WiX project's `bin` output directory, for example:

```text
StudyTaskManager.Setup\bin\Debug\StudyTaskManagerInstaller.msi
```

## Installation

Run the generated MSI:

```text
StudyTaskManagerInstaller.msi
```

On the test machine used for this task, the application was installed under:

```text
C:\Program Files (x86)\Study Task Manager
```

The installed directory should contain the main executable, main application assembly, two custom DLL dependencies and the required .NET configuration files.

The installed application can then be launched using:

```text
StudyTaskManager.App.exe
```

## Verification of Multiple DLL Dependencies

This project uses two genuine custom class-library dependencies:

```text
StudyTaskManager.Core.dll
StudyTaskManager.Validation.dll
```

These DLLs are referenced by the main application and are required for normal application functionality. The Core library provides task-management functionality, while the Validation library provides input-validation functionality.

This structure is used to demonstrate deployment of an application with multiple DLLs/dependencies for Task 1.3.

## Troubleshooting Encountered During Deployment

### WiX Installer Hash Verification Failure

The WiX installer originally provided through the unit resources failed with error:

```text
0x80091007
Hash mismatch
Failed to verify hash of payload: Wix
```

The issue occurred while the WiX bootstrapper attempted to acquire and verify its own core MSI payload.

The WiX command-line tool was instead installed through the .NET global tool mechanism. The working version was verified using:

```powershell
wix --version
```

which returned:

```text
4.0.6+73c89738
```

### Visual Studio Setup Project Displayed as Incompatible

`StudyTaskManager.Setup` initially appeared as incompatible in Visual Studio.

The WiX command-line build was already functioning, which indicated that the issue was related to Visual Studio integration rather than the WiX project itself.

The HeatWave Visual Studio extension was installed and the setup project was reloaded, allowing the WiX project to be opened normally in Visual Studio.

### WIX0368 Component Error

The initial WiX build returned:

```text
WIX0368
```

The original configuration placed several application files inside one WiX component while using an automatically generated GUID.

The installer configuration was corrected by placing the executable, main application DLL, Core DLL, Validation DLL and .NET configuration files into separate WiX components.

After the correction, the WiX project built successfully.

### Installed Application Did Not Launch

The first MSI installed successfully, but the installed application did not open.

Investigation showed that:

```text
StudyTaskManager.App.dll
```

was missing from the installed directory.

The main application DLL was added to `Package.wxs` as its own WiX component. The previous version was uninstalled, the installer was rebuilt and the new MSI was installed.

After this correction, the installed application launched successfully.

## Microsoft Store Deployment

For Task 1.4, the project report explains how this WiX-generated MSI could be prepared for Microsoft Store distribution using the MSI/EXE submission route.

A production Store submission would additionally require steps such as creating or using a Microsoft Partner Center developer account, preparing and testing a production MSI, supporting silent installation, digitally signing the installer and executable files, hosting the installer at a stable versioned HTTPS URL, entering the package details in Partner Center, preparing Store screenshots and listing information, submitting the application for certification, and publishing it after successful certification.

The full Microsoft Store deployment explanation and references are included in the submitted deployment report.

## Repository Purpose

This repository is provided as the **public verification source** for the assignment.

It contains the final source code and WiX configuration required to verify the implementation.

Generated binaries and build output should not be committed to the repository. Typical excluded files include:

```text
bin/
obj/
.vs/
*.msi
```

## Important Files

The main files for verification are:

```text
StudyTaskManager.App\Program.cs
StudyTaskManager.App\MainForm.cs
StudyTaskManager.App\StudyTaskManager.App.csproj

StudyTaskManager.Core\TaskItem.cs
StudyTaskManager.Core\TaskService.cs
StudyTaskManager.Core\StudyTaskManager.Core.csproj

StudyTaskManager.Validation\TaskValidator.cs
StudyTaskManager.Validation\StudyTaskManager.Validation.csproj

StudyTaskManager.Setup\Package.wxs
StudyTaskManager.Setup\StudyTaskManager.Setup.wixproj

StudyTaskManager.sln
```

## Unit Information

**Unit:** SWE40006 Software Deployment and Evolution  
**Assessment:** Deployment Activity 1 – Desktop Deployment Using WiX  
**Task Level Attempted:** Task 1.4 – High Distinction
