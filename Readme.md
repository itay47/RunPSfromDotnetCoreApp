This project is intended to provide a working POC for calling PS1 scripts from a dotnet 5.0 application.
we are using here Runspaces.
look closely for the initialSessionState which must set to Unrestricted - otherwise, the application will fail to run the PS1 scripts
