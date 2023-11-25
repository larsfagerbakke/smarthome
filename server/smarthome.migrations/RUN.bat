:: Duplicate and edit message for each Migrations

:: dotnet ef migrations add InitialCreate --context AppDatabaseContext -o Migrations/App --project ../smarthome.shared --startup-project ../smarthome.migrations
:: dotnet ef migrations add AddedDeviceTokenTable --context AppDatabaseContext -o Migrations/App --project ../smarthome.shared --startup-project ../smarthome.migrations

set /p a=Press [ENTER] to continue...