#!/bin/sh

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" <<EOF
\c dungeon-delvers;
create extension pg_trgm;
select * FROM pg_extension;
EOF