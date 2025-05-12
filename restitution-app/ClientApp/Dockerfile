# Create build image layer
FROM node:8.9.4 AS build

ARG BUILD_ID
ARG BUILD_VERSION

COPY ./restitution-app/ClientApp ./restitution-app/ClientApp

WORKDIR /restitution-app/ClientApp

RUN npm install
RUN npm run build -- --base-href /restwebforms --prod

# Create runtime image layer, copy build image contents into this image
FROM caddy:latest AS final

# Accept and expose build metadata
ARG BUILD_ID
ARG BUILD_VERSION
ENV BUILD_ID=${BUILD_ID}
ENV BUILD_VERSION=${BUILD_VERSION}

COPY ./restitution-app/ClientApp/Caddyfile /etc/caddy/Caddyfile
COPY --from=build /restitution-app/ClientApp/dist/ /site

# Inject version/build files
RUN echo "${BUILD_ID}" > /site/build_id.txt && echo "${BUILD_VERSION}" > /site/version.txt

ENV BASE_URL=
ENV API_URL=
EXPOSE 2015