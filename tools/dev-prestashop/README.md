# Development PrestaShop

A local PrestaShop 8 instance for running PrestaKit's integration tests during
development. Unlike the ephemeral Testcontainers instances used in CI, this one
persists between runs so you can iterate on tests without waiting for PrestaShop
to reinstall each time.

## Prerequisites

- **Docker Desktop** installed and running, with the **WSL 2 backend** enabled
  (Settings → General → "Use the WSL 2 based engine").
- A WSL 2 distro (e.g. Ubuntu) with **Docker Desktop's WSL integration enabled**
  for it (Settings → Resources → WSL Integration → toggle on your distro →
  Apply & Restart).
- The commands below are run from a **WSL terminal** (not PowerShell), since the
  setup script is a Bash script.

## First-time Windows / WSL setup

If you've never run Docker from WSL on this machine, do these once:

1. **Enable WSL integration** in Docker Desktop (see Prerequisites). Verify from
   a WSL terminal:

```bash
   docker compose version
```

2. **Grant Docker socket access** to your WSL user (fixes
   `permission denied ... /var/run/docker.sock`):

```bash
   sudo usermod -aG docker $USER
```

   Then fully restart WSL so the group change takes effect — from **Windows
   PowerShell**:

```powershell
   wsl --shutdown
```

   Reopen your WSL terminal and verify:

```bash
   docker ps
```

3. **Fix line endings** if the repo was cloned/edited on Windows. Bash scripts
   need Unix (LF) line endings, or you'll get
   `/bin/bash^M: bad interpreter`:

```bash
   sed -i 's/\r$//' setup.sh seed.sql
   chmod +x setup.sh
```

   (A `.gitattributes` entry — `*.sh text eol=lf` — prevents this recurring.)

## Setup

Navigate to this directory in WSL. Windows paths are mounted under `/mnt/`, e.g.:

```bash
cd /mnt/c/Users/<you>/source/repos/PrestaKit/tools/dev-prestashop
```

Then run:

```bash
./setup.sh
```

This will:

1. Start PrestaShop 8 and MariaDB via Docker Compose
2. Wait for PrestaShop to finish installing (first run takes a few minutes)
3. Enable the webservice, create the test API key, grant permissions, and
   associate the key with the default shop
4. Clear PrestaShop's config cache

When it finishes, the API is available at `http://localhost:8080/api/`.

## Credentials

| Setting        | Value                              |
|----------------|------------------------------------|
| API base URL   | `http://localhost:8080/api/`       |
| API key        | `TESTKEY000000000000000000000000A` |
| Admin email    | `admin@prestakit.test`             |
| Admin password | `PrestaKit123`                     |

The integration test fixture points at the API base URL and key by default.

## Managing the instance

| Action                   | Command                                 |
|--------------------------|-----------------------------------------|
| Start (after stopping)   | `docker compose start`                  |
| Stop (keep data)         | `docker compose stop`                   |
| View PrestaShop logs     | `docker compose logs -f prestashop`     |
| Reset to a clean install | `docker compose down -v && ./setup.sh`  |

Data (database and PrestaShop files) persists in Docker volumes across
`stop`/`start`. Use `down -v` to wipe everything and start fresh — you'll need
to run `./setup.sh` again to re-seed.

> **Note:** Use `docker compose` (v2, with a space), not the older
> `docker-compose` (v1, hyphenated), which may not be installed.

## Admin panel

The back office is available for visually inspecting data, but PrestaShop
randomizes the admin folder name on install (a security feature). Find the
actual folder:

```bash
docker compose exec prestashop ls /var/www/html | grep admin
```

Then log in at `http://localhost:8080/<that-folder>/` with the admin email and
password from the Credentials table above.

> Admin credentials only apply on a **fresh install**. If you change
> `ADMIN_MAIL` / `ADMIN_PASSWD` in `docker-compose.yml` after the volumes
> already exist, run `docker compose down -v && ./setup.sh` to reinstall with
> the new values.

## Notes

- The fixed port `8080` avoids the dynamic-domain issues that random ports cause
  with PrestaShop's canonical URL redirects.
- If integration tests accumulate test data over time, reset with `down -v` when
  it gets in the way.
- This setup is for **local development only**. CI uses ephemeral Testcontainers
  instances (fresh per run) — see the integration test fixture.