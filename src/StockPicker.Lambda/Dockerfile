FROM public.ecr.aws/lambda/dotnet:8 AS base

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /src
COPY ["StockPicker.Lambda.csproj", "StockPicker.Lambda/"]
RUN dotnet restore "StockPicker.Lambda/StockPicker.Lambda.csproj"

WORKDIR "/src/StockPicker.Lambda"
COPY . .
RUN dotnet build "StockPicker.Lambda.csproj" --configuration Release --output /app/build

FROM build AS publish
RUN dotnet publish "StockPicker.Lambda.csproj" \
            --configuration Release \ 
            --runtime linux-x64 \
            --self-contained false \ 
            --output /app/publish \
            -p:PublishReadyToRun=true  

FROM base AS final
WORKDIR /var/task
COPY --from=publish /app/publish .