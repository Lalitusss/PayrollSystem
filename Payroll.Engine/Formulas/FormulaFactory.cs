namespace Payroll.Engine.Formulas;
public class FormulaFactory
{
    public IFormula GetFormula(string formula)
    {
        // Si el string en la DB empieza con "ESPECIAL_", usamos una clase fija
        //if (formula.StartsWith("GANANCIAS"))
        //    return new FormulaGanancias();

        // Para todo lo demás (BASICO * 0.11, GET_NOV, etc), usamos NCalc
        return new FormulaDinamica();
    }
}