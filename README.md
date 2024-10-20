Windows Form Application with WSL and ABB Robotics Integration
This repository contains a Windows Form Application that leverages the Windows Subsystem for Linux (WSL) to run gphoto2 in combination with the ABB Robotics PC SDK to manipulate a robot. The application provides a user-friendly interface for controlling the robot and managing camera operations.

Features
Robot Manipulation: Utilize the ABB Robotics PC SDK to control and manipulate the robot.
Camera Control: Use gphoto2 through WSL to manage camera operations.
User Interface: A Windows Form Application that provides an intuitive interface for interacting with the robot and camera.
Project Structure
Rapid.cs: Contains commands and controls for interfacing with the robot.
gphoto.cs: Manages the gphoto2 and WSL interfaces.
Getting Started
Prerequisites
Windows 11 or later with WSL installed. (Requires usbipd)
ABB Robotics PC SDK.
gphoto2 installed on WSL.
Installation
Clone the repository:
git clone https://github.com/spydazcomputers/Robot_Photogrammetry_Inspection.git
Open the solution in Visual Studio.
Build the solution to restore the necessary packages.
Usage
Launch the Windows Form Application.
Use the provided interface to control the robot and manage camera operations.
Contributing
Contributions are welcome! Please fork the repository and submit a pull request with your changes.

License
This project is licensed under the MIT License. See the LICENSE file for details.
