#!/bin/sh

dotnet restore
dotnet test --no-restore /p:CollectCoverage=true
