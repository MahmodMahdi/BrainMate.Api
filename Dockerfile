# 1. Base Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# 2. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# ملاحظة: الفولدر اسمه BrainMate والملف جواه اسمه BrainMate.Api.csproj
COPY ["BrainMate/BrainMate.Api.csproj", "BrainMate/"]
COPY ["BrainMate.Core/BrainMate.Core.csproj", "BrainMate.Core/"]
COPY ["BrainMate.Data/BrainMate.Data.csproj", "BrainMate.Data/"]
COPY ["BrainMate.Infrastructure/BrainMate.Infrastructure.csproj", "BrainMate.Infrastructure/"]
COPY ["BrainMate.Service/BrainMate.Service.csproj", "BrainMate.Service/"]

# عمل Restore بناءً على الملف الصحيح
RUN dotnet restore "BrainMate/BrainMate.Api.csproj"

# نسخ كل الملفات وعمل Build
COPY . .
WORKDIR "/src/BrainMate"
RUN dotnet build "BrainMate.Api.csproj" -c Release -o /app/build

# 3. Publish Stage
FROM build AS publish
RUN dotnet publish "BrainMate.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 4. Final Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# تأكد أن اسم الـ dll الناتج هو BrainMate.Api.dll
ENTRYPOINT ["dotnet", "BrainMate.Api.dll"]