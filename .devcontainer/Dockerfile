# [Choice] .NET version: 6.0, 5.0, 3.1
ARG VARIANT="6.0"
FROM mcr.microsoft.com/devcontainers/dotnet:0-${VARIANT}

# Install SQL Tools: SQLPackage and sqlcmd
COPY mssql/installSQLtools.sh installSQLtools.sh
RUN bash ./installSQLtools.sh \
     && apt-get clean -y \
     && rm -rf /var/lib/apt/lists/* /tmp/library-scripts 

# [Optional] Uncomment this section to install additional OS packages.
# RUN apt-get update && export DEBIAN_FRONTEND=noninteractive \
#     && apt-get -y install --no-install-recommends <your-package-list-here>

# Install HTTPS Certs for Development
COPY dotnet/installHttpsCert.sh installHttpsCert.sh
RUN su vscode -c "bash ./installHttpsCert.sh"

# [Optional] Uncomment this line to install global node packages.
# RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g <your-package-here>" 2>&1