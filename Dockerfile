FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /ShoppingTrackAPI

COPY /ShoppingTrackAPI/*.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /ShoppingTrackAPI
EXPOSE 80
COPY --from=build-env ShoppingTrackAPI/out .
RUN chmod +x ShoppingTrackAPI.dll

ENTRYPOINT ["dotnet", "ShoppingTrackAPI.dll"]