# Real-Time Weather Monitoring System

A C# console application that monitors weather data in **real-time** and activates specialized **weather bots** based on temperature and humidity thresholds.  
Data is processed from **JSON** and **XML** formats with automatic format detection.

## Objective

Build a maintainable console app that processes weather data from multiple formats and triggers appropriate weather response bots based on configurable thresholds.

## Features

### Weather Data Processing

- **Multi-Format Support** for weather data input: **JSON** and **XML** formats
- **Automatic Format Detection** using parser factory pattern
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
- Clean architecture:
  - **Domain**: Entities, Enums, Interfaces, Services, Factories
  - **API**: Console UI (menu system)
  - **Configuration**: JSON-based bot settings
  - **Parsing**: Factory pattern for format detection

## Project Structure

```
/RTWMS
  ├── API                         # UI layer
  │   └── WeatherMonitoringMenu   # Console interface and user interaction
  ├── Configuration               # Configuration layer
  │   └── bot-settings.json      # Bot configuration file
  ├── Domain                      # Domain/Business layer
  │   ├── Bots                    # Weather bot implementations
  │   │   ├── WeatherBot          # Abstract base class
  │   │   ├── RainBot             # Humidity-based bot
  │   │   ├── SunBot              # High temperature bot
  │   │   └── SnowBot             # Low temperature bot
  │   ├── Enums                   # BotType enumeration
  │   ├── Factories               # Factory pattern implementations
  │   │   ├── ParserFactory       # Creates appropriate data parser
  │   │   └── WeatherBotFactory   # Creates weather bot instances
  │   ├── Interfaces              # Contracts for services and factories
  │   ├── Models                  # Domain models (WeatherData, BotConfig)
  │   ├── Parsers                 # Data parsing implementations
  │   │   ├── JsonDataParser      # JSON weather data parser
  │   │   └── XmlDataParser       # XML weather data parser
  │   └── Services                # Application logic
  │       ├── BotConfigurationService  # Bot configuration management
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
2. **Enter weather data** in JSON or XML format:
   - **JSON Example**: `{"Location": "City Name", "Temperature": 32, "Humidity": 40}`
   - **XML Example**: `<WeatherData><Location>City Name</Location><Temperature>32</Temperature><Humidity>40</Humidity></WeatherData>`
3. **View bot responses** - Active bots will display their messages based on thresholds
4. **Type '0'** to quit the application

## Sample Usage Flow

```
Weather Monitoring System Started!
Enter weather data (JSON or XML format):
Type '0' to quit

Enter weather data: {"Location": "Desert City", "Temperature": 35, "Humidity": 20}
SunBot activated!
SunBot: "Wow, it's a scorcher out there!"

Enter weather data: {"Location": "Rainy City", "Temperature": 25, "Humidity": 80}
RainBot activated!
RainBot: "It looks like it's about to pour down!"

Enter weather data: 0
Goodbye!
```

## Design Highlights

- **Factory Pattern** for parser and bot creation
- **Strategy Pattern** for different parsing approaches
- **Dependency Injection** throughout the system
- **Service Layer** for business logic orchestration
- **Configuration-driven** bot behavior
- **Clean separation** of concerns across layers

## :stars: Acknowledgment

Special thanks to [**Foothill Technology Solutions**](https://www.foothillsolutions.com/) for the opportunity to work on this project during my internship. The experience and knowledge gained have been invaluable.
