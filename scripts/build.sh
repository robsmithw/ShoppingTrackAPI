#!/bin/sh

dotnet restore app/ShoppingTrackAPI
dotnet restore app/ShoppingTrackAPITest
dotnet test app/ShoppingTrackAPITest --no-restore /p:CollectCoverage=true
