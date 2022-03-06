FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build-env
WORKDIR /ShoppingTrackAPI

COPY /ShoppingTrackAPI/*.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out
# RUN if [ "$ASP_ENV" = "dev" ] ; \
#     then dotnet publish -c Debug -o out; \
#     else dotnet publish -c Release -o out; \
#     fi 

FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal
WORKDIR /ShoppingTrackAPI
EXPOSE 80
COPY --from=build-env ShoppingTrackAPI/out .
RUN chmod +x ShoppingTrackAPI.dll

ENTRYPOINT ["dotnet", "ShoppingTrackAPI.dll"]