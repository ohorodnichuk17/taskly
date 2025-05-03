FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Taskly_Api/Taskly_Api.csproj", "Taskly_Api/"]
COPY ["Taskly_Application/Taskly_Application.csproj", "Taskly_Application/"]
COPY ["Taskly_Domain/Taskly_Domain.csproj", "Taskly_Domain/"]
COPY ["Taskly_Infrastructure/Taskly_Infrastructure.csproj", "Taskly_Infrastructure/"]

RUN dotnet restore "Taskly_Api/Taskly_Api.csproj"

COPY . .

WORKDIR /src/Taskly_Api
RUN dotnet publish "Taskly_Api.csproj" -c Release -o /app/publish

COPY ./Taskly_Api/images /app/images

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "Taskly_Api.dll"]


# HOW TO RUN

#docker build -t taskly-backend .

#docker run -d -p 5000:80 --name taskly-backend taskly-backend
