FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["OpenshiftTest/OpenshiftTest.csproj", "OpenshiftTest/"]
RUN dotnet restore "OpenshiftTest/OpenshiftTest.csproj"
COPY . .
WORKDIR "/src/OpenshiftTest"
RUN dotnet build "OpenshiftTest.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "OpenshiftTest.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OpenshiftTest.dll"]