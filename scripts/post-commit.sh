#!/usr/bin/env bash

echo "Running post-commit hook"
powershell ./JsonDiffer.ps1 sonarqubeBuild