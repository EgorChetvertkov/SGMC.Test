using ErrorOr;

namespace SGMC.Test.Application.Links;
public static class LinkErrors
{
    public static Error LinkAlreadyExists => Error.Conflict("Link.AlreadyExists", "Данная продукция уже связана");
    public static Error ParentNotFound => Error.NotFound("Link.ParentNotFound", "Родительская продукция не обнаружена");
    public static Error ChildNotFound => Error.NotFound("Link.ChildNotFound", "Дочерняя продукция не обнаружена");
    public static Error NotFound => Error.NotFound("Link.NotFound", "Связь не обнаружена");
}
