FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["FitnessTracker.Presentation.Workout.MessageHub/FitnessTracker.Presentation.Workout.MessageHub.csproj", "FitnessTracker.Presentation.Workout.MessageHub/"]

# Copy the nuget config file to the image folder.  This nuget file will be used to resolve the local nuget pakckages
COPY ["FitnessTracker.Presentation.Workout.MessageHub/nuget.config","FitnessTracker.Presentation.Workout.MessageHub/"] #Copy the Nuget file to the container
# Copy the local nuget packages to the container this is needed to resolve the ones on my server.  The nuget.config has a reference to this folder below
copy ["./ng/*.*","FitnessTracker.Presentation.Workout.MessageHub/"] #Copy all the local nuget packges to the container so they can resolve. 

RUN dotnet restore "FitnessTracker.Presentation.Workout.MessageHub/FitnessTracker.Presentation.Workout.MessageHub.csproj"

COPY . .
WORKDIR "/src/FitnessTracker.Presentation.Workout.MessageHub"
RUN dotnet build "FitnessTracker.Presentation.Workout.MessageHub.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FitnessTracker.Presentation.Workout.MessageHub.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FitnessTracker.Presentation.Workout.MessageHub.dll"]