# Docker Setup

## Quick Start

To start BeEF, PostgreSQL and a docker container with SQLMAP at the same time, use

```
docker-compose up -d
```

from this folder.

BeEF and PostgreSQL will be started as daemons, while SQLMAP will be started in interactive mode.
To connect to SQLMAP, use the command

```
docker attach docker_sqlmap_1
```

Note that exiting from this terminal will stop the docker container for SQLMAP, though the other two
services will continue running.

To access the web service from SQLMap, use the following URL:
```
https://host.docker.internal:5001/History?OrderBy=Date
```
