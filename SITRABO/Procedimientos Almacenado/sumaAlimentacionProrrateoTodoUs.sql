CREATE DEFINER=`root`@`localhost` PROCEDURE `sumaAlimentacionProrrateoTodoUs`(
	in fecD date,
    in fecH date,
    in idU int(11)
)
BEGIN
	SELECT
		Sum(KgGrano) as KgGrano,
        sum(KgSilo) as KgSilo,
        Sum(KgConcentrado) as KgConcentrado,
        Sum(KgFibra) as KgFibra,
        Sum(KgOtros) as KgOtros,
        Sum(KgRacion) as KgRacion,
        Sum(ImporteGrano) as ImporteGrano,
        Sum(ImporteSilo) as ImporteSilo,
        Sum(ImporteConcentrado) as ImporteConcentrado,
        Sum(ImporteFibra) as ImporteFibra,
        Sum(ImporteOtros) as ImporteOtros,
        Sum(ImporteTotal) as ImporteTotal
	FROM
		Alimentacion_Prorrateo
	WHERE Fecha >= fecD AND Fecha <= fecH AND USUARIOS_idUsuario = idU
	ORDER BY
		Fecha ASC;
END