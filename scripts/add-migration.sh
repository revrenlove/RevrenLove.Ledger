#!/bin/bash

if [ -z "$1" ]; then
    echo "Error: Migration name is required"
    echo "Usage: ./add-migration.sh <MigrationName>"
    exit 1
fi

MIGRATION_NAME=$1

echo "Adding migration: $MIGRATION_NAME"
echo "Startup project: RevrenLove.Ledger.Api"
echo "Target project: RevrenLove.Ledger.Persistence.SQLite"

dotnet ef migrations add "$MIGRATION_NAME" \
    --startup-project RevrenLove.Ledger.Api/RevrenLove.Ledger.Api.csproj \
    --project RevrenLove.Ledger.Persistence.SQLite/RevrenLove.Ledger.Persistence.SQLite.csproj

if [ $? -ne 0 ]; then
    echo "Failed to add migration"
    exit 1
fi

echo "Migration '$MIGRATION_NAME' added successfully"
echo ""
echo "Updating database..."

dotnet ef database update \
    --startup-project RevrenLove.Ledger.Api/RevrenLove.Ledger.Api.csproj \
    --project RevrenLove.Ledger.Persistence.SQLite/RevrenLove.Ledger.Persistence.SQLite.csproj

if [ $? -ne 0 ]; then
    echo ""
    echo "Database update failed. Reverting migration '$MIGRATION_NAME'..."

    dotnet ef migrations remove \
        --startup-project RevrenLove.Ledger.Api/RevrenLove.Ledger.Api.csproj \
        --project RevrenLove.Ledger.Persistence.SQLite/RevrenLove.Ledger.Persistence.SQLite.csproj \
        --force

    if [ $? -eq 0 ]; then
        echo "Migration '$MIGRATION_NAME' has been reverted successfully"
    else
        echo "Warning: Failed to revert migration. You may need to manually remove it."
    fi

    exit 1
fi

echo "Database updated successfully"
