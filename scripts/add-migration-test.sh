#!/bin/bash

# Exit if no argument is provided
if [ -z "$1" ]; then
  echo "Usage: $0 <MigrationName>"
  exit 1
fi

MIGRATION_NAME="$1"

WORKSPACE_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
PERSISTENCE_PATH="$WORKSPACE_ROOT/RevrenLove.Ledger.Persistence.SQLite"
STARTUP_PATH="$WORKSPACE_ROOT/RevrenLove.Ledger.Api"

# Build the project with the Test configuration
echo "Building $STARTUP_PATH with Test configuration..."
dotnet clean "$STARTUP_PATH" --configuration Test
dotnet build "$STARTUP_PATH" --configuration Test

# Add the migration
echo "Adding $MIGRATION_NAME migration..."
ASPNETCORE_ENVIRONMENT=Test dotnet ef migrations add "$MIGRATION_NAME" -p "$PERSISTENCE_PATH" -s "$STARTUP_PATH" --no-build --configuration Test

# Try to update the database
echo "Updating database..."
if ASPNETCORE_ENVIRONMENT=Test dotnet ef database update -p "$PERSISTENCE_PATH" -s "$STARTUP_PATH" --configuration Test; then
  echo "Database updated successfully."
else
  echo "Database update failed. Removing the migration..."
  dotnet ef migrations remove -p "$PERSISTENCE_PATH" -s "$STARTUP_PATH" --no-build --configuration Test --force
  exit 1
fi
