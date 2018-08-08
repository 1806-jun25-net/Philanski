FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Philanski.Frontend.MVC/*.csproj ./Philanski.Frontend.MVC/
#COPY Philanski.Frontend.Library/*.csproj ./Philanski.Frontend.Library/
COPY Philanski.Frontend.Testing/*.csproj ./Philanski.Frontend.Testing/
#COPY XUnitTestProject1/*.csproj ./XUnitTestProject1/
RUN dotnet restore

# copy everything else and build app
COPY . ./
WORKDIR /app/Philanski.Frontend.MVC
RUN dotnet publish -c Release -o out


FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/Philanski.Frontend.MVC/out ./
ENTRYPOINT ["dotnet", "Philanski.Frontend.MVC.dll"]
