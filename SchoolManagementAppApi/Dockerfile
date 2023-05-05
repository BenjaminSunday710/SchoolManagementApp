#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SchoolManagementAppApi/SchoolManagementAppApi.csproj", "SchoolManagementAppApi/"]
COPY ["SchoolManagementApp.Application/SchoolManagementApp.Application.csproj", "SchoolManagementApp.Application/"]
COPY ["SchoolManagementApp.Domain/SchoolManagementApp.Domain.csproj", "SchoolManagementApp.Domain/"]
COPY ["Shared.Domain/Shared.Domain.csproj", "Shared.Domain/"]
COPY ["SchoolManagementApp.Infrastructure/SchoolManagementApp.Infrastructure.csproj", "SchoolManagementApp.Infrastructure/"]
COPY ["Shared.Infrastructure/Shared.Infrastructure.csproj", "Shared.Infrastructure/"]
COPY ["Utilities/Utilities.csproj", "Utilities/"]
COPY ["Shared.Application/Shared.Application.csproj", "Shared.Application/"]
COPY ["UserManagement.Domain/UserManagement.Domain.csproj", "UserManagement.Domain/"]
COPY ["UserManagement.Application/UserManagement.Application.csproj", "UserManagement.Application/"]
COPY ["UserManagement.Infrastructure/UserManagement.Infrastructure.csproj", "UserManagement.Infrastructure/"]
RUN dotnet restore "SchoolManagementAppApi/SchoolManagementAppApi.csproj"
COPY . .
WORKDIR "/src/SchoolManagementAppApi"
RUN dotnet build "SchoolManagementAppApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SchoolManagementAppApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SchoolManagementAppApi.dll"]