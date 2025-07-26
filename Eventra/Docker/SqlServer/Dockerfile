FROM mcr.microsoft.com/mssql/server:2019-latest

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=YourStrong!Pass123
ENV DEBIAN_FRONTEND=noninteractive

USER root

# Install tools
RUN apt-get update && \
    apt-get install -y --fix-missing curl apt-transport-https gnupg software-properties-common && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/ubuntu/22.04/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y --fix-missing mssql-tools unixodbc-dev && \
    ln -sfn /opt/mssql-tools/bin/* /usr/local/bin && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

EXPOSE 1433

CMD ["/opt/mssql/bin/sqlservr"]
