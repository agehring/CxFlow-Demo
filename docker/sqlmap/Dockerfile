FROM kalilinux/kali-linux-docker:latest

RUN apt-get update && apt-get install -y \
	sqlmap

WORKDIR "/root"
ENTRYPOINT ["/bin/bash"]