FROM mcr.microsoft.com/dotnet/aspnet:10.0

ENV DOTNET_EnableDiagnostics=0
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_URLS=http://+:8080

WORKDIR /app

COPY ./Todo.Minimalist.Api/publish/ .

ENTRYPOINT ["dotnet", "Todo.Minimalist.Api.dll"]
