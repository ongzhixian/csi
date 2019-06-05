# About this project

CSI is a ticket tracking web application.
The name came from the primary three types of tickets that this application will support.
1.  Change requests
2.  Service requests
3.  Incidents


## Data design

Somethings that are always good to know:

1.	Who last Create, Update or Delete?
2.  When last Create, Update or Delete?
1.  Status of this record

Choice of words for Create, Update or Delete:
CRE    Create    Add       New     Written
UPD    Update    Edit      Modified Revise    Amend
DEL    Delete    Remove    Destroy

The "last" in Create, Update or Delete fields is implicit for non-audit records.
This allows us to shorten the field names (cre_by vs last_cre_by)

So the standard fields that we want to have on all tables are:

cre_by
cre_dt
upd_by
upd_dt
del_by
del_dt

id
name
status