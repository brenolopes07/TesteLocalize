
# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia tudo
COPY . .

# Restaura dependências
RUN dotnet restore ./TesteLocalize.WebAPI/TesteLocalize.WebAPI.csproj

# Publica a aplicação em modo release
RUN dotnet publish ./TesteLocalize.WebAPI/TesteLocalize.WebAPI.csproj -c Release -o /publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia a build gerada na etapa anterior
COPY --from=build /publish .

# Expõe a porta da API
EXPOSE 80

# Comando padrão ao iniciar o container
ENTRYPOINT ["dotnet", "TesteLocalize.WebAPI.dll"]