#!/bin/bash

set -e

dotnet tool restore -v q
dotnet test --configuration Release --nologo -v q