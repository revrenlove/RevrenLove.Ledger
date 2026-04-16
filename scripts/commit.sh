#!/bin/bash

if [ -z "$1" ]; then
    echo "Error: Commit message is required"
    echo "Usage: ./commit.sh <CommitMessage>"
    exit 1
fi

COMMIT_MESSAGE=$1

echo "Staging all changes..."

git add -A

if [ $? -ne 0 ]; then
    echo "Failed to stage changes"
    exit 1
fi

echo "Committing changes with message: '$COMMIT_MESSAGE'..."

git commit -m "$COMMIT_MESSAGE"

if [ $? -ne 0 ]; then
    echo "Failed to commit changes"
    exit 1
fi

echo "Pushing changes"

git push

if [ $? -ne 0 ]; then
    echo "Failed to push changes"
    exit 1
fi