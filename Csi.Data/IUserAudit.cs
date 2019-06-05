using System;

public interface IUserAudit
{
    string Cre_By { get; set; }
    DateTime Cre_Dt { get; set; }
    string Upd_By { get; set; }
    DateTime? Upd_Dt { get; set; }
    string Del_By { get; set; }
    DateTime? Del_Dt { get; set; }
}