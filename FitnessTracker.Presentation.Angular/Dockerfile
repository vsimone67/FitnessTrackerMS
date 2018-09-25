FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["FitnessTracker.Presentation.Angular/FitnessTracker.Presentation.Angular.csproj", "FitnessTracker.Presentation.Angular/"]

# Copy hte nuget config file to the image folder.  This nuget file will be used to resolve the local nuget pakckages
COPY ["FitnessTracker.Workout.Service/nuget.config","FitnessTracker.Presentation.Angular/"] #Copy the Nuget file to the container
# Copy the local nuget packages to the container this is needed to resolve the ones on my server.  The nuget.config has a reference to this folder below
copy ["./ng/*.*","FitnessTracker.Presentation.Angular/"] #Copy all the local nuget packges to the container so they can resolve. 

#RUN powershell.exe -File .\BuildAngular.ps1

RUN dotnet restore "FitnessTracker.Presentation.Angular/FitnessTracker.Presentation.Angular.csproj"
COPY . .
WORKDIR "/src/FitnessTracker.Presentation.Angular"
RUN dotnet build "FitnessTracker.Presentation.Angular.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FitnessTracker.Presentation.Angular.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app

COPY --from=publish /app .

#copy ["./dist/*.*","ClientApp/dist/"]
ENTRYPOINT ["dotnet", "FitnessTracker.Presentation.Angular.dll"]