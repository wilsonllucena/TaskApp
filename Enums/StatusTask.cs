using System.ComponentModel;

namespace TaskApp.Enums;

public enum StatusTask
{
    [Description("Pendente")]
    PENDENTE = 1,
    [Description("Iniciado")]
    INICIADO = 2,
    [Description("Concluído")]
    CONCLUIDO = 3
}