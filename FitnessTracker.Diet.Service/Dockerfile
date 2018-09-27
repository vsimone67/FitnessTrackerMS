FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["FitnessTracker.Diet.Service/FitnessTracker.Diet.Service.csproj", "FitnessTracker.Diet.Service/"]

# Copy hte nuget config file to the image folder.  This nuget file will be used to resolve the local nuget pakckages
COPY ["FitnessTracker.Diet.Service/nuget.config","FitnessTracker.Diet.Service/"] #Copy the Nuget file to the container
# Copy the local nuget packages to the container this is needed to resolve the ones on my server.  The nuget.config has a reference to this folder below
copy ["./ng/*.*","FitnessTracker.Diet.Service/"] #Copy all the local nuget packges to the container so they can resolve. 

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