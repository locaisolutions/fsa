#!/bin/bash

set -e

dotnet tool restore -v q
dotnet fantomas ./src ./tests -r 