[![Build and Test](https://github.com/Mohammad-Mans/real-time-weather-monitoring-service/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/Mohammad-Mans/real-time-weather-monitoring-service/actions/workflows/build-and-test.yml)

# Real-Time Weather Monitoring System

A C# console application that monitors weather data in **real-time** and activates specialized **weather bots** based on temperature and humidity thresholds.  
Data is processed from **JSON**, **XML**, and **YAML** formats with automatic format detection using the **Adapter Pattern** for third-party library integration.

## Objective

Build a maintainable console app that processes weather data from multiple formats and triggers appropriate weather response bots based on configurable thresholds.

## Features

### Weather Data Processing

- **Multi-Format Support** for weather data input: **JSON**, **XML**, and **YAML** formats
- **Automatic Format Detection** using parser selector pattern
- **Adapter Pattern** for third-party library integration (YamlDotNet)
- **Real-Time Processing** of incoming weather data
- **Error Handling** for unsupported formats and parsing failures

### Weather Bots

- **RainBot** - Activates when humidity exceeds threshold (default: 70%)
- **SunBot** - Activates when temperature exceeds threshold (default: 30°C)
- **SnowBot** - Activates when temperature falls below threshold (default: 0°C)
- **Configurable Messages** and thresholds per bot
- **Enable/Disable** individual bots via configuration

### Configuration Management

- **JSON-based Configuration** for bot settings
- **Runtime Configuration Loading** from external file
- **Flexible Threshold Management** per weather condition

## Tech

- .NET 8 (C#)
- Storage: JSON configuration files
- Third-party libraries: YamlDotNet (v13.7.1)
- Clean architecture:
  - **Domain**: Entities, Enums, Interfaces, Services, Factories, Adapters
  - **API**: Console UI (menu system)
  - **Configuration**: JSON-based bot settings
  - **Parsing**: Parser selector pattern with adapter integration

## Project Structure

```
/RTWMS
  ├── API                         # UI layer
  │   ├── WeatherDataEditMenu     # Weather data editing interface
  │   └── WeatherMonitoringMenu   # Console interface and user interaction
  ├── Configuration               # Configuration layer
  │   └── bot-settings.json      # Bot configuration file
  ├── Domain                      # Domain/Business layer
  │   ├── Adapters                # Third-party library adapters
  │   │   └── YamlDotNetAdapter   # YamlDotNet library adapter
  │   ├── Bots                    # Weather bot implementations
  │   │   ├── WeatherBot          # Abstract base class
  │   │   ├── RainBot             # Humidity-based bot
  │   │   ├── SunBot              # High temperature bot
  │   │   └── SnowBot             # Low temperature bot
  │   ├── Decorators              # Decorator pattern implementations
  │   │   ├── LoggingDecorator    # Logging functionality
  │   │   └── LoggingWeatherBotDecorator # Weather bot logging
  │   ├── Enums                   # BotType enumeration
  │   ├── Factories               # Factory pattern implementations
  │   │   └── WeatherBotFactory   # Creates weather bot instances
  │   ├── Interfaces              # Contracts for services and factories
  │   ├── Models                  # Domain models (WeatherData, BotConfig)
  │   ├── Parsers                 # Data parsing implementations
  │   │   ├── JsonDataParser      # JSON weather data parser
  │   │   ├── XmlDataParser       # XML weather data parser
  │   │   └── ParserSelector      # Parser selection logic
  │   └── Services                # Application logic
  │       ├── BotManager          # Bot management service
  │       ├── WeatherDataSubject  # Observer pattern subject
  │       └── WeatherMonitoringService # Core weather processing service
  ├── Utils                       # Helper Classes
  │   └── ConfigurationReader     # JSON configuration loader
  └── Program.cs                  # Program entrypoint
```

## Getting Started

1. **Clone**

```
git clone https://github.com/Mohammad-Mans/real-time-weather-monitoring-service.git
cd RTWMS
```

2. **Run**

```
dotnet run
```

> Requires .NET 8 SDK (or later).

## Configuration

- The **Configuration/bot-settings.json** file contains bot settings and thresholds
- Modify bot **enabled** status, **thresholds**, and **messages** as needed
- Configuration is loaded at startup and supports case-insensitive JSON properties

## How to Use

1. **Start the application** - The weather monitoring system will initialize
2. **Enter weather data** in JSON, XML, or YAML format:
   - **JSON Example**: `{"Location": "City Name", "Temperature": 32, "Humidity": 40}`
   - **XML Example**: `<WeatherData><Location>City Name</Location><Temperature>32</Temperature><Humidity>40</Humidity></WeatherData>`
   - **YAML Example**: `{ Location: "City Name", Temperature: 32, Humidity: 40 }`
3. **View bot responses** - Active bots will display their messages based on thresholds
4. **Edit weather data** - Option to modify previously entered data
5. **Type '3'** to quit the application

## Sample Usage Flow

```
Weather Monitoring System Started!

------------------------
1. Enter Weather Data
2. Edit Weather Data
3. Quit
------------------------
Enter your choice (1-3): 1

Enter weather data (JSON, XML, or YAML format):
Weather data: { Location: "Desert City", Temperature: 35, Humidity: 20 }
SunBot activated!
SunBot: "Wow, it's a scorcher out there!"
[17:56:33.496] SunBot: processed in 0ms
Weather data processed successfully!

------------------------
1. Enter Weather Data
2. Edit Weather Data
3. Quit
------------------------
Enter your choice (1-3): 1

Enter weather data (JSON, XML, or YAML format):
Weather data: { Location: "Rainy City", Temperature: 25, Humidity: 80 }
RainBot activated!
RainBot: "It looks like it's about to pour down!"
[17:57:17.937] RainBot: processed in 0ms
Weather data processed successfully!

------------------------
1. Enter Weather Data
2. Edit Weather Data
3. Quit
------------------------
Enter your choice (1-3): 3
Goodbye!
```

## Design Highlights

- **Factory Pattern** for bot creation
- **Adapter Pattern** for third-party library integration (YamlDotNet)
- **Observer Pattern** for weather bot notifications
- **Decorator Pattern** for logging functionality
- **Parser Selector Pattern** for automatic format detection
- **Dependency Injection** throughout the system
- **Service Layer** for business logic orchestration
- **Configuration-driven** bot behavior
- **Clean separation** of concerns across layers

## System Architecture

For a visual representation of the system architecture and design patterns, see the [UML-style diagram](https://excalidraw.com/#json=WdTnWn-BmR7enM60klxJ5,uPvUhdhE9I4qCfJ0Qq6A_A) created with Excalidraw.

<img width="5652" height="2358" alt="image" src="https://github.com/user-attachments/assets/20ed8574-2478-418e-b7c0-fc76b9a4e23b" />


## :stars: Acknowledgment

Special thanks to [**Foothill Technology Solutions**](https://www.foothillsolutions.com/) for the opportunity to work on this project during my internship. The experience and knowledge gained have been invaluable.
