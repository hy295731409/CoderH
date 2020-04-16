# The `FROM` instruction specifies the base image. You are
# extending the `microsoft/aspnet` image.

FROM mcr.microsoft.com/dotnet/core-nightly/aspnet
#FROM mcr.microsoft.com/dotnet/framework/runtime:4.8

ENV LC_ALL zh_CN.UTF-8
WORKDIR /app


# The final instruction copies the site you published earlier into the container.
COPY . .

# EXPOSE 80



#There is no ENTRYPOINT command in this Dockerfile. You don't need one. 
#When running Windows Server with IIS, the IIS process is the entrypoint, 
#which is configured to start in the aspnet base image.
ENTRYPOINT  ["dotnet", "CoreWebApi4Docker.dll"]
