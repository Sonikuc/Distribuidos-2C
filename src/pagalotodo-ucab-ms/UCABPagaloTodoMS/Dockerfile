#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001
EXPOSE 44339

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UCABPagaloTodoMS/UCABPagaloTodoMS.csproj", "UCABPagaloTodoMS/"]
COPY ["UCABPagaloTodoMS.Application/UCABPagaloTodoMS.Application.csproj", "UCABPagaloTodoMS.Application/"]
COPY ["UCABPagaloTodoMS.Core/UCABPagaloTodoMS.Core.csproj", "UCABPagaloTodoMS.Core/"]
COPY ["UCABPagaloTodoMS.Infrastructure/UCABPagaloTodoMS.Infrastructure.csproj", "UCABPagaloTodoMS.Infrastructure/"]
RUN dotnet restore "UCABPagaloTodoMS/UCABPagaloTodoMS.csproj"
COPY . .
WORKDIR "/src/UCABPagaloTodoMS"
RUN dotnet build "UCABPagaloTodoMS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UCABPagaloTodoMS.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UCABPagaloTodoMS.dll"]