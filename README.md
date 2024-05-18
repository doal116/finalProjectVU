# Brute Force Password Cracker with WPF Interface

This project implements a brute force password cracker with a graphical user interface (GUI) built using WPF. It allows users to:

- Create passwords with a randomly generated salt and store them securely with AES encryption.
- Select a password from a saved list and attempt to crack it using a brute force attack.
- Specify the number of threads to use for the attack, leveraging multi-threading for faster processing.

## Features

- WPF graphical user interface for ease of use.
- Secure password storage with AES encryption.
- Multi-threaded brute force attack for improved performance.
- Displays the time taken to crack the password and the permutations attempted.

## Project Structure

The project is organized into several class files:

- `MainWindow.xaml.cs`: Handles the main window logic, including creating new windows for password creation and brute force attack selection.
- `PasswordCreation.xaml.cs`: Manages the password creation window, allowing users to enter usernames and passwords, generating a salt, and saving the encrypted password to a CSV file.
- `BreakPasswordSelect.xaml.cs`: Reads usernames, encrypted passwords, and expected passwords from the CSV file and displays them in a list. It also opens the brute force attack window for a selected user.
- `BruteForce.xaml.cs`: Controls the brute force attack window. It allows users to specify the number of threads and displays the results, including the time taken and the cracked password (if found).
- `AESAlgo.cs`: Provides methods for encrypting and decrypting passwords using the AES standard with a predefined key and initialization vector (IV).
- `PermutationGenerator.cs`: Generates all possible combinations of characters for a brute force attack, utilizing multi-threading for faster execution.

## Running the Project

1. Clone the repository to your local machine.
2. Open the solution file (.sln) in Visual Studio.
3. Build and run the project.

## Dependencies

This project requires the following:

- .NET Framework
- `System.Windows.Forms` (for functionalities like file reading/writing)
- `System.Drawing` (for UI elements)
