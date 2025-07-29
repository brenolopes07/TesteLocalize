# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de projeto e restaura as dependências
COPY TesteLocalize.WebAPI/TesteLocalize.WebAPI.csproj TesteLocalize.WebAPI/
COPY TesteLocalize.Application/TesteLocalize.Application.csproj TesteLocalize.Application/
COPY TesteLocalize.Infra/TesteLocalize.Infra.csproj TesteLocalize.Infra/
COPY TesteLocalize.Domain/TesteLocalize.Domain.csproj TesteLocalize.Domain/
RUN dotnet restore TesteLocalize.WebAPI/TesteLocalize.WebAPI.csproj

# Copia todo o restante do código
COPY . .

# Publica a aplicação em modo Release
RUN dotnet publish TesteLocalize.WebAPI/TesteLocalize.WebAPI.csproj -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia os arquivos publicados da etapa anterior
COPY --from=build /app/publish .

# Expõe a porta padrão do Kestrel
EXPOSE 80
EXPOSE 443

# Define o entrypoint
ENTRYPOINT ["dotnet", "TesteLocalize.WebAPI.dll"]