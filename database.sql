-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versión del servidor:         10.11.13-MariaDB-0ubuntu0.24.04.1 - Ubuntu 24.04
-- SO del servidor:              debian-linux-gnu
-- HeidiSQL Versión:             12.8.0.6908
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Volcando estructura de base de datos para proyecto_QA
CREATE DATABASE IF NOT EXISTS `proyecto_QA` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `proyecto_QA`;

-- Volcando estructura para tabla proyecto_QA.area
CREATE TABLE IF NOT EXISTS `area` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) NOT NULL,
  `id_usuario` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_usuario` (`id_usuario`),
  CONSTRAINT `area_ibfk_1` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla proyecto_QA.bitacoraEquipo
CREATE TABLE IF NOT EXISTS `bitacoraEquipo` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_empleado` int(11) DEFAULT NULL,
  `id_equipo` int(11) NOT NULL,
  `fecha_commit` datetime DEFAULT current_timestamp(),
  PRIMARY KEY (`id`),
  KEY `idx_bitacora_empleado` (`id_empleado`),
  KEY `idx_bitacora_equipo` (`id_equipo`),
  CONSTRAINT `bitacoraEquipo_ibfk_1` FOREIGN KEY (`id_empleado`) REFERENCES `empleado` (`id`),
  CONSTRAINT `bitacoraEquipo_ibfk_2` FOREIGN KEY (`id_equipo`) REFERENCES `equipo` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla proyecto_QA.causa
CREATE TABLE IF NOT EXISTS `causa` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) NOT NULL,
  `descripcion` varchar(300) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla proyecto_QA.electronico
CREATE TABLE IF NOT EXISTS `electronico` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `imei` varchar(20) DEFAULT NULL,
  `sistema_operativo` varchar(20) DEFAULT NULL,
  `conectividad` enum('bluetooth','wifi','gsm','nfc','bluetooth_wifi','bluetooth_gsm','bluetooth_nfc','wifi_gsm','wifi_nfc','gsm_nfc','bluetooth_wifi_gsm','bluetooth_wifi_nfc','bluetooth_gsm_nfc','wifi_gsm_nfc','bluetooth_wifi_gsm_nfc','ninguno') DEFAULT 'ninguno',
  `operador` enum('starlink','claro','tigo','comnet','verasat','telecom','ninguno') DEFAULT 'ninguno',
  `id_equipo` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_equipo` (`id_equipo`),
  CONSTRAINT `electronico_ibfk_1` FOREIGN KEY (`id_equipo`) REFERENCES `equipo` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla proyecto_QA.empleado
CREATE TABLE IF NOT EXISTS `empleado` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `primer_nombre` varchar(50) NOT NULL,
  `segundo_nombre` varchar(50) DEFAULT NULL,
  `primer_apellido` varchar(50) NOT NULL,
  `segundo_apellido` varchar(50) DEFAULT NULL,
  `id_usuario` int(11) DEFAULT NULL,
  `estado` enum('activo','inactivo','vacaciones') DEFAULT 'activo',
  `id_area` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_usuario` (`id_usuario`),
  KEY `idx_empleado_usuario` (`id_usuario`),
  KEY `idx_empleado_area` (`id_area`),
  CONSTRAINT `empleado_ibfk_1` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id`),
  CONSTRAINT `empleado_ibfk_2` FOREIGN KEY (`id_area`) REFERENCES `area` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla proyecto_QA.equipo
CREATE TABLE IF NOT EXISTS `equipo` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `identificador` varchar(20) DEFAULT NULL,
  `nombre` varchar(50) NOT NULL,
  `id_marca` int(11) NOT NULL,
  `color` varchar(50) NOT NULL,
  `valor` decimal(12,2) NOT NULL,
  `serie` varchar(50) NOT NULL,
  `extras` text DEFAULT NULL,
  `tipo_alimentacion` enum('110v','220v','diesel','regular','super','bateria','ninguna') DEFAULT 'ninguna',
  `id_empleado` int(11) DEFAULT NULL,
  `estado` enum('activo','inactivo','mantenimiento','suspendido') DEFAULT 'activo',
  `fecha_commit` datetime DEFAULT current_timestamp(),
  `tipo` enum('mobiliario','vehiculo','electronico','herramienta') NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idx_equipo_marca` (`id_marca`),
  KEY `idx_equipo_empleado` (`id_empleado`),
  KEY `idx_equipo_tipo` (`tipo`),
  CONSTRAINT `equipo_ibfk_1` FOREIGN KEY (`id_marca`) REFERENCES `marca` (`id`),
  CONSTRAINT `equipo_ibfk_2` FOREIGN KEY (`id_empleado`) REFERENCES `empleado` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla proyecto_QA.herramienta
CREATE TABLE IF NOT EXISTS `herramienta` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `material` varchar(50) NOT NULL,
  `id_equipo` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_equipo` (`id_equipo`),
  CONSTRAINT `herramienta_ibfk_1` FOREIGN KEY (`id_equipo`) REFERENCES `equipo` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla proyecto_QA.imagen
CREATE TABLE IF NOT EXISTS `imagen` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `url` text NOT NULL,
  `id_reporte` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_reporte` (`id_reporte`),
  CONSTRAINT `imagen_ibfk_1` FOREIGN KEY (`id_reporte`) REFERENCES `reporte` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla proyecto_QA.marca
CREATE TABLE IF NOT EXISTS `marca` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla proyecto_QA.mobiliario
CREATE TABLE IF NOT EXISTS `mobiliario` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `material` varchar(50) NOT NULL,
  `altura` float(8,2) NOT NULL,
  `ancho` float(8,2) NOT NULL,
  `profundidad` float(8,2) NOT NULL,
  `cantidad_piezas` int(11) DEFAULT 1,
  `id_equipo` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_equipo` (`id_equipo`),
  CONSTRAINT `mobiliario_ibfk_1` FOREIGN KEY (`id_equipo`) REFERENCES `equipo` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla proyecto_QA.reporte
CREATE TABLE IF NOT EXISTS `reporte` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `observacion` text DEFAULT NULL,
  `id_causa` int(11) NOT NULL,
  `id_equipo` int(11) NOT NULL,
  `id_empleado` int(11) NOT NULL,
  `fecha_commit` datetime DEFAULT current_timestamp(),
  PRIMARY KEY (`id`),
  KEY `id_causa` (`id_causa`),
  KEY `idx_reporte_equipo` (`id_equipo`),
  KEY `idx_reporte_empleado` (`id_empleado`),
  CONSTRAINT `reporte_ibfk_1` FOREIGN KEY (`id_causa`) REFERENCES `causa` (`id`),
  CONSTRAINT `reporte_ibfk_2` FOREIGN KEY (`id_equipo`) REFERENCES `equipo` (`id`),
  CONSTRAINT `reporte_ibfk_3` FOREIGN KEY (`id_empleado`) REFERENCES `empleado` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla proyecto_QA.usuario
CREATE TABLE IF NOT EXISTS `usuario` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `usuario` varchar(50) NOT NULL,
  `is_admin` tinyint(1) NOT NULL,
  `password` varchar(100) NOT NULL,
  `fecha_commit` datetime DEFAULT current_timestamp(),
  `activo` bit(1) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `usuario` (`usuario`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla proyecto_QA.vehiculo
CREATE TABLE IF NOT EXISTS `vehiculo` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `no_motor` varchar(20) NOT NULL,
  `vin` varchar(20) NOT NULL,
  `cilindrada` int(11) NOT NULL,
  `placa` varchar(10) NOT NULL,
  `modelo` int(11) NOT NULL,
  `id_equipo` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_equipo` (`id_equipo`),
  CONSTRAINT `vehiculo_ibfk_1` FOREIGN KEY (`id_equipo`) REFERENCES `equipo` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para disparador proyecto_QA.tr_equipo_empleado_cambio
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE TRIGGER tr_equipo_empleado_cambio
    AFTER UPDATE ON equipo
    FOR EACH ROW
BEGIN
    IF (OLD.id_empleado != NEW.id_empleado) OR 
       (OLD.id_empleado IS NULL AND NEW.id_empleado IS NOT NULL) OR 
       (OLD.id_empleado IS NOT NULL AND NEW.id_empleado IS NULL) THEN
        
        INSERT INTO bitacoraEquipo (id_empleado, id_equipo, fecha_commit) 
        VALUES (NEW.id_empleado, NEW.id, NOW());
        
    END IF;
END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

-- Volcando estructura para disparador proyecto_QA.tr_equipo_empleado_insert
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE TRIGGER tr_equipo_empleado_insert
    AFTER INSERT ON equipo
    FOR EACH ROW
BEGIN
    -- Solo insertar en bitácora si el equipo se crea con un empleado asignado
    IF NEW.id_empleado IS NOT NULL THEN
        
        INSERT INTO bitacoraEquipo (id_empleado, id_equipo, fecha_commit) 
        VALUES (NEW.id_empleado, NEW.id, NOW());
        
    END IF;
END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

-- Volcando estructura para disparador proyecto_QA.tr_generar_identificador_equipo
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE TRIGGER tr_generar_identificador_equipo
    BEFORE INSERT ON equipo
    FOR EACH ROW
BEGIN
    DECLARE siguiente_numero INT DEFAULT 1;
    DECLARE abreviatura VARCHAR(3);
    
    -- Solo generar identificador si viene vacío o NULL
    IF NEW.identificador IS NULL OR NEW.identificador = '' THEN
        
        -- Determinar la abreviatura según el tipo de equipo
        CASE NEW.tipo
            WHEN 'herramienta' THEN SET abreviatura = 'HER';
            WHEN 'electronico' THEN SET abreviatura = 'ELE';
            WHEN 'mobiliario' THEN SET abreviatura = 'MOB';
            WHEN 'vehiculo' THEN SET abreviatura = 'VEH';
            ELSE SET abreviatura = 'EQU'; -- Fallback por si hay un tipo no contemplado
        END CASE;
        
        -- Obtener el siguiente número en secuencia para este tipo de equipo
        SELECT IFNULL(COUNT(*), 0) + 1 INTO siguiente_numero
        FROM equipo 
        WHERE tipo = NEW.tipo;
        
        -- Generar el identificador con formato: ABREVIATURA + número con padding de 3 dígitos
        SET NEW.identificador = CONCAT(abreviatura, LPAD(siguiente_numero, 3, '0'));
        
    END IF;
END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
