CREATE PROCEDURE sp_ListarCategorias
@categoria VARCHAR(100) = ''
AS
	SELECT idCategoria, descripcion 
	FROM TB_CATEGORIAS
	WHERE estado = 1 
	AND 
	UPPER(TRIM(CAST(idCategoria AS CHAR))) LIKE '%' + UPPER(TRIM(@categoria)) + '%'
	OR
	UPPER(TRIM(descripcion)) LIKE '%' + UPPER(TRIM(@categoria)) + '%';
GO
