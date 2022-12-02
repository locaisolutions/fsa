#!/bin/bash

set -e

dotnet tool restore -v q
dotnet build --configuration Release --nologo -v q