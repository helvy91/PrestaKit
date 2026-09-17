#!/bin/bash
docker compose up -d
echo "Waiting for PrestaShop to install..."
until curl -sf http://localhost:8080/robots.txt > /dev/null; do sleep 5; done
sleep 10  # extra buffer for install completion
docker-compose exec -T mysql mariadb -uroot -padmin prestashop < seed.sql
docker-compose exec prestashop sh -c "rm -rf /var/www/html/var/cache/*"
echo "Done — PrestaShop ready at http://localhost:8080/api/"