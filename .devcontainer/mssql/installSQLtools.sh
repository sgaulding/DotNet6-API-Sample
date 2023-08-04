#!/bin/bash

echo "Find Linux Information for SQL Server install"
DISTRO=$(lsb_release -is | tr '[:upper:]' '[:lower:]')
VERSION=$(lsb_release -rs | cut -d. -f1)

echo "Add the Microsoft to Apt Linux repository"
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
curl "https://packages.microsoft.com/config/$DISTRO/$VERSION/prod.list" | tee /etc/apt/sources.list.d/microsoft-prod.list

echo "Update your sources"
sudo apt-get update

echo "Install the SQL Server command-line tools"
sudo ACCEPT_EULA=Y apt-get install mssql-tools18 unixodbc-dev msodbcsql18 libunwind8 -y

echo "Installing SQL Package"
curl -sSL -o sqlpackage.zip "https://aka.ms/sqlpackage-linux"
mkdir /opt/sqlpackage
unzip sqlpackage.zip -d /opt/sqlpackage 
rm sqlpackage.zip
chmod a+x /opt/sqlpackage/sqlpackage 
