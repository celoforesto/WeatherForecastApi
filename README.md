This is a .NET-based backend application that provides weather forecast functionality, including the ability to manage favorite cities, retrieve weather forecasts, and secure access through JWT authentication.

Features
Backend Functionality
API REST:

Developed using .NET 8 or higher.
Endpoints:
Get Weather by City: Fetch the current weather for a given city, integrated with OpenWeatherMap/WeatherAPI.
Add City to Favorites: Allows the user to add a city to their list of favorite cities.
List Favorite Cities: Retrieve a list of all favorite cities for the authenticated user.
Remove City from Favorites: Allows the user to remove a city from their list of favorites.
5-Day Forecast: Retrieve the weather forecast for the next 5 days for a given city.

Requirements
.NET 8 or higher installed.
SQL Server instance for storing data.

Run the following script to create a sample user

INSERT INTO [dbo].[Users] (Id, Username, PasswordHash)
VALUES 
(
    NEWID(),
    'testuser', -- Test username
    'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=' -- SHA-256 Hash (Base64) of password "123456"
);

The application should be run using HTTPS. You can find the configuration in the launchSettings.json file:
json

Frontend
To access the frontend, visit the following repository: [Angular Weather App](https://github.com/celoforesto/angular-weather-app). This app consumes the backend API to display weather forecasts and manage favorite cities.
