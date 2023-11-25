# Smarthome migrations

This is the project for running database migrations for the smarthome project. All the models are in the shared project, but thats a class library and entity Framework needs a running application to run migrations. This is also a good candidate the a init container or something, running before any server applications/containers.

## Installing tools

Install the tool for your version of .NET. I'm a bit behind :)

```dotnet tool install --global dotnet-ef --version 7.0.4```

## Creating migrations

Look at the RUN.bat file and create entries while commenting (good pratice) previous ones.