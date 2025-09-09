# Guía de Seguridad - RCE Providers Microservice

## Configuración de Seguridad Implementada

### 1. Validaciones de Entrada
- **FluentValidation** implementado para todos los DTOs
- Validaciones de longitud, formato y rangos de datos
- Mensajes de error localizados en español

### 2. Manejo de Errores
- **GlobalExceptionMiddleware** captura todas las excepciones
- Logging estructurado con Serilog
- Respuestas de error consistentes sin exposición de información sensible

### 3. Rate Limiting
- Límite de 1000 requests/minuto (apropiado para microservicio interno)
- Identificación por header `X-Service-Name` o IP del cliente
- Limpieza automática de requests antiguos

### 4. Base de Datos
- **Soft Delete** implementado en todas las entidades
- Índices optimizados para performance
- Query filters automáticos para excluir registros eliminados
- Timestamps automáticos (CreatedAt, UpdatedAt, DeletedAt)

### 5. Logging y Monitoreo
- **Serilog** configurado con rotación diaria
- Health checks en `/health`
- Logs estructurados en consola y archivo

## Configuración de Producción

### Variables de Entorno Requeridas
```bash
# Base de datos
DB_HOST=your_db_host
DB_PORT=5432
DB_NAME=rce_providers_db
DB_USER=your_db_user
DB_PASSWORD=your_secure_password

# Rate limiting
RATE_LIMIT_MAX_REQUESTS_PER_MINUTE=1000

# Service identity
SERVICE_NAME=RCE_Providers_Service
```

### Recomendaciones Adicionales

1. **Usar User Secrets en desarrollo:**
```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=..."
```

2. **Configurar el middleware para identificar servicios internos:**
   - Agregar header `X-Service-Name` en las llamadas desde el middleware
   - Configurar whitelist de IPs internas si es necesario

3. **Monitoreo:**
   - Configurar alertas en el endpoint `/health`
   - Monitorear logs de rate limiting
   - Revisar logs de excepciones regularmente

4. **Base de Datos:**
   - Configurar backups automáticos
   - Usar conexiones SSL en producción
   - Implementar rotación de credenciales

## Endpoints de Monitoreo

- `GET /health` - Health check del servicio y base de datos
- Logs disponibles en `logs/rce-providers-{date}.log`

## Consideraciones de Seguridad

Este microservicio está diseñado para ser llamado únicamente desde servicios internos autorizados. No debe exponerse directamente a internet sin un gateway o proxy que maneje:

- Autenticación/Autorización
- CORS apropiado
- Rate limiting adicional
- Filtrado de IPs

## Soft Delete

Todas las operaciones de eliminación son soft deletes:
- Los registros se marcan como `IsDeleted = true`
- Se establece `DeletedAt = DateTime.UtcNow`
- Los query filters automáticos excluyen registros eliminados
- Para recuperar registros eliminados, usar `IgnoreQueryFilters()` en las consultas