## [PostgreSQL: How to backup only One Schema from a database and restore it on another server](https://dba.stackexchange.com/questions/53185/postgresql-how-to-backup-only-one-schema-from-a-database-and-restore-it-on-anot)

Create a dump of schema some_schema:
```
pg_dump  -Fc -n some_schema >dump.dmp
```
Restore the dump file:
```
pg_restore -d somedb dump.dmp
```

## Other PostgreSql Commands

* Restart PostgreSql service:
```
sudo /etc/init.d/postgresql restart
```
*   Login PostgreSql
```
#remote:
psql -h 127.0.0.1 -d somedb -U codeyu

#localhost:
psql -U postgres
```
