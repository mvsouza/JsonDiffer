#!/usr/bin/env bash

GIT_DIR=$(git rev-parse --git-dir)

echo "Installing hooks..."
# this command creates symlink to our pre-commit script
ln -s ../../scripts/pre-commit.sh $GIT_DIR/hooks/pre-commit
ln -s ../../scripts/post-commit.sh $GIT_DIR/hooks/post-commit
echo "Done!"