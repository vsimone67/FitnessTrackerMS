FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["FitnessTracker.Service.Workout/FitnessTracker.Service.Workout.csproj", "FitnessTracker.Service.Workout/"]

# Copy hte nuget config file to the image folder.  This nuget file will be used to resolve the local nuget pakckages
COPY ["FitnessTracker.Service.Workout/nuget.config","FitnessTracker.Service.Workout/"] #Copy the Nuget file to the container
# Copy the local nuget packages to the container this is needed to resolve the ones on my server.  The nuget.config has a reference to this folder below
copy ["./ng/*.*","FitnessTracker.Service.Workout/"] #Copy all the local nuget packges to the container so they can resolve. 
#COPY ["FitnessTracker.Service.Workout/NLog.config","FitnessTracker.Service.Workout/"] #Copy the Nuget file to the container


RUN dotnet restore "FitnessTracker.Service.Workout/FitnessTracker.Service.Workout.csproj"
COPY . .
WORKDIR "/src/FitnessTracker.Service.Workout"
RUN dotnet build "FitnessTracker.Service.Workout.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FitnessTracker.Service.Workout.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FitnessTracker.Service.Workout.dll"]
