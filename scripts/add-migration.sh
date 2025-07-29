#!/bin/bash

# Exit if no argument is provided
if [ -z "$1" ]; then
  echo "Usage: $0 <MigrationName>"
  exit 1
fi

MIGRATION_NAME="$1"

WORKSPACE_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
PERSISTENCE_PATH="$WORKSPACE_ROOT/RevrenLove.Ledger.Persistence"
STARTUP_PATH="$WORKSPACE_ROOT/RevrenLove.Ledger.Api"

# Add the migration
dotnet ef migrations add "$MIGRATION_NAME" -p "$PERSISTENCE_PATH" -s "$STARTUP_PATH"

# Try to update the database
if dotnet ef database update -p "$PERSISTENCE_PATH" -s "$STARTUP_PATH"; then
  echo "Database updated successfully."
else
  echo "Database update failed. Removing the migration..."
  dotnet ef migrations remove -p "$PERSISTENCE_PATH" -s "$STARTUP_PATH" --force
  exit 1
fi
