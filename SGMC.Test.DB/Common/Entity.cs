namespace SGMC.Test.DB.Common;
public abstract class Entity
{
    //NOTE : Можно использовать и не числовые идентификаторы, например ULID или встроенный GUID
    public long Id { get; set; }
    //NOTE : при необходимости можно расширить иерархию или внедрить в Entity аудиторские атрибуты или метку холодного удаления
}
