FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["FitnessTracker.Presentation.WebStatus/FitnessTracker.Presentation.WebStatus.csproj", "FitnessTracker.Presentation.WebStatus/"]

# Copy hte nuget config file to the image folder.  This nuget file will be used to resolve the local nuget pakckages
COPY ["FitnessTracker.Presentation.WebStatus/nuget.config","FitnessTracker.Presentation.WebStatus/"] #Copy the Nuget file to the container
# Copy the local nuget packages to the container this is needed to resolve the ones on my server.  The nuget.config has a reference to this folder below
copy ["./ng/*.*","FitnessTracker.Presentation.WebStatus/"] #Copy all the local nuget packges to the container so they can resolve. 

RUN dotnet restore "FitnessTracker.Presentation.WebStatus/FitnessTracker.Presentation.WebStatus.csproj"
COPY . .
WORKDIR "/src/FitnessTracker.Presentation.WebStatus"
RUN dotnet build "FitnessTracker.Presentation.WebStatus.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FitnessTracker.Presentation.WebStatus.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FitnessTracker.Presentation.WebStatus.dll"]