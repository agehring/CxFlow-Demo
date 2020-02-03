FROM kalilinux/kali-linux-docker:latest

RUN apt-get update && apt-get install -y \
	beef-xss \
	locales

RUN sed -i -e 's/# en_US.UTF-8 UTF-8/en_US.UTF-8 UTF-8/' /etc/locale.gen && \
    dpkg-reconfigure --frontend=noninteractive locales && \
    update-locale LANG=en_US.UTF-8

ENV LANG en_US.UTF-8 

COPY ./config.yaml /etc/beef-xss/config.yaml

EXPOSE 3000/tcp

WORKDIR /usr/share/beef-xss
ENTRYPOINT ["/usr/share/beef-xss/beef"]