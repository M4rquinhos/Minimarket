--CREATE PROCEDURE sp_ListarCategorias
--@categoria VARCHAR(100) = ''
--AS
--	SELECT idCategoria, descripcion 
--	FROM TB_CATEGORIAS
--	WHERE estado = 1 
--	AND 
--	UPPER(TRIM(CAST(idCategoria AS CHAR))) LIKE '%' + UPPER(TRIM(@categoria)) + '%'
--	OR
--	UPPER(TRIM(descripcion)) LIKE '%' + UPPER(TRIM(@categoria)) + '%';
--GO

CREATE PROCEDURE sp_GuardarCategoria
@opcion INT = 0,
@idCategoria INT = 0,
@descripcion VARCHAR(50) = ''
AS
	IF @opcion = 1 --Nuevo Registro
		BEGIN
			INSERT INTO TB_CATEGORIAS (descripcion, estado) 
			VALUES 
				(@descripcion, 1);
		END;
	ELSE			--Actulizar Registro
		BEGIN
			UPDATE TB_CATEGORIAS
			SET descripcion = @descripcion
			WHERE idCategoria = @idCategoria
		END;
GO