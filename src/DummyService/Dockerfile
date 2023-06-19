FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine as base

RUN apk add --no-cache icu-libs

## -- Create non-root-user environment
ARG UID=1000
ARG GID=2000
ARG USERNAME=appuser
ARG GROUP=appgroup

ENV USERNAME=${USERNAME}
ENV UID=${UID}
ENV GID=${GID}
ENV GROUP=${GROUP}
ENV HOME=/home/${USERNAME}
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# New Relic app settings
ENV CORECLR_NEWRELIC_HOME=${HOME}/app/newrelic \
    CORECLR_PROFILER_PATH=${HOME}/app/newrelic/libNewRelicProfiler.so    


RUN addgroup -g ${GID} ${GROUP}
RUN adduser  -u ${UID} -h ${HOME} -D -g "${GROUP}" ${USERNAME} 

## -----------------------------------------

FROM mcr.microsoft.com/dotnet/sdk:7.0 as builder
WORKDIR /src
# Prepare pacakges from MI-General and Nuget.config for restoring packages

COPY . /src 

WORKDIR /src/DummyService
ARG assemblyVersion=1.0.0

RUN dotnet publish -c Release -o /out /p:Version=${assemblyVersion} ./DummyService.csproj

# app image
FROM base AS final
USER ${USERNAME}

WORKDIR ${HOME}/app

COPY --chown=${USERNAME}:${GROUP} --from=builder /out/ .

ENTRYPOINT ["dotnet", "DummyService.dll"]
