FROM mcr.microsoft.com/mssql/server

COPY . /app

WORKDIR /app

USER root

RUN chmod +x /app/initialization.sh

EXPOSE 1433

USER mssql

CMD /bin/bash /app/entrypoint.sh