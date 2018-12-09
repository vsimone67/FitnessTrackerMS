FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["FitnessTracker.Presentation.Diet.MessageHub/FitnessTracker.Presentation.Diet.MessageHub.csproj", "FitnessTracker.Presentation.Diet.MessageHub/"]
RUN dotnet restore "FitnessTracker.Presentation.Diet.MessageHub/FitnessTracker.Presentation.Diet.MessageHub.csproj"

# Copy the nuget config file to the image folder.  This nuget file will be used to resolve the local nuget pakckages
COPY ["FitnessTracker.Presentation.Diet.MessageHub/nuget.config","FitnessTracker.Presentation.Diet.MessageHub/"] #Copy the Nuget file to the container
# Copy the local nuget packages to the container this is needed to resolve the ones on my server.  The nuget.config has a reference to this folder below
copy ["./ng/*.*","FitnessTracker.Presentation.Diet.MessageHub/"] #Copy all the local nuget packges to the container so they can resolve. 

COPY . .
WORKDIR "/src/FitnessTracker.Presentation.Diet.MessageHub"
RUN dotnet build "FitnessTracker.Presentation.Diet.MessageHub.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FitnessTracker.Presentation.Diet.MessageHub.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FitnessTracker.Presentation.Diet.MessageHub.dll"]