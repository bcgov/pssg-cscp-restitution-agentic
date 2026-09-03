# ##################################################################################################
#                                                                                                 #
#   RESTITUTION-API: ASP.NET Core Web API (Backend Only)                                          #
#                                                                                                 #
#   LOCAL BUILD & RUN INSTRUCTIONS                                                                #
#   --------------------------------------------------------------------------------------------  #
#   1. Build the Docker image:                                                                    #
#        docker build \                                                                           #
#          -t restitution-api:local \                                                             #
#          -f restitution-app/Dockerfile \                                                        #
#          --build-arg BUILD_IMAGE_NAME=restitution-api \                                         #
#          --build-arg BUILD_VERSION=local-dev \                                                  #
#          .                                                                                      #
#                                                                                                 #
#   2. Run the container:                                                                         #
#        docker run --rm -it -p 8080:8080 restitution-api:local                                   #
#                                                                                                 #
#   3. Inspect the running container (in another terminal):                                       #
#        docker exec -it <container_id_or_name> bash                                              #
#        whoami            # should print 'appuser'                                               #
#        touch /app/testfile  # should succeed                                                    #
#        printenv BUILD_IMAGE_NAME                                                                #
#        printenv BUILD_VERSION                                                                   #
#                                                                                                 #
#   ARCHITECTURE & BUILD OVERVIEW                                                                 #
#   --------------------------------------------------------------------------------------------  #
#   - Multi-stage Docker build for .NET backend (restitution-app)                                 #
#   - No SPA integration: Angular frontend is built and served separately (see restitution-ui)    #
#   - This image contains only the .NET API (no static SPA files in wwwroot)                      #
#   - Runtime container runs as non-root user (appuser) for security                              #
#   - All files in /app are owned by appuser:appgroup                                             #
#   - Build metadata (BUILD_IMAGE_NAME, BUILD_VERSION) available as env vars                      #
#                                                                                                 #
#   TECHNOLOGIES                                                                                  #
#   --------------------------------------------------------------------------------------------  #
#   - Backend: ASP.NET Core (.NET 10)                                                             #
#   - Frontend: (none in this image; see restitution-ui for Angular SPA)                          #
#   - Build: Docker builds backend separately                                                     #
#                                                                                                 #
# ##################################################################################################
# --- Build Arguments (Global) ---
ARG BUILD_IMAGE_NAME
ARG BUILD_VERSION

# --- Runtime Base Image ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine@sha256:c4b29bf368004ad9076c1ab9bc91fb373561e3905b4345637e14e8b8c57e3be8 AS base
ARG BUILD_IMAGE_NAME
ARG BUILD_VERSION
WORKDIR /app

# --- Build Stage (SDK) & Publish Stage ---
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine@sha256:620e765fe18186c08399f7aa978f79f04b6bbf0ee1b3b8a91e2d5c9619e59da1 AS build
ARG BUILD_IMAGE_NAME
ARG BUILD_VERSION

COPY . .

# Restore .NET dependencies
RUN dotnet restore ./restitution-app/restitution-app.csproj
RUN dotnet publish ./restitution-app/restitution-app.csproj -c Release -o /app/publish --no-restore --verbosity minimal

# --- Final Runtime Image ---
FROM base AS final
ARG BUILD_IMAGE_NAME
ARG BUILD_VERSION

# Update all system packages for latest security patches FIRST.
# This ensures OS-level vulnerabilities are fixed.
RUN apk update && \
    apk upgrade && \
    rm -rf /var/cache/apk/*

WORKDIR /app

# Debug/Fail Fast: See if args are really set!
RUN echo "BUILD_IMAGE_NAME='$BUILD_IMAGE_NAME'" && if [ -z "$BUILD_IMAGE_NAME" ]; then echo "BUILD_IMAGE_NAME is empty!"; exit 1; fi
RUN echo "BUILD_VERSION='$BUILD_VERSION'" && if [ -z "$BUILD_VERSION" ]; then echo "BUILD_VERSION is empty!"; exit 1; fi

# 1. Create non-root user and group for security
RUN addgroup -S appgroup && adduser -S -G appgroup appuser

# 2. Copy published app from build stage as root
COPY --from=build /app/publish ./

# 3. Set ownership of all app files to appuser:appgroup (key for security!)
RUN chown -R appuser:appgroup /app

# 4. Accept build-time arguments and set environment variables
ENV BUILD_IMAGE_NAME=${BUILD_IMAGE_NAME}
ENV BUILD_VERSION=${BUILD_VERSION}

# 5. Switch to non-root user for runtime
USER appuser

# --- Expose and Run ---
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "restitution-app.dll"]