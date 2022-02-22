FROM mcr.microsoft.com/mssql/server

COPY . /app

WORKDIR /app

USER root

RUN chmod +x /app/initialization.sh

ENV SA_PASSWORD=Your_password123
ENV ACCEPT_EULA=Y

EXPOSE 1433

USER mssql

CMD /bin/bash /app/entrypoint.sh