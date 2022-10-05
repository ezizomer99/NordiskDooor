# Controller

Mappe for å utføre handlinger. Severside som kobler views og modeller sammen.

### HomeController.cs
Hovedsiden. 
RazorViewModel - erstatning for JS.

### RESTController.cs
Vet ikke hva er 

### SuggestionsController.cs
Skal egentlig gjøre det mulig å legge inn forslag, returnerer view - suggestions - index
For øyeblikket får man feilmelding etter lagring

### UserController.cs




# DataAccess
Tilgang til data. Koble seg til server

### DataContext.cs

### ISqlConnector.cs
En interface. Alle som bruker connectoren må ha GetDBConnection-metode

### SqlConnector.cs
Kobler prosjekt med mariadatabasen.
Kan se at GetConnectionStrings ligger i appsettings.json som bli mysqlconnection.
Leser ting som kommer fra databasen.

# Entities
Det man henter fra tabellen og legger opp

### User.cs
Henter og deklarer ting. Det som legges inn i tabellen.

# Models
Hvordan det skal se ut på view siden.

## Suggestions
Forslag

### SuggestionViewModel.cs
Funker ikke per nå, men skal være formen til å sende inn forslag

## Users
Objekt som blir brukt i UserController 

### UserViewModel.cs

# Repositories
Der du lagrer ting

### IUserRepository.cs

### SqlUserRepository.cs
Sender sql statements til databasen. Og henter users fra database og legger det inn i en liste. Og sletter ting fra databasen.

# views
Det man kan se




