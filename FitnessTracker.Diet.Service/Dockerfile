FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["FitnessTracker.Diet.Service/FitnessTracker.Diet.Service.csproj", "FitnessTracker.Diet.Service/"]
RUN dotnet restore "FitnessTracker.Diet.Service/FitnessTracker.Diet.Service.csproj"
COPY . .
WORKDIR "/src/FitnessTracker.Diet.Service"
RUN dotnet build "FitnessTracker.Diet.Service.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FitnessTracker.Diet.Service.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FitnessTracker.Diet.Service.dll"]