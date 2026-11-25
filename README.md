# Overwatch Stadium Database

This repository provides a historical snapshot of Overwatch Stadium data. Scheduled refreshes are paused, and the snapshot may not reflect later balance changes. Check the [official Overwatch patch notes](https://overwatch.blizzard.com/en-us/news/patch-notes/) for balance updates.

## Data Access

The **[Data](Data/)** directory contains the snapshot in two formats:

- **[overwatch_stadium.db](Data/overwatch_stadium.db)**: SQLite database file.
- **[full_data_dump.sql](Data/full_data_dump.sql)**: SQL export script (compatible with SQLite, MySQL, PostgreSQL, SQL Server).

The [Update Database workflow](https://github.com/fengb3/OverwatchStadiumDatabase/actions/workflows/update-db.yml) can still be run manually. A successful run does not guarantee the resulting data is current.
