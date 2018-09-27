FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["FitnessTracker.Presentation.SignalRHub/FitnessTracker.Presentation.SignalRHub.csproj", "FitnessTracker.Presentation.SignalRHub/"]

# Copy hte nuget config file to the image folder.  This nuget file will be used to resolve the local nuget pakckages
COPY ["FitnessTracker.Presentation.SignalRHub/nuget.config","FitnessTracker.Presentation.SignalRHub/"] #Copy the Nuget file to the container
# Copy the local nuget packages to the container this is needed to resolve the ones on my server.  The nuget.config has a reference to this folder below
copy ["./ng/*.*","FitnessTracker.Presentation.SignalRHub/"] #Copy all the local nuget packges to the container so they can resolve. 

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