FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["FitnessTracker.Presentation.SignalRHub/FitnessTracker.Presentation.SignalRHub.csproj", "FitnessTracker.Presentation.SignalRHub/"]
RUN dotnet restore "FitnessTracker.Presentation.SignalRHub/FitnessTracker.Presentation.SignalRHub.csproj"
COPY . .
WORKDIR "/src/FitnessTracker.Presentation.SignalRHub"
RUN dotnet build "FitnessTracker.Presentation.SignalRHub.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FitnessTracker.Presentation.SignalRHub.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FitnessTracker.Presentation.SignalRHub.dll"]