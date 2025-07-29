
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


COPY TesteLocalize.WebAPI/TesteLocalize.WebAPI.csproj TesteLocalize.WebAPI/
COPY TesteLocalize.Application/TesteLocalize.Application.csproj TesteLocalize.Application/
COPY TesteLocalize.Infra/TesteLocalize.Infra.csproj TesteLocalize.Infra/
COPY TesteLocalize.Domain/TesteLocalize.Domain.csproj TesteLocalize.Domain/
RUN dotnet restore TesteLocalize.WebAPI/TesteLocalize.WebAPI.csproj


COPY . .


RUN dotnet publish TesteLocalize.WebAPI/TesteLocalize.WebAPI.csproj -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app


COPY --from=build /app/publish .


EXPOSE 80
EXPOSE 443


ENTRYPOINT ["dotnet", "TesteLocalize.WebAPI.dll"]