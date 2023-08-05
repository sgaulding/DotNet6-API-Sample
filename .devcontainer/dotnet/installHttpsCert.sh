#!/bin/bash

echo "Generate a certificate and configure the local machine"

dotnet dev-certs https --clean

if [ ! -f /usr/local/share/ca-certificates/aspnet/https.crt ]; then
    echo "Certificate not found, generating..."
    mkdir -p /usr/local/share/ca-certificates/aspnet
    dotnet dev-certs https -ep /usr/local/share/ca-certificates/aspnet/https.crt
    dotnet dev-certs https --import /usr/local/share/ca-certificates/aspnet/https.crt
    chmod 644 /usr/local/share/ca-certificates/aspnet/https.crt
    update-ca-certificates
else
    echo "Certificate found, skipping generation..."
fi

dotnet dev-certs https --trust