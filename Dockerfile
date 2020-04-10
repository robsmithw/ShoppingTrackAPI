FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /ShoppingTrackAPI

COPY /ShoppingTrackAPI/*.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /ShoppingTrackAPI
EXPOSE 80
COPY --from=build-env ShoppingTrackAPI/out .
RUN chmod +x ShoppingTrackAPI.dll

ENTRYPOINT ["dotnet", "ShoppingTrackAPI.dll"]