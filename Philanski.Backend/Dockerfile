#base image 
#FROM microsoft/dotnet:2.1-sdk

# Set first working directory. This creates dir if it doesnt exist
#WORKDIR /app

# copy. 1st parameter: where from my filesystem. 2nd parameter: where inside docker image
# this copy command copies current directory contents to current directory in docker (/app)
#COPY . ./


#WORKDIR /app/Project1.WebApp 
# reference to where we need to call dotnet run 
#CMD [ "dotnet", "run"] 
#compile file outside dockerfile then pass to container
#CMD ["bin/Debug/netcoreapp2.1/Project1.WebApp.dll"]

#each line in the dockerfile represents a new image, without a name though

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .

COPY Philanski.Backend.DataContext/*.csproj ./Philanski.Backend.DataContext/
COPY Philanski.Backend.Library/*.csproj ./Philanski.Backend.Library/
COPY Philanski.Backend.Testing/*.csproj ./Philanski.Backend.Testing/
COPY Philanski.Backend.WebAPI/*.csproj ./Philanski.Backend.WebAPI/
RUN dotnet restore

# copy everything else and build app
COPY . ./
WORKDIR /app/Philanski.Backend.WebAPI
RUN dotnet publish -c Release -o out


FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/Philanski.Backend.WebAPI/out ./
ENTRYPOINT ["dotnet", "Philanski.Backend.WebAPI.dll"]
