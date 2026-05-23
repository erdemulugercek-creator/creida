# Creida — .NET 10 Razor Pages site
# Render bu Dockerfile'ı görüp otomatik build edip yayına alır.

# ---- Build aşaması: SDK kullan, projeyi derle ----
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# Önce csproj kopyala, paketleri restore et (cache verimi için)
COPY *.csproj ./
RUN dotnet restore

# Sonra geri kalan kaynak kodu kopyala ve publish et
COPY . ./
RUN dotnet publish -c Release -o /app/publish --no-restore

# ---- Runtime aşaması: küçük runtime image ----
FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

# Render varsayılan olarak $PORT environment değişkenine bir port verir.
# ASP.NET'in bunu dinlemesini sağla.
ENV ASPNETCORE_URLS=http://+:${PORT:-8080}
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

ENTRYPOINT ["dotnet", "Creida.dll"]
