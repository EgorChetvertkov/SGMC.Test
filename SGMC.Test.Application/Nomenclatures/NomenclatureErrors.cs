using ErrorOr;

namespace SGMC.Test.Application.Nomenclatures;
public static class NomenclatureErrors
{
    public static Error NotFound => Error.NotFound("Nomenclatures.NotFound", "Продукция не обнаружена");
    public static Error NameAlreadyExists => Error.Conflict("Nomenclatures.NameAlreadyExists", "Имя продукции уже занято");
}
