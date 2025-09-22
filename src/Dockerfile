FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS runtime

ENV DOTNET_EnableDiagnostics=0 \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    ASPNETCORE_URLS=http://+:8080 \

WORKDIR /app

COPY ./Todo.Minimalist.Api/publish/ .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Todo.Minimalist.Api.dll"]