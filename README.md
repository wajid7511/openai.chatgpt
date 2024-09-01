# OpenAI ChatGPT Integration

Welcome to the OpenAI ChatGPT Integration project! This repository demonstrates how to integrate OpenAI's ChatGPT API into a .NET Core application, providing a simple and efficient way to interact with the ChatGPT model via a web interface.

## Features

- **User Interaction**: Users can input text prompts and receive responses from the ChatGPT model in real-time.
- **API Integration**: Securely connects to the OpenAI API to communicate with ChatGPT, ensuring seamless interaction.
- **Modular Design**: The project follows clean architecture principles, making it easy to maintain, extend, and scale.
- **Asynchronous Processing**: Utilizes async/await patterns for handling API requests, ensuring responsive and efficient operation.
- **Scalable Architecture**: The design allows for easy scalability and future enhancements.

## Technologies Used

- **ASP.NET Core**: A cross-platform framework for building modern, cloud-based, internet-connected applications.
- **C#**: The primary programming language used in the project.
- **OpenAI API**: Provides access to OpenAI's powerful language models, including ChatGPT.
- **Dependency Injection**: Ensures loose coupling and enhances testability.
- **Clean Architecture**: Organizes code into layers to enforce a separation of concerns and facilitate scalability.

## Getting Started

### Prerequisites

- .NET Core SDK 6.0 or later
- An OpenAI API key (You can obtain it by signing up on the [OpenAI website](https://beta.openai.com/signup/))

### Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/wajid7511/openai.chatgpt.git
   cd openai.chatgpt
Set up your OpenAI API key:

Create a appsettings.json file in the project's root directory or replace is appsettings.json and add your OpenAI API key:
 
```bash
    {
      "OpenAI": {
        "ApiKey": "your-openai-api-key"
        }
    }
```
    
Restore dependencies and build the project:
```bash 
dotnet restore
dotnet build
```
Run the application:

```bash 
dotnet run
```
Access the application:

Open your web browser and navigate to http://localhost:5000 to start interacting with ChatGPT.

Usage
Enter your text prompt in the provided input field and submit it.
The application will send the prompt to the ChatGPT model using the OpenAI API.
You'll receive a response from ChatGPT, which will be displayed on the screen.

## Contributing
Contributions are welcome! If you'd like to contribute to this project, please fork the repository, make your changes, and submit a pull request.

## License
This project is licensed under the MIT License. See the LICENSE file for details.

## Acknowledgements
OpenAI for providing the powerful ChatGPT model.

The .NET Core community for their extensive documentation and support.