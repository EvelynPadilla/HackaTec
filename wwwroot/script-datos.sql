USE `hackatec`;

-- 1. Insertar 5 Usuarios (Donantes)
-- Ocuparán los IDs del 1 al 5.
INSERT INTO `usuario` (`nombre`, `apellidos`, `correo`, `contrasena_hash`, `telefono`, `rol`) VALUES
('Carlos', 'Martínez', 'carlos.m@email.com', 'hash_secreto_1', '8611234567', 'Persona'),
('Lucía', 'Gómez', 'lucia.g@email.com', 'hash_secreto_2', '8612345678', 'Persona'),
('Jorge', 'Pérez', 'jorge.p@email.com', 'hash_secreto_3', '8613456789', 'Persona'),
('Ana', 'López', 'ana.l@email.com', 'hash_secreto_4', '8614567890', 'Persona'),
('Miguel', 'Torres', 'miguel.t@email.com', 'hash_secreto_5', '8615678901', 'Persona');

-- 2. Insertar 3 Instituciones Educativas (Escuelas)
-- Ocuparán los IDs 1, 2 y 3. El trigger creará 3 usuarios extra con rol 'Institución'.
INSERT INTO `instituciones_educativas` (`cct`, `nombre`, `contrasena`, `direccion`, `persona_responsable`, `telefono_escuela`, `telefono_responsable`, `estado`) VALUES
('05DIT0001Z', 'TecNM Campus Región Carbonífera', 'pass_tec', 'Carretera 57 Km 120, Agujita', 'Dr. Luis García', '8616130000', '8616130001', 1),
('05DCT0002A', 'CBTis 20', 'pass_cbtis', 'Blvd. Paseo de los Leones, Sabinas', 'Mtra. María F.', '8616120000', '8616120001', 1),
('05DPR0003B', 'Escuela Primaria Ignacio Zaragoza', 'pass_primaria', 'Centro, Sabinas', 'Prof. Roberto M.', '8616110000', '8616110001', 1);

-- 3. Insertar 10 Publicaciones de Necesidad
-- Las primeras 2 estarán CERRADAS (estado = 0). Las otras 8 estarán ABIERTAS (estado = 1).
INSERT INTO `publicaciones_necesidad` (`id_institucion`, `titulo`, `descripcion`, `estado`) VALUES
(1, 'Donación de Computadoras', 'Necesitamos 5 equipos en buen estado para el laboratorio de Sistemas Computacionales.', 0), -- ID 1 (Cerrada)
(2, 'Proyectores para Aulas', 'Requerimos proyectores para facilitar las clases multimedia en los talleres.', 0), -- ID 2 (Cerrada)
(3, 'Material Deportivo', 'Balones de fútbol, básquetbol y redes para la clase de educación física.', 1), -- ID 3
(1, 'Routers y Switches Cisco', 'Para prácticas de redes (CCNA) de los estudiantes de ingeniería.', 1), -- ID 4
(2, 'Libros de Literatura', 'Buscamos novelas y cuentos para fomentar la lectura en nuestra biblioteca escolar.', 1), -- ID 5
(3, 'Pintura para Salones', 'Urge mantenimiento de pintura en 3 aulas de nuestra primaria.', 1), -- ID 6
(1, 'Mobiliario (Mesas y Sillas)', 'Pupitres nuevos para equipar las nuevas aulas del edificio T.', 1), -- ID 7
(2, 'Equipo de Limpieza', 'Escobas, trapeadores, cloro y botes de basura.', 1), -- ID 8
(3, 'Material Didáctico', 'Mapas, ábacos y juegos educativos de destreza para niños de primer grado.', 1), -- ID 9
(1, 'Kits de Arduino y Sensores', 'Material para prácticas de electrónica e IoT (sensores ultrasónicos, movimiento).', 1); -- ID 10

-- 4. Insertar Salas de Chat para las 2 publicaciones cerradas
-- El donante 1 platica con la escuela 1 por la necesidad 1.
-- El donante 2 platica con la escuela 2 por la necesidad 2.
INSERT INTO `salas_chat` (`id_usuario_donante`, `id_institucion`, `id_post_necesidad`) VALUES
(1, 1, 1), -- Sala 1
(2, 2, 2); -- Sala 2

-- 5. Insertar Mensajes de Chat (Simulando la conversación)
-- Conversación Sala 1 (Carlos con TecNM)
INSERT INTO `mensajes` (`id_sala`, `remitente_tipo`, `id_remitente`, `contenido`, `estado_leido`) VALUES
(1, 'Donante', 1, 'Hola, leí su publicación. Tengo 5 computadoras de escritorio que quiero donar.', 1),
(1, 'Institucion', 1, '¡Hola Carlos! Muchísimas gracias. Nos serían muy útiles. ¿Podrías traerlas al campus?', 1),
(1, 'Donante', 1, 'Claro, mañana a las 10 AM estaré por allá.', 1);

-- Conversación Sala 2 (Lucía con CBTis 20)
INSERT INTO `mensajes` (`id_sala`, `remitente_tipo`, `id_remitente`, `contenido`, `estado_leido`) VALUES
(2, 'Donante', 2, 'Buenas tardes, mi empresa compró dos proyectores nuevos para donar a su escuela.', 1),
(2, 'Institucion', 2, '¡Qué maravilla, Lucía! ¿Gusta que pasemos por ellos?', 1),
(2, 'Donante', 2, 'No se preocupen, yo se los llevo a la dirección terminando mi turno.', 1);

-- 6. Insertar 2 Publicaciones de Agradecimiento
INSERT INTO `publicaciones_agradecimiento` (`id_publicacion_necesidad`, `id_usuario_donante`, `descripcion`) VALUES
(1, 1, 'Queremos agradecer infinitamente a Carlos Martínez por su generosa donación de 5 computadoras. ¡Nuestros alumnos de Sistemas Computacionales le sacarán mucho provecho para sus proyectos!'),
(2, 2, 'Un agradecimiento especial a Lucía Gómez, ahora nuestras aulas cuentan con nuevos proyectores. ¡Esto mejorará mucho la experiencia visual de nuestras clases!');