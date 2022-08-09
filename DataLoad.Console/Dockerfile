#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DataLoad.Console/DataLoad.Console.csproj", "DataLoad.Console/"]
COPY ["DataLoad.Application/DataLoad.Application.csproj", "DataLoad.Application/"]
COPY ["HawkerSearch.Data/HawkerSearch.Domain.csproj", "HawkerSearch.Data/"]
RUN dotnet restore "DataLoad.Console/DataLoad.Console.csproj"
COPY . .
WORKDIR "/src/DataLoad.Console"
RUN dotnet build "DataLoad.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataLoad.Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataLoad.Console.dll"]