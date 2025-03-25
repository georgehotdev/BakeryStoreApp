FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Discount/Discount.Grpc/Discount.Grpc.csproj", "src/Discount/Discount.Grpc/"]
COPY ["src/Discount/Discount.Domain/Discount.Domain.csproj", "src/Discount/Discount.Domain/"]
COPY ["src/Discount/Discount.Infrastructure/Discount.Infrastructure.csproj", "src/Discount/Discount.Infrastructure/"]
RUN dotnet restore "src/Discount/Discount.Grpc/Discount.Grpc.csproj"
COPY . .
WORKDIR "/src/src/Discount/Discount.Grpc"
RUN dotnet build "Discount.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Discount.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.Grpc.dll"]
