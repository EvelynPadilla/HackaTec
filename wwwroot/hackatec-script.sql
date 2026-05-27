
CREATE DATABASE Hackatec;
USE Hackatec;

CREATE TABLE instituciones_educativas (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    cct VARCHAR(10) UNIQUE NOT NULL,
    nombre VARCHAR(200) NOT NULL,
    contrasena varchar(255) not null,
    direccion VARCHAR(255) NOT NULL,
    persona_responsable VARCHAR(100) NOT NULL, 
    telefono_escuela VARCHAR(10) NOT NULL,
    telefono_responsable VARCHAR(10) NOT NULL,
    estado BOOL NOT NULL DEFAULT TRUE
);

CREATE TABLE usuario (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    apellidos VARCHAR(100) NULL,
    correo VARCHAR(150) NOT NULL UNIQUE,
    contrasena_hash VARCHAR(255) NOT NULL,
    telefono VARCHAR(10) NOT NULL,
    rol VARCHAR(50) NOT NULL DEFAULT 'Persona',
    foto_perfil VARCHAR(255) NULL
);

CREATE TABLE publicaciones_necesidad (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    id_institucion INT NOT NULL,
    titulo VARCHAR(150) NOT NULL,
    descripcion TEXT NOT NULL,
    fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
    estado BOOL NOT NULL DEFAULT TRUE, -- TRUE = Activo, FALSE = Cubierto
    CONSTRAINT FK_institucion_publicaciones FOREIGN KEY (id_institucion) REFERENCES instituciones_educativas(id)
);


CREATE TABLE imagenes_necesidad (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    id_publicacion INT NOT NULL,
    ruta_imagen VARCHAR(255) NOT NULL,
    CONSTRAINT FK_Imagen_Necesidad FOREIGN KEY (id_publicacion) REFERENCES publicaciones_necesidad(id) ON DELETE CASCADE
);

CREATE TABLE publicaciones_agradecimiento (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    id_publicacion_necesidad INT NOT NULL,
    id_usuario_donante INT NOT NULL,
    descripcion TEXT NOT NULL,
    ruta_fotografia VARCHAR(255) NULL,
    fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Necesidad_Agradecimiento FOREIGN KEY (id_publicacion_necesidad) REFERENCES publicaciones_necesidad(id),
    CONSTRAINT FK_Usuario_Agradecimiento FOREIGN KEY (id_usuario_donante) REFERENCES usuario(id)
);

CREATE TABLE salas_chat (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    id_usuario_donante INT NOT NULL,
    id_institucion INT NOT NULL,
    id_post_necesidad INT NULL, -- NULL si es oferta libre, ID si es "Quiero Donar"
    fecha_creacion DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Chat_Donante FOREIGN KEY (id_usuario_donante) REFERENCES usuario(id),
    CONSTRAINT FK_Chat_Institucion FOREIGN KEY (id_institucion) REFERENCES instituciones_educativas(id),
    CONSTRAINT FK_Chat_Post FOREIGN KEY (id_post_necesidad) REFERENCES publicaciones_necesidad(id)
);

CREATE TABLE mensajes (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    id_sala INT NOT NULL,
    remitente_tipo ENUM('Donante', 'Escuela') NOT NULL,
    id_remitente INT NOT NULL,
    contenido TEXT NULL,
    ruta_imagen VARCHAR(255) NULL,
    estado_leido BOOL NOT NULL DEFAULT FALSE, -- FALSE = no_leido
    fecha_envio DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Mensaje_Sala FOREIGN KEY (id_sala) REFERENCES salas_chat(id)
);

-- Administrador:

create table Administrador(
id int not null primary key auto_increment,
nombre varchar(80) not null,
contrasena varchar(255) not null
);

insert into Administrador(nombre,contrasena) values("Admin","Admin");

-- Trigger Institucion
DELIMITER //
DROP TRIGGER IF EXISTS `hackatec`.`instituciones_educativas_AFTER_INSERT`//

CREATE DEFINER = CURRENT_USER TRIGGER `hackatec`.`instituciones_educativas_AFTER_INSERT` AFTER INSERT ON `instituciones_educativas` FOR EACH ROW
BEGIN
	insert into usuario(nombre, correo, contrasena_hash, telefono, rol) values(new.nombre, new.cct, new.contrasena, new.telefono_escuela, "Institución");
END //
DELIMITER ;
