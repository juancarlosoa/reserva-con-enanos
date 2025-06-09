DO
$$
BEGIN
   IF NOT EXISTS (
      SELECT FROM pg_catalog.pg_roles WHERE rolname = 'rce_user'
   ) THEN
      CREATE ROLE rce_user WITH LOGIN;
      ALTER ROLE rce_user SET password_encryption = 'scram-sha-256';
      ALTER ROLE rce_user WITH PASSWORD 'RCEPassword';
   END IF;
END
$$;

ALTER ROLE rce_user WITH SUPERUSER;

CREATE DATABASE rce_auth_db OWNER rce_user;
CREATE DATABASE rce_reservations_db OWNER rce_user;
CREATE DATABASE rce_providers_db OWNER rce_user;