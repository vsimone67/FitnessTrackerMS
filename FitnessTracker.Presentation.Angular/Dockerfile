FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["FitnessTracker.Presentation.Angular/FitnessTracker.Presentation.Angular.csproj", "FitnessTracker.Presentation.Angular/"]
RUN dotnet restore "FitnessTracker.Presentation.Angular/FitnessTracker.Presentation.Angular.csproj"
COPY . .
WORKDIR "/src/FitnessTracker.Presentation.Angular"
RUN dotnet build "FitnessTracker.Presentation.Angular.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FitnessTracker.Presentation.Angular.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FitnessTracker.Presentation.Angular.dll"]