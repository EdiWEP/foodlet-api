# Foodlet API 
The back-end API for the [Foodlet web application](https://github.com/EdiWEP/foodlet), developed in **ASP.NET Core**


## Implementation details
- **REST API** that acts as the connection between the front-end and the web server's SQL Server database
- Built using the **Repository Pattern**, the API has a layered architecture that separates controllers, business logic classes(managers) and repositories
- The database was created Code-First using **Entity Framework**
- Handles input validation and manages authentication using **Entity Framework Identity** and **JWT**


## Credits

Default ingredients were taken from the [Nutrition 5k Dataset](https://github.com/google-research-datasets/Nutrition5k)
